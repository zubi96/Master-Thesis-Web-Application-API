using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Models;

namespace MasterThesisWebApplication.Data.Interfaces
{
    public interface IMobileAuthRepository
    {
        Task<MobileUser> Register(MobileUser user, string password);
        Task<MobileUser> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
