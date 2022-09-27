using ForMyWebPage.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForMyWebPage.Services
{
    public class RefreshTokenGenerator
    {
        public static string refreshTokenSecret = "venPaGM6SfrmO-TBHbjTk6JhP_3CMsIvmSdo4KrbQNvp4vHO3w1_0zJ3URkmkYGhz2tgPlfd7v1l2I6QkIh4Bumdj6FyFZEBpxjE4MpfdNVcNINvVj87cLyTRmIcaGxmfylY7QErP8GFA-k4UoH_eQmGKGK44TRzYj5hZYGWIC8";
        public string RefreshToken (User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret));
            SigningCredentials credidentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken token = new JwtSecurityToken(
                "https://localhost:7219",
                "https://localhost:7219",
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(131400),
                credidentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
