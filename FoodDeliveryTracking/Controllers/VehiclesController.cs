using FoodDeliveryTracking.Data.Contracts;
using FoodDeliveryTracking.Models.Request;
using FoodDeliveryTracking.Models.Response;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryTracking.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IVehiclesRepository _vehiclesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="vehiclesRepository">The repository for retrieving information about vehicles.</param>
        public VehiclesController(ILoggerManager logger, IVehiclesRepository vehiclesRepository)
        {
            _logger = logger;
            _vehiclesRepository = vehiclesRepository;
        }

        /// <summary>
        /// Retrieves a collection of all vehicles asynchronously.
        /// </summary>
        /// <returns>An asynchronous task that represents the action's result, which contains a collection of vehicles.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> AllVehiclesAsync()
        {
            try
            {
                // Repository response.
                var repositoryResponse = await _vehiclesRepository.GetAllAsync();

                // ICollection to response.
                ICollection<VehicleResponse> vehicles = new List<VehicleResponse>();
                repositoryResponse.ToList().ForEach(v =>
                {
                    vehicles.Add(new VehicleResponse(v));
                });

                return Ok(MessageResponse<ICollection<VehicleResponse>>.Success(vehicles));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to request AllVehicle. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"No se pudo obtener el listado de vehiculos."));
            }
        }

        /// <summary>
        /// Inserts a new vehicle into the system.
        /// </summary>
        /// <param name="vehicle">The vehicle object to insert.</param>
        /// <returns>An IActionResult indicating the result of the insert operation.</returns>
        [HttpPost]
        [Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertVehicleAsync([FromBody] AddVehicleRequest vehicle)
        {
            try
            {
                var result = await _vehiclesRepository.InsertVehicleAsync(vehicle);
                if (result)
                {
                    return Ok(MessageResponse<String>.Success($"Vehicle added successfully."));
                }
                return BadRequest(MessageResponse<String>.Fail($"Failed to add the vehicle."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to add new vehicle. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"Failed to add the vehicle."));
            }
        }

        /// <summary>
        /// Updates the location of a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <param name="location">The new location object.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
        [HttpPut]
        [Authorize]
        [Route("update-location/{vehicleId}")]
        public async Task<IActionResult> UpdateVehicleLocationAsync(int vehicleId, [FromBody] LocalitationRequest location)
        {
            try
            {
                var result = await _vehiclesRepository.UpdateVehicleLocationAsync(vehicleId, location);
                if (result)
                {
                    return Ok(MessageResponse<String>.Success("Vehicle location updated successfully."));
                }
                return NotFound(MessageResponse<String>.Fail("Vehicle not found or failed to update location."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to update the vehicle with id {vehicleId}. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"Failed to update the vehicle."));
            }
        }

        /// <summary>
        /// Retrieves the location of a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <returns>An IActionResult with the vehicle location or an error message.</returns>        
        [HttpGet]
        [Authorize]
        [Route("location/{vehicleId}")]
        public async Task<IActionResult> GetVehicleLocationAsync(int vehicleId)
        {
            try
            {
                var location = await _vehiclesRepository.GetVehicleLocationAsync(vehicleId);
                if (location != null)
                {
                    return Ok(MessageResponse<ILocation>.Success(location));
                }
                return NotFound(MessageResponse<String>.Fail("Vehicle not found."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled error when trying to get the localitation of the vehicle with id {vehicleId}. Message: {ex.Message}");
                return BadRequest(MessageResponse<String>.Fail($"Failed to get the localitation of the vehicle."));
            }
        }
    }
}
