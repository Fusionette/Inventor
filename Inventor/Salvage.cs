using Newtonsoft.Json;

namespace Inventor
{
	class Salvage
	{
		public enum Origin
		{
			Arcane,
			Tech
		}
		public enum Rarity
		{
			Common,
			Uncommon,
			Rare
		}
		public enum Level
		{
			Low,
			Mid,
			High
		}

		[JsonIgnore] public string listDisplay { get { return "" + rarity + ": " + displayName; } }
		public string name;
		public string displayName;
		public Origin origin;
		public Rarity rarity;
		public Level level;
	}
}
