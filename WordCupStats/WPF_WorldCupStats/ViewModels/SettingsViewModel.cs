using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataLayer.Managers;
using DataLayer.Models;

namespace WPF_WorldCupStats.ViewModels
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
		private readonly SettingsManager _settingsManager = SettingsManager.Instance;

		// Properties
		public WindowSize[] WindowSizes => (WindowSize[])Enum.GetValues(typeof(WindowSize));

		private WindowSize _selectedWindowSize;
		public WindowSize SelectedWindowSize
		{
			get => _selectedWindowSize;
			set
			{
				if (_selectedWindowSize != value)
				{
					_selectedWindowSize = value;
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

		// Commands
		public ICommand SaveCommand { get; private set; }
		public ICommand CancelCommand { get; private set; }

		public SettingsViewModel()
		{
			LoadSettings();
			InitializeCommands();
		}

		private void LoadSettings()
		{
			SelectedWindowSize = _settingsManager.GetSetting(s => s.WindowSize);
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
			_settingsManager.SetSetting(s => s.WindowSize, SelectedWindowSize);
			_settingsManager.SetSetting(s => s.DataSource, DataSource);
			_settingsManager.SetSetting(s => s.Championship, Championship);
			_settingsManager.SetSetting(s => s.Language, Language);
			_settingsManager.SetSetting(s => s.FavoriteTeamMen, FavoriteTeamMen);
			_settingsManager.SetSetting(s => s.FavoriteTeamWomen, FavoriteTeamWomen);
			// Close the window
		}

		private void CancelChanges()
		{
			// Close the window without saving
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}