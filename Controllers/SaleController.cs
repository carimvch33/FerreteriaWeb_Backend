using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Sales;
using FerreteríaWeb_Backend.Services.Interfaces;
using FerreteríaWeb_Backend.DAOs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FerreteríaWeb_Backend.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Employee")]
[Route("api/sale")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _service;
    private readonly IEmployeeDao _employeeDao;

    public SaleController(ISaleService service, IEmployeeDao employeeDao)
    {
        _service = service;
        _employeeDao = employeeDao;
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

    [HttpGet("cut")]
    public IActionResult GenerateCut([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var employeeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (employeeIdClaim == null) return Unauthorized();

        var employeeId = int.Parse(employeeIdClaim.Value);

        var employee = _employeeDao.GetById(employeeId);
        if (employee == null) return NotFound(new { Msg = "Empleado no encontrado" });

        var result = _service.GenerateCut(from, to, employee);

        return Ok(result.Data);
    }

    [HttpGet("cut/details")]
    public IActionResult GetCutDetails([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var details = _service.GetCutDetails(from, to);
        return Ok(details);
    }

    [HttpGet("{id}")]
    public IActionResult GetSaleTicket(int id)
    {
        var ticket = _service.GetSaleTicket(id);

        if (ticket == null)
            return NotFound(new { Msg = "Venta no encontrada" });

        return Ok(ticket);
    }
}