﻿using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    /// <summary>
    /// Represents a historical record of a location with latitude and longitude coordinates at a specific date and time.
    /// This class inherits from the base class 'Location'.
    /// </summary>
    public class LocationHistory : Location
    {
        /// <summary>
        /// Gets or sets location history vehicle id.
        /// </summary>
        public int? LhVehicleId { get; set; }

        /// <summary>
        /// Gets or sets vehicle.
        /// </summary>
        public virtual Vehicle? Vehicle { get; set; }

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

            modelBuilder.Entity<LocationHistory>()
                .Property(l => l.LhVehicleId)
                .HasColumnType("int")
                .HasColumnName($"{nameof(LhVehicleId)}")
                .HasColumnOrder(40);

            modelBuilder.Entity<LocationHistory>()
                .HasOne(cl => cl.Vehicle)
                .WithOne()
                .HasForeignKey<LocationHistory>(cl => cl.LhVehicleId);
        }
    }
}