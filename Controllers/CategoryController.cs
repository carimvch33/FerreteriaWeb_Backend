using FerreteríaWeb_Backend.Models.DTOs.Categories;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FerreteríaWeb_Backend.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult RegisterCategory([FromBody] RegisterCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = _categoryService.RegisterCategory(dto);

                var response = new CategoryResponseDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    IsActive = category.IsActive
                };

                return Created("", response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [HttpGet("active")]
        public IActionResult GetActiveCategories()
        {
            try
            {
                return Ok(_categoryService.GetActiveCategories());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }
        [HttpGet("{id}/products")]
        public IActionResult GetProductsByCategory(int id)
        {
            try
            {
                return Ok(_categoryService.GetActiveProductsByCategory(id));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }


    }
}
