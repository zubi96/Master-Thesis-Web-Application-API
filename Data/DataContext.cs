using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MasterThesisWebApplication.Data
{
    public class DataContext : IdentityDbContext<Admin, Role, int, IdentityUserClaim<int>, AdminRole,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<MobileUser> MobileUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AdminRole>(adminRole =>
            {
                adminRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                adminRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.AdminRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                adminRole.HasOne(ur => ur.Admin)
                    .WithMany(r => r.AdminRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}