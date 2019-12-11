using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Dtos;
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

        public async Task<int> GetNumberOfLocations()
        {
            var locations = await _context.Locations.ToListAsync();
            return !locations.Any() ? 0 : locations.Count;
        }

        public async Task<int> GetNumberOfUsers()
        {
            var users = await _context.MobileUsers.ToListAsync();
            return !users.Any() ? 0 : users.Count;
        }

        public async Task<int> GetNumberOfTodayDiscoveredLocations()
        {
            var today = DateTime.Now.Date;
            var todayDiscoveredLocations = await _context.MobileUserLocations.Where(dc => dc.CreatedAt.Date == today).ToListAsync();

            return !todayDiscoveredLocations.Any() ? 0 : todayDiscoveredLocations.Count;
        }

        public async Task<List<int>> UsersGenderCount()
        {
            var maleCount =  await _context.MobileUsers.CountAsync(g => g.Gender == "Male");
            var femaleCount = await _context.MobileUsers.CountAsync(g => g.Gender == "Female");
            var otherCount = await _context.MobileUsers.CountAsync(g => g.Gender == "Other");

            var genderCount = new List<int>
            {
                maleCount, 
                femaleCount, 
                otherCount
            };

            return genderCount;
        }

        public async Task<List<int>> UsersAgeCount()
        {
            var usersAge = await _context.MobileUsers.Select(db => db.DateOfBirth).ToListAsync();
            var firstAgeGroup = 0; // 0-18
            var secondAgeGroup = 0; // 19-30
            var thirdAgeGroup = 0;  // 31-40
            var fourthAgeGroup = 0; // 41+
            foreach (var userAge in usersAge)
            {
                if (GetAge(userAge) <= 18)
                    firstAgeGroup++;
                else if (GetAge(userAge) > 18 && GetAge(userAge) <= 30)
                    secondAgeGroup++;
                else if (GetAge(userAge) > 30 && GetAge(userAge) <= 40)
                    thirdAgeGroup++;
                else
                {
                    fourthAgeGroup++;
                }
            }

            var ageCount = new List<int>
            {
                firstAgeGroup,
                secondAgeGroup,
                thirdAgeGroup,
                fourthAgeGroup
            };

            return ageCount;
        }

        private int GetAge(DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;
        }

        public async Task<List<int>> UsersCountryCount()
        {
            var croatiaCount = await _context.MobileUsers.CountAsync(c => c.Country == "Croatia");
            var ukCount = await _context.MobileUsers.CountAsync(c => c.Country == "United Kingdom");
            var germanyCount = await _context.MobileUsers.CountAsync(c => c.Country == "Germany");
            var russiaCount = await _context.MobileUsers.CountAsync(c => c.Country == "Russia");

            var countryCount = new List<int>
            {
                croatiaCount,
                ukCount,
                germanyCount,
                russiaCount
            };

            return countryCount;
        }

        public async Task<List<int>> GetScansByMonth()
        {
            var scannedLocations = await _context.MobileUserLocations.ToListAsync();

            var monthsCount = new List<int>();
            for (var i = 1; i <= 12; i++)
            {
                monthsCount.Add(scannedLocations.Count(c => c.CreatedAt.Month == i));
            }
            return monthsCount;
        }

        public async Task<IEnumerable<LocationWithScanCount>> GetLocationsWithTimesScanned()
        {
            var locations = await _context.Locations.ToListAsync();
            var scannedLocations = await _context.MobileUserLocations.Select(l => l.Location).ToListAsync();

            var locationsByScanToReturn = new List<LocationWithScanCount>();
            foreach (var location in locations)
            {
                var locationByScan = new LocationWithScanCount
                {
                    Name = location.Name,
                    ScanCount = scannedLocations.Count(l => l.Name == location.Name)
                };

                locationsByScanToReturn.Add(locationByScan);
            }

            return locationsByScanToReturn;
        }
    }
}
