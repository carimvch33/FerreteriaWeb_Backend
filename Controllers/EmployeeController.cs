using FerreteríaWeb_Backend.Models.DTOs.Employees;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerreteríaWeb_Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult RegisterEmployee([FromBody] RegisterEmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = _employeeService.RegisterEmployee(dto);

                var response = new EmployeeResponseDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Role = employee.Role.ToString()
                };

                return Created("", response);
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

        [HttpGet("active")]
        public IActionResult GetActiveEmployees()
        {
            return Ok(_employeeService.GetActiveEmployees());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeRequestDto dto)
        {
            try
            {
                var response = _employeeService.UpdateEmployee(id, dto);
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
    }
}
