using Artour.Mobile.Models;
using Artour.Mobile.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Artour.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new LoginViewModel();

            var title = new Label
            {
                Text = "Welcome to ArTour",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var register = new Button
            {
                Text = "Register",
            };

            register.Clicked += (object sender, EventArgs e) => {
                RootPage.NavigateFromMenu((int)MenuItemType.Register);
            };

            var email = new Entry
            {
                Placeholder = "E-Mail",
                FontSize = 14
            };

            var password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                FontSize = 14
            };

            var login = new Button
            {
                Text = "Login",
            };

            login.Clicked += async (sender, e) => {
                Task.Run(async () =>
                {
                    try
                    {
                        string jwtToken = await RootPage.ArtourApiService.LoginUserAsync(new Services.LoginViewModel
                        {
                            Login = email.Text,
                            Password = password.Text,
                            Remember = true
                        });
                        Application.Current.Properties["jwt_token"] = jwtToken;
                    }
                    catch
                    {

                    }
                }).Wait();
                await RootPage.Login();
            };

            Content = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children = { title, email, password, login, register }
            };
        }
    }
}