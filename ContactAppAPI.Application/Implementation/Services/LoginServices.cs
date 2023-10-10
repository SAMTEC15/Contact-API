using ContactAppAPI.Application.DTO;
using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactAppAPI.Application.Implementation.Services
{
    public class LoginServices : ILoginServices
    {
        UserManager<ContactUser> _userManager; SignInManager<ContactUser> _signInManager; IConfiguration _config;

        public LoginServices(UserManager<ContactUser> userManager, SignInManager<ContactUser> signInManager, IConfiguration config)
        {
            _userManager = userManager; _signInManager = signInManager; _config = config;
        }



        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return "Email not found";
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                return GenerateJwtToken(user, role);
            }
            return "Invalid credentials";
        }

        private string GenerateJwtToken(ContactUser contactUser, string roles)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, contactUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, contactUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, roles)
            };

            // Set a default expiration in minutes if "AccessTokenExpiration" is missing or not a valid numeric value.
            if (!double.TryParse(jwtSettings["AccessTokenExpiration"], out double accessTokenExpirationMinutes))
            {
                accessTokenExpirationMinutes = 30; // Default expiration of 30 minutes.
            }

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
