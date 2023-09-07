using FoodDeliveryTracking.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTracking.Data.Models
{
    public class User : IUser
    {
        /// <summary>
        /// Initialize a new empty instance of <see cref="User"/> class.
        /// </summary>
        public User() { }

        /// <summary>
        /// Initialize a new instance of <see cref="User"/> class.
        /// </summary>
        /// <param name="user">User contract.</param>
        public User(IUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
            Token = user.Token;
        }

        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string? Name { get; set; }

        /// <inheritdoc/>
        public string? Password { get; set; }

        /// <inheritdoc/>
        public string? Token { get; set; }

        /// <summary>
        /// Configures the entity mapping for the 'User' model, specifying the table name as 'Vehicles'.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance used to configure the entity mapping.</param>
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(e => e.Id);

            modelBuilder.Entity<User>()
                .Property(o => o.Id)
                .HasColumnName("User")
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(v => v.Name)
                .HasColumnType("nvarchar(50)")
                .HasColumnName(nameof(Name))
                .HasColumnOrder(10)
                .IsRequired();

            // ToDo: Encriptar password
            modelBuilder.Entity<User>()
                .Property(v => v.Password)
                .HasColumnType("nvarchar(100)")
                .HasColumnName(nameof(Password))
                .HasColumnOrder(20)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(v => v.Token)
                .HasColumnType("nvarchar(2000)")
                .HasColumnName(nameof(Token))
                .HasColumnOrder(30);
        }
    }
}
