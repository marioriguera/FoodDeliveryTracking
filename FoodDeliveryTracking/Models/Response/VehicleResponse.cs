using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Response
{
    /// <summary>
    /// Manage vehicle http response.
    /// </summary>
    public class VehicleResponse : IVehicle
    {
        /// <summary>
        /// Initialize a new empty instance of <see cref="VehicleResponse"/> class.
        /// </summary>
        public VehicleResponse() { }

        /// <summary>
        /// Initialize a new instance of <see cref="VehicleResponse"/> class.
        /// </summary>
        /// <param name="vehicle"></param>
        public VehicleResponse(IVehicle vehicle)
        {
            Plate = vehicle.Plate;
            OrdersCollection.Clear();
            OrdersCollection.Concat(vehicle.Orders);
            LocationHistoryCollection.Clear();
            LocationHistoryCollection.Concat(vehicle.LocationHistory);
            CurrentLocationId = vehicle.CurrentLocationId;
            CurrentLocationObject = new LocationResponse(vehicle.CurrentLocation);
        }

        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets a collection of orders associated with the vehicle.
        /// </summary>
        [JsonIgnore]
        public ICollection<IOrder> Orders => OrdersCollection?.Cast<IOrder>()?.ToList();
        [JsonPropertyName("orders")]
        public ICollection<OrderResponse> OrdersCollection { get; set; } = new HashSet<OrderResponse>();

        /// <summary>
        /// Gets or sets a collection of location history records for the vehicle.
        /// </summary>
        [JsonIgnore]
        public ICollection<ILocation> LocationHistory => LocationHistoryCollection?.Cast<ILocation>()?.ToList();
        [JsonPropertyName("locationsHistory")]
        public ICollection<LocationResponse> LocationHistoryCollection { get; set; } = new HashSet<LocationResponse>();

        /// <summary>
        /// Gets or sets the unique identifier of the vehicle's current location.
        /// </summary>
        public int CurrentLocationId { get; set; }

        /// <summary>
        /// Gets or sets the current location of the vehicle.
        /// </summary>
        [JsonIgnore]
        public ILocation CurrentLocation => CurrentLocationObject;
        [JsonPropertyName("currentLocation")]
        public LocationResponse CurrentLocationObject { get; set; }
    }
}
