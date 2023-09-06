using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Response
{
    public class LocationOrderResponse : ILocation
    {
        /// <summary>
        /// Initialize a new empty instance of <see cref="LocationResponse"/> class.
        /// </summary>
        public LocationOrderResponse() { }

        /// <summary>
        /// Initialize a new instance of <see cref="LocationResponse"/> class.
        /// </summary>
        /// <param name="location">Location contract.</param>
        public LocationOrderResponse(ILocation location)
        {
            Date = DateTime.Now;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }

        /// <inheritdoc/>
        [JsonIgnore]
        public int Id { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public DateTime Date { get; set; }

        /// <inheritdoc/>
        public decimal Latitude { get; set; }

        /// <inheritdoc/>
        public decimal Longitude { get; set; }
    }
}
