using DNProjectAPI.Data;
using DNProjectAPI.Dto;
using DNProjectAPI.Entity;
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
                var employees = await _context.Employees.AsNoTracking()
                    .Select(e => new EmployeeDto
                {
                    id = e.id,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department,
                    Position = e.Position,
                    Salary = e.Salary,
                    DOB = e.DOB,
                }).ToListAsync();

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


        public async Task<ApiResponseDto<EmployeeDto>> CreateEmployee(EmployeeDto dto)
        {
            try
            {
                var existingEmployee = await _context.Employees
                    .AnyAsync(e => e.Email == dto.Email);

                if (existingEmployee)
                {
                    return new ApiResponseDto<EmployeeDto>
                    {
                        StatusCode = 409,
                        Message = "Already Exist, try a different email."
                    };
                }

                var employee = new Employee
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Department = dto.Department,
                    Position = dto.Position,
                    Salary = dto.Salary,
                    DOB = dto.DOB,
                };

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                var responseEmployee = new EmployeeDto
                {
                    id = employee.id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    Position = employee.Position,
                    Salary = employee.Salary,
                    DOB = employee.DOB,
                };

                return new ApiResponseDto<EmployeeDto>
                {
                    StatusCode = 201,
                    Message = "Employee created successfully.",
                    Data = responseEmployee
                };
            }
            catch(Exception)
            {
                return new ApiResponseDto<EmployeeDto>
                {
                    StatusCode = 500,
                    Message = "An unexpected error occur.",
                };
            }
        }
    }
}
