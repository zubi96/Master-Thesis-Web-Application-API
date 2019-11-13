using Microsoft.AspNetCore.Identity;

namespace MasterThesisWebApplication.Models
{
    public class AdminRole : IdentityUserRole<int>
    {
        public virtual Admin Admin { get; set; }
        public virtual Role Role { get; set; }
    }
}
