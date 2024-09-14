using Serilog;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer.Helpers;

namespace WPF_WorldCupStats.Configuration
{
	public static class SerilogConfig
	{
		public static async Task ConfigureAsync()
		{
			//await ClearSeqLogsAsync();

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.File(FilePathHelper.LogFilePath, rollingInterval: RollingInterval.Day)
				.WriteTo.Seq("http://localhost:5341")
				.CreateLogger();
		}

		public static async Task ClearSeqLogsAsync()
		{
			using (HttpClient client = new HttpClient())
			{
				var seqUrl = "http://localhost:5341/api/events";

				var response = await client.DeleteAsync(seqUrl);

				if (response.IsSuccessStatusCode)
				{
					Log.Information("Seq logs cleared successfully.");
				}
				else
				{
					Log.Error($"Failed to clear Seq logs: {response.ReasonPhrase}");
				}
			}
		}
	}
}
