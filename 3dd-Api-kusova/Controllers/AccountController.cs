using _3dd_Data;
using _3dd_Data.Models;
using _3dd_Data.Models.ViewModels;
using AutoMapper;
using Compass.Data.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace _3dd_Api_kusova.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<MyAppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;


        public AccountController(UserManager<MyAppUser> userManager,IJwtTokenService jwtTokenService,IMapper mapper)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;        
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationViewModel model)
        {
            var validator = new RegistrationValidation();
            var validatorResult = await validator.ValidateAsync(model);

            if (validatorResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if( user == null) {

                    MyAppUser new_user = _mapper.Map<MyAppUser>(model);


                    await _userManager.CreateAsync(new_user, model.Password);
                    await _userManager.AddToRoleAsync(new_user, model.Role);
                    return Ok("Created User!");
                }
                return BadRequest(new {error = "Email is used by another user"});
            }

            return BadRequest("Not Valide Input");
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
                        Name = data.Name,
                        Surname = data.FamilyName
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
