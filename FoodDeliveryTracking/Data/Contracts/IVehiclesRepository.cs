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
        /// <summary>
        /// Inserts a new vehicle into the database.
        /// </summary>
        /// <param name="vehicle">The vehicle object to insert.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        Task<bool> InsertVehicleAsync(IVehicle vehicle);

        /// <summary>
        /// Updates the location of a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <param name="newLocation">The new location to set.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        Task<bool> UpdateVehicleLocationAsync(int vehicleId, ILocation newLocation); 

        /// <summary>
        /// Retrieves the location of a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <returns>The location of the vehicle if found; otherwise, null.</returns> 
        Task<ILocation> GetVehicleLocationAsync(int vehicleId);
    }
}
