using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasterThesisWebApplication.Data;
using MasterThesisWebApplication.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterThesisWebApplication.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public DashboardController(IAdminRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("getNumberOfLocations")]
        public async Task<IActionResult> GetNumberOfLocations()
        {
            var numberOfLocations = await _repo.GetNumberOfLocations();
            return Ok(new
            {
                value = numberOfLocations
            });
        }

        [HttpGet("getNumberOfUsers")]
        public async Task<IActionResult> GetNumberOfUsers()
        {
            var numberOfUsers = await _repo.GetNumberOfUsers();
            return Ok(new
            {
                value = numberOfUsers
            });
        }

        [HttpGet("getTodayDiscoveredLocations")]
        public async Task<IActionResult> GetNumberOfTodayDiscoveredLocations()
        {
            var numberOfTodayDicoveredLocations = await _repo.GetNumberOfTodayDiscoveredLocations();
            return Ok(new
            {
                value = numberOfTodayDicoveredLocations
            });
        }
    }
}