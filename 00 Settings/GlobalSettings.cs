using System;

namespace MyInventory
{
    public static class GlobalSettings
    {
        public static ApplicationSettings Application { get; set; } = new ApplicationSettings();

        public static string GetDashboardUrl() => $"http://localhost:{Application.Port}/{Application.DashboardFile}";

        public static string GetLogDirectory()
        {
            var logDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.Name, Application.LogDirectoryName);
            if (!System.IO.Directory.Exists(logDirectory)) System.IO.Directory.CreateDirectory(logDirectory);
            return logDirectory;
        }

        public static string GetDataFile()
        {
            var dataDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.Name);
            if (!System.IO.Directory.Exists(dataDirectory)) System.IO.Directory.CreateDirectory(dataDirectory);
            return System.IO.Path.Combine(dataDirectory, "Data.json");
        }

        public static string GetBackupDataFile()
        {
            var backupFileName = $"Data_{Guid.NewGuid()}.backup";
            var backupDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.Name, Application.DataBackupDirectoryName);
            if (!System.IO.Directory.Exists(backupDirectory)) System.IO.Directory.CreateDirectory(backupDirectory);
            return System.IO.Path.Combine(backupDirectory, backupFileName);
        }

        public static string GetLogFile() => System.IO.Path.Combine(GetLogDirectory(), "Log.txt");
    }

    public class ApplicationSettings
    {
        public string Name { get; set; } = "My Inventory";
        public string DashboardFile { get; set; } = "Dashboard.html";
        public int DefaultPort { get; set; } = 5000;
        public int Port { get; set; }
        public string LogDirectoryName = "Logs";
        public string DataBackupDirectoryName = "Backup";
        public int Version = 1;
    }
}
