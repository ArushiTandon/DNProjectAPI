using DNProjectAPI.Dto;

namespace DNProjectAPI.iService
{
    public interface IEmployeeService
    {
        Task<ApiResponseDto<List<EmployeeDto>>> GetAllEmployees();

        Task<ApiResponseDto<EmployeeDto>> CreateEmployee(EmployeeDto dto);
    }
}
