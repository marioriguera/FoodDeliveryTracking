using FoodDeliveryTracking.Data.Contracts;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryTracking.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IVehiclesRepository _vehiclesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="vehiclesRepository">The repository for retrieving information about vehicles.</param>
        public VehiclesController(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        /// <summary>
        /// Retrieves a collection of all vehicles asynchronously.
        /// </summary>
        /// <returns>An asynchronous task that represents the action's result, which contains a collection of vehicles.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<MessageResponse<ICollection<Vehicle>>>> AllVehiclesAsync()
        {
            try
            {
                // ToDo: No enviar la clase Vehicle como respuesta.
                var vehicles = await _vehiclesRepository.GetAllAsync();
                return Ok(MessageResponse<ICollection<Vehicle>>.Success(vehicles));
            }
            catch (Exception ex)
            {
                //ToDo: Implementar el logger
                Console.WriteLine(ex);
                return BadRequest(MessageResponse<String>.Fail("No se pudo obtener el listado de vehiculos."));
            }
        }
    }
}
