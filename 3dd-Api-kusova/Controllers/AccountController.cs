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


        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginRequest model)
        {
            var data = await _jwtTokenService.VerifyGoogle(model);

            if(data == null) { return BadRequest("Google token is successfuly verified"); }
            var info = new UserLoginInfo(model.Provider,data.Subject,model.Provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider,info.ProviderKey);

            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(data.Email);
                if(user == null)
                {
                    user = new MyAppUser
                    {
                        Email = data.Email,
                        UserName = data.Email,
                        Surname = data.Name,
                        Name = data.FamilyName
                    };
                    var resultCreate = await _userManager.CreateAsync(user);
                    if(!resultCreate.Succeeded)
                    {
                        return BadRequest(new { error = "Не вдалось створити користувача" });
                    }
                }
                var resultLogin = await _userManager.AddLoginAsync(user, info);
                if(!resultLogin.Succeeded)
                {
                    return BadRequest(new { error = "Не вдалося увійти" });
                }
            }

            var token = await _jwtTokenService.CreateToken(user);

            return Ok(token);
        }
    }
}
