using Microsoft.EntityFrameworkCore;
using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Implementation of the IOrdersRepository interface for creating orders in the database.
    /// </summary>
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        private readonly ApplicationDC _context;
        private readonly ILoggerManager _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger for tracing.</param>
        public OrdersRepository(ApplicationDC context, ILoggerManager logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task CreateOrderAsync(IOrder order)
        {
            _logger.LogTrace($"Attempting to save {order}");
            Order orderEF = new Order(order);
            await _context.Orders.AddAsync(orderEF);
        }

        /// <inheritdoc />
        public async Task DeleteOrderAsync(int orderId)
        {
            _logger.LogTrace($"Attempting to delete {orderId}");
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                _logger.LogTrace($"Order dest'nt exits {orderId}");
            }

            _context.Orders.Remove(order);
            _logger.LogTrace($"Delete was successful {orderId}");
        }

        /// <inheritdoc />
        public async Task AssignVehicleToOrderAsync(int orderId, int vehicleId)
        {
            _logger.LogTrace($"Attempting to Assign a vehicle to order:  {orderId}");
            var order = await _context.Orders.AsQueryable().Where(x => x.Id.Equals(orderId)).FirstOrDefaultAsync();
            if (order == null) throw new Exception($"Order with id {orderId} does not exist.");

            var vehicle = await _context.Vehicles.AsQueryable().Where(x => x.Id.Equals(vehicleId)).FirstOrDefaultAsync();
            if (vehicle == null) throw new Exception($"Vehicle with id {vehicleId} does not exist.");

            order.AssignedVehicleObject = vehicle;
            // When assigning an order to a vehicle, I consider that the order changes status to in transit.
            order.Status = OrderStatus.InTransit;
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
