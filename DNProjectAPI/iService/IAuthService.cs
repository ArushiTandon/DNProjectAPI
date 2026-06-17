using DNProjectAPI.Dto;
using DNProjectAPI.Entity;

namespace DNProjectAPI.iService
{
    public interface IAuthService
    {
        Task<ApiResponseDto<UserResponseDto>> LoginUser(LoginDto dto);

        Task<ApiResponseDto<UserResponseDto>> RegisterUser(UserDto dto);
    }
}
