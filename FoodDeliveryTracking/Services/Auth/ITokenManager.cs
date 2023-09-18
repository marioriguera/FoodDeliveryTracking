using FoodDeliveryTracking.Models.Response;

namespace FoodDeliveryTracking.Services.Auth
{
    public interface ITokenManager
    {
        /// <summary>
        /// Get token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>String token.</returns>
        string GetToken(AuthUserResponse user, string secret);
    }
}
