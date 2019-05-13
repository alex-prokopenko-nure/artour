using Artour.Mobile.Models;
using Artour.Mobile.ViewModels;
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
    public partial class RegisterPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        RegisterViewModel viewModel;

        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RegisterViewModel();

            var title = new Label
            {
                Text = "Sign up here",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var register = new Button
            {
                Text = "Register",
            };

            var email = new Entry
            {
                Placeholder = "E-Mail",
                FontSize = 14
            };

            var firstName = new Entry
            {
                Placeholder = "First Name",
                FontSize = 14
            };

            var lastName = new Entry
            {
                Placeholder = "Last Name",
                FontSize = 14
            };

            var username = new Entry
            {
                Placeholder = "Username",
                FontSize = 14
            };

            var password = new Entry
            {
                Placeholder = "Password",
                FontSize = 14,
                IsPassword = true
            };

            register.Clicked += async (sender, e) => {
                await RootPage.ArtourApiService.RegisterUserAsync(
                    new Services.RegisterViewModel
                    {
                        Email = email.Text,
                        FirstName = firstName.Text,
                        LastName = lastName.Text,
                        Username = username.Text,
                        Password = password.Text,
                        DateOfBirth = DateTimeOffset.Now
                    }
                );
                await RootPage.NavigateFromMenu((int)MenuItemType.Login);
            };

            Content = new StackLayout
            {
                Padding = 20,
                Spacing = 8,
                Children = { title, email, firstName, lastName, username, password, register }
            };
        }
    }
}