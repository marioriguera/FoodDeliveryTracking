using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    /// <summary>
    /// Represents a historical record of a location with latitude and longitude coordinates at a specific date and time.
    /// This class inherits from the base class 'Location'.
    /// </summary>
    public class LocationHistory : ILocation
    {
        /// <summary>
        /// Initialize a new empty instance of <see cref="LocationHistory"/> class.
        /// </summary>
        public LocationHistory() { }

        /// <summary>
        /// Initialize a new instance of <see cref="LocationHistory"/> class.
        /// </summary>
        /// <param name="location">Location contract.</param>
        public LocationHistory(ILocation location)
        {
            Date = DateTime.Now;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }

        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public DateTime Date { get; set; }

        /// <inheritdoc />
        public decimal Latitude { get; set; }

        /// <inheritdoc />
        public decimal Longitude { get; set; }

        /// <summary>
        /// Configures the entity mapping for the 'Order' model, specifying the table name as 'Orders'.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance used to configure the entity mapping.</param>
        public static void Configure_LocationHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationHistory>().ToTable("LocationsHistory");
            modelBuilder.Entity<LocationHistory>().HasKey(lh => lh.Id);

            modelBuilder.Entity<LocationHistory>()
                .Property(l => l.Id)
                .HasColumnName("LocationHistoryId")
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LocationHistory>()
                .Property(l => l.Date)
                .HasColumnType("datetime")
                .HasColumnName(nameof(Date))
                .HasColumnOrder(10)
                .IsRequired();

            modelBuilder.Entity<LocationHistory>()
                .Property(l => l.Latitude)
                .HasColumnType("decimal(10, 7)")
                .HasPrecision(10, 7)
                .HasColumnName(nameof(Latitude))
                .HasColumnOrder(20)
                .IsRequired();

            modelBuilder.Entity<LocationHistory>()
                .Property(l => l.Longitude)
                .HasColumnType("decimal(10, 7)")
                .HasPrecision(10, 7)
                .HasColumnName(nameof(Longitude))
                .HasColumnOrder(30)
                .IsRequired();
        }
    }
}