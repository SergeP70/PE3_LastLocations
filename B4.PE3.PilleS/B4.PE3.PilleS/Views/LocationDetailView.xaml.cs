using B4.PE3.PilleS.Domain.Models;
using B4.PE3.PilleS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE3.PilleS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationDetailView : ContentPage
    {
        public LocationDetailView(Location location)
        {
            InitializeComponent();
            BindingContext = new LocationViewModel(location, this.Navigation);
        }
    }
}