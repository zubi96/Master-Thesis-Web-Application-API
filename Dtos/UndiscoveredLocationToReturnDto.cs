using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Dtos
{
    public class UndiscoveredLocationToReturnDto
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<PhotoToReturnDto> Photos { get; set; }
    }
}
