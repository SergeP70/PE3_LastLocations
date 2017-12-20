using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.PilleS.Domain.Models
{
    public class LocationList
    {
        public Guid Id { get; set; }
        public string ListName { get; set; }

        public ICollection<Location> TaggedLocations { get; set; }
    }
}
