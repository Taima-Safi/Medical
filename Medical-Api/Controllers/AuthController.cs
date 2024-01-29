
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using M_Services.IServices;
using M_Core.Dtos.UserDto;

namespace Medical.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _authService.GetUsers());
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterUserDto dto)
        {
            return Ok(await _authService.Register(dto));
        }
        

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            return Ok(await _authService.Login(dto));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshtoken)
        {
            return Ok(await _authService.RefreshToken(refreshtoken));
        }
        
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string userName)
        {
            return Ok(await _authService.ForgetPassword(userName));
        }


        [HttpPost("ResetPasswordToken")]
        public async Task<IActionResult> ResetPasswordToken(string restToken)
        {
            return Ok(await _authService.ResetPasswordToken(restToken));
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            return Ok(await _authService.ResetPassword(dto));
        }

        [HttpGet("Verify")]
        public async Task<IActionResult> Verify(string token)
        {
            return Ok(await _authService.Verify(token));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(LoginDto dto)
        {
            return Ok(await _authService.Delete(dto));
        }
    }
}
