using Artour.Mobile.Services;
using Artour.Mobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace Artour.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        AboutPage AboutPage { get; set; }
        private Int32 userId;
        public UserViewModel User { get; set; }
        public UserStatisticsViewModel UserStatistics { get; set; }
        private string username;
        public String Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        private string name;
        public String Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        public ObservableCollection<VisitInfoViewModel> Visits { get; set; }
        public ICommand OpenVisitPageCommand { get; set; }
        public Command LoadItemsCommand { get; set; }
        public AboutViewModel(Int32 userId, AboutPage page)
        {
            this.userId = userId;
            AboutPage = page;
            Title = "Profile";
            Visits = new ObservableCollection<VisitInfoViewModel>();
            ExecuteLoadItemsCommand();
            OpenVisitPageCommand = new Command<Guid>(async (guid) =>
                {
                    await AboutPage.Navigation.PushAsync(new VisitPage(new VisitViewModel(guid)));
                });
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                User = await RootPage.ArtourApiService.GetUserAsync(userId);
                UserStatistics = await RootPage.ArtourApiService.GetUserStatisticsAsync(userId);
                Username = User.Username;
                Name = $"{User.FirstName} {User.LastName}";
                Visits.Clear();
                foreach (var visit in UserStatistics.Visits)
                {
                    Visits.Add(visit);
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