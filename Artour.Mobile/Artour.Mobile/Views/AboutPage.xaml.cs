using Artour.Mobile.Services;
using Artour.Mobile.ViewModels;
using System;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Artour.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class AboutPage : ContentPage
    {
        AboutViewModel viewModel;
        public AboutPage()
        {
            InitializeComponent();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(Application.Current.Properties["jwt_token"].ToString());
            int userId = Convert.ToInt32(token.Claims.First(x => x.Type == "sub").Value);
            Application.Current.Properties["user_id"] = userId;

            BindingContext = viewModel = new AboutViewModel(userId, this);
        }

        public async Task OpenVisitPage(Guid visitId)
        {
            await Navigation.PushAsync(new VisitPage(new ViewModels.VisitViewModel(visitId))); 
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as VisitInfoViewModel;
            if (item == null)
                return;

            ItemsListView.SelectedItem = null;
        }
    }
}