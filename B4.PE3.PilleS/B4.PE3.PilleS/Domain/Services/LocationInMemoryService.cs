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
        static List<Location> InMemLocations = new List<Location>
        {
            new Location
            {
                Id=Guid.NewGuid(),
                Latitude = 51.1117755,
                Longitude = 3.2281144,
                LocationTime = new DateTimeOffset(2017,12,13,15,23,00,TimeSpan.Zero), 
                LocationName="Waardamme",
                ListName="Ronde1"
            },
            new Location
            {
                Id=Guid.NewGuid(),
                Latitude = 51.064980,
                Longitude = 3.101570,
                LocationTime = new DateTimeOffset(2017,11,23,14,30,00, TimeSpan.Zero),
                LocationName="Torhout",
                ListName="Ronde2"

            },
            new Location
            {
                Id=Guid.NewGuid(),
                Latitude = 51.209348,
                Longitude = 3.224700,
                LocationTime = new DateTimeOffset(2012,08,08,21,30,00, TimeSpan.Zero),
                LocationName="Brugge",
                ListName="Ronde1"
            }
        };

        /// <summary>
        /// Gets all locations in Memory collection
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Location>> GetAll()
        {
            await Task.Delay(0);
            return InMemLocations.OrderBy(e => e.LocationTime);
        }

        /// <summary>
        /// Gets the locations belonging to the listName
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Location>> GetByListName(string listName)
        {
            await Task.Delay(0);
            return InMemLocations.Where(l => l.ListName == listName);
        }

        public async Task SaveLocation(Location location)
        {
            await Task.Delay(1);
            InMemLocations.Add(new Location
            {
                Id = Guid.NewGuid(),
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                LocationTime = location.LocationTime,
                LocationName = location.LocationName,
                ListName = location.ListName
            });
        }
    }
}
