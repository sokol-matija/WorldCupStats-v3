using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DataLayer.Models
{
	public class Settings
	{
		[JsonProperty("WindowSize")]
		public WindowSize WindowSize { get; set; } = WindowSize.Large; 

		[JsonProperty("DataSource")]
        public String DataSource { get; set; } = "api";

        [JsonProperty("Championship")]
        public String Championship { get; set; } = "men";

        [JsonProperty("Language")]
        public String Language { get; set; } = "en";

        [JsonProperty("FavoriteTeamMen")]
        public String FavoriteTeamMen { get; set; } = "CRO";

        [JsonProperty("FavoriteTeamWomen")]
        public String FavoriteTeamWomen { get; set; } = "USA";

        [JsonProperty("FavoritePlayers")]
        public FavoritePlayers favoritePlayers { get; set; } = new FavoritePlayers();

    }
}
