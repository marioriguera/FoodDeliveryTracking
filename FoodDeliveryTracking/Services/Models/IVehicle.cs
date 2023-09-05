using FoodDeliveryTracking.Data.Models;

namespace FoodDeliveryTracking.Services.Models
{
    public interface IVehicle
    {
        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets a collection of orders associated with the vehicle.
        /// </summary>
        public ICollection<IOrder> Orders { get; }

        /// <summary>
        /// Gets or sets a collection of location history records for the vehicle.
        /// </summary>
        public ICollection<ILocation> LocationHistory { get; }

        /// <summary>
        /// Gets or sets the unique identifier of the vehicle's current location.
        /// </summary>
        public int CurrentLocationId { get; set; }

        /// <summary>
        /// Gets or sets the current location of the vehicle.
        /// </summary>
        public ILocation CurrentLocation { get; }
    }
}
