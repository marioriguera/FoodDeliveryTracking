using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Request
{
    public class AddVehicleLocationRequest : ILocation
    {
        /// <inheritdoc />
        [JsonIgnore]
        public int Id { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public DateTime Date { get; set; }

        /// <inheritdoc />
        public decimal Latitude { get; set; }

        /// <inheritdoc />
        public decimal Longitude { get; set; }
    }
}
