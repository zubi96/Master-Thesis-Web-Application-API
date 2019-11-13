﻿using System;
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
        Task<bool> RegionExists(string username);
        Task<Region> GetRegion(int regionId);
        Task<ICollection<Region>> GetRegions();
        Task<bool> ModeratorExists(string username);
        Task<Admin> GetModerator(int moderatorId);
    }
}