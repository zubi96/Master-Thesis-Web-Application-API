using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MasterThesisWebApplication.Models
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
    }
}
