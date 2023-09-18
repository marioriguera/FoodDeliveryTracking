using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Response
{
    public class AuthUserResponse : IUser
    {
        /// <summary>
        /// Initialize a new empty instance of <see cref="AuthUserResponse"/> class.
        /// </summary>
        public AuthUserResponse() { }

        /// <summary>
        /// Initialize a new instance of <see cref="AuthUserResponse"/> class.
        /// </summary>
        /// <param name="user">User contract.</param>
        public AuthUserResponse(IUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Role = user.Role;
            Password = user.Password;
            Token = user.Token;
        }

        ///<inheritdoc/>
        [JsonIgnore]
        public int Id { get; set; }

        ///<inheritdoc/>
        public string? Name { get; set; }

        /// <inheritdoc/>
        public UserRole? Role { get; set; }

        ///<inheritdoc/>
        [JsonIgnore]
        public string? Password { get; set; }

        ///<inheritdoc/>        
        public string? Token { get; set; }
    }
}
