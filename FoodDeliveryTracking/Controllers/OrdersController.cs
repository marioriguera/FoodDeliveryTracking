using FoodDeliveryTracking.Data.Contracts;
using FoodDeliveryTracking.Models.Request;
using FoodDeliveryTracking.Models.Response;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;
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
        private readonly ILoggerManager _logger;
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class with the provided logger manager and orders repository.
        /// </summary>
        /// <param name="loggerManager">The logger manager used for logging.</param>
        /// <param name="ordersRepository">The repository for managing orders.</param>
        public OrdersController(ILoggerManager loggerManager, IOrdersRepository ordersRepository)
        {
            _logger = loggerManager;
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Insert a new order to the system.
        /// </summary>
        /// <param name="newOrder">The order details to be added.</param>
        /// <returns>A message response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertOrder([FromBody] AddOrderRequest newOrder)
        {
            try
            {
                if (await _ordersRepository.CreateOrderAsync(newOrder))
                    return Ok(MessageResponse<String>.Success($"The new order has been saved."));
                return Ok(MessageResponse<String>.Fail($"The order could not be saved and the cause is unknown."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to save a new order. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"The order could not be saved."));
            }
        }

        /// <summary>
        /// Deletes an existing order based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the order to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var result = await _ordersRepository.DeleteOrderAsync(id);
                if (!result)
                {
                    _logger.LogInfo("Order not found.");
                    return NotFound(MessageResponse<String>.Fail($"Order not found."));
                }

                return Ok(MessageResponse<String>.Success($"Order successfully deleted."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to delete the order with id {id}. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"The order could not be deleted."));
            }
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
            try
            {
                var result = await _ordersRepository.AssignVehicleToOrderAsync(orderId, vehicleId);
                if (result)
                {
                    return Ok(MessageResponse<String>.Success("Vehicle assigned to order successfully."));
                }
                _logger.LogInfo($"Order not found with id {orderId} and vehicle id {vehicleId}.");
                return NotFound(MessageResponse<String>.Fail("Order not found or failed to assign vehicle."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to save a new order. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"The order could not be saved."));
            }
        }

        /// <summary>
        /// Retrieves the order and its associated vehicle's location using the order ID.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>An IActionResult with the order and vehicle location or an error message.</returns>
        [HttpGet("{orderId}/location")]
        public async Task<IActionResult> GetVehicleLocation(int orderId)
        {
            try
            {
                var vehicleLocation = await _ordersRepository.GetVehicleLocationAsync(orderId);
                if (vehicleLocation == null)
                {
                    _logger.LogInfo("Order not found.");
                    return NotFound(MessageResponse<String>.Success("Order or vehicle not found."));
                }

                return Ok(MessageResponse<ILocation>.Success(vehicleLocation));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to save a new order. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"The order could not be saved."));
            }
        }
    }
}
