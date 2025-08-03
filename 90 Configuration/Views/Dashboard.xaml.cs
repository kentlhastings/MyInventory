using Microsoft.AspNetCore.Builder;
using Microsoft.Web.WebView2.Core;
using Serilog;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyInventory.Views
{
    public partial class Dashboard : Window
    {
        private WebApplication? _webServer;

        public Dashboard()
        {
            StartWebServerAsync();
            InitializeComponent();
            Loaded += Dashboard_Loaded;
        }

        private async void StartWebServerAsync()
        {
            try
            {
                _webServer = await Startup.StartWebServer();
                if (_webServer is null) throw new InvalidOperationException("Application configuration failed.");
            }
            catch
            {
                Application.Current.Shutdown();
            }
        }

        private async void Dashboard_Loaded(object sender, RoutedEventArgs e) => await LaunchDashboard();

        private async Task LaunchDashboard()
        {
            var version = CoreWebView2Environment.GetAvailableBrowserVersionString();
            if (string.IsNullOrWhiteSpace(version))
            {
                MessageBox.Show("The Microsoft Edge WebView2 Runtime is required. Installing now...");
                var installProcess = Process.Start("WebView2Setup.exe", "/silent /install");
                if (installProcess != null) await installProcess.WaitForExitAsync();

                MessageBox.Show("WebView2 Runtime has been installed. Please restart the application.");

                Shutdown();
                return;
            }

            await DashboardWebView.EnsureCoreWebView2Async();

            //Set the port in the front end
            var localHostScript = $"window.localHost = '{GlobalSettings.Application.Port}';";
            await DashboardWebView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(localHostScript);

            DashboardWebView.CoreWebView2.Navigate(GlobalSettings.GetDashboardUrl());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var dialog = new Confirmation("Confirm Exit", "Are you sure you want to shutdown?")
            {
                Owner = this
            };
            dialog.ShowDialog();

            if (!dialog.Result)
            {
                e.Cancel = true;
                return;
            }

            Log.Information("Shutdown requested.");
        }

        protected override async void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (_webServer is not null) await Startup.Shutdown(_webServer);

            Shutdown();
        }

        private void Shutdown()
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
    }
}
