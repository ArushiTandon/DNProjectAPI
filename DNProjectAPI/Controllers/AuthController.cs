using DNProjectAPI.Dto;
using DNProjectAPI.iService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DNProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginUser(dto);

                if(result.StatusCode == 404)
                {
                    return NotFound(result.Message);
                }

                if(result.StatusCode == 401)
                {
                    return Unauthorized(result.Message);
                }


                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserDto user)
        {
            try
            {
                var result = await _authService.RegisterUser(user);

                if(result.StatusCode == 409)
                {
                    return Conflict(result.Message);
                }

                if(result.StatusCode == 201)
                {
                    return StatusCode(201, result);
                }

                return StatusCode(result.StatusCode, result.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
