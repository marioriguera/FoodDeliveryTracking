using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Implementation of the IOrdersRepository interface for creating orders in the database.
    /// </summary>
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDC _context;
        private readonly ILoggerManager _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger for tracing.</param>
        public OrdersRepository(ApplicationDC context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<bool> CreateOrderAsync(IOrder order)
        {
            _logger.LogTrace($"Attempting to save {order}");
            Order orderEF = new Order(order);
            await _context.Orders.AddAsync(orderEF);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            _logger.LogTrace($"Attempting to delete {orderId}");
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                _logger.LogTrace($"Order dest'nt exits {orderId}");
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            _logger.LogTrace($"Delete was successful {orderId}");
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> AssignVehicleToOrderAsync(int orderId, int vehicleId)
        {
            _logger.LogTrace($"Attempting to Assign a vehicle to order:  {orderId}");
            var order = await _context.Orders.AsQueryable().Where(x => x.Id.Equals(orderId)).FirstOrDefaultAsync();
            if (order == null) return false;

            var vehicle = await _context.Vehicles.AsQueryable().Where(x => x.Id.Equals(vehicleId)).FirstOrDefaultAsync();
            if (vehicle == null) return false;

            order.AssignedVehicleObject = vehicle;
            // When assigning an order to a vehicle, I consider that the order changes status to in transit.
            order.Status = OrderStatus.InTransit;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<ILocation> GetOrderLocationAsync(int orderId)
        {
            _logger.LogTrace($"Attempting to Assign a vehicle to order {orderId}");
            var order = await _context.Orders.AsQueryable()
                                      .Include(o => o.AssignedVehicleObject)
                                      .ThenInclude(o => o.CurrentLocationObject)
                                      .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) throw new NullReferenceException($"Order with id {orderId} it's not found in database.");
            if (order!.AssignedVehicleObject == null) return null;

            return order.AssignedVehicleObject.CurrentLocation;
        }
    }
}
