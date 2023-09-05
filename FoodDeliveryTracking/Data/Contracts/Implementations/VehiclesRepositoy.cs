using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Represents a repository for retrieving information about vehicles.
    /// </summary>
    public class VehiclesRepositoy : IVehiclesRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initialize a new instance of <see cref="VehiclesRepositoy"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing vehicle data.</param>
        public VehiclesRepositoy(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ICollection<IVehicle>> GetAllAsync()
        {
            var vehicles = _context.Vehicles.AsQueryable().Include(x => x.CurrentLocationObject);
            ICollection<IVehicle> ivehicles = await vehicles.Cast<IVehicle>().ToListAsync();
            return ivehicles;
        }
        /// <summary>
        /// Inserts a new vehicle into the database.
        /// </summary>
        /// <param name="vehicle">The vehicle object to insert.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> InsertVehicleAsync(IVehicle vehicle)
        {
            Vehicle vehicleToInsert = new Vehicle(vehicle);
            await _context.Vehicles.AddAsync(vehicleToInsert);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Updates the location of a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <param name="newLocation">The new location to set.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> UpdateVehicleLocationAsync(int vehicleId, CurrentLocation newLocation)
        {
            var vehicle = await _context.Vehicles.Include(v => v.LocationHistory)
                                             .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null) return false;

            // Add the current location to the history before updating
            if (vehicle.CurrentLocation != null)
            {
                vehicle.LocationHistory.Add(vehicle.CurrentLocation);
            }

            // Now update the current location
            vehicle.CurrentLocationObject = newLocation;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Retrieves the location of a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <returns>The location of the vehicle if found; otherwise, null.</returns>
        public async Task<ILocation> GetVehicleLocationAsync(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            return vehicle?.CurrentLocation;
        }
    }
}
