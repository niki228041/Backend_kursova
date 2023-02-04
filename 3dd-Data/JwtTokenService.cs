using _3dd_Data.Models;
using _3dd_Data.Models.ViewModels;
using _3dd_Data.Settings;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(MyAppUser user);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogle(ExternalLoginRequest request);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<MyAppUser> _userManager;
        private readonly GoogleAuthSettings _googleAuthSettings;

        public JwtTokenService(IConfiguration configuration,UserManager<MyAppUser> userManager,
            GoogleAuthSettings googleAuthSettings)
        {
            _configuration = configuration;
            _userManager = userManager;
            _googleAuthSettings = googleAuthSettings;
        }


        public async Task<string> CreateToken(MyAppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("name",user.Email)
            };

            foreach (var role in roles)
                claims.Add(new Claim("roles", role));

            var key = _configuration.GetValue<string>("JwtKey");
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(signinKey,SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials:signinCredentials,
                expires: DateTime.Now.AddDays(10),
                claims:claims
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogle(ExternalLoginRequest request)
        {
            var setting = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string>() { _googleAuthSettings.ClientId }
            };

            var asf = _googleAuthSettings.ClientId;

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, setting);
            return payload;
        }
    }
}
