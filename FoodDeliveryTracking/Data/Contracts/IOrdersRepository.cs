using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts
{
    /// <summary>
    /// Defines an interface for an orders repository, providing a method to create a new order asynchronously.
    /// </summary>
    public interface IOrdersRepository
    {
        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="order">The order to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<bool> CreateOrderAsync(IOrder order);


        /// <summary>
        /// Delete a new order asynchronously.
        /// </summary>
        /// <param name="orderId">The orderId to be delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<bool> DeleteOrderAsync(int orderId);

        /// <summary>
        /// Assign a Vehicle To an Order asynchronously.
        /// </summary>
        /// <param name="orderId">The orderId to assign the vehicle.</param>
        /// <param name="vehicleId">The vehicleId to be Assign.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<bool> AssignVehicleToOrderAsync(int orderId, int vehicleId);

        /// <summary>
        /// Retrieve the order ans the current location.
        /// </summary>
        /// <param name="orderId">The orderId to be search.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<ILocation> GetVehicleLocationAsync(int orderId);
    }
}
