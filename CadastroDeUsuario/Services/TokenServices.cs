using CadastroDeUsuario.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroDeUsuario.Services
{
    public class TokenServices
    {
        public string GenereteToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("userName", user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()),
                new Claim("loginTimesStamp", DateTime.UtcNow.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("578f38d89a7df5fa9f31f2cec381a638"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(expires: DateTime.Now.AddSeconds(86400), claims: claims, signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
