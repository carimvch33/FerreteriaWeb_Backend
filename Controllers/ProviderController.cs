using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerreteríaWeb_Backend.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/provider")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _service;

    public ProviderController(IProviderService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult AddProvider([FromBody] ProviderDto provider)
    {
        if (!provider.IsValid())
        {
            return BadRequest(new{ Msg = "Payload inválido"});
        }

        Result<ProviderDto> result = _service.AddProvider(provider);

        if(result.IsAccomplished)
        {
            return Created($"api/provider/{result.Data!.Id}", result.Data);
        }
        if(result.InnerException is not null)
        {
            return StatusCode(500, new{ Msg = result.Message });
        }

        return BadRequest(new{ Msg = result.Message });
    }

    [HttpGet("{id}")]
    public IActionResult GetProvider(int id)
    {
        Result<ProviderDto?> result = _service.GetProvider(id);

        if(result.IsAccomplished)
        {
            return Ok(result.Data);
        }
        if(result.InnerException is not null)
        {
            return StatusCode(500, new{ Msg = result.Message });
        }

        return BadRequest(new{ Msg = result.Message });
    }

    [HttpGet]
    public IActionResult GetAllProviders()
    {
        Result<List<ProviderDto>> result = _service.GetAllProviders();

        if(result.IsAccomplished)
        {
            return Ok(result.Data);
        }
        if(result.InnerException is not null)
        {
            return StatusCode(500, new{ Msg = result.Message });
        }

        return BadRequest(new{ Msg = result.Message });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProvider(int id, [FromBody] ProviderDto provider)
    {
        if (!provider.IsValid())
        {
            return BadRequest();
        }

        provider.Id = id;
        Result<bool> result = _service.UpdateProvider(provider);

        if(result.IsAccomplished)
        {
            if (result.Data)
            {
                return StatusCode(204);   
            }
            else
            {
                return NotFound();
            }
        }
        if(result.InnerException is not null)
        {
            return StatusCode(500, new{ Msg = result.Message });
        }

        return BadRequest(new{ Msg = result.Message });
    }

    [HttpPatch("{id}/state")]
    public IActionResult UpdateProviderState(int id)
    {
        Result<bool> result = _service.UpdateProviderState(id);

        if(result.IsAccomplished)
        {
            if (result.Data)
            {
                return StatusCode(204);   
            }
            else
            {
                return NotFound();
            }
        }
        if(result.InnerException is not null)
        {
            return StatusCode(500, new{ Msg = result.Message });
        }

        return BadRequest(new{ Msg = result.Message });
    }
}