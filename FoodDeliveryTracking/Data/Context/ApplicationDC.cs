using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Context
{
    /// <summary>
    /// Class to manage data context.
    /// </summary>
    public class ApplicationDC : DbContext
    {
        /// <summary>
        /// Inicitalize a new instance of <see cref="ApplicationDC"/> class.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDC(DbContextOptions<ApplicationDC> options) : base(options)
        {
        }

        /// <summary>
        /// Called when the database model is created.
        /// Allows you to configure the relationships and constraints of the database.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CurrentLocation.Configure(modelBuilder);
            LocationHistory.Configure(modelBuilder);
            Order.Configure(modelBuilder);
            Vehicle.Configure(modelBuilder);
            User.Configure(modelBuilder);

            SeedLocations(modelBuilder);
            SeedVehicles(modelBuilder);
            SeedUsers(modelBuilder);
        }

        /// <summary>
        /// Seeds the database with locations data.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance for configuring the database context.</param>
        private void SeedLocations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentLocation>().HasData
                (
                    new CurrentLocation() { Id = 1, Latitude = 40.4189m, Longitude = -3.6919m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 2, Latitude = 40.4193m, Longitude = -3.6905m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 3, Latitude = 40.4176m, Longitude = -3.6890m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 4, Latitude = 40.4172m, Longitude = -3.6883m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 5, Latitude = 40.4163m, Longitude = -3.6871m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 6, Latitude = 40.4158m, Longitude = -3.6862m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 7, Latitude = 40.4151m, Longitude = -3.6854m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 8, Latitude = 40.4146m, Longitude = -3.6847m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 9, Latitude = 40.4139m, Longitude = -3.6838m, Date = DateTime.Now },
                    new CurrentLocation() { Id = 10, Latitude = 40.4133m, Longitude = -3.6827m, Date = DateTime.Now }
                );
        }

        /// <summary>
        /// Seeds the database with vehicle data, including license plates and current locations.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance for configuring the database context.</param>
        private void SeedVehicles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData
                (
                    new Vehicle() { Id = 1, Plate = "AB123CD", CurrentLocationId = 1 },
                    new Vehicle() { Id = 2, Plate = "XY456ZW", CurrentLocationId = 2 },
                    new Vehicle() { Id = 3, Plate = "FG789HI", CurrentLocationId = 3 },
                    new Vehicle() { Id = 4, Plate = "JK012LM", CurrentLocationId = 4 },
                    new Vehicle() { Id = 5, Plate = "NO345PQ", CurrentLocationId = 5 },
                    new Vehicle() { Id = 6, Plate = "RS678TU", CurrentLocationId = 6 },
                    new Vehicle() { Id = 7, Plate = "VW901YZ", CurrentLocationId = 7 },
                    new Vehicle() { Id = 8, Plate = "BC234EF", CurrentLocationId = 8 },
                    new Vehicle() { Id = 9, Plate = "GH567IJ", CurrentLocationId = 9 },
                    new Vehicle() { Id = 10, Plate = "KL890MN", CurrentLocationId = 10 }
                );
        }

        /// <summary>
        /// Seeds the database with users.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance for configuring the database context.</param>
        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
            (
                /// pass: AdministratorPass
                new User() { Id = 1, Name = "Administrator", Role = UserRole.Admin, Password = "EzxxJ+FkzeLEGUYgqRhKqkPD0Ua2/Cbn06ZhG7Sl+Jc=" },
                /// pass: Password1Hash
                new User() { Id = 2, Name = "John Doe", Role = UserRole.User, Password = "Uv2qc2qg1Zbeklz9BqhGnw==" },
                /// pass: Password2Hash
                new User() { Id = 3, Name = "Alice Smith", Role = UserRole.Moderator, Password = "mUmlmeymvz42Pm2RpcoFWA==" },
                /// pass: Password3Hash
                new User() { Id = 4, Name = "Bob Johnson", Role = UserRole.Editor, Password = "QqNUyy2dYs55zai7iK2XvQ==" },
                /// pass: Password4Hash
                new User() { Id = 5, Name = "Guest User", Role = UserRole.Guest, Password = "4BZxSez+PerVXkyO7MkvnQ==" },
                /// pass: Password5Hash
                new User() { Id = 6, Name = "Mary Brown", Role = UserRole.User, Password = "SQ6gGPt0d3lihQey/WNCvw==" },
                /// pass: Password6Hash
                new User() { Id = 7, Name = "David Lee", Role = UserRole.User, Password = "ydy8uaKXXRbcWcgxMsCGdA==" },
                /// pass: Password7Hash
                new User() { Id = 8, Name = "Sarah Wilson", Role = UserRole.User, Password = "TTJ/uLY6ZtaT7uKVDSNAvA==" },
                /// pass: Password8Hash
                new User() { Id = 9, Name = "Michael Clark", Role = UserRole.User, Password = "WmBsPlK4crj9DdHQ/g+LYg==" },
                /// pass: Password9Hash
                new User() { Id = 10, Name = "Emily Davis", Role = UserRole.User, Password = "sy23QAi50qJ18O8KS3qLVQ==" }
            );
        }

        /// <summary>
        /// Gets or sets data base set currents locations.
        /// </summary>
        public DbSet<CurrentLocation> CurrentLocations { get; set; }

        /// <summary>
        /// Gets or sets data base set locations history.
        /// </summary>
        public DbSet<LocationHistory> LocationsHistory { get; set; }

        /// <summary>
        /// Gets or sets data base set orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets data base set vehicles.
        /// </summary>
        public DbSet<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets data base set users.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
