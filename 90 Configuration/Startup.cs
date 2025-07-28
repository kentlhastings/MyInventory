using MyInventory.Logic;
using System;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace MyInventory
{
    public static class Startup
    {
        public static async Task<WebApplication> StartWebServer()
        {
            WebApplication? app = null;

            try
            {
                Logging.InitializeLogger();
                Logging.LogStartup();

                AssignPort();

                var builder = WebApplication.CreateBuilder();
                builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(GlobalSettings.Application.Port));
                builder.ConfigureServices();

                Logging.LogEnvironment(builder);

                app = builder.Build();
                app.ConfigurePipeline();
                app.Lifetime.ApplicationStopping.Register(Logging.LogShutdown);
                app.Start();

                return app;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to configure application.");
                if (app is not null) await Shutdown(app, ex);
                throw;
            }
        }

        private static void AssignPort()
        {
            var port = 0;

            try
            {
                port = FindAvailablePort(GlobalSettings.Application.DefaultPort);
            }
            catch
            {
                port = FindAvailablePort(0);
            }

            GlobalSettings.Application.Port = port;
        }

        private static int FindAvailablePort(int startingPort)
        {
            try
            {
                using var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, startingPort);
                listener.Start();
                var openPort = ((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
                Log.Information($"Found open port: {openPort}");
                return openPort;
            }
            catch (Exception)
            {
                Log.Information($"Port {startingPort} was busy.");
                throw;
            }
        }

        private static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            builder.Services.AddScoped<InventoryLogic>();
            builder.Services.AddScoped<ApplicationLogic>();
            builder.Services.AddScoped<ImageLogic>();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            if (builder.Environment.IsDevelopment()) builder.Services.AddSwaggerGen();
        }

        private static void ConfigurePipeline(this WebApplication app)
        {
            app.UseStaticFiles();
            app.UseCors();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
        }

        public static async Task Shutdown(WebApplication app, Exception? ex = null)
        {
            if (ex is not null) Logging.LogStartupFailure(ex);

            try
            {
                await app.StopAsync();
            }
            catch (Exception stopEx)
            {
                Log.Error(stopEx, "Error occured while stopping the web server");
            }

            try
            {
                await app.DisposeAsync();
            }
            catch (Exception disposeEx)
            {
                Log.Error(disposeEx, "Error while disposing the web application");
            }
        }
    }
}
