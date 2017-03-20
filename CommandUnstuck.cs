using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Core.Commands;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace ArenaUnstuck
{
	public class CommandUnstuck : IRocketCommand
	{
		public List<string> Aliases
		{
			get
			{
				return new List<string>() { "unstuck", "stuck" };
			}
		}

		public AllowedCaller AllowedCaller
		{
			get
			{
				return	AllowedCaller.Player;
			}
		}

		public string Help
		{
			get
			{
				return "";
			}
		}

		public string Name
		{
			get
			{
				return "stuck";
			}
		}

		public List<string> Permissions
		{
			get
			{
				return new List<string>() { "stuck" };
			}
		}

		public string Syntax
		{
			get
			{
				return "/stuck";
			}
		}

		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer player = (UnturnedPlayer)caller;


			ArenaPlayer aPlayer = LevelManager.arenaPlayers.Find((ply) => ply.steamPlayer.playerID.steamID == player.CSteamID);

			if (aPlayer == null || aPlayer.hasDied)
			{
				UnturnedChat.Say(player, Main.Instance.Translate("cant"), Main.Instance._configColour);
			}


			UnturnedChat.Say(player, Main.Instance.Translate("unstuck"), Main.Instance._configColour);
			Main.Instance.Unstuck(player);



		}
	}
}
