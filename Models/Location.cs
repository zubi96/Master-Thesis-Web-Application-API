using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Dtos;

namespace MasterThesisWebApplication.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string LatLong { get; set; }
        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
