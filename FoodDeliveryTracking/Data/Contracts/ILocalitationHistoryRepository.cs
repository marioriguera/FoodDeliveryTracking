using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts
{
    /// <summary>
    /// Defines an interface for a repository that manages the historical location data of vehicles.
    /// </summary>
    public interface ILocalitationHistoryRepository
    {
        /// <summary>
        /// Adds historical location data for the specified vehicle and location asynchronously.
        /// </summary>
        /// <param name="location">The location data to add to the history.</param>
        /// <param name="idVehicle">The ID of the vehicle for which to add historical location data.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        // Task AddHistoryLocalitationAsync(ILocation location, int idVehicle);
    }

}
