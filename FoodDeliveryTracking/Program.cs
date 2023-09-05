using FoodDeliveryTracking.Config;
using FoodDeliveryTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;

namespace FoodDeliveryTracking
{
    /// <summary>
    /// Application main class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">Main function arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            NlogConfigurator.Initialize();
            NlogConfigurator.AddConsole();
            NlogConfigurator.AddDebugger();
            NlogConfigurator.Start();

            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                //Apply configs
                NlogConfigurator.ApplyConfigurationToLogs();

                // Data context configuration.
                builder.Services.AddTransient<ApplicationDC>();
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<ApplicationDC>(options =>
                {
                    options.UseSqlServer(connectionString);
                }, ServiceLifetime.Transient);

                // Dependencies injection
                Services.Register.ServicesDI.AddDependencies(builder.Services);
                Data.Register.DataDI.AddDependencies(builder.Services);

                // Add services to the container.
                builder.Services.AddControllers();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(
                    c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "Food Delivery Tracking HENRY",
                            Version = "v1"
                        });

                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Ingrese el token JWT en este formato: 'Bearer {token}'",
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey
                        });

                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                        });
                    }
                );

                var app = builder.Build();
                logger.Fatal($"The configuration has been loaded.");

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                logger.Fatal($"The API will start.");
                app.Run();
            }
            catch (Exception ex)
            {
                // NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}