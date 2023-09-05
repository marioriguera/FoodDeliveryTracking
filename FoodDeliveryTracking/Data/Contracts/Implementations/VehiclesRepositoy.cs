using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Represents a repository for retrieving information about vehicles.
    /// </summary>
    public class VehiclesRepositoy : IVehiclesRepository
    {
        private readonly ApplicationDC _context;

        /// <summary>
        /// Initialize a new instance of <see cref="VehiclesRepositoy"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing vehicle data.</param>
        public VehiclesRepositoy(ApplicationDC context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ICollection<Vehicle>> GetAllAsync()
        {
            var vehicles = _context.Vehicles.AsQueryable().Include(x => x.CurrentLocation);
            return await vehicles.ToListAsync();
        }
    }
}
