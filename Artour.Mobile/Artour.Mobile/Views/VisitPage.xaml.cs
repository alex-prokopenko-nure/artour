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
        public VisitPage(VisitViewModel visitViewModel)
        {
            InitializeComponent();
        }
    }
}