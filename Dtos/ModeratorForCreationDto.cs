using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Dtos
{
    public class ModeratorForCreationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RegionId { get; set; }
    }
}
