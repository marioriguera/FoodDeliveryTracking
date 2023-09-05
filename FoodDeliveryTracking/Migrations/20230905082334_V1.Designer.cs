﻿// <auto-generated />
using System;
using FoodDeliveryTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodDeliveryTracking.Migrations
{
    [DbContext(typeof(ApplicationDC))]
    [Migration("20230905082334_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.CurrentLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CurrentLocationId")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClVehicleId")
                        .HasColumnType("int")
                        .HasColumnName("ClVehicleId")
                        .HasColumnOrder(40);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("Date")
                        .HasColumnOrder(10);

                    b.Property<decimal>("Latitude")
                        .HasPrecision(10, 7)
                        .HasColumnType("decimal(10,7)")
                        .HasColumnName("Latitude")
                        .HasColumnOrder(20);

                    b.Property<decimal>("Longitude")
                        .HasPrecision(10, 7)
                        .HasColumnType("decimal(10,7)")
                        .HasColumnName("Longitude")
                        .HasColumnOrder(30);

                    b.HasKey("Id");

                    b.HasIndex("ClVehicleId")
                        .IsUnique()
                        .HasFilter("[ClVehicleId] IS NOT NULL");

                    b.ToTable("CurrentLocations", (string)null);
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.LocationHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LocationHistoryId")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("Date")
                        .HasColumnOrder(10);

                    b.Property<decimal>("Latitude")
                        .HasPrecision(10, 7)
                        .HasColumnType("decimal(10,7)")
                        .HasColumnName("Latitude")
                        .HasColumnOrder(20);

                    b.Property<int?>("LhVehicleId")
                        .HasColumnType("int")
                        .HasColumnName("LhVehicleId")
                        .HasColumnOrder(40);

                    b.Property<decimal>("Longitude")
                        .HasPrecision(10, 7)
                        .HasColumnType("decimal(10,7)")
                        .HasColumnName("Longitude")
                        .HasColumnOrder(30);

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LhVehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("LocationsHistory", (string)null);
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderId")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AssignedVehicleId")
                        .HasColumnType("int")
                        .HasColumnName("AssignedVehicleId")
                        .HasColumnOrder(20);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Description")
                        .HasColumnOrder(10);

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status")
                        .HasColumnOrder(30);

                    b.HasKey("Id");

                    b.HasIndex("AssignedVehicleId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("VehicleId")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CurrentLocationId")
                        .HasColumnType("int")
                        .HasColumnName("CurrentLocationId")
                        .HasColumnOrder(20);

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Plate")
                        .HasColumnOrder(10);

                    b.HasKey("Id");

                    b.HasIndex("CurrentLocationId")
                        .IsUnique();

                    b.ToTable("Vehicles", (string)null);
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.CurrentLocation", b =>
                {
                    b.HasOne("FoodDeliveryTracking.Data.Models.Vehicle", null)
                        .WithOne("CurrentLocation")
                        .HasForeignKey("FoodDeliveryTracking.Data.Models.CurrentLocation", "ClVehicleId");
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.LocationHistory", b =>
                {
                    b.HasOne("FoodDeliveryTracking.Data.Models.Vehicle", null)
                        .WithMany("LocationHistory")
                        .HasForeignKey("LhVehicleId");

                    b.HasOne("FoodDeliveryTracking.Data.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.Order", b =>
                {
                    b.HasOne("FoodDeliveryTracking.Data.Models.Vehicle", "AssignedVehicle")
                        .WithMany("Orders")
                        .HasForeignKey("AssignedVehicleId");

                    b.Navigation("AssignedVehicle");
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.Vehicle", b =>
                {
                    b.HasOne("FoodDeliveryTracking.Data.Models.CurrentLocation", null)
                        .WithOne("Vehicle")
                        .HasForeignKey("FoodDeliveryTracking.Data.Models.Vehicle", "CurrentLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.CurrentLocation", b =>
                {
                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("FoodDeliveryTracking.Data.Models.Vehicle", b =>
                {
                    b.Navigation("CurrentLocation");

                    b.Navigation("LocationHistory");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
