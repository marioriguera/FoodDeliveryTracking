using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    /// <summary>
    /// Represents a vehicle entity.
    /// </summary>
    public class Vehicle : IVehicle
    {
        /// <summary>
        /// Initialize a new empty instace of <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle() { }

        /// <summary>
        /// Initialize a new instance of <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="vehicle">Vehicle contract.</param>
        public Vehicle(IVehicle vehicle)
        {
            Plate = vehicle.Plate;
            OrdersCollection.Clear();
            OrdersCollection.Concat(vehicle.Orders);
            LocationHistory.Clear();
            LocationHistory.Concat(vehicle.LocationHistory);
            CurrentLocationId = vehicle.CurrentLocationId;
            CurrentLocationObject = new CurrentLocation(vehicle.CurrentLocation);
        }

        /// <summary>
        /// Gets or sets the unique identifier for the vehicle.
        /// </summary>
        public int Id { get; set; }

        /// <inheritdoc />
        public string Plate { get; set; }

        /// <inheritdoc />
        public virtual ICollection<IOrder> Orders => OrdersCollection?.Cast<IOrder>()?.ToList();
        public virtual ICollection<Order> OrdersCollection { get; set; } = new HashSet<Order>();

        /// <inheritdoc />
        public virtual ICollection<ILocation> LocationHistory => LocationHistoryCollection?.Cast<ILocation>()?.ToList();
        public virtual ICollection<LocationHistory> LocationHistoryCollection { get; set; } = new HashSet<LocationHistory>();

        /// <inheritdoc />
        public int CurrentLocationId { get; set; }

        /// <inheritdoc />
        public virtual ILocation CurrentLocation => CurrentLocationObject;
        public virtual CurrentLocation CurrentLocationObject { get; set; }

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
                .HasMany(o => o.OrdersCollection)
                .WithOne(v => v.AssignedVehicleObject)
                .HasForeignKey(v => v.AssignedVehicleId);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.CurrentLocationId)
                .HasColumnType("int")
                .HasColumnName(nameof(CurrentLocationId))
                .HasColumnOrder(20)
                .IsRequired();

            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.CurrentLocationObject)
                .WithOne();

            modelBuilder.Entity<Vehicle>()
                .HasMany(lh => lh.LocationHistoryCollection)
                .WithOne();
        }
    }
}