using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace MasterThesisWebApplication.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<Admin> adminManager, RoleManager<Role> roleManager)
        {
            if (!adminManager.Users.Any())
            {
                var roles = new List<Role>
                {
                    new Role {Name = "Admin"},
                    new Role {Name = "Moderator"}
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                // Create admin user
                var adminUser = new Admin
                {
                    UserName = "admin"
                };

                var result = adminManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = adminManager.FindByNameAsync("admin").Result;
                    adminManager.AddToRoleAsync(admin, "Admin").Wait();
                }

            }
        }
    }
}
