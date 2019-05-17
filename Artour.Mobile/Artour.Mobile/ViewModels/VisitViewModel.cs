using Artour.Mobile.Models;
using Artour.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Artour.Mobile.ViewModels
{
    public class VisitViewModel : BaseViewModel
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        private Guid visitId;
        public Services.VisitViewModel Visit { get; set; }
        public ObservableCollection<SightSeen> Sights { get; set; }
        public Command LoadItemsCommand { get; set; }

        public VisitViewModel(Guid visitId)
        {
            this.visitId = visitId;
            Title = "Visit";
            Sights = new ObservableCollection<SightSeen>();
            ExecuteLoadItemsCommand();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Visit = await RootPage.ArtourApiService.GetVisitAsync(visitId);
                Sights.Clear();
                foreach (var sight in Visit.SightSeens)
                {
                    SightSeen sightModel = new SightSeen
                    {
                        DateSeen = sight.DateSeen,
                        VisitId = sight.VisitId,
                        SightId = sight.SightId,
                        SightSeenId = sight.SightSeenId,
                        Source = $"{RootPage.ArtourApiService.BaseUrl}/api/sight-images/{Visit.Tour.Sights.First(x => x.SightId == sight.SightId).Images.First(x => x.Order == 0).SightImageId}/data",
                        Text = $"{Visit.Tour.Sights.First(x => x.SightId == sight.SightId).Title} had been seen at {sight.DateSeen.Value.ToString()}"
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
