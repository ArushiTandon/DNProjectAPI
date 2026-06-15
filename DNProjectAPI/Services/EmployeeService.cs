using DNProjectAPI.Data;
using DNProjectAPI.Dto;
using DNProjectAPI.iService;
using Microsoft.EntityFrameworkCore;
namespace DNProjectAPI.Services

{
    public class EmployeeService(AppDbContext _context) : IEmployeeService
    {
        public async Task<ApiResponseDto<List<EmployeeDto>>> GetAllEmployees()
        {
            try
            {
                var employees = await _context.Employees.Select(e => new EmployeeDto
                {
                    id = e.id,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department,
                    Position = e.Position,
                    Salary = e.Salary,
                    DOB = e.DOB,
                })
                    .ToListAsync();

                return new ApiResponseDto<List<EmployeeDto>>
                {
                    StatusCode = 200,
                    Message = "Employees retrieved successfully",
                    Data = employees
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<EmployeeDto>>
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }   
    }
}
