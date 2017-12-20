using B4.PE3.PilleS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace B4.PE3.PilleS.Domain.Services
{
    public class LocationInMemoryService
    {
        static List<LocationList> InMemLocations = new List<LocationList>
        {
            new LocationList
            {
                Id=Guid.NewGuid(),
                ListName = "Ronde 1",
                TaggedLocations = new List<Location>
                {
                    new Location
                    {
                        Id=Guid.NewGuid(),
                        Latitude = 51.1117755,
                        Longitude = 3.2281144,
                        LocationTime = new DateTimeOffset(2017,12,13,15,23,00,TimeSpan.Zero),
                        LocationName="Waardamme",
                    },
                    new Location
                    {
                        Id=Guid.NewGuid(),
                        Latitude = 51.209348,
                        Longitude = 3.224700,
                        LocationTime = new DateTimeOffset(2012,08,08,21,30,00, TimeSpan.Zero),
                        LocationName="Brugge",
                    }
                }
            },
            new LocationList
            {
                Id=Guid.NewGuid(),
                ListName = "Ronde 2",
                TaggedLocations = new List<Location>
                {
                    new Location
                    {
                        Id=Guid.NewGuid(),
                        Latitude = 51.064980,
                        Longitude = 3.101570,
                        LocationTime = new DateTimeOffset(1970,11,23,14,30,00, TimeSpan.Zero),
                        LocationName="Torhout",
                    },
                    new Location
                    {
                        Id=Guid.NewGuid(),
                        Latitude = 51.1284823,
                        Longitude = 2.748015799999962,
                        LocationTime = new DateTimeOffset(2017,08,08,21,30,00, TimeSpan.Zero),
                        LocationName="Nieuwpoort",
                    }
                }
            },
            new LocationList
            {
                Id=Guid.NewGuid(),
                ListName = "Ronde Leeg",
                TaggedLocations = null
            }
        };

        /// <summary>
        /// Gets all locations in Memory collection
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LocationList>> GetAll()
        {
            await Task.Delay(0);
            return InMemLocations.OrderBy(e => e.ListName);
        }

        /// <summary>
        /// Get all LocationLists in Memory Collection
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LocationList>> GetLocationLists()
        {
            await Task.Delay(0);
            return InMemLocations.OrderBy(e => e.ListName);
        }

        /// <summary>
        /// Gets the locations belonging to the listName
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Location>> GetByListName(string listName)
        {
            await Task.Delay(0);
            LocationList currentLocationList = InMemLocations.Where(l => l.ListName == listName).FirstOrDefault();
            if (currentLocationList.TaggedLocations == null)
            {
                return null;
            }
            return currentLocationList.TaggedLocations.ToList().OrderBy(l => l.LocationTime);
        }

        public async Task SaveLocationList(string locationListName)
        {
            await Task.Delay(1);
            InMemLocations.Add(new LocationList
            {
                Id = Guid.NewGuid(),
                ListName = locationListName,
                TaggedLocations= null
            });
        }

        public async Task SaveLocation(Location location, LocationList locationList)
        {
            await Task.Delay(1);
            LocationList currentLocationList = InMemLocations.FirstOrDefault(l => l.ListName == locationList.ListName);

            if (locationList.TaggedLocations == null)
            {
                // Reference null exception
                locationList.TaggedLocations.Add(
                    new Location
                    {
                        Id = Guid.NewGuid(),
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        LocationTime = location.LocationTime,
                        LocationName = location.LocationName
                    });
            }

            locationList.TaggedLocations.Add(new Location
            {
                Id = Guid.NewGuid(),
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                LocationTime = location.LocationTime,
                LocationName = location.LocationName,
            });
            await Task.Delay(0);
        }
    }
}

