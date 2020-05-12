using SmartBot.Plugins.API;
using System;

namespace SmartBot.Plugins
{
	[Serializable]
    public class bPluginDataContainer : PluginDataContainer
    {
		
		//Init vars
        public bPluginDataContainer()
		{
			Name = "CompleteQuestsAndExit";
		}
    }

    public class bPlugin : Plugin
    {
		//Constructor
		public override void OnPluginCreated()
		{
		}
		
		//Bot tick event
        public override void OnTick()
        {
            //Bot.Log("[PLUGIN] -> OnTick");
        }
		
		//Bot starting event
        public override void OnStarted()
        {
            //Bot.Log("[PLUGIN] -> OnStarted");
        }

		//Bot stopping event
        public override void OnStopped()
        {
            //Bot.Log("[PLUGIN] -> OnStopped");
        }

		//Turn begin event
        public override void OnTurnBegin()
        {
			//Bot.Log("[PLUGIN] -> OnTurnBegin");
        }

		//Turn end event
        public override void OnTurnEnd()
        {
			//Bot.Log("[PLUGIN] -> OnTurnEnd");
        }

		//Simulation event (AI calculation)
        public override void OnSimulation()
        {
			//Bot.Log("[PLUGIN] -> OnSimulation");
        }

		//Match begin event
        public override void OnGameBegin()
        {
			//Bot.Log("[PLUGIN] -> OnGameBegin");
        }

		//Match end event
        public override void OnGameEnd()
        {
			//Bot.Log("[PLUGIN] -> OnGameEnd");

			if(Bot.GetQuests().Count == 0)
			{
				Bot.StopRelogger();
				Bot.CloseHs();
				Bot.CloseBot(); 
			}
        }

		//gold balance changed event
        public override void OnGoldAmountChanged()
        {
			//Bot.Log("[PLUGIN] -> OnGoldAmountChanged");
        }

		//arena 12 wins or 3 losses event
        public override void OnArenaEnd()
        {
			//Bot.Log("[PLUGIN] -> OnArenaEnd");
        }

		//lethal found event (during a game)
        public override void OnLethal()
        {
			//Bot.Log("[PLUGIN] -> OnLethal");
        }

		//all quests completed event
        public override void OnAllQuestsCompleted()
        {
			//Bot.Log("[PLUGIN] -> OnAllQuestsCompleted");
				
			Bot.StopRelogger();
			Bot.CloseHs();
			Bot.CloseBot();
        }

		//concede event
        public override void OnConcede()
        {
			//Bot.Log("[PLUGIN] -> OnConcede");
        }
		
		public override void OnWhisperReceived(Friend friend, string message)
        {
            
        }
    }
}
