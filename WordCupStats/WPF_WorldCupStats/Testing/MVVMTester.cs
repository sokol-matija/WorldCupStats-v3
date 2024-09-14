using System;
using Serilog;
using WPF_WorldCupStats.ViewModels;
using DataLayer.Models;

namespace WPF_WorldCupStats.Testing
{
	public static class MVVMTester
	{
		public static void RunAllTests()
		{
			TestPropertyChanges();
			TestCommandExecution();
			TestDataBinding();
		}

		public static void TestPropertyChanges()
		{
			Log.Information("========== Test MVVM Property Changes ==========");
			var viewModel = new MainViewModel();

			// Simulate changing properties
			viewModel.WindowSize = WindowSize.Small;
			viewModel.DataSource = "api";
			viewModel.Championship = "women";
			viewModel.Language = "fr";
			viewModel.FavoriteTeamMen = "ESP";
			viewModel.FavoriteTeamWomen = "GER";

			// Verify changes
			Log.Information("Window Size: {WindowSize}", viewModel.WindowSize);
			Log.Information("Data Source: {DataSource}", viewModel.DataSource);
			Log.Information("Championship: {Championship}", viewModel.Championship);
			Log.Information("Language: {Language}", viewModel.Language);
			Log.Information("Favorite team men: {FavoriteTeamMen}", viewModel.FavoriteTeamMen);
			Log.Information("Favorite team women: {FavoriteTeamWomen}", viewModel.FavoriteTeamWomen);

			Log.Information("========== Test MVVM Property Changes End ==========");
		}

		public static void TestCommandExecution()
		{
			Log.Information("========== Test MVVM Command Execution ==========");
			var viewModel = new MainViewModel();

			// Simulate adding favorite players
			viewModel.AddFavoritePlayerCommand.Execute(new Tuple<string, string, string>("men", "ESP", "Sergio RAMOS"));
			viewModel.AddFavoritePlayerCommand.Execute(new Tuple<string, string, string>("women", "GER", "Alexandra POPP"));

			// Simulate removing a favorite player
			viewModel.RemoveFavoritePlayerCommand.Execute(new Tuple<string, string, string>("men", "ESP", "Sergio RAMOS"));

			// Verify changes
			Log.Information("Favorite Players after commands:");
			LogFavoritePlayers(viewModel.FavoritePlayers);

			Log.Information("========== Test MVVM Command Execution End ==========");
		}

		public static void TestDataBinding()
		{
			Log.Information("========== Test MVVM Data Binding ==========");
			var viewModel = new MainViewModel();

			// Simulate View binding to ViewModel properties
			viewModel.PropertyChanged += (sender, e) =>
			{
				Log.Information("Property changed: {PropertyName}", e.PropertyName);
			};

			// Change properties to trigger PropertyChanged event
			viewModel.WindowSize = WindowSize.Medium;
			viewModel.DataSource = "db";
			viewModel.Championship = "men";

			// Simulate View invoking commands
			viewModel.AddFavoritePlayerCommand.Execute(new Tuple<string, string, string>("women", "USA", "Megan RAPINOE"));

			Log.Information("========== Test MVVM Data Binding End ==========");
		}

		private static void LogFavoritePlayers(FavoritePlayers favoritePlayers)
		{
			foreach (var team in favoritePlayers.Men)
			{
				Log.Information("Men's Team {Team}: {Players}", team.Key, string.Join(", ", team.Value));
			}

			foreach (var team in favoritePlayers.Women)
			{
				Log.Information("Women's Team {Team}: {Players}", team.Key, string.Join(", ", team.Value));
			}
		}
	}
}