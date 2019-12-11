using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Models
{
    public class MobileUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<MobileUserLocation> MobileUserLocations { get; set; }
    }
}
