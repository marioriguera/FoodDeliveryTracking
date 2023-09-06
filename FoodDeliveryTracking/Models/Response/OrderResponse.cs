using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Response
{
    /// <summary>
    /// Manage order http response.
    /// </summary>
    public class OrderResponse : IOrder
    {
        /// <summary>
        /// Initilize a new empty instance of <see cref="OrderResponse"/> class.
        /// </summary>
        public OrderResponse() { }

        /// <summary>
        /// Initialize a new instance of <see cref="OrderResponse"/> class.
        /// </summary>
        /// <param name="order"></param>
        public OrderResponse(IOrder order)
        {
            Description = order.Description;
            AssignedVehicleId = order.AssignedVehicleId;
            AssignedVehicleObject = order.AssignedVehicle != null ? new VehicleResponse(order.AssignedVehicle) : null;
            Status = order.Status;
        }

        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public int? AssignedVehicleId { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public IVehicle? AssignedVehicle => AssignedVehicleObject;
        [JsonPropertyName("vehicle")]
        public VehicleResponse? AssignedVehicleObject { get; set; }

        /// <inheritdoc />
        public OrderStatus Status { get; set; }
    }
}
