using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataLayer.Managers;
using DataLayer.Models;

namespace WPF_WorldCupStats.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly SettingsManager _settingsManager = SettingsManager.Instance;

		// Properties
		private WindowSize _windowSize;
		public WindowSize WindowSize
		{
			get => _windowSize;
			set
			{
				if (_windowSize != value)
				{
					_windowSize = value;
					_settingsManager.SetSetting(s => s.WindowSize, value);
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
					_settingsManager.SetSetting(s => s.DataSource, value);
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
					_settingsManager.SetSetting(s => s.Championship, value);
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
					_settingsManager.SetSetting(s => s.Language, value);
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
					_settingsManager.SetSetting(s => s.FavoriteTeamMen, value);
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
					_settingsManager.SetSetting(s => s.FavoriteTeamWomen, value);
					OnPropertyChanged();
				}
			}
		}

		public FavoritePlayers FavoritePlayers { get; private set; }

		// Commands
		public ICommand AddFavoritePlayerCommand { get; private set; }
		public ICommand RemoveFavoritePlayerCommand { get; private set; }

		public MainViewModel()
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
			FavoritePlayers = _settingsManager.GetSetting(s => s.FavoritePlayers);
		}

		private void InitializeCommands()
		{
			AddFavoritePlayerCommand = new RelayCommand<Tuple<string, string, string>>(AddFavoritePlayer);
			RemoveFavoritePlayerCommand = new RelayCommand<Tuple<string, string, string>>(RemoveFavoritePlayer);
		}

		private void AddFavoritePlayer(Tuple<string, string, string> param)
		{
			if (param != null)
			{
				string gender = param.Item1;
				string team = param.Item2;
				string playerName = param.Item3;
				_settingsManager.AddFavoritePlayer(gender, team, playerName);
				OnPropertyChanged(nameof(FavoritePlayers));
			}
		}

		private void RemoveFavoritePlayer(Tuple<string, string, string> param)
		{
			if (param != null)
			{
				string gender = param.Item1;
				string team = param.Item2;
				string playerName = param.Item3;
				_settingsManager.RemoveFavoritePlayer(gender, team, playerName);
				OnPropertyChanged(nameof(FavoritePlayers));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	// Simple implementation of ICommand for the example
	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);
		public void Execute(object parameter) => _execute((T)parameter);
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}