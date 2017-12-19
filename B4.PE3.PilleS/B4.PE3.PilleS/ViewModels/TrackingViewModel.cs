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
        // Deze ViewModel zal de TrackingContentView (MainView) ondersteunen en bevat bijgevolg een collectie van Location objecten:
        private INavigation navigation;
        private LocationInMemoryService locationService;
        private Location location;
        public ICommand ShowLocationDetailsCommand { get; private set; }

        public TrackingViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            locationService = new LocationInMemoryService();

            // Initialize the classmates collection
            Locations = new ObservableCollection<Location>(locationService.GetAll().Result);

            // Listen to the messaging center to inform if a location is saved and refresh the Listview
            MessagingCenter.Subscribe(this, MessageLocations.LocationSaved,
                async (LocationViewModel sender, Location loc) => {
                    Locations = new ObservableCollection<Location>(await locationService.GetAll());
                });

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

        public event PropertyChangedEventHandler PropertyChanged;

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
            async () => {
                // refresh the list and sort data by Time
                var sortedLocations = (await locationService.GetAll()).OrderBy(e => e.LocationTime).ToList();
                // reset the collection
                Locations = new ObservableCollection<Location>(sortedLocations);
            });

        public ICommand ViewLocationDetailCommand => new Command<Location>(
            (Location loc) => {
                navigation.PushAsync(new LocationDetailView(loc));
            });

        public ICommand RefreshCommand => new Command(
            async () =>
            {
            Locations = new ObservableCollection<Location>(
                (await locationService.GetAll()).OrderBy(e => e.LocationTime).ToList());
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
                        ListName = "ronde2",
                    };
                }
                await navigation.PushAsync(new LocationDetailView(location));

            });
    }
}
