using FoodDeliveryTracking.Data.Contracts;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Models.Response;
using FoodDeliveryTracking.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryTracking.Controllers
{
    public record NewOrder
    {
        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id vehicle.
        /// </summary>
        public int AssignedVehicleId { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public OrderStatus Status { get; private set; } = OrderStatus.Placed;
    }

    /// <summary>
    /// Controller with orders endpoints.
    /// </summary>
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// Initialize a new instance of <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="loggerManager"></param>
        /// <param name="ordersRepository"></param>
        public OrdersController(ILoggerManager loggerManager, IOrdersRepository ordersRepository)
        {
            _logger = loggerManager;
            _ordersRepository = ordersRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<MessageResponse<String>>> AddOrder([FromBody] OrderResponse newOrder)
        {
            try
            {
                if (await _ordersRepository.CreateOrderAsync(newOrder)) return Ok(MessageResponse<String>.Success($"Se ha guardado la nueva orden."));
                return Ok(MessageResponse<String>.Fail($"No se ha podido guardar el pedido y se desconoce la causa."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to save a new order. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"No se ha podido guardar el pedido."));
            }
        }
    }
}
