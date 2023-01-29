using _3dd_Data.Models;
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
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<MyAppUser> _userManager;

        public JwtTokenService(IConfiguration configuration,UserManager<MyAppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }


        public async Task<string> CreateToken(MyAppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("name",user.UserName)
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
    }
}
