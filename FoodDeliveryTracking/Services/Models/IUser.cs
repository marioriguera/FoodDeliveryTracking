namespace FoodDeliveryTracking.Services.Models
{
    /// <summary>
    /// User role - Normal user role.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// User role - Normal user role.
        /// </summary>
        User,

        /// <summary>
        /// Admin role - Administrator role.
        /// </summary>
        Admin,

        /// <summary>
        /// Moderator role - Moderator role.
        /// </summary>
        Moderator,

        /// <summary>
        /// Editor role - Editor role.
        /// </summary>
        Editor,

        /// <summary>
        /// Guest role - Guest role with limited access.
        /// </summary>
        Guest
    }


    /// <summary>
    /// User interface contract.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets user role.
        /// </summary>
        public UserRole? Role { get; set; }

        /// <summary>
        /// Gets or sets user password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets user token.
        /// </summary>
        public string? Token { get; set; }
    }
}
