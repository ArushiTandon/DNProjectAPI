using DNProjectAPI.Dto;
using DNProjectAPI.iService;
using DNProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DNProjectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService EmployeeService) : ControllerBase
    {
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await EmployeeService.GetAllEmployees();

                return StatusCode(result.StatusCode, result);
                 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                var result = await EmployeeService.CreateEmployee(employee);

                if(result.StatusCode == 409)
                    return Conflict(result.Message);
                

                if(result.StatusCode == 201)
                 return Created("", result);
                

                return StatusCode(result.StatusCode, result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employee)
        {
            try
            {
                var result = await EmployeeService.UpdateEmployee(id, employee);

                if(result.StatusCode == 404)
                    return NotFound(result.Message);


                return StatusCode(result.StatusCode, result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await EmployeeService.DeleteEmployee(id);

                if (result.StatusCode == 404)
                    return NotFound(result.Message);


                return StatusCode(result.StatusCode, result.Message);


            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}