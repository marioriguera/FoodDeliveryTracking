using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    /// <summary>
    /// Represents a vehicle entity.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the unique identifier for the vehicle.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets a collection of orders associated with the vehicle.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        /// <summary>
        /// Gets or sets a collection of location history records for the vehicle.
        /// </summary>
        public virtual ICollection<LocationHistory> LocationHistory { get; set; } = new HashSet<LocationHistory>();

        /// <summary>
        /// Gets or sets the unique identifier of the vehicle's current location.
        /// </summary>
        public int CurrentLocationId { get; set; }

        /// <summary>
        /// Gets or sets the current location of the vehicle.
        /// </summary>
        public virtual CurrentLocation CurrentLocation { get; set; }

        /// <summary>
        /// Configures the entity mapping for the 'Vehicle' model, specifying the table name as 'Vehicles'.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance used to configure the entity mapping.</param>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
            modelBuilder.Entity<Vehicle>().HasKey(e => e.Id);

            modelBuilder.Entity<Vehicle>()
                .Property(o => o.Id)
                .HasColumnName("VehicleId")
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.Plate)
                .HasColumnType("nvarchar(10)")
                .HasColumnName(nameof(Plate))
                .HasColumnOrder(10)
                .IsRequired();

            modelBuilder.Entity<Vehicle>()
                .HasMany(o => o.Orders)
                .WithOne(v => v.AssignedVehicle)
                .HasForeignKey(v => v.AssignedVehicleId);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.CurrentLocationId)
                .HasColumnType("int")
                .HasColumnName(nameof(CurrentLocationId))
                .HasColumnOrder(20)
                .IsRequired();

            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.CurrentLocation)
                .WithOne()
                .HasForeignKey<CurrentLocation>(v => v.ClVehicleId);

            modelBuilder.Entity<Vehicle>()
                .HasMany(o => o.LocationHistory)
                .WithOne()
                .HasForeignKey(v => v.LhVehicleId);
        }
    }
}