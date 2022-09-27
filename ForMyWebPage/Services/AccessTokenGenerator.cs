using ForMyWebPage.Models;
using ForMyWebPage.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForMyWebPage.Services
{
    public class AccessTokenGenerator
    {
        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ven83GM6SfrmO-TBHbjTk6JhP_3CMsIvmSdo4KrbQNvp4vHO3w1_0zJ3URkmkYGhz2tgPlfd7v1l2I6QkIh4Bumdj6FyFZEBpxjE4MpfdNVcNINvVj87cLyTRmIcaGxmfylY7QErP8GFA-k4UoH_eQmGKGK44TRzYj5hZYGWIC8"));
            SigningCredentials credidentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            JwtSecurityToken token = new JwtSecurityToken(
                "https://localhost:7219",
                "https://localhost:7219",
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(3),
                credidentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
