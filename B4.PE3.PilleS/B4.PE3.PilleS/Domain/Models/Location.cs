using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.PilleS.Domain.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTimeOffset LocationTime { get; set; }
        public string LocationName { get; set; }

        public string LocationTimeString
        {
            get
            {
                string locationTimeString = LocationTime.ToString("HH:mm:ss");
                return locationTimeString;
            }
        }
    }
}
