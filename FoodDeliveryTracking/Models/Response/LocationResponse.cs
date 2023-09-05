using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Models.Response
{
    /// <summary>
    /// Manage location http response.
    /// </summary>
    public class LocationResponse : ILocation
    {
        /// <summary>
        /// Initialize a new empty instance of <see cref="LocationResponse"/> class.
        /// </summary>
        public LocationResponse() { }

        /// <summary>
        /// Initialize a new instance of <see cref="LocationResponse"/> class.
        /// </summary>
        /// <param name="location">Location contract.</param>
        public LocationResponse(ILocation location)
        {
            Date = DateTime.Now;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }

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