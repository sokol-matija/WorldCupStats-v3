using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DataLayer.Managers;
using DataLayer.Models;
using Serilog;
using WPF_WorldCupStats.Configuration;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WPF_WorldCupStats
{
    public partial class MainWindow : Window
    {
        private readonly SettingsManager _settingsManager = SettingsManager.Instance;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
			TestSettings();
		}

		private async void InitializeAsync()
        {
            await SerilogConfig.ConfigureAsync();
        }

        private void TestSettings()
        {
            Log.Information("========== Test Settings ==========");
            WindowSize windowSize = _settingsManager.GetSetting(s => s.WindowSize);
            string dataSource = _settingsManager.GetSetting(s => s.DataSource);
            string championship = _settingsManager.GetSetting(s => s.Championship);
            string language = _settingsManager.GetSetting(s => s.Language);
            string favoriteTeamMen = _settingsManager.GetSetting(s => s.FavoriteTeamMen);
            string favoriteTeamWomen = _settingsManager.GetSetting(s => s.FavoriteTeamWomen);
            FavoritePlayers favoritePlayers = _settingsManager.GetSetting(s => s.favoritePlayers);
           
            Log.Information($"Window Size: {windowSize}");
            Log.Information($"Data Source: {dataSource}");
            Log.Information($"Championship: {championship}");
            Log.Information($"Language: {language}");
            Log.Information($"Favorite team men {favoriteTeamMen}");
            Log.Information($"Favorite team women {favoriteTeamWomen}");
            Log.Information($"Favorite players:");
            
            foreach(var team in favoritePlayers.Men)
            {
                Log.Information($"Men's Team {team.Key} : {string.Join(", ", team.Value)}");
            }

			foreach (var team in favoritePlayers.Women)
			{
				Log.Information($"Men's Team {team.Key} : {string.Join(", ", team.Value)}");
			}

			Log.Information("========== Test Settings End ==========");
        }
    }
}