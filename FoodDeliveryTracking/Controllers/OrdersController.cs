using FoodDeliveryTracking.Data.Contracts;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Models.Request;
using FoodDeliveryTracking.Models.Response;
using FoodDeliveryTracking.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryTracking.Controllers
{
    /// <summary>
    /// Controller with orders endpoints.
    /// </summary>
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ILoggerManager _logger;

        /// <summary>
        /// Initialize a new instance of <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="loggerManager"></param>
        /// <param name="ordersRepository"></param>
        public OrdersController( IOrdersRepository ordersRepository,ILoggerManager loggerManager)
        {
            _ordersRepository = ordersRepository;
            _logger = loggerManager;
        }

        /// <summary>
        /// Adds a new order to the system.
        /// </summary>
        /// <param name="newOrder">The order details to be added.</param>
        /// <returns>A message response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<MessageResponse<String>>> AddOrder([FromBody] OrderRequest newOrder)
        {
            try
            {
                if (await _ordersRepository.CreateOrderAsync(newOrder)) 
                    return Ok(MessageResponse<String>.Success($"Se ha guardado la nueva orden."));
                return Ok(MessageResponse<String>.Fail($"No se ha podido guardar el pedido y se desconoce la causa."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to save a new order. Message: {ex.Message}");                
                return BadRequest(MessageResponse<String>.Fail($"No se ha podido guardar el pedido."));
            }
        }

        /// <summary>
        /// Deletes an existing order based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the order to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _ordersRepository.DeleteOrderAsync(id);
            if (!result)
            {
                _logger.LogInfo("Order not found.");                
                return NotFound(MessageResponse<String>.Success("Order not found."));
            }

            return NoContent(); // Devuelve un 204 No Content cuando la eliminación es exitosa.
        }

        /// <summary>
        /// Assigns a vehicle to an order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <param name="vehicleId">The ID of the vehicle to assign.</param>
        /// <returns>An IActionResult indicating the result of the assignment.</returns>
        [HttpPut("{orderId}/assign-vehicle/{vehicleId}")]
        public async Task<IActionResult> AssignVehicleToOrder(int orderId, int vehicleId)
        {
            var result = await _ordersRepository.AssignVehicleToOrderAsync(orderId, vehicleId);
            if (result)
            {
                return Ok(MessageResponse<String>.Success("Vehicle assigned to order successfully." ));
            }
            _logger.LogInfo("Order not found."); 
            return NotFound(MessageResponse<String>.Success("Order not found or failed to assign vehicle."));
        }

        /// <summary>
        /// Retrieves the order and its associated vehicle's location using the order ID.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>An IActionResult with the order and vehicle location or an error message.</returns>
        [HttpGet("{orderId}/location")]
        public async Task<IActionResult> GetVehicleLocation(int orderId)
        {
            var  vehicleLocation = await _ordersRepository.GetVehicleLocationAsync(orderId);
            if (vehicleLocation == null)
            {
                _logger.LogInfo("Order not found."); 
                return NotFound(MessageResponse<String>.Success("Order or vehicle not found." ));
            }

            return Ok(new
            {
                VehicleLocation = vehicleLocation
            });
        }
    }
}
