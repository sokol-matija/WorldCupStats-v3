using DataLayer.Helpers;
using DataLayer.Models;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using Serilog;

namespace DataLayer.Managers
{
	public class SettingsManager
	{
		private static readonly Lazy<SettingsManager> _instance = new Lazy<SettingsManager>(() => new SettingsManager());
		private readonly string _settingsFilePath;
		private Settings _settings;

		public static SettingsManager Instance => _instance.Value;

		private SettingsManager()
		{
			_settingsFilePath = FilePathHelper.SettingsFilePath;
			LoadSettings();
		}

		private void LoadSettings()
		{
			try
			{
				if (File.Exists(_settingsFilePath))
				{
					string json = File.ReadAllText(_settingsFilePath);
					_settings = JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
				}
				else
				{
					_settings = new Settings();
					SaveSettings();
					Log.Information("Settings file not found. Created default settings.");
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error loading settings. Using default settings.");
				_settings = new Settings();
			}
		}

		public void SaveSettings()
		{
			try
			{
				string json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_settingsFilePath, json);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error saving settings");
				throw; // Rethrow the exception after logging
			}
		}

		public T GetSetting<T>(Expression<Func<Settings, T>> propertySelector)
		{
			try
			{
				return propertySelector.Compile()(_settings);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error getting setting");
				throw;
			}
		}

		public void SetSetting<T>(Expression<Func<Settings, T>> propertySelector, T value)
		{
			try
			{
				var propertyInfo = ((MemberExpression)propertySelector.Body).Member as PropertyInfo;
				if (propertyInfo == null)
				{
					throw new ArgumentException("The lambda expression 'propertySelector' should point to a valid Property");
				}

				propertyInfo.SetValue(_settings, value);
				SaveSettings();
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error setting setting");
				throw;
			}
		}

		public List<string> GetFavoritePlayersForTeam(string gender, string team)
		{
			var genderDict = gender.ToLower() == "men" ? _settings.FavoritePlayers.Men : _settings.FavoritePlayers.Women;
			return genderDict.TryGetValue(team, out var players) ? players : new List<string>();
		}

		public void AddFavoritePlayer(string gender, string team, string playerName)
		{
			var genderDict = gender.ToLower() == "men" ? _settings.FavoritePlayers.Men : _settings.FavoritePlayers.Women;

			if (!genderDict.TryGetValue(team, out var players))
			{
				players = new List<string>();
				genderDict[team] = players;
			}

			if (!players.Contains(playerName))
			{
				players.Add(playerName);
				SaveSettings();
			}
		}

		public void RemoveFavoritePlayer(string gender, string team, string playerName)
		{
			var genderDict = gender.ToLower() == "men" ? _settings.FavoritePlayers.Men : _settings.FavoritePlayers.Women;

			if (genderDict.TryGetValue(team, out var players) && players.Remove(playerName))
			{
				SaveSettings();
			}
		}
	}
}