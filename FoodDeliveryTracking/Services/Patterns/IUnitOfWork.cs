using FoodDeliveryTracking.Data.Contracts;

namespace FoodDeliveryTracking.Services.Patterns
{
    /// <summary>
    /// Represents a unit of work for managing repositories and database transactions.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository for managing users.
        /// </summary>
        IUsersRepository UsersRepository { get; }

        /// <summary>
        /// Gets the repository for managing orders.
        /// </summary>
        IOrdersRepository OrdersRepository { get; }

        /// <summary>
        /// Gets the repository for managing vehicles.
        /// </summary>
        IVehiclesRepository VehiclesRepository { get; }

        /// <summary>
        /// Saves changes made in the unit of work to the underlying database.
        /// </summary>
        Task<bool> SaveAsync();
    }
}
