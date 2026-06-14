using DNProjectAPI.Data;
using DNProjectAPI.Dto;
using DNProjectAPI.Entity;
using DNProjectAPI.iService;
using Microsoft.AspNetCore.Identity;
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

        public async Task<ApiResponseDto<UserResponseDto>> LoginUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

                if (existingUser == null)
                {
                    return new ApiResponseDto<UserResponseDto>
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }


                var passwordHasher = new PasswordHasher<string>();

                var verifyPassword = passwordHasher.VerifyHashedPassword(dto.Email, existingUser.Password, dto.Password);

                if(verifyPassword == PasswordVerificationResult.Failed)
                {

                    return new ApiResponseDto<UserResponseDto>
                    {
                        StatusCode = 401,
                        Message = "Invalid password or email"
                    };

                }

                var responseUser = new UserResponseDto
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                };

                return new ApiResponseDto<UserResponseDto>
                {
                    StatusCode = 200,
                    Message = "Login successful",
                    Data = responseUser
                };
            }
            catch (Exception)
            {
                return new ApiResponseDto<UserResponseDto>
                {
                    StatusCode = 500,
                    Message = "An error occurred while processing your request"
                };
            }
        }

        public async Task<ApiResponseDto<UserResponseDto>> RegisterUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

                if(existingUser != null)
                {
                    return new ApiResponseDto<UserResponseDto>
                    {
                        StatusCode = 409,
                        Message = "Email already exists"
                    };

                }

                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = PasswordHashing(dto)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var responseUser = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };

                return new ApiResponseDto<UserResponseDto>
                {
                    StatusCode = 201,
                    Message = "User registered successfully",
                    Data = responseUser
                };
            }

            catch(Exception)
            {
                return new ApiResponseDto<UserResponseDto>
                {
                    StatusCode = 500,
                    Message = "An error Occured",
                };
            }
        }

        private string PasswordHashing(UserDto dto)
        {
            var PassHasher = new PasswordHasher<string>();

            var hash = PassHasher.HashPassword(dto.Email, dto.Password);

            return hash;
        }
    }
}
