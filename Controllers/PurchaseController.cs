using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FerreteríaWeb_Backend.Models.DTOs.Purchases;

namespace FerreteríaWeb_Backend.Controllers;

[ApiController]
///[Authorize(Roles = "Admin,Employee")]
[Route("api/purchase")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _service;

    public PurchaseController(IPurchaseService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult AddPurchase([FromBody] PurchaseDto purchase)
    {
        Result<PurchaseDto> result = _service.AddPurchase(purchase);

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