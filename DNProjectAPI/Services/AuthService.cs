using DNProjectAPI.Data;
using DNProjectAPI.Dto;
using DNProjectAPI.iService;
using Microsoft.EntityFrameworkCore;

namespace DNProjectAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tuple<int, string>> LoginUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

                if(existingUser == null)
                {
                    return new Tuple<int, string>(404, "User not found");
                }
                
                if(existingUser.Password != dto.Password)
                {
                    return new Tuple<int, string>(401, "Invalid password");
                }

                return new Tuple<int, string>(200, "Login successful");
            }
            catch (Exception)
            {
                return new Tuple<int, string>(500, "An error occurred while processing your request");
            }
        }
    }
}
