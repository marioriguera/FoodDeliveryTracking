﻿using System;
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
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLocations", x => x.CurrentLocationId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User);
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
                    VehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsHistory", x => x.LocationHistoryId);
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

            migrationBuilder.InsertData(
                table: "CurrentLocations",
                columns: new[] { "CurrentLocationId", "Date", "Latitude", "Longitude" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3698), 40.4189m, -3.6919m },
                    { 2, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3740), 40.4193m, -3.6905m },
                    { 3, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3744), 40.4176m, -3.6890m },
                    { 4, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3748), 40.4172m, -3.6883m },
                    { 5, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3751), 40.4163m, -3.6871m },
                    { 6, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3755), 40.4158m, -3.6862m },
                    { 7, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3759), 40.4151m, -3.6854m },
                    { 8, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3763), 40.4146m, -3.6847m },
                    { 9, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3766), 40.4139m, -3.6838m },
                    { 10, new DateTime(2023, 9, 18, 15, 57, 1, 395, DateTimeKind.Local).AddTicks(3770), 40.4133m, -3.6827m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User", "Name", "Password", "Role", "Token" },
                values: new object[,]
                {
                    { 1, "Administrator", "EzxxJ+FkzeLEGUYgqRhKqkPD0Ua2/Cbn06ZhG7Sl+Jc=", "Admin", null },
                    { 2, "John Doe", "Uv2qc2qg1Zbeklz9BqhGnw==", "User", null },
                    { 3, "Alice Smith", "mUmlmeymvz42Pm2RpcoFWA==", "Moderator", null },
                    { 4, "Bob Johnson", "QqNUyy2dYs55zai7iK2XvQ==", "Editor", null },
                    { 5, "Guest User", "4BZxSez+PerVXkyO7MkvnQ==", "Guest", null },
                    { 6, "Mary Brown", "SQ6gGPt0d3lihQey/WNCvw==", "User", null },
                    { 7, "David Lee", "ydy8uaKXXRbcWcgxMsCGdA==", "User", null },
                    { 8, "Sarah Wilson", "TTJ/uLY6ZtaT7uKVDSNAvA==", "User", null },
                    { 9, "Michael Clark", "WmBsPlK4crj9DdHQ/g+LYg==", "User", null },
                    { 10, "Emily Davis", "sy23QAi50qJ18O8KS3qLVQ==", "User", null }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "CurrentLocationId", "Plate" },
                values: new object[,]
                {
                    { 1, 1, "AB123CD" },
                    { 2, 2, "XY456ZW" },
                    { 3, 3, "FG789HI" },
                    { 4, 4, "JK012LM" },
                    { 5, 5, "NO345PQ" },
                    { 6, 6, "RS678TU" },
                    { 7, 7, "VW901YZ" },
                    { 8, 8, "BC234EF" },
                    { 9, 9, "GH567IJ" },
                    { 10, 10, "KL890MN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationsHistory_VehicleId",
                table: "LocationsHistory",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssignedVehicleId",
                table: "Orders",
                column: "AssignedVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CurrentLocationId",
                table: "Vehicles",
                column: "CurrentLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationsHistory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "CurrentLocations");
        }
    }
}
