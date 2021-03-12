using System;
using System.IO;
using System.Diagnostics;
using SmartBot.Plugins.API;
using System.ComponentModel;

namespace SmartBot.Plugins
{
	[Serializable]
	public class bPluginDataContainer : PluginDataContainer
	{
		//Init vars
		public bPluginDataContainer()
		{
			Name = "DiscoverUpdater";
		}

		[DisplayName("Update on injection instead of start")]
		public bool _UpdateOnInject { get; set; }
	}

	public class bPlugin : Plugin
	{
		// Declarations
		private bool oneShot = false;
		string filePath = Directory.GetCurrentDirectory() + @"\EEDiscoverUpdater.exe";

		/* --------------- Execute EvilEyes Discover Updater -------------- */

		//Bot starting event
		public override void OnStarted()
		{
			if (!DataContainer.Enabled)
				return;

			if (!oneShot && !Data()._UpdateOnInject)
			{
				Process.Start(filePath);
				Bot.Log("[PLUGIN] -> EvilEyesDiscovery : Updating Files ...");
				oneShot = true;
			}
		}

		// Bot injection event
		public override void OnInjection()
		{
			if (!DataContainer.Enabled)
				return;

			if (!oneShot && Data()._UpdateOnInject)
			{
				Process.Start(filePath);
				Bot.Log("[PLUGIN] -> EvilEyesDiscovery : Updating Files ...");
				oneShot = true;
			}
		}

		private bPluginDataContainer Data()
		{
			return (bPluginDataContainer)DataContainer;
		}
	}
}
