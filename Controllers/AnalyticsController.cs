using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasterThesisWebApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterThesisWebApplication.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public AnalyticsController(IAdminRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("getUsersGenderCount")]
        public async Task<IActionResult> GetUsersGenderCount()
        {
            var usersGenderCount = await _repo.UsersGenderCount();
            return Ok(usersGenderCount);
        }

        [HttpGet("getUsersAgeCount")]
        public async Task<IActionResult> GetUsersAgeCount()
        {
            var usersAgeCount = await _repo.UsersAgeCount();
            return Ok(usersAgeCount);
        }

        [HttpGet("getUsersCountryCount")]
        public async Task<IActionResult> GetUsersCountryCount()
        {
            var usersCountryCount = await _repo.UsersCountryCount();
            return Ok(usersCountryCount);
        }

        [HttpGet("getScansByMonth")]
        public async Task<IActionResult> GetScansByMonth()
        {
            var scansByMonth = await _repo.GetScansByMonth();
            return Ok(scansByMonth);
        }

        [HttpGet("getLocationsWithTimesScanned")]
        public async Task<IActionResult> GetLocationsWithTimesScanned()
        {
            var locationsWithTimesScanned = await _repo.GetLocationsWithTimesScanned();
            return Ok(locationsWithTimesScanned);
        }
    }
}