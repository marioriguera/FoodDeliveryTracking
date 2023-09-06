namespace FoodDeliveryTracking.Data.Contracts
{
    /// <summary>
    /// Defines an interface for a repository that manages the current location data of vehicles.
    /// </summary>
    public interface ICurrentLocalitationRepository
    {
        /// <summary>
        /// Updates the current location of the specified vehicle asynchronously.
        /// </summary>
        /// <param name="currentLocalitationId">The ID of the current location data to update.</param>
        /// <param name="latitude">The new latitude coordinate of the vehicle's current location.</param>
        /// <param name="longitude">The new longitude coordinate of the vehicle's current location.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCurrentVehicleLocationAsync(int currentLocalitationId, decimal latitude, decimal longitude);
    }

}
