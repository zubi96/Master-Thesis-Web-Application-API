using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterThesisWebApplication.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RegionExists(string username)
        {
            if (await _context.Regions.AnyAsync(r => r.Name == username))
            {
                return true;
            }

            return false;
        }

        public async Task<Region> GetRegion(int regionId)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == regionId);
        }

        public async Task<ICollection<Region>> GetRegions()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<bool> ModeratorExists(string username)
        {
            if (await _context.Users.AnyAsync(r => r.UserName == username))
            {
                return true;
            }

            return false;
        }

        public async Task<Admin> GetModerator(int moderatorId)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Id == moderatorId);
        }
    }
}
