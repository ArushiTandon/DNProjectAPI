using DNProjectAPI.Dto;

namespace DNProjectAPI.iService
{
    public interface IAuthService
    {
        Task<Tuple<int, string>> LoginUser(UserDto dto);
    }
}
