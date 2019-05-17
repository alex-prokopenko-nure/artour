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
    public partial class VisitPage : ContentPage
    {
        VisitViewModel viewModel;
        public VisitPage(VisitViewModel visitViewModel)
        {
            InitializeComponent();

            BindingContext = viewModel = visitViewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as SightSeen;
            if (item == null)
                return;

            ItemsListView.SelectedItem = null;
        }
    }
}