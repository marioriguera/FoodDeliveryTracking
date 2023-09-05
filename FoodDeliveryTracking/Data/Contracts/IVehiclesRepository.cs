using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts
{
    /// <summary>
    /// Represents a repository for retrieving information about vehicles.
    /// </summary>
    public interface IVehiclesRepository
    {
        /// <summary>
        /// Asynchronously retrieves a collection of all vehicles.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation and contains a collection of vehicles.</returns>
        Task<ICollection<IVehicle>> GetAllAsync();
        Task<bool> InsertVehicleAsync(IVehicle vehicle);
        Task<bool> UpdateVehicleLocationAsync(int vehicleId, CurrentLocation newLocation);  
        Task<ILocation> GetVehicleLocationAsync(int vehicleId);
    }
}
