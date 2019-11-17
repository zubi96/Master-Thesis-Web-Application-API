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

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }
        public async Task<bool> CategoryByNameExist(string categoryName)
        {
            return await _context.Categories.AnyAsync(c => c.Name.ToLower() == categoryName);
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> GetLocation(int locationId)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Id == locationId);
        }

        public async Task<Photo> GetPhoto(int photoId)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.Id == photoId);
        }
    }
}
