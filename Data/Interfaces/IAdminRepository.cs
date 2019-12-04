using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Models;

namespace MasterThesisWebApplication.Data
{
    public interface IAdminRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int categoryId);
        Task<bool> CategoryByNameExist(string categoryName);
        Task<IEnumerable<Location>> GetLocations();
        Task<Location> GetLocation(int locationId);
        Task<Photo> GetPhoto(int photoId);
    }
}
