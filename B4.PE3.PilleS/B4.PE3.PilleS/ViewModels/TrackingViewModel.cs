using B4.PE3.PilleS.Constants;
using B4.PE3.PilleS.Domain.Models;
using B4.PE3.PilleS.Domain.Services;
using B4.PE3.PilleS.Views;
using Plugin.Geolocator;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.PilleS.ViewModels
{
    public class TrackingViewModel : INotifyPropertyChanged
    {
        // Deze ViewModel zal de TrackingContentView ondersteunen en bevat bijgevolg een collectie van LocationList en Location objecten:
        private INavigation navigation;
        private LocationInMemoryService locationService;
        private LocationList locationList, selectedLocationList;
        private Location location;
        public ICommand ShowLocationDetailsCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public TrackingViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            locationService = new LocationInMemoryService();

            // Initialize the location collection, 
            LocationLists = new ObservableCollection<LocationList>(locationService.GetAll().Result);
            
            // take the first LocationList
            selectedLocationList = (LocationList)(locationService.GetLocationLists().Result.Where(l => l.ListName == "Ronde 1").FirstOrDefault());
            // vervang door: selectedLocationList = (LocationList)(locationService.GetLocationLists().Result.FirstOrDefault());
            //this.ListName = selectedLocationList.ListName;

            if (locationService.GetByListName(selectedLocationList.ListName).Result == null)
                Locations = null;
            else
                Locations = new ObservableCollection<Location>(locationService.GetByListName(selectedLocationList.ListName).Result);

            // Listen to the messaging center to inform if a location is saved and refresh the Listview
            MessagingCenter.Subscribe(this, MessageLocations.LocationSaved,
                async (LocationViewModel sender, Location loc) =>
                {
                    Locations = new ObservableCollection<Location>(await locationService.GetByListName(selectedLocationList.ListName));
                });
        }

        //private string listName;
        //public string ListName
        //{
        //    get { return listName; }
        //    set
        //    {
        //        listName = value;
        //        RaisePropertyChanged(nameof(ListName));
        //    }
        //}

        private ObservableCollection<LocationList> locationLists;
        public ObservableCollection<LocationList> LocationLists
        {
            get { return locationLists; }
            set
            {
                locationLists = value;
                RaisePropertyChanged(nameof(LocationLists));
            }
        }

        private ObservableCollection<Location> locations;
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                RaisePropertyChanged(nameof(Locations));
            }
        }

        private int locationListSelectedIndex;
        public int LocationListSelectedIndex
        {
            get { return locationListSelectedIndex; }
            set
            {
                if (locationListSelectedIndex != value)
                {
                    locationListSelectedIndex = value;
                    // trigger some action to take
                    RaisePropertyChanged(nameof(LocationListSelectedIndex));
                    selectedLocationList = LocationLists[locationListSelectedIndex];
                    Locations = new ObservableCollection<Location>(locationService.GetByListName(selectedLocationList.ListName).Result);
                }
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

        public ICommand SortCommand => new Command(
            async () =>
            {
                // refresh the list and sort data by Time
                var sortedLocations = (await locationService.GetByListName(selectedLocationList.ListName)).OrderBy(e => e.LocationTime).ToList();
                // reset the collection
                Locations = new ObservableCollection<Location>(sortedLocations);
            });

        public ICommand ViewLocationDetailCommand => new Command<Location>(
            (Location loc) =>
            {

                navigation.PushAsync(new LocationDetailView(selectedLocationList, loc));

            });

        public ICommand RefreshCommand => new Command(
            async () =>
            {
                Locations = new ObservableCollection<Location>(
                    (await locationService.GetByListName(selectedLocationList.ListName)).OrderBy(e => e.LocationTime).ToList());
            });

        public ICommand SaveLocationCommand => new Command(
            async () =>
            {
                // Find current Position and put it in a Location
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);

                if (position == null)
                {
                    //Maak melding (new Label { Text = "no GPS found" });
                    location = null;
                }
                else
                {
                    location = new Location
                    {
                        Latitude = position.Latitude,
                        Longitude = position.Longitude,
                        LocationTime = (DateTimeOffset)position.Timestamp,
                        LocationName = "testje",
                    };
                }
                await navigation.PushAsync(new LocationDetailView(selectedLocationList, location));
            });
    }
}
