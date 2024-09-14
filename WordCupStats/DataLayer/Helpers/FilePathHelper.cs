using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace DataLayer.Helpers
{
	public static class FilePathHelper
	{
		private const string SettingsFileName = "settings.json";
		private const string LogFileName = "wpf-worldcupstats.log";

		private static readonly Lazy<string> LazySettingsFilePath = new Lazy<string>(() => GetFilePath("DataLayer", "Assets", SettingsFileName));
		private static readonly Lazy<string> LazyLogFilePath = new Lazy<string>(() => GetFilePath("WPF_WorldCupStats", "Logs", LogFileName));

		public static string SettingsFilePath => LazySettingsFilePath.Value;
		public static string LogFilePath => LazyLogFilePath.Value;

		public static void LogAllPaths()
		{
			Log.Information("========== File Path Helper - All Paths ==========");
			Log.Information("Settings file path: {SettingsFilePath}", SettingsFilePath);
			Log.Information("Log file path: {LogFilePath}", LogFilePath);
			Log.Information("========== File Path Helper - End ==========");
		}

		private static string GetFilePath(string projectName, string subdirectory, string fileName)
		{
			string solutionDirectory = FindSolutionDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			if (string.IsNullOrEmpty(solutionDirectory))
				throw new DirectoryNotFoundException("Solution directory not found.");

			string projectDirectory = Path.Combine(solutionDirectory, projectName);

			if (!Directory.Exists(projectDirectory))
				throw new DirectoryNotFoundException($"{projectName} project directory not found.");

			string targetDirectory = Path.Combine(projectDirectory, subdirectory);
			EnsureDirectoryExists(targetDirectory);

			return Path.Combine(targetDirectory, fileName);
		}

		private static string FindSolutionDirectory(string startDirectory)
		{
			while (!string.IsNullOrEmpty(startDirectory))
			{
				if (Directory.GetFiles(startDirectory, "*.sln").Length > 0)
					return startDirectory;

				startDirectory = Directory.GetParent(startDirectory)?.FullName;
			}
			return null;
		}

		public static void EnsureDirectoryExists(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				Log.Information("Created directory: {DirectoryPath}", path);
			}
		}
	}
}