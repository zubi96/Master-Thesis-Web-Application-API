using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Dtos
{
    public class RegionForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public RegionForCreationDto()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
