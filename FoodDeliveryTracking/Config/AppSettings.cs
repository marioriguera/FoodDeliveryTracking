namespace FoodDeliveryTracking.Config
{
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets secret.
        /// </summary>
        public string? Secret { get; set; }

        /// <summary>
        /// Gets or sets secret key for passwords.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets initial vector for passwords.
        /// </summary>
        public string InitialVector { get; set; }
    }
}
