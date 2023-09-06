using FoodDeliveryTracking.Data.Models;

namespace FoodDeliveryTracking.Services.Models
{
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
