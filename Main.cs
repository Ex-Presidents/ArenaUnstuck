using System;
using Rocket.API;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Core.Plugins;
using System.Collections.Generic;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.Unturned.Chat;

namespace ArenaUnstuck 
{
	public class Main : RocketPlugin<ScootyConfig>
	{

		public static Main Instance;
		public Color _configColour;
		private DateTime lastArenaCheck;
		protected override void Load()
		{
			_configColour = Rocket.Unturned.Chat.UnturnedChat.GetColorFromName(Configuration.Instance.ChatColour, Color.green);
			LevelManager.onArenaMessageUpdated += ArenaState;
			Instance = this;
			Rocket.Core.Logging.Logger.LogError("\n  _________                    __          \n /   _____/ ____  ____   _____/  |_ ___.__.\n \\_____  \\_/ ___\\/  _ \\ /  _ \\   __<   |  |\n /        \\  \\__(  <_> (  <_> |  |  \\___  |\n/_______  /\\___  \\____/ \\____/|__|  / ____|\n        \\/     \\/                   \\/     ");
			Rocket.Core.Logging.Logger.Log("\n.___        \n|   | ______\n|   |/  ___/\n|   |\\___ \\ \n|___/____  >\n         \\/ ");
			Rocket.Core.Logging.Logger.LogWarning("\n___.                  \n\\_ |__ _____    ____  \n | __ \\\\__  \\ _/ __ \\ \n | \\_\\ \\/ __ \\\\  ___/ \n |___  (____  /\\___  >\n     \\/     \\/     \\/ ");


		}
		protected override void Unload()
		{
			LevelManager.onArenaMessageUpdated -= ArenaState;

		}

		public override Rocket.API.Collections.TranslationList DefaultTranslations
		{
			get
			{
				return new Rocket.API.Collections.TranslationList
				{
					{"cant", "You cannot use this command right now"},
					{"unstuck", "You're now free!"}
				};
			}
		}



			
		void ArenaState(EArenaMessage newArenaMessage)
		{
			if(newArenaMessage == EArenaMessage.WARMUP)
				lastArenaCheck = DateTime.Now;


		}

		public void Unstuck(UnturnedPlayer player)
		{
			List<PlayerSpawnpoint> spawns = LevelPlayers.getAltSpawns();

			PlayerSpawnpoint point = closestSpawn(player, spawns);

			if (Vector3.Distance(point.point, player.Position) > 16)
			{
				UnturnedChat.Say(player, Main.Instance.Translate("cant"), Main.Instance._configColour);
				return;
			}


			if ((DateTime.Now - lastArenaCheck).TotalSeconds > Instance.Configuration.Instance.commandTimeUse)
			{
				UnturnedChat.Say(player, Main.Instance.Translate("cant"), Main.Instance._configColour);
				return;

			}



			player.Teleport(
				new Vector3(point.point.x,
				            point.point.y + 2,
				            point.point.z),
				  			player.Rotation);
		}



		public PlayerSpawnpoint closestSpawn(UnturnedPlayer player, List<PlayerSpawnpoint> spawns)
		{
			PlayerSpawnpoint point = null;
			float distance = float.MaxValue;
			for (int i = 0; i < spawns.Count; i++)
			{
				PlayerSpawnpoint spawn = spawns[i];
				float dist = Vector3.Distance(spawn.point, player.Position);

				if (dist < distance)
				{
					point = spawn;
					distance = dist;
				}
			}

			return point;
		}

		
	}
}
