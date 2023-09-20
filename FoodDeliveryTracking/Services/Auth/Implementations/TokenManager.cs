using FoodDeliveryTracking.Models.Response;
using FoodDeliveryTracking.Services.Logger;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDeliveryTracking.Services.Auth.Implementations
{
    public class TokenManager : ITokenManager
    {
        private readonly ILoggerManager _logger;

        /// <summary>
        /// Initialize a new instance of <see cref="TokenManager"/> class.
        /// </summary>
        /// <param name="logger"></param>
        public TokenManager(ILoggerManager logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public string GetToken(AuthUserResponse user, string secret)
        {
            _logger.LogTrace($"Get token starting");

            JwtSecurityTokenHandler tokenHandler = new();
            string tokenWrite = string.Empty;
            byte[] key = Encoding.ASCII.GetBytes(secret);
            ClaimsIdentity claims = new();

            claims.AddClaim(new(ClaimTypes.Name, user.Name ?? string.Empty));
            claims.AddClaim(new(ClaimTypes.Role, user.Role.ToString() ?? string.Empty));

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = "FoodDeliveryTrackingTokenIssuer",
                Audience = "MiApplicationFoodDeliveryTracking",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            tokenWrite = tokenHandler.WriteToken(token);

            return tokenWrite;
        }
    }
}
