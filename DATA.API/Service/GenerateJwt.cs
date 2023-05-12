using E_BOOK.API.Service.Service_Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MODEL.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_BOOK.API.Service
{
    public class GenerateJwt : IGenerateJwt
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public GenerateJwt(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(AppUser user, string Email)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var name = user.FirstName + " " + user.LastName;
            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, Email),
        new Claim(JwtRegisteredClaimNames.Jti, user.Id),
        new Claim(ClaimTypes.Name, name),
        new Claim(ClaimTypes.UserData, user.Id)
    };
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha384Signature));
            var Jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return Jwttoken;
        }
    }
}

