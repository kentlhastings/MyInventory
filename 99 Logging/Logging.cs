using System;
using Serilog;
using Microsoft.AspNetCore.Builder;

namespace MyInventory
{
    public static class Logging
    {
        public static void InitializeLogger()
        {
            System.IO.Directory.CreateDirectory(GlobalSettings.GetLogDirectory());

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(GlobalSettings.GetLogFile(), rollingInterval: RollingInterval.Infinite, shared: true, fileSizeLimitBytes: null, rollOnFileSizeLimit: false )
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public static void LogStartup()
        {
            LogBanner();
            Log.Information($"🟢 {GlobalSettings.Application.Name} starting up.");
        }

        public static void LogEnvironment(WebApplicationBuilder builder) => Log.Information($"🌐 Running in {builder.Environment.EnvironmentName} mode.");

        public static void LogShutdown()
        {
            Log.Information($"🔴 {GlobalSettings.Application.Name} shutting down.");
            LogBanner();
            Log.CloseAndFlush();
        }

        public static void LogStartupFailure(Exception ex)
        {
            Log.Fatal(ex, $"❌ {GlobalSettings.Application.Name} terminated unexpectedly.");
            LogBanner();
            Log.CloseAndFlush();
        }

        private static void LogBanner() => Log.Information(new string('-', 80));
    }
}
