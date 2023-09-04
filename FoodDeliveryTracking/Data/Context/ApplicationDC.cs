using FoodDeliveryTracking.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Context
{
    /// <summary>
    /// Class to manage data context.
    /// </summary>
    public class ApplicationDC : DbContext
    {
        /// <summary>
        /// Inicitalize a new instance of <see cref="ApplicationDC"/> class.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDC(DbContextOptions<ApplicationDC> options) : base(options)
        {
        }

        /// <summary>
        /// Called when the database model is created.
        /// Allows you to configure the relationships and constraints of the database.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Location.Configure(modelBuilder);
            LocationHistory.Configure_LocationHistory(modelBuilder);
            Order.Configure(modelBuilder);
        }
    }
}
