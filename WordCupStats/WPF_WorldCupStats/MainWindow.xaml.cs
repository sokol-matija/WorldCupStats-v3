﻿using System.Windows;
using WPF_WorldCupStats.Configuration;
using WPF_WorldCupStats.Testing;
using WPF_WorldCupStats.ViewModels;

namespace WPF_WorldCupStats
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
			InitializeAsync();
			DataTester.RunAllTests();
		}

		private async void InitializeAsync()
		{
			await SerilogConfig.ConfigureAsync();
		}
	}
}
