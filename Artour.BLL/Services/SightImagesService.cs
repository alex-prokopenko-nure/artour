using Artour.BLL.Models;
using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Artour.Domain.EntityFramework.Context;
using Artour.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Artour.BLL.Services
{
    public class SightImagesService : BaseService, ISightImagesService
    {
        public SightImagesService(ApplicationDbContext applicationDbContext, IMapper mapper, IConfiguration configuration)
            : base(applicationDbContext, mapper, configuration)
        { }

        public async Task<SightImageViewModel> CreateImage(IFormFile fileToSave, String description, Int32 sightId)
        {
            var outputFilename = GetOutputFilename(fileToSave.FileName, ApplicationSettingsManager<ApplicationSettings>.Settings.ImageDirectory);
            var fileInfo = await SaveFileAsync(fileToSave, outputFilename);
            var allImages = _applicationDbContext.SightImages.Where(x => x.SightId == sightId);

            var result = new SightImage()
            {
                FileSize = (Int32)fileInfo.Length,
                FullFilename = outputFilename,
                Description = description,
                UploadedOn = DateTime.UtcNow,
                Order = allImages.ToList().Count > 0 ? await allImages.MaxAsync(x => x.Order) + 1 : 0,
                SightId = sightId
            };

            var bindingItem = await _applicationDbContext.SightImages.AddAsync(result);

            await _applicationDbContext.SaveChangesAsync();

            return _mapper.Map<SightImageViewModel>(bindingItem.Entity);
        }

        public async Task<SightImageViewModel> GetImageByIdAsync(Int32 imageId)
        {
            var result = await _applicationDbContext.SightImages
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.SightImageId == imageId);
            return _mapper.Map<SightImageViewModel>(result);
        }

        private static void EnsureDirectoryExists(String directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private String GetOutputFilename(String filename, String saveDirectory)
        {
            EnsureDirectoryExists(saveDirectory);
            var fileInfo = new FileInfo(filename);
            var uniqueSeed = $"{DateTime.UtcNow.Ticks.ToString("X016")}_{Thread.CurrentThread.ManagedThreadId}_";
            var outputFilename = uniqueSeed + fileInfo.Name;
            return Path.Combine(saveDirectory, outputFilename);
        }

        public FileInfo GetFileInfo(SightImageViewModel image)
        {
            return new FileInfo(image.FullFilename);
        }

        public String GetImageMimeType(String extension)
        {
            return $"image/{extension.TrimStart('.')}";
        }

        private async Task<FileInfo> SaveFileAsync(IFormFile fileInfo, String outputFileName)
        {
            try
            {
                using (var inputFileStream = fileInfo.OpenReadStream())
                {
                    using (var outputStream = new FileStream(outputFileName, FileMode.Create))
                    {
                        await inputFileStream.CopyToAsync(outputStream);
                    }
                }

                return new FileInfo(outputFileName);
            }
            catch (Exception ex)
            {
                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }

                throw ex;
            }
        }

        public async Task ChangeOrder(Int32 previousOrder, Int32 currentOrder, Int32 sightId)
        {
            if (previousOrder > currentOrder)
            {
                var images = _applicationDbContext.SightImages
                    .Where(x => x.Order >= currentOrder && x.Order < previousOrder && x.SightId == sightId);
                var changedImage = await _applicationDbContext.SightImages
                    .FirstOrDefaultAsync(x => x.Order == previousOrder && x.SightId == sightId);
                foreach (var image in images)
                {
                    ++image.Order;
                }
                changedImage.Order = currentOrder;
                _applicationDbContext.SightImages.UpdateRange(images);
                _applicationDbContext.SightImages.Update(changedImage);
            }
            else
            {
                var images = _applicationDbContext.SightImages
                    .Where(x => x.Order <= currentOrder && x.Order > previousOrder && x.SightId == sightId);
                var changedImage = await _applicationDbContext.SightImages
                    .FirstOrDefaultAsync(x => x.Order == previousOrder && x.SightId == sightId);
                foreach (var image in images)
                {
                    --image.Order;
                }
                changedImage.Order = currentOrder;
                _applicationDbContext.SightImages.UpdateRange(images);
                _applicationDbContext.SightImages.Update(changedImage);
            }
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateImage(Int32 id, IFormFile fileToSave)
        {
            var outputFileName = GetOutputFilename(fileToSave.FileName, ApplicationSettingsManager<ApplicationSettings>.Settings.ImageDirectory);
            var fileInfo = await SaveFileAsync(fileToSave, outputFileName);
            var changedImage = await _applicationDbContext.SightImages.FirstOrDefaultAsync(x => x.SightImageId == id);
            changedImage.FileSize = (Int32)fileInfo.Length;
            changedImage.FullFilename = outputFileName;
            changedImage.UploadedOn = DateTime.UtcNow;

            _applicationDbContext.SightImages.Update(changedImage);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateInfo(Int32 id, SightImageViewModel model)
        {
            var changedImage = await _applicationDbContext.SightImages.FirstOrDefaultAsync(x => x.SightImageId == id);
            changedImage.Description = model.Description;
            _applicationDbContext.SightImages.Update(changedImage);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteImage(Int32 id)
        {
            var deletedImage = await _applicationDbContext.SightImages.FirstOrDefaultAsync(x => x.SightImageId == id);
            _applicationDbContext.Attach(deletedImage);
            _applicationDbContext.Remove(deletedImage);
            UpdateOrders(deletedImage.Order, deletedImage.SightId);
            await _applicationDbContext.SaveChangesAsync();
            try
            {
                File.Delete(deletedImage.FullFilename);
            }
            catch (IOException ex)
            {
                throw new Exception("File was not present on server");
            }
        }

        private void UpdateOrders(Int32 deletedOrder, Int32 sightId)
        {
            var images = _applicationDbContext.SightImages.Where(x => x.Order > deletedOrder && x.SightId == sightId);
            foreach (var image in images)
            {
                --image.Order;
            }
            _applicationDbContext.SightImages.UpdateRange(images);
        }
    }
}
