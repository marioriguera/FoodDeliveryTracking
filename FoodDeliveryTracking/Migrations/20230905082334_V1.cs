using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryTracking.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentLocations",
                columns: table => new
                {
                    CurrentLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: false),
                    ClVehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLocations", x => x.CurrentLocationId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plate = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CurrentLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_CurrentLocations_CurrentLocationId",
                        column: x => x.CurrentLocationId,
                        principalTable: "CurrentLocations",
                        principalColumn: "CurrentLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationsHistory",
                columns: table => new
                {
                    LocationHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: false),
                    LhVehicleId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsHistory", x => x.LocationHistoryId);
                    table.ForeignKey(
                        name: "FK_LocationsHistory_Vehicles_LhVehicleId",
                        column: x => x.LhVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                    table.ForeignKey(
                        name: "FK_LocationsHistory_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    AssignedVehicleId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Vehicles_AssignedVehicleId",
                        column: x => x.AssignedVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentLocations_ClVehicleId",
                table: "CurrentLocations",
                column: "ClVehicleId",
                unique: true,
                filter: "[ClVehicleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsHistory_LhVehicleId",
                table: "LocationsHistory",
                column: "LhVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsHistory_VehicleId",
                table: "LocationsHistory",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssignedVehicleId",
                table: "Orders",
                column: "AssignedVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CurrentLocationId",
                table: "Vehicles",
                column: "CurrentLocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentLocations_Vehicles_ClVehicleId",
                table: "CurrentLocations",
                column: "ClVehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentLocations_Vehicles_ClVehicleId",
                table: "CurrentLocations");

            migrationBuilder.DropTable(
                name: "LocationsHistory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "CurrentLocations");
        }
    }
}
