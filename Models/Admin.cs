using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MasterThesisWebApplication.Models
{
    public class Admin : IdentityUser<int>
    {
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }
        
    }
}
