using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_WorldCupStats.Commands;
using WPF_WorldCupStats.Views;

namespace WPF_WorldCupStats.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly SettingsViewModel _settingsViewModel;

		public ICommand OpenSettingsCommand { get; private set; }
		public ICommand AddFavoritePlayerCommand { get; private set; }
		public ICommand RemoveFavoritePlayerCommand { get; private set; }

		public MainViewModel()
		{
			_settingsViewModel = new SettingsViewModel();
			InitializeCommands();
		}

		private void InitializeCommands()
		{
			OpenSettingsCommand = new RelayCommand(OpenSettings);
			AddFavoritePlayerCommand = new RelayCommand<Tuple<string, string, string>>(AddFavoritePlayer);
			RemoveFavoritePlayerCommand = new RelayCommand<Tuple<string, string, string>>(RemoveFavoritePlayer);
		}

		private void OpenSettings()
		{
			var settingsWindow = new SettingsWindow();
			settingsWindow.ShowDialog();
		}

		private void AddFavoritePlayer(Tuple<string, string, string> param)
		{
			if (param != null)
			{
				string gender = param.Item1;
				string team = param.Item2;
				string playerName = param.Item3;
				_settingsViewModel.AddFavoritePlayer(gender, team, playerName);
			}
		}

		private void RemoveFavoritePlayer(Tuple<string, string, string> param)
		{
			if (param != null)
			{
				string gender = param.Item1;
				string team = param.Item2;
				string playerName = param.Item3;
				_settingsViewModel.RemoveFavoritePlayer(gender, team, playerName);
			}
		}

		public List<string> GetFavoritePlayersForTeam(string gender, string team)
		{
			return _settingsViewModel.GetFavoritePlayersForTeam(gender, team);
		}

		// Implement INotifyPropertyChanged if needed
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}