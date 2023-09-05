using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;

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

        /// <inheritdoc/>
        public async Task<bool> CreateOrderAsync(IOrder order)
        {
            _logger.LogTrace($"Attempting to save {order}");

            Order orderEF = new Order(order);
            await _context.Orders.AddAsync(orderEF);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
