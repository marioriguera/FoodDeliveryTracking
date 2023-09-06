namespace FoodDeliveryTracking.Services.Models
{
    public interface ILocation
    {
        /// <summary>
        /// Gets or sets the unique identifier of the location.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the location was recorded.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the location.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the location.
        /// </summary>
        public decimal Longitude { get; set; }
    }
}
