using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Models;

namespace MasterThesisWebApplication.Dtos
{
    public class LocationForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double Lat { get; set; } = 45.3286;
        public double Lng { get; set; } = 14.4665;
        public DateTime DateCreated { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public LocationForCreationDto()
        {
            DateCreated = DateTime.Now;
        }
    }
}
