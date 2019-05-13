using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Artour.Mobile.Models;
using Artour.Mobile.Views;
using Artour.Mobile.Services;
using System.Linq;

namespace Artour.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public ObservableCollection<Tour> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Active Tours";
            Items = new ObservableCollection<Tour>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var tours = await RootPage.ArtourApiService.GetAllToursAsync();
                foreach (var tour in tours)
                {
                    Tour tourModel = new Tour
                    {
                        City = tour.City,
                        CityId = tour.CityId,
                        Comments = tour.Comments,
                        Description = tour.Description,
                        OwnerId = tour.OwnerId,
                        Sights = tour.Sights,
                        Source = $"{RootPage.ArtourApiService.BaseUrl}/api/sight-images/{tour.Sights.First().Images.First().SightImageId}/data",
                        Title = tour.Title,
                        TourId = tour.TourId,
                        Visits = tour.Visits
                    };
                    Items.Add(tourModel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}