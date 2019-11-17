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
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string LongDescription { get; set; }
        [Required]
        public string LatLong { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public LocationForCreationDto()
        {
            DateCreated = DateTime.Now;
        }
    }
}
