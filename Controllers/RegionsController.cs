using System.Collections.Generic;
using AutoMapper;
using MasterThesisWebApplication.Data;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Castle.Core.Internal;

namespace MasterThesisWebApplication.Controllers
{
    [ApiController]
    [Route("api/{adminId}/[controller]/")]
    public class RegionsController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public RegionsController(IAdminRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("{regionId}", Name = "GetRegion")]
        public async Task<IActionResult> GetRegion(int adminId, int regionId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var region = await _repo.GetRegion(regionId);

            if (region == null)
                return BadRequest("Region does not exist");

            var regionToReturn = _mapper.Map<RegionToReturnDto>(region);

            return Ok(regionToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRegion(int adminId, [FromBody] RegionForCreationDto regionForCreationDto)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (await _repo.RegionExists(regionForCreationDto.Name.ToLower()))
                return BadRequest("Region with that name already exists");

            var region = _mapper.Map<Region>(regionForCreationDto);

            _repo.Add(region);

            if (await _repo.SaveAll())
                return Ok("Region created");

            return BadRequest("Failed while creating region");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> GetRegions(int adminId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var regions = await _repo.GetRegions();

            var regionsToReturn = _mapper.Map<ICollection<RegionToReturnDto>>(regions);

            if (regions.IsNullOrEmpty())
                return BadRequest("No regions added yet");

            return Ok(regionsToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{regionId}")]
        public async Task<IActionResult> UpdateRegion(int adminId, int regionId, RegionForCreationDto regionForCreationDto)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var regionFromRepo = await _repo.GetRegion(regionId);

            if (regionFromRepo == null)
                return BadRequest("Region does not exist");

            if (await _repo.RegionExists(regionForCreationDto.Name) && regionFromRepo.Name != regionForCreationDto.Name)
                return BadRequest("Region already exists");

            _mapper.Map(regionForCreationDto, regionFromRepo);

            if (await _repo.SaveAll())
                return Ok("Region updated successfully");

            return BadRequest("Failed on region update");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{regionId}")]
        public async Task<IActionResult> DeleteRegion(int adminId, int regionId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var regionFromRepo = await _repo.GetRegion(regionId);

            if (regionFromRepo == null)
                return BadRequest("Region does not exist");

            _repo.Delete(regionFromRepo);

            if (await _repo.SaveAll())
                return Ok("Region deleted successfully");

            return BadRequest("Failed on region delete");
        }
    }
}