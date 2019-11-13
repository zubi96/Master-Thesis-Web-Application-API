using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MasterThesisWebApplication.Data;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MasterThesisWebApplication.Controllers
{
    [ApiController]
    [Route("api/{adminId}/[controller]/")]
    public class ModeratorsController : Controller
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<Admin> _adminManager;
        private readonly RoleManager<Role> _roleManager;

        public ModeratorsController(IAdminRepository repo, IMapper mapper, UserManager<Admin> adminManager, RoleManager<Role> roleManager)
        {
            _repo = repo;
            _mapper = mapper;
            _adminManager = adminManager;
            _roleManager = roleManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> CreateModerator(int adminId,
            [FromBody] ModeratorForCreationDto moderatorForCreationDto)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (await _repo.ModeratorExists(moderatorForCreationDto.Username.ToLower()))
                return BadRequest("User with that username already exists");

            var region = await _repo.GetRegion(moderatorForCreationDto.RegionId);

            if (region == null)
                return BadRequest("Region does not exist");

            var moderator = new Admin()
            {
                UserName = moderatorForCreationDto.Username,
                RegionId = moderatorForCreationDto.RegionId
            };

            var result = _adminManager.CreateAsync(moderator, moderatorForCreationDto.Password).Result;

            if (result.Succeeded)
            {
                var createdModerator = _adminManager.FindByNameAsync(moderator.UserName).Result;
                _adminManager.AddToRoleAsync(createdModerator, "Moderator").Wait();
            }

            return Ok("Created");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{moderatorId}")]
        public async Task<IActionResult> UpdateModerator(int adminId, int moderatorId, ModeratorForCreationDto moderatorForCreationDto)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var moderatorFromRepo = await _repo.GetModerator(moderatorId);
            if (moderatorFromRepo == null)
                return BadRequest("Moderator does not exist");

            if (await _repo.ModeratorExists(moderatorForCreationDto.Username))
                return BadRequest("Moderator with that username already exists");

            var regionFromRepo = await _repo.GetRegion(moderatorForCreationDto.RegionId);
            if (regionFromRepo == null)
            {
                return BadRequest("Region does not exist");
            }

            _mapper.Map(moderatorForCreationDto, moderatorFromRepo);

            if (await _repo.SaveAll())
                return Ok("Moderator updated successfully");

            return BadRequest("Failed on moderator update");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{moderatorId}")]
        public async Task<IActionResult> DeleteModerator(int adminId, int moderatorId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var moderatorFromRepo = await _repo.GetModerator(moderatorId);

            if (moderatorFromRepo == null)
                return BadRequest("Moderator does not exist");

            if (await _adminManager.IsInRoleAsync(moderatorFromRepo, "Admin"))
                return BadRequest("Admin can not be deleted");

            _repo.Delete(moderatorFromRepo);

            if (await _repo.SaveAll())
                return Ok("Moderator deleted successfully");

            return BadRequest("Failed on region delete");
        }
    }
}