using B4.PE3.PilleS.Constants;
using B4.PE3.PilleS.Domain.Models;
using B4.PE3.PilleS.Domain.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.PilleS.ViewModels
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        // Deze ViewModel zal de TrackingViewModel (~classmateView) ondersteunen en bevat de nodige velden overeenkomstig het formulier

        private LocationInMemoryService locationService;
        private LocationList currentLocationList;
        private Location currentLocation;
        private INavigation navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        public LocationViewModel(LocationList currentLocationList, Location currentLocation, INavigation navigation)
        {
            this.navigation = navigation;
            this.currentLocation = currentLocation;
            locationService = new LocationInMemoryService();

            // initialize the properties with data from current location
            this.Latitude = currentLocation.Latitude;
            this.Longitude = currentLocation.Longitude;
            this.LocationTime = currentLocation.LocationTime;
            this.LocationName = currentLocation.LocationName;
            this.ListName = currentLocationList.ListName;
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                RaisePropertyChanged(nameof(Latitude));
            }
        }

        public string LatitudeString
        {
            get
            {
                string positionNZ = (Latitude < 0) ? "Z " : "N ";
                string latitudeString = positionNZ + Latitude.ToString() + "°";
                return latitudeString;
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                RaisePropertyChanged(nameof(Longitude));
            }
        }

        public string LongitudeString
        {
            get
            {
                string positionOW = (Longitude < 0) ? "W " : "O ";
                string latitudeString = positionOW + Longitude.ToString() + "°";
                return latitudeString;
            }
        }

        private DateTimeOffset locationTime;
        public DateTimeOffset LocationTime
        {
            get { return locationTime; }
            set
            {
                locationTime = value;
                RaisePropertyChanged(nameof(LocationTime));
            }
        }

        public string LocationTimeString
        {
            get
            {
                string locationTimeString = locationTime.ToString("HH:mm:ss");
                return locationTimeString;
            }
        }

        public string LocationDateString
        {
            get
            {
                string locationDateString = LocationTime.Date.ToString("dd/MM/yyyy");
                return locationDateString;
            }
        }

        private string locationName;
        public string LocationName
        {
            get { return locationName; }
            set
            {
                locationName = value;
                RaisePropertyChanged(nameof(LocationName));
            }
        }

        private string listName;
        public string ListName
        {
            get { return listName; }
            set
            {
                listName = value;
                RaisePropertyChanged(nameof(ListName));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event if it has handlers attached to it
        /// </summary>
        /// <param name="propertyName">name of the prop that was changed</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveCommand => new Command(
            async () =>
            {
                currentLocationList = (LocationList)(locationService.GetLocationLists().Result.Where(l => l.ListName == ListName).FirstOrDefault());
                currentLocation.LocationName = LocationName;
                // Save Location & Publish Message
                await locationService.SaveLocation(currentLocation, currentLocationList);
                MessagingCenter.Send<LocationViewModel, Location>(this, MessageLocations.LocationSaved, currentLocation);
                await navigation.PopAsync(true);
            });


    }
}
