using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataLayer.Managers;
using DataLayer.Models;
using WPF_WorldCupStats.Commands;

namespace WPF_WorldCupStats.ViewModels
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
		private readonly SettingsManager _settingsManager = SettingsManager.Instance;

		public WindowSize[] WindowSizes => (WindowSize[])Enum.GetValues(typeof(WindowSize));
		public string[] DataSources => new[] { "api", "json" };
		public string[] Championships => new[] { "men", "women" };
		public string[] Languages => new[] { "en", "hr" };


		private WindowSize _windowSize;
		public WindowSize WindowSize
		{
			get => _windowSize;
			set
			{
				if (_windowSize != value)
				{
					_windowSize = value;
					OnPropertyChanged();
				}
			}
		}

		private string _dataSource;
		public string DataSource
		{
			get => _dataSource;
			set
			{
				if (_dataSource != value)
				{
					_dataSource = value;
					OnPropertyChanged();
				}
			}
		}

		private string _championship;
		public string Championship
		{
			get => _championship;
			set
			{
				if (_championship != value)
				{
					_championship = value;
					OnPropertyChanged();
				}
			}
		}

		private string _language;
		public string Language
		{
			get => _language;
			set
			{
				if (_language != value)
				{
					_language = value;
					OnPropertyChanged();
				}
			}
		}

		private string _favoriteTeamMen;
		public string FavoriteTeamMen
		{
			get => _favoriteTeamMen;
			set
			{
				if (_favoriteTeamMen != value)
				{
					_favoriteTeamMen = value;
					OnPropertyChanged();
				}
			}
		}

		private string _favoriteTeamWomen;
		public string FavoriteTeamWomen
		{
			get => _favoriteTeamWomen;
			set
			{
				if (_favoriteTeamWomen != value)
				{
					_favoriteTeamWomen = value;
					OnPropertyChanged();
				}
			}
		}

		public ICommand SaveCommand { get; private set; }
		public ICommand CancelCommand { get; private set; }

		public event EventHandler CloseRequested;
		public event PropertyChangedEventHandler PropertyChanged;

		public SettingsViewModel()
		{
			LoadSettings();
			InitializeCommands();
		}

		private void LoadSettings()
		{
			WindowSize = _settingsManager.GetSetting(s => s.WindowSize);
			DataSource = _settingsManager.GetSetting(s => s.DataSource);
			Championship = _settingsManager.GetSetting(s => s.Championship);
			Language = _settingsManager.GetSetting(s => s.Language);
			FavoriteTeamMen = _settingsManager.GetSetting(s => s.FavoriteTeamMen);
			FavoriteTeamWomen = _settingsManager.GetSetting(s => s.FavoriteTeamWomen);
		}

		private void InitializeCommands()
		{
			SaveCommand = new RelayCommand(SaveSettings);
			CancelCommand = new RelayCommand(CancelChanges);
		}

		private void SaveSettings()
		{
			_settingsManager.SetSetting(s => s.WindowSize, WindowSize);
			_settingsManager.SetSetting(s => s.DataSource, DataSource);
			_settingsManager.SetSetting(s => s.Championship, Championship);
			_settingsManager.SetSetting(s => s.Language, Language);
			_settingsManager.SetSetting(s => s.FavoriteTeamMen, FavoriteTeamMen);
			_settingsManager.SetSetting(s => s.FavoriteTeamWomen, FavoriteTeamWomen);

			CloseRequested?.Invoke(this, EventArgs.Empty);
		}

		private void CancelChanges()
		{
			LoadSettings(); // Revert to original settings
			CloseRequested?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public List<string> GetFavoritePlayersForTeam(string gender, string team)
		{
			return _settingsManager.GetFavoritePlayersForTeam(gender, team);
		}

		public void AddFavoritePlayer(string gender, string team, string playerName)
		{
			_settingsManager.AddFavoritePlayer(gender, team, playerName);
		}

		public void RemoveFavoritePlayer(string gender, string team, string playerName)
		{
			_settingsManager.RemoveFavoritePlayer(gender, team, playerName);
		}
	}
}