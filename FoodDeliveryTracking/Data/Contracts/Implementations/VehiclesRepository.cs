using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Represents a repository for retrieving information about vehicles.
    /// </summary>
    public class VehiclesRepository : GenericRepository<Vehicle>, IVehiclesRepository
    {
        private readonly ILoggerManager _logger;
        private readonly ApplicationDC _context;

        /// <summary>
        /// Initialize a new instance of <see cref="VehiclesRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing vehicle data.</param>
        public VehiclesRepository(ILoggerManager logger, ApplicationDC context) : base(context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ICollection<IVehicle>> GetAllVehiclesAsync()
        {
            _logger.LogTrace("Attempting to get all vehicles");
            var vehicles = _context.Vehicles.AsQueryable().Include(x => x.CurrentLocationObject);
            ICollection<IVehicle> ivehicles = await vehicles.Cast<IVehicle>().ToListAsync();
            return ivehicles;
        }

        /// <inheritdoc />
        public async Task InsertVehicleAsync(IVehicle vehicle)
        {
            _logger.LogTrace($"Attempting to save a vehicle:{vehicle}");
            Vehicle vehicleToInsert = new Vehicle(vehicle);
            await _context.Vehicles.AddAsync(vehicleToInsert);
        }

        /// <inheritdoc />
        public async Task UpdateVehicleLocationAsync(int vehicleId, ILocation newLocation)
        {
            _logger.LogTrace($"Attempting to update a vehicle location:{vehicleId}");
            var vehicle = await _context.Vehicles.AsQueryable().Where(x => x.Id.Equals(vehicleId))
                                                    .Include(v => v.CurrentLocationObject)
                                                    .FirstOrDefaultAsync();
            // Validations
            if (vehicle == null) throw new Exception($"Vehicle with id {vehicleId} does not exist.");
            if (vehicle.CurrentLocation == null) throw new Exception($"The vehicle with ID {vehicleId} does not have a defined location.");

            // Add the current location to the history before updating
            LocationHistory newLocationHistory = new LocationHistory(vehicle.CurrentLocation);
            vehicle.LocationHistoryCollection.Add(newLocationHistory);

            // Now update the current location
            vehicle.CurrentLocationObject.Latitude = newLocation.Latitude;
            vehicle.CurrentLocationObject.Longitude = newLocation.Longitude;
        }

        /// <inheritdoc />
        public async Task<ILocation> GetVehicleLocationAsync(int vehicleId)
        {
            _logger.LogTrace($"Attempting to retrieve a vehicle location:{vehicleId}");
            var vehicle = await _context.Vehicles.AsQueryable().Where(x => x.Id.Equals(vehicleId)).Include(x => x.CurrentLocationObject).FirstOrDefaultAsync();
            return vehicle?.CurrentLocation;
        }
    }
}
