using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;
using DailyReportSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyReportSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
            {
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });
            }
            return Ok(result);
        }
    }
}
