using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Dtos
{
    public class CategoryForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        public CategoryForCreationDto()
        {
            DateCreated = DateTime.Now;
        }
    }
}
