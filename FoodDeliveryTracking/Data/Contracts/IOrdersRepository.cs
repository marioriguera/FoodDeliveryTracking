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
    }
}
