using FoodDeliveryTracking.Models.Request;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDeliveryTracking.Services.Auth
{
    public static class TokenManager
    {
        /// <summary>
        /// Get token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>String token.</returns>
        public static string GetToken(AuthUserRequest user, string secret)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            string tokenWrite = string.Empty;
            byte[] key = Encoding.ASCII.GetBytes(secret);
            ClaimsIdentity claims = new();

            claims.AddClaim(new(ClaimTypes.NameIdentifier, user.Name ?? string.Empty));

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
            tokenWrite = tokenHandler.WriteToken(token);

            return tokenWrite;
        }
    }
}
