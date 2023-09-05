using System.Net.Mime;

namespace FoodDeliveryTracking.Config
{
    /// <summary>
    /// Manages api configuration values.
    /// </summary>
    public sealed class ApiConfiguration
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="ApiConfiguration"/> class.
        /// </summary>
        /// <remarks>
        /// Explicit static constructor to tell C# compiler not to mark type as before field initialization.
        /// </remarks>
        static ApiConfiguration()
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ApiConfiguration"/> class from being created.
        /// </summary>
        private ApiConfiguration()
        {
        }
        #endregion

        #region Core Properties
        /// <summary>
        /// Gets current api configuration.
        /// </summary>
        public static ApiConfiguration Current { get; } = new ApiConfiguration();
        #endregion

        #region LogProperties
        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        public NLog.LogLevel LogLevel { get; set; } = NLog.LogLevel.Trace;

        /// <summary>
        /// Gets or sets the log path to store log as file.
        /// </summary>
        public string LogPath { get; set; } = @"C:\Logs\FoodDeliveryTracking.log";

        /// <summary>
        /// Gets or sets the log max file size before rolling.
        /// </summary>
        public long LogMaxFileSize { get; set; } = 104857600;
        #endregion
    }
}
