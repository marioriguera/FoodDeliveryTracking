namespace FoodDeliveryTracking.Services.Models
{
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
        /// Gets or sets user password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets user token.
        /// </summary>
        public string? Token { get; set; }
    }
}
