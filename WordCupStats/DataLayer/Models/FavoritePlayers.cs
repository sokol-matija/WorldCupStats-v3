using Newtonsoft.Json;
namespace DataLayer.Models
{
	public class FavoritePlayers
	{
		[JsonProperty("men")]
		public Dictionary<string, List<string>> Men { get; set; } = new Dictionary<string, List<string>>
		{
			{"CRO", new List<string>{ "Danijel SUBASIC " } }
		};

		[JsonProperty("women")]
		public Dictionary<string, List<string>> Women { get; set; } = new Dictionary<string, List<string>>
		{
			{ "USA", new List<string> { "Alex MORGAN", "Megan RAPINOE" } }
		};
	}
}