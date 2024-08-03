using ClinicApp2.Entities;
using ClinicApp2.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AppUser appUser, [FromServices] TokenService tokenService)
        {
            if (appUser.UserName == "demo" && appUser.Password == "demo123")
            {
                var user = new AppUser { UserName = appUser.UserName };

                var token = tokenService.CreateToken(user);

                return Ok(new { token, success = true });
            }

            return BadRequest(new { message = "Invalid username or password" });
        }
    }
}
