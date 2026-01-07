using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerreteríaWeb_Backend.Controllers;

[ApiController]
//[Authorize(Roles = "Admin,Employee")]
[Route("api/sale")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _service;

    public SaleController(ISaleService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult AddSale([FromBody] SaleDto sale)
    {
        Result<SaleDto> result = _service.AddSale(sale);

        if(result.IsAccomplished)
        {
            return Created($"api/sale/{result.Data!.Id}", result.Data);
        }
        if(result.InnerException is not null)
        {
            return StatusCode(500, new{ Msg = result.Message });
        }

        return BadRequest(new{ Msg = result.Message });
    }
}