using Artour.BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface ISightImagesService
    {
        Task<SightImageViewModel> CreateImage(IFormFile file, String description, Int32 sightId);
        Task<SightImageViewModel> GetImageByIdAsync(Int32 imageId);
        FileInfo GetFileInfo(SightImageViewModel image);
        String GetImageMimeType(String extension);
        Task ChangeOrder(Int32 previousOrder, Int32 currentOrder);
        Task UpdateImage(Int32 id, IFormFile fileToSave);
        Task UpdateInfo(Int32 id, SightImageViewModel model);
        Task DeleteImage(Int32 id);
    }
}
