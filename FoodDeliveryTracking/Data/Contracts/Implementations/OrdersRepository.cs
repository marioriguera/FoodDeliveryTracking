using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Implementation of the IOrdersRepository interface for creating orders in the database.
    /// </summary>
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _context;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger for tracing.</param>
        public OrdersRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously creates and saves a new order to the database.
        /// </summary>
        /// <param name="order">The order object implementing the IOrder interface.</param>
        /// <returns>True if the order is successfully saved; otherwise, false.</returns>
        public async Task<bool> CreateOrderAsync(IOrder order)
        {
            Order orderEF = new Order(order);
            await _context.Orders.AddAsync(orderEF);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Asynchronously dalete an order by orderId to the database.
        /// </summary>
        /// <param name="orderId">The orderId is a Order key.</param>
        /// <returns>True if the order is successfully deleted; otherwise, false.</returns>
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Assigns a vehicle to an order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> AssignVehicleToOrderAsync(int orderId, int vehicleId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.AssignedVehicleId = vehicleId;
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Gets the vehicle's location based on the order ID.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>An object containing the order and vehicle location or null if not found.</returns>
        public async Task<(Order order, ILocation location)> GetOrderAndVehicleLocationAsync(int orderId)
        {
            var order = await _context.Orders
                                      .Include(o => o.AssignedVehicleObject)                                      
                                      .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order?.AssignedVehicle == null)
            {
                return (null, null);
            }

            return (order, order.AssignedVehicleObject.CurrentLocation);
        }
    }
}
