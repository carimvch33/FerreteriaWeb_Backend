using FerreteríaWeb_Backend.Models.DTOs.Products;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult RegisterProduct([FromBody] RegisterProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var product = _productService.RegisterProduct(dto);

                return Created("", new ProductResponseDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    IsActive = product.IsActive
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var response = _productService.UpdateProduct(id, dto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("{id}/add-inventory")]
        public IActionResult AddInventory(int id, [FromBody] AddInventoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = _productService.AddInventory(id, dto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Por favor intente más tarde.");
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("active")]
        public IActionResult GetActiveProducts()
        {
            try
            {
                return Ok(_productService.GetActiveProducts());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Por favor intente más tarde.");
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet()]
        public IActionResult GetProductsBySearchString([FromQuery] string s)
        {
            if(string.IsNullOrWhiteSpace(s))
            {
                return NoContent();
            }

            Result<List<ProductListItemDto>> result = _productService.GetProductsBySearchString(s);

            if(result.IsAccomplished)
            {
                if (result.Data!.Count == 0)
                {
                    return NoContent();
                }
                return Ok(result.Data);
            }
            if(result.InnerException is not null)
            {
                return StatusCode(500, new{ Msg = result.Message });
            }

            return BadRequest(new{ Msg = result.Message });
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Result<ProductListItemDto?> result = _productService.GetProductById(id);

            if(result.IsAccomplished)
            {
                if (result.Data is null)
                {
                    return NotFound();
                }
                return Ok(result.Data);
            }
            if(result.InnerException is not null)
            {
                return StatusCode(500, new{ Msg = result.Message });
            }

            return BadRequest(new{ Msg = result.Message });      
        }
     }
}
