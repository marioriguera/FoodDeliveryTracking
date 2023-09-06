using FoodDeliveryTracking.Data.Context;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts.Implementations
{
    /// <summary>
    /// Implementation of the ILocalitationHistoryRepository interface for interacting with location history data in the database.
    /// </summary>
    public class LocalitationHistoryRepository : ILocalitationHistoryRepository
    {
        private ILoggerManager _loggerManager;
        private ApplicationDC _applicationDC;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalitationHistoryRepository"/> class with the provided logger manager and database context.
        /// </summary>
        /// <param name="loggerManager">The logger manager used for logging.</param>
        /// <param name="applicationDC">The database context for accessing current location data.</param>
        public LocalitationHistoryRepository(ILoggerManager loggerManager, ApplicationDC applicationDC)
        {
            _loggerManager = loggerManager;
            _applicationDC = applicationDC;
        }
    }
}
