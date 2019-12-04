using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Dtos
{
    public class DiscoveredLocationToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime DateCreated { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<PhotoToReturnDto> Photos { get; set; }
    }
}
