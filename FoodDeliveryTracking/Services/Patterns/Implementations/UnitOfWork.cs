using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Data.Contracts;
using FoodDeliveryTracking.Services.Logger;

namespace FoodDeliveryTracking.Services.Patterns.Implementations
{
    /// <summary>
    /// Represents an implementation of the unit of work pattern.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ILoggerManager _loggerManager;
        private ApplicationDC _applicationDC;
        private IUsersRepository _userRepository;
        private IOrdersRepository _ordersRepository;
        private IVehiclesRepository _vehiclesRepository;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with dependencies.
        /// </summary>
        /// <param name="loggerManager">The logger manager.</param>
        /// <param name="applicationDC">The application data context.</param>
        /// <param name="userRepository">The repository for managing users.</param>
        /// <param name="ordersRepository">The repository for managing orders.</param>
        /// <param name="vehiclesRepository">The repository for managing vehicles.</param>
        public UnitOfWork(ILoggerManager loggerManager,
                            ApplicationDC applicationDC,
                            IUsersRepository userRepository,
                            IOrdersRepository ordersRepository,
                            IVehiclesRepository vehiclesRepository)
        {
            _loggerManager = loggerManager;
            _applicationDC = applicationDC;
            _userRepository = userRepository;
            _ordersRepository = ordersRepository;
            _vehiclesRepository = vehiclesRepository;
        }

        /// <inheritdoc/>
        public IUsersRepository UsersRepository => _userRepository;

        /// <inheritdoc/>
        public IOrdersRepository OrdersRepository => _ordersRepository;

        /// <inheritdoc/>
        public IVehiclesRepository VehiclesRepository => _vehiclesRepository;

        /// <inheritdoc/>
        public async Task<bool> SaveAsync()
        {
            _loggerManager.LogTrace($"Starting save changes of data context in {nameof(UnitOfWork)} .");
            return await _applicationDC.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _applicationDC.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _loggerManager.LogTrace($"Starting dispose in {nameof(UnitOfWork)} .");
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
