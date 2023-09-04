using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    /// <summary>
    /// Represents a location with latitude and longitude coordinates at a specific date and time.
    /// </summary>
    public abstract class Location
    {
        /// <summary>
        /// Gets or sets the unique identifier of the location.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the location was recorded.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the location.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the location.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets vehicle id.
        /// </summary>
        public int? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets vehicle.
        /// </summary>
        public virtual Vehicle? Vehicle { get; set; }

        /// <summary>
        /// Configures the entity mapping for the 'Location' model, specifying the table name as 'Locations'.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance used to configure the entity mapping.</param>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().ToTable("CurrentLocations");
            modelBuilder.Entity<Location>().HasKey(e => e.Id);

            modelBuilder.Entity<Location>()
                .Property(l => l.Id)
                .HasColumnName("LocationId")
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Location>()
                .Property(l => l.Date)
                .HasColumnType("datetime")
                .HasColumnName(nameof(Date))
                .HasColumnOrder(10)
                .IsRequired();

            modelBuilder.Entity<Location>()
                .Property(l => l.Latitude)
                .HasColumnType("decimal(10, 7)")
                .HasPrecision(10, 7)
                .HasColumnName(nameof(Latitude))
                .HasColumnOrder(20)
                .IsRequired();

            modelBuilder.Entity<Location>()
                .Property(l => l.Longitude)
                .HasColumnType("decimal(10, 7)")
                .HasPrecision(10, 7)
                .HasColumnName(nameof(Longitude))
                .HasColumnOrder(30)
                .IsRequired();

            modelBuilder.Entity<Location>()
                .Property(l => l.VehicleId)
                .HasColumnType("int")
                .HasColumnName(nameof(VehicleId))
                .HasColumnOrder(40);
        }
    }
}