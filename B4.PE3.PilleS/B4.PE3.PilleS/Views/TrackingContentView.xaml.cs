using B4.PE3.PilleS.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE3.PilleS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackingContentView : ContentView
    {
        public TrackingContentView()
        {
            InitializeComponent();
            BindingContext = new TrackingViewModel(this.Navigation);
        }


        private async void OnBtnSaveLocationClicked(object sender, EventArgs e)
        {
            //LocationInMemoryService locationService = new LocationInMemoryService();
            //Location location;
            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 100;

            //var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);
            //if (position == null)
            //{
            //    //lstTracking.Children.Add(new Label { Text = "no GPS found" });

            //}
            //else
            //{
            //    location = new Location
            //    {
            //        Latitude = position.Latitude,
            //        Longitude = position.Longitude,
            //        LocationTime = (DateTimeOffset)position.Timestamp,
            //        LocationName = "testje",
            //        ListName = "ronde2",
            //    };

            //    await locationService.SaveLocation(location); 
            //}
        }
    }
}