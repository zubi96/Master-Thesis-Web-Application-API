using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterThesisWebApplication.Data;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace MasterThesisWebApplication.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public CategoriesController(IAdminRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repo.GetCategories();

            if (!categories.Any())
                return NoContent();

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryToReturnDto>>(categories);

            return Ok(categoriesToReturn);
        }

        [HttpGet("{categoryId}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _repo.GetCategory(categoryId);

            if (category == null)
                return NoContent();

            var categoryToReturn = _mapper.Map<CategoryToReturnDto>(category);

            return Ok(categoryToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto categoryForCreationDto)
        {
            if (await _repo.CategoryByNameExist(categoryForCreationDto.Name.ToLower()))
                return BadRequest("Category with that name already exists");

            var category = _mapper.Map<Category>(categoryForCreationDto);
            _repo.Add(category);

            if (await _repo.SaveAll())
            {
                var categoryToReturn = _mapper.Map<CategoryToReturnDto>(category);
                return CreatedAtRoute("GetCategory", new {categoryId = category.Id}, categoryToReturn);
            }

            return BadRequest("Creating the category failed.");
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryForCreationDto categoryForCreationDto)
        {
            var categoryFromRepo = await _repo.GetCategory(categoryId);

            if (categoryFromRepo == null)
                return NoContent();

            if (await _repo.CategoryByNameExist(categoryForCreationDto.Name.ToLower()))
                return BadRequest("Category with that name already exists");

            _mapper.Map(categoryForCreationDto, categoryFromRepo);

            if (await _repo.SaveAll())
                return Ok("Category updated");

            return BadRequest("Updating the category failed.");
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _repo.GetCategory(categoryId);

            if (category == null)
                return NoContent();

            _repo.Delete(category);

            if (await _repo.SaveAll())
                return Ok("Category deleted");

            return BadRequest("Deleting category failed.");
        }
        
    }
}
