using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataLayer.Managers;

namespace WPF_WorldCupStats.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly SettingsManager _settingsManager = SettingsManager.Instance;

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

		// Add other properties similarly

		public MainViewModel()
		{
			LoadSettings();
		}

		private void LoadSettings()
		{
			DataSource = _settingsManager.GetSetting(s => s.DataSource);
			// Load other settings similarly
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}