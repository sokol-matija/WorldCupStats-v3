using Newtonsoft.Json;
namespace DataLayer.Models
{
	public class FavoritePlayers
	{
		[JsonProperty("men")]
		public Dictionary<String, List<String>> Men { get; set; } = new Dictionary<string, List<String>>
		{
			{"CRO", new List<String>{ "Danijel SUBASIC " } }
		};

		[JsonProperty("women")]
		public Dictionary<String, List<String>> Women { get; set; } = new Dictionary<string, List<String>>
		{
			{ "USA", new List<string> { "Alex MORGAN", "Megan RAPINOE" } }
		};
	}
}