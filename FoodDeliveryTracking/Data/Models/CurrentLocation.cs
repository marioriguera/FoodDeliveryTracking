using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    /// <summary>
    /// Represents a current location with latitude and longitude coordinates at a specific date and time.
    /// This class inherits from the base class 'Location'.
    /// </summary>
    public class CurrentLocation : Location
    {
        /// <summary>
        /// Configures the entity mapping for the 'CurrentLocation' model, specifying the table name as 'CurrentLocations'.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance used to configure the entity mapping.</param>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentLocation>().ToTable("CurrentLocations");
            modelBuilder.Entity<CurrentLocation>().HasKey(e => e.Id);

            modelBuilder.Entity<CurrentLocation>()
                .Property(l => l.Id)
                .HasColumnName("CurrentLocationId")
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CurrentLocation>()
                .Property(l => l.Date)
                .HasColumnType("datetime")
                .HasColumnName(nameof(Date))
                .HasColumnOrder(10)
                .IsRequired();

            modelBuilder.Entity<CurrentLocation>()
                .Property(l => l.Latitude)
                .HasColumnType("decimal(10, 7)")
                .HasPrecision(10, 7)
                .HasColumnName(nameof(Latitude))
                .HasColumnOrder(20)
                .IsRequired();

            modelBuilder.Entity<CurrentLocation>()
                .Property(l => l.Longitude)
                .HasColumnType("decimal(10, 7)")
                .HasPrecision(10, 7)
                .HasColumnName(nameof(Longitude))
                .HasColumnOrder(30)
                .IsRequired();
        }
    }
}
