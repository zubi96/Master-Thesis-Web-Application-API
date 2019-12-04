using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Models;

namespace MasterThesisWebApplication.Data.Interfaces
{
    public interface IMobileRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<MobileUser> GetUser(int userId);
        Task<IEnumerable<Location>> GetDiscoveredLocations(int userId);
        Task<IEnumerable<Location>> GetUndiscoveredLocations(int userId);
        Task<Location> GetLocation(int locationId);
        Task<bool> UserLocationAlreadyExist(int locationId, int userId);
    }
}
