using System.Windows;
using DataLayer.Managers;
using Serilog;
using WPF_WorldCupStats.Configuration;
using WPF_WorldCupStats.Testing;

namespace WPF_WorldCupStats
{
	public partial class MainWindow : Window
	{
		public readonly SettingsManager _settingsManager = SettingsManager.Instance;
		public MainWindow()
		{
			InitializeComponent();
			InitializeAsync();
			RunTests();
		}

		private async void InitializeAsync()
		{
			await SerilogConfig.ConfigureAsync();
		}

		private void RunTests()
		{
			Log.Information("Starting data tests...");
			DataTester.RunAllTests();
			Log.Information("Data tests completed.");
		}
	}
}