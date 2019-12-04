using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MasterThesisWebApplication.Data.Interfaces;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MasterThesisWebApplication.Controllers
{
    [Route("[controller]/{userId}/")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IMobileRepository _repo;
        private readonly IMapper _mapper;

        public MobileController(IMobileRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("discoverLocation/{locationId}")]
        public async Task<IActionResult> discoverLocation(int locationId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);
            if (user == null)
                return NotFound();

            var location = await _repo.GetLocation(locationId);
            if (location == null)
                return NotFound();

            if (await _repo.UserLocationAlreadyExist(locationId, userId))
                return BadRequest("User has already discovered this location.");

            var mobileUserLocation = new MobileUserLocation
            {
                LocationId = locationId,
                MobileUserId = userId
            };

            _repo.Add(mobileUserLocation);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to add location to user");
        }

        [HttpDelete("undiscoverLocation/{locationId}")]
        public async Task<IActionResult> undiscoverLocation(int locationId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);
            if (user == null)
                return NotFound();

            var location = await _repo.GetLocation(locationId);
            if (location == null)
                return NotFound();

            var mobileUserLocation = new MobileUserLocation
            {
                LocationId = locationId,
                MobileUserId = userId
            };

            _repo.Delete(mobileUserLocation);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to remove location from user");
        }

        [HttpGet("getDiscoveredLocations")]
        public async Task<IActionResult> getDiscoveredLocations(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var discoveredLocations = await _repo.GetDiscoveredLocations(userId);

            if (!discoveredLocations.Any())
                return BadRequest("No locations yet discovered");

            var discoveredLocationsToReturn = _mapper.Map<IEnumerable<DiscoveredLocationToReturnDto>>(discoveredLocations);

            return Ok(discoveredLocationsToReturn);
        }

        [HttpGet("getUndiscoveredLocations")]
        public async Task<IActionResult> getUndiscoveredLocations(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var undiscoveredLocations = await _repo.GetUndiscoveredLocations(userId);

            var undiscoveredLocationsToReturn = _mapper.Map<IEnumerable<UndiscoveredLocationToReturnDto>>(undiscoveredLocations);

            return Ok(undiscoveredLocationsToReturn);
        }
    }
}