using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Request
{
    /// <summary>
    /// Represents an Vehicle request that implements the <see cref="IVehicle"/> interface.
    /// </summary>
    public class VehicleRequest : IVehicle
    {
        /// <inheritdoc />
        public string Plate { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public ICollection<IOrder> Orders { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public ICollection<ILocation> LocationHistory { get; set; }

        /// <inheritdoc />
        public int CurrentLocationId { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public ILocation CurrentLocation { get; set; }
    }
}
