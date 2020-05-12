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
			Name = "AutoDeclineFriendRequests";
		}
    }

    public class bPlugin : Plugin
    {
		//Constructor
		public override void OnPluginCreated()
		{
		}
		
		private DateTime _lastTick = DateTime.MinValue;
		//Bot tick event
        public override void OnTick()
        {
			if(_lastTick.AddMilliseconds(10000) > DateTime.Now) return;
			_lastTick = DateTime.Now;
			
            //Bot.Log("[PLUGIN] -> OnTick");
			foreach(FriendRequest request in Bot.GetFriendRequests())
			{
				Bot.DeclineFriendRequest(request);
			}
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
