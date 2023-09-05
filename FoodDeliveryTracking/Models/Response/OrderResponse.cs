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

        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id vehicle.
        /// </summary>
        public int? AssignedVehicleId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle assigned to fulfill the order.
        /// </summary>
        [JsonIgnore]
        public IVehicle? AssignedVehicle => AssignedVehicleObject;
        [JsonPropertyName("vehicle")]
        public VehicleResponse? AssignedVehicleObject { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public OrderStatus Status { get; set; }
    }
}
