using BaseLibrary.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repository.Contract;
using static System.Collections.Specialized.BitVector32;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUserAccount accountInterface) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {

            if (user == null) return BadRequest("Model is empty");
            var result = await accountInterface.CreateAsync(user);
            return Ok(result);

        }


        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync(Login user)
        {

            if (user == null) return BadRequest("Model is empty");
            var result = await accountInterface.SignInAsync(user);
            return Ok(result);

        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null) return BadRequest("Model is Empty");
            var result = await accountInterface.RefreshTokenAsync(token);
            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult>GetUsersAsync()
        {
            var result = await accountInterface.GetUsersAsync();
            return Ok(result);
        }

    }
}
