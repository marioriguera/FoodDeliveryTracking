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

        /// <summary>
        /// Initialize a new instance of <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="loggerManager"></param>
        public OrdersController(ILoggerManager loggerManager)
        {
            _logger = loggerManager;
        }

        // [HttpPost]
        // [Route("add")]
        // public async Task<ActionResult<MessageResponse<String>>> AddOrder([FromBody] NewOrder newOrder)
        // {
        //     try
        //     {
        // 
        //         return Ok(MessageResponse<String>.Success($"Se ha guardado la nueva orden."));
        //     }
        //     catch (Exception ex)
        //     {
        // 
        //         return BadRequest(MessageResponse<String>.Fail($"No se ha podido guardar el pedido."));
        //     }
        // }
    }
}
