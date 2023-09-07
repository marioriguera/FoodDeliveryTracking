using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;
using FoodDeliveryTracking.SignalRComunication;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Represents a repository for retrieving information about vehicles.
    /// </summary>
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ILoggerManager _logger;
        private readonly ApplicationDC _context;
        private readonly IHubContext<VehicleLocationHub> _hubContext;

        /// <summary>
        /// Initialize a new instance of <see cref="VehiclesRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing vehicle data.</param>
        public VehiclesRepository(ILoggerManager logger, ApplicationDC context, IHubContext<VehicleLocationHub> hubContext)
        {
            _logger = logger;
            _context = context;
            _hubContext = hubContext;
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
            var vehicle = await _context.Vehicles.AsQueryable().Where(x => x.Id.Equals(vehicleId))
                                                    .Include(v => v.CurrentLocationObject)
                                                    .FirstOrDefaultAsync();
            // Validations
            if (vehicle == null) return false;
            if (vehicle.CurrentLocation == null) return false;

            // Add the current location to the history before updating
            LocationHistory newLocationHistory = new LocationHistory(vehicle.CurrentLocation);
            vehicle.LocationHistoryCollection.Add(newLocationHistory);

            // Now update the current location
            vehicle.CurrentLocationObject.Latitude = newLocation.Latitude;
            vehicle.CurrentLocationObject.Longitude = newLocation.Longitude;

            // Send updated loqations to real time clients
            await _hubContext.Clients.All.SendAsync("receivevehiclelocation", vehicleId, newLocation.Latitude,
                                                    newLocation.Longitude);


            return await _context.SaveChangesAsync() > 0;
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
