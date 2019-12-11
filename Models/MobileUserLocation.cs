using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Models
{
    public class MobileUserLocation
    {
        public int MobileUserId { get; set; }
        public virtual MobileUser MobileUser { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public DateTime CreatedAt { get; set; }

        public MobileUserLocation()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
