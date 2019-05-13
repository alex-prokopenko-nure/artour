using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Artour.Mobile.Models;
using Artour.Mobile.Services;
using Artour.Mobile.Views;
using Xamarin.Forms;

namespace Artour.Mobile.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public Tour Item { get; set; }
        public ObservableCollection<Sight> Sights { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ItemDetailViewModel(Tour item = null)
        {
            Title = $"{item?.Title} Sights";
            Item = item;
            Sights = new ObservableCollection<Sight>();
            foreach (var sight in Item.Sights)
            {
                Sight sightModel = new Sight
                {
                    Title = sight.Title,
                    Description = sight.Description,
                    Images = sight.Images,
                    SightId = sight.SightId,
                    SightSeens = sight.SightSeens,
                    TourId = sight.TourId,
                    Source = $"{RootPage.ArtourApiService.BaseUrl}/api/sight-images/{sight.Images.First().SightImageId}/data",
                };
                Sights.Add(sightModel);
            }
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Sights.Clear();
                foreach (var sight in Item.Sights)
                {
                    Sight sightModel = new Sight
                    {
                        Title = sight.Title,
                        Description = sight.Description,
                        Images = sight.Images,
                        SightId = sight.SightId,
                        SightSeens = sight.SightSeens,
                        TourId = sight.TourId,
                        Source = $"{RootPage.ArtourApiService.BaseUrl}/api/sight-images/{sight.Images.First().SightImageId}/data",
                    };
                    Sights.Add(sightModel);
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

