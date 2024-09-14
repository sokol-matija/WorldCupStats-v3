using System;
using System.Collections.Generic;
using DataLayer.Managers;
using DataLayer.Models;
using Serilog;

namespace WPF_WorldCupStats.Testing
{
	public static class DataTester
	{
		public static void RunAllTests()
		{
			TestGettingSettings();
			TestSettingSettings();
			TestNewDefaultSettings();
		}

		public static void TestGettingSettings()
		{
			Log.Information("========== Test Getting Settings ==========");
			var settingsManager = SettingsManager.Instance;

			WindowSize windowSize = settingsManager.GetSetting(s => s.WindowSize);
			string dataSource = settingsManager.GetSetting(s => s.DataSource);
			string championship = settingsManager.GetSetting(s => s.Championship);
			string language = settingsManager.GetSetting(s => s.Language);
			string favoriteTeamMen = settingsManager.GetSetting(s => s.FavoriteTeamMen);
			string favoriteTeamWomen = settingsManager.GetSetting(s => s.FavoriteTeamWomen);
			FavoritePlayers favoritePlayers = settingsManager.GetSetting(s => s.favoritePlayers);

			Log.Information("Window Size: {WindowSize}", windowSize);
			Log.Information("Data Source: {DataSource}", dataSource);
			Log.Information("Championship: {Championship}", championship);
			Log.Information("Language: {Language}", language);
			Log.Information("Favorite team men: {FavoriteTeamMen}", favoriteTeamMen);
			Log.Information("Favorite team women: {FavoriteTeamWomen}", favoriteTeamWomen);
			Log.Information("Favorite players:");

			LogFavoritePlayers(favoritePlayers);

			Log.Information("========== Test Getting Settings End ==========");
		}

		public static void TestSettingSettings()
		{
			Log.Information("========== Test Setting Settings ==========");
			var settingsManager = SettingsManager.Instance;

			settingsManager.SetSetting(s => s.WindowSize, WindowSize.Small);
			settingsManager.SetSetting(s => s.DataSource, "db");
			settingsManager.SetSetting(s => s.Championship, "women");
			settingsManager.SetSetting(s => s.Language, "hr");
			settingsManager.SetSetting(s => s.FavoriteTeamMen, "FRA");
			settingsManager.SetSetting(s => s.FavoriteTeamWomen, "SRB");
			settingsManager.SetSetting(s => s.favoritePlayers,
				new FavoritePlayers
				{
					Men = new Dictionary<string, List<string>>
					{
						{"CRO", new List<string>{"MyPlayer SUBASIC", "MyPlayer MODRIC"}}
					},
					Women = new Dictionary<string, List<string>>
					{
						{"USA", new List<string>{"MyPlayer MORGAN", "MyPlayer RAPINOE"}}
					}
				});
			Log.Information("Settings have been updated. Run TestGettingSettings to verify changes.");
			Log.Information("========== Test Setting Settings End ==========");
		}

		public static void TestNewDefaultSettings()
		{
			Log.Information("========== Test New Default Settings ==========");
			Settings settings = new Settings();

			Log.Information("Window Size: {WindowSize}", settings.WindowSize);
			Log.Information("Data Source: {DataSource}", settings.DataSource);
			Log.Information("Championship: {Championship}", settings.Championship);
			Log.Information("Language: {Language}", settings.Language);
			Log.Information("Favorite team men: {FavoriteTeamMen}", settings.FavoriteTeamMen);
			Log.Information("Favorite team women: {FavoriteTeamWomen}", settings.FavoriteTeamWomen);
			Log.Information("Favorite players:");

			LogFavoritePlayers(settings.favoritePlayers);

			Log.Information("========== Test New Default Settings End ==========");
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