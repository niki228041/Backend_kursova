using _3dd_Data;
using _3dd_Data.Models;
using _3dd_Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _3dd_Api_kusova.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<MyAppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountController(UserManager<MyAppUser> userManager,IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var checkPassword = await _userManager.CheckPasswordAsync(user,model.Password);


            if(user == null || !checkPassword) { return BadRequest(); }

            var token = await _jwtTokenService.CreateToken(user);

            return Ok(token);
        }
    }
}
