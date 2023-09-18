using FoodDeliveryTracking.Data.Models;

namespace FoodDeliveryTracking.Services.Models
{
    /// <summary>
    /// Represents the possible status values of an order.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order has been placed but not yet in transit.
        /// </summary>
        Placed,

        /// <summary>
        /// The order is in transit.
        /// </summary>
        InTransit,

        /// <summary>
        /// The order has been delivered.
        /// </summary>
        Delivered,

        /// <summary>
        /// The order has been canceled.
        /// </summary>
        Canceled
    }
    public interface IOrder
    {
        /// <summary>
        /// Gets or sets the unique identifier of the order.
        /// </summary>
        public int Id { get; set; }

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
        public IVehicle? AssignedVehicle { get; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public OrderStatus Status { get; set; }
    }
}
