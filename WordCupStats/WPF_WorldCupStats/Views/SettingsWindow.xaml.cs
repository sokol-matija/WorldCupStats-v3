using System.Windows;
using WPF_WorldCupStats.ViewModels;

namespace WPF_WorldCupStats.Views
{
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
			DataContext = new SettingsViewModel();
		}
	}
}