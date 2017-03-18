using System;
using Rocket.API;
namespace ArenaUnstuck
{
	public class ScootyConfig : IRocketPluginConfiguration
	{
		public string ChatColour;
		public int commandTimeUse;
		public void LoadDefaults()
		{
			commandTimeUse = 30;
			ChatColour = "Blue";

		}
	}
}
