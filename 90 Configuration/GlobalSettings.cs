using System;

namespace MyInventory
{
    public static class GlobalSettings
    {
        public static ApplicationSettings Application { get; set; } = new ApplicationSettings();

        public static string GetDashboardUrl() => $"http://localhost:{Application.Port}/{Application.DashboardFile}";

        public static string GetLogDirectory() => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.Name, "Logs");

        public static string GetLogFile() => System.IO.Path.Combine(GetLogDirectory(), "Log.txt");
    }

    public class ApplicationSettings
    {
        public string Name { get; set; } = "My Inventory";
        public string DashboardFile { get; set; } = "Dashboard.html";
        public int DefaultPort { get; set; } = 5000;
        public int Port { get; set; }
    }
}
