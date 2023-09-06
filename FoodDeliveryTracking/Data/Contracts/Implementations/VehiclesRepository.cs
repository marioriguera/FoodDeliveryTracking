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
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ILoggerManager _logger;
        private readonly ApplicationDC _context;

        /// <summary>
        /// Initialize a new instance of <see cref="VehiclesRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing vehicle data.</param>
        public VehiclesRepository(ILoggerManager logger, ApplicationDC context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ICollection<IVehicle>> GetAllAsync()
        {
            _logger.LogTrace("Attempting to get all vehicles");
            var vehicles = _context.Vehicles.AsQueryable().Include(x => x.CurrentLocationObject);
            ICollection<IVehicle> ivehicles = await vehicles.Cast<IVehicle>().ToListAsync();
            return ivehicles;
        }

        /// <inheritdoc />
        public async Task<bool> InsertVehicleAsync(IVehicle vehicle)
        {
            _logger.LogTrace($"Attempting to save a vehicle:{vehicle}");
            Vehicle vehicleToInsert = new Vehicle(vehicle);
            await _context.Vehicles.AddAsync(vehicleToInsert);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateVehicleLocationAsync(int vehicleId, ILocation newLocation)
        {
            _logger.LogTrace($"Attempting to update a vehicle location:{vehicleId}");
            var vehicle = await _context.Vehicles.Include(v => v.LocationHistory)
                                             .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null) return false;
            // Add the current location to the history before updating
            if (vehicle.CurrentLocation == null) return false;

            vehicle.LocationHistory.Add(vehicle.CurrentLocation);
            // Now update the current location
            vehicle.CurrentLocationObject = new CurrentLocation(newLocation);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<ILocation> GetVehicleLocationAsync(int vehicleId)
        {
            _logger.LogTrace($"Attempting to retrieve a vehicle location:{vehicleId}");
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            return vehicle?.CurrentLocation;
        }
    }
}
