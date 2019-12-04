using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Data.Interfaces;
using MasterThesisWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterThesisWebApplication.Data.Repositories
{
    public class MobileRepository : IMobileRepository
    {
        private readonly DataContext _context;

        public MobileRepository(DataContext context)
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

        public async Task<MobileUser> GetUser(int userId)
        {
            return await _context.MobileUsers.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<Location>> GetDiscoveredLocations(int userId)
        {
            var discoveredLocationsId = await _context.MobileUserLocations
                .Where(u => u.MobileUserId == userId)
                .Select(l => l.LocationId)
                .ToListAsync();

            var discoveredLocations = await _context.Locations.ToListAsync();
            discoveredLocations.RemoveAll(l => !discoveredLocationsId.Contains(l.Id));

            return discoveredLocations;
        }

        public async Task<IEnumerable<Location>> GetUndiscoveredLocations(int userId)
        {
            var discoveredLocationsId = await _context.MobileUserLocations
                .Where(u => u.MobileUserId == userId)
                .Select(l => l.LocationId)
                .ToListAsync();

            var undiscoveredLocations = await _context.Locations.ToListAsync();

            if(discoveredLocationsId.Any())
                undiscoveredLocations.RemoveAll(l => discoveredLocationsId.Contains(l.Id));

            return undiscoveredLocations;
        }

        public async Task<Location> GetLocation(int locationId)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Id == locationId);
        }

        public async Task<bool> UserLocationAlreadyExist(int locationId, int userId)
        {
            var userLocation = await _context.MobileUserLocations.FirstOrDefaultAsync(l => l.LocationId == locationId && l.MobileUserId == userId);

            return userLocation != null;
        }
    }
}
