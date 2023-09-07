using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Request
{
    /// <summary>
    /// Represents an order request that implements the <see cref="IOrder"/> interface.
    /// </summary>
    public class AddOrderRequest : IOrder
    {
        /// <inheritdoc />
        [JsonIgnore]
        public int Id { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public int? AssignedVehicleId { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public IVehicle AssignedVehicle { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public OrderStatus Status { get; set; } = OrderStatus.Placed;
    }


}
