using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasterThesisWebApplication.Data;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterThesisWebApplication.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public LocationsController(IAdminRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            var locations = await _repo.GetLocations();

            if (!locations.Any())
                return NoContent();

            var locationsToReturn = _mapper.Map<IEnumerable<LocationToReturnDto>>(locations);

            return Ok(locationsToReturn);
        }

        [HttpGet("{locationId}", Name = "GetLocation")]
        public async Task<IActionResult> GetLocation(int locationId)
        {
            var location = await _repo.GetCategory(locationId);

            if (location == null)
                return NoContent();

            var locationToReturn = _mapper.Map<LocationToReturnDto>(location);

            return Ok(locationToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationForCreationDto locationForCreationDto)
        {
            var location = _mapper.Map<Location>(locationForCreationDto);
            _repo.Add(location);

            if (await _repo.SaveAll())
            {
                var locationToReturn = _mapper.Map<LocationToReturnDto>(location);
                return CreatedAtRoute("GetLocation", new { locationId = location.Id }, locationToReturn);
            }

            return BadRequest("Creating the location failed.");
        }

        [HttpPut("{locationId}")]
        public async Task<IActionResult> UpdateLocation(int locationId, [FromBody] LocationForCreationDto locationForCreationDto)
        {
            var locationFromRepo = await _repo.GetLocation(locationId);

            if (locationFromRepo == null)
                return NoContent();

            _mapper.Map(locationForCreationDto, locationFromRepo);

            if (await _repo.SaveAll())
                return Ok("Location updated");

            return BadRequest("Updating the location failed.");
        }

        [HttpDelete("{locationId}")]
        public async Task<IActionResult> DeleteLocation(int locationId)
        {
            var location = await _repo.GetLocation(locationId);

            if (location == null)
                return NoContent();

            _repo.Delete(location);

            if (await _repo.SaveAll())
                return Ok("Location deleted");

            return BadRequest("Deleting location failed.");
        }
    }
}