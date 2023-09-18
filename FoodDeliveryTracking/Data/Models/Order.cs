using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    /// <summary>
    /// Represents an order with an identifier, description, assigned vehicle, and status.
    /// </summary>
    public class Order : IOrder
    {
        /// <summary>
        /// Initialize a new empty instance of <see cref="Order"/> class.
        /// </summary>
        public Order() { }

        /// <summary>
        /// Initialize a new instance of <see cref="Order"/> class.
        /// </summary>
        /// <param name="order">Order contract.</param>
        public Order(IOrder order)
        {
            Description = order.Description;
            AssignedVehicleId = order.AssignedVehicleId;
            if (order.AssignedVehicle != null) AssignedVehicleObject = new Vehicle(order.AssignedVehicle);
            Status = order.Status;
        }

        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public int? AssignedVehicleId { get; set; }

        /// <inheritdoc />
        public virtual IVehicle? AssignedVehicle => AssignedVehicleObject;
        public virtual Vehicle? AssignedVehicleObject { get; set; }

        /// <inheritdoc />
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Configures the entity mapping for the 'Order' model, specifying the table name as 'Orders'.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance used to configure the entity mapping.</param>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .HasColumnName("OrderId")
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(l => l.Description)
                .HasColumnType("nvarchar(255)")
                .HasColumnName(nameof(Description))
                .HasColumnOrder(10)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(l => l.AssignedVehicleId)
                .HasColumnType("int")
                .HasColumnName(nameof(AssignedVehicleId))
                .HasColumnOrder(20);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.AssignedVehicleObject)
                .WithMany(v => v.OrdersCollection)
                .HasForeignKey(o => o.AssignedVehicleId);

            modelBuilder.Entity<Order>()
                .Property(l => l.Status)
                .HasColumnType("int")
                .HasColumnName(nameof(Status))
                .HasColumnOrder(30)
                .IsRequired();
        }
    }

}
