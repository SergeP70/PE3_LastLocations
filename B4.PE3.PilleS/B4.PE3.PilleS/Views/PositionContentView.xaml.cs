using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

namespace B4.PE3.PilleS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PositionContentView : ContentView
    {
        public PositionContentView()
        {
            InitializeComponent();
        }

        private async void OnBtnGetLocationClicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);

            if (position == null)
            {
                lblPositionStatus.Text = "no GPS found";
                return;
            }
            lblPositionStatus.Text = position.Timestamp.ToString();
            lblPositionStatus.IsEnabled = true;
            lblPositionLatitude.Text = position.Latitude.ToString();
            lblPositionLatitude.IsEnabled = true;
            lblPositionLongitude.Text = position.Longitude.ToString();
            lblPositionLongitude.IsEnabled = true;

            var address = await locator.GetAddressesForPositionAsync(position, "RJHqIE53Onrqons5CNOx~FrDr3XhjDTyEXEjng-CRoA~Aj69MhNManYUKxo6QcwZ0wmXBtyva0zwuHB04rFYAPf7qqGJ5cHb03RCDw1jIW8l");
            if (address == null || address.Count() == 0)
            {
                lblStreet.Text = "Unable to find address";
            }
            var a = address.FirstOrDefault();
            lblStreet.Text = a.Thoroughfare;
            lblStreet.IsEnabled = true;
            lblNumber.Text = a.SubThoroughfare;
            lblNumber.IsEnabled = true;
            lblPostal.Text = a.PostalCode;
            lblPostal.IsEnabled = true;
            lblCity.Text = a.Locality;
            lblCity.IsEnabled = true;
            lblCountryCode.Text = a.CountryCode;
            lblCountryCode.IsEnabled = true;
            lblCountry.Text = a.CountryName;
            lblCountry.IsEnabled = true;
        }

        private void OnBtnSaveLocationClicked(object sender, EventArgs e)
        {

        }
    }
}