using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Request
{
    public class AddAuthUserRequest : IUser
    {
        ///<inheritdoc/>
        [JsonIgnore]
        public int Id { get; set; }

        ///<inheritdoc/>
        public string? Name { get; set; }

        /// <inheritdoc/>
        public UserRole? Role { get; set; }

        ///<inheritdoc/>
        public string? Password { get; set; }

        ///<inheritdoc/>
        [JsonIgnore]
        public string? Token { get; set; }
    }
}
