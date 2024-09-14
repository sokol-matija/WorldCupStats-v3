using DataLayer.Helpers;
using DataLayer.Models;
using Newtonsoft.Json;
using Serilog;
using System.Linq.Expressions;
using System.Reflection;

namespace DataLayer.Managers
{
	public class SettingsManager
	{
		private const string SettingsFileName = "settings.json";
		private static readonly Lazy<SettingsManager> _instance = new Lazy<SettingsManager>(() => new SettingsManager());
		private readonly string _settingsFilePath = FilePathHelper.SettingsFilePath;
		private Settings _settings;

		public static SettingsManager Instance => _instance.Value;

		private SettingsManager()
		{
			LoadSettings();
		}

		private static string FindSolutionDirectory(string startDirectory)
		{
			while (startDirectory != null)
			{
				if (Directory.GetFiles(startDirectory, "*.sln").Length > 0)
				{
					return startDirectory;
				}
				startDirectory = Directory.GetParent(startDirectory)?.FullName;
			}
			return null;
		}

		private void LoadSettings()
		{
			try
			{
				_settings = File.Exists(_settingsFilePath)
					? JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_settingsFilePath))
					: new Settings();

				if (!File.Exists(_settingsFilePath))
				{
					Log.Information("Settings file not found. Created default settings.");
					SaveSettings();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error loading settings");
				_settings = new Settings();
			}
		}

		public void SaveSettings()
		{
			try
			{
				File.WriteAllText(_settingsFilePath, JsonConvert.SerializeObject(_settings, Formatting.Indented));
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error saving settings");
			}
		}

		public T GetSetting<T>(Expression<Func<Settings, T>> propertySelector)
		{
			return propertySelector.Compile()(_settings);
		}

		public void SetSetting<T>(Expression<Func<Settings, T>> propertySelector, T value)
		{
			var propertyInfo = ((MemberExpression)propertySelector.Body).Member as System.Reflection.PropertyInfo;
			if (propertyInfo == null)
			{
				throw new ArgumentException("The lambda expression 'propertySelector' should point to a valid Property");
			}

			propertyInfo.SetValue(_settings, value);
			SaveSettings();
		}

		public List<string> GetFavoritePlayersForTeam(string gender, string team)
		{
			var genderDict = gender.ToLower() == "men" ? _settings.favoritePlayers.Men : _settings.favoritePlayers.Women;
			return genderDict.TryGetValue(team, out var players) ? players : new List<string>();
		}

		public void AddFavoritePlayer(string gender, string team, string playerName)
		{
			var genderDict = gender.ToLower() == "men" ? _settings.favoritePlayers.Men : _settings.favoritePlayers.Women;

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
			var genderDict = gender.ToLower() == "men" ? _settings.favoritePlayers.Men : _settings.favoritePlayers.Women;

			if (genderDict.TryGetValue(team, out var players) && players.Remove(playerName))
			{
				SaveSettings();
			}
		}
	}
}