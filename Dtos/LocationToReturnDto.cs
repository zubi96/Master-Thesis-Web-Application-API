using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Models;

namespace MasterThesisWebApplication.Dtos
{
    public class LocationToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<PhotoToReturnDto> Photos { get; set; }
    }
}
