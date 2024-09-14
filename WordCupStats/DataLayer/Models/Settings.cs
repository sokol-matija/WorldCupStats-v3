using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DataLayer.Models
{
	public class Settings
	{
		[JsonPropertyName("windowSize")]
		public WindowSize WindowSize { get; set; } = WindowSize.Large;

		[JsonPropertyName("dataSource")]
		public string DataSource { get; set; } = "api";

		[JsonPropertyName("championship")]
		public string Championship { get; set; } = "men";

		[JsonPropertyName("language")]
		public string Language { get; set; } = "en";

		[JsonPropertyName("favoriteTeamMen")]
		public string FavoriteTeamMen { get; set; } = "CRO";

		[JsonPropertyName("favoriteTeamWomen")]
		public string FavoriteTeamWomen { get; set; } = "USA";

		[JsonPropertyName("favoritePlayers")]
		public FavoritePlayers FavoritePlayers { get; set; } = new FavoritePlayers();
	}
}
