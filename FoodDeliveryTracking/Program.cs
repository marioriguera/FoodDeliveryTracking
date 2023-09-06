using FoodDeliveryTracking.Config;
using FoodDeliveryTracking.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System.Text;

namespace FoodDeliveryTracking
{
    /// <summary>
    /// Application main class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Gets or sets secret key.
        /// </summary>
        public static AppSettings? AppSettings { get; private set; } = new AppSettings();

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

                // JWT
                // Identity Framework will be used in future projects.
                var appSettingsSection = builder.Configuration.GetSection("AppSettings");
                builder.Services.Configure<AppSettings>(appSettingsSection);

                var appSettings = appSettingsSection.Get<AppSettings>();
                AppSettings.Secret = appSettings.Secret!;
                var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
                builder.Services.AddAuthentication(d =>
                {
                    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(d =>
                    {
                        d.RequireHttpsMetadata = false;
                        d.SaveToken = true;
                        d.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

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

                app.UseAuthentication();
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