using System.Text.Json.Serialization;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Models.Request
{
    /// <summary>
    /// Represents an order request that implements the <see cref="IOrder"/> interface.
    /// </summary>
    public class OrderRequest : IOrder
    {
        /// <inheritdoc />
        public string Description { get; set; }
        /// <inheritdoc />
        public int? AssignedVehicleId { get; set; }
        /// <inheritdoc />
        [JsonIgnore]
        public IVehicle AssignedVehicle { get; set; }
        /// <inheritdoc />
        public OrderStatus Status { get; set; }
    }


}
