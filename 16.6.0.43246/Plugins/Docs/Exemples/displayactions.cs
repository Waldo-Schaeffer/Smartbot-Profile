using SmartBot.Plugins.API;
using System.Collections.Generic;
using SmartBot.Plugins.API.Actions;
using System;

namespace SmartBot.Plugins
{
	[Serializable]
    public class bPluginDataContainer : PluginDataContainer
    {
		
		//Init vars
        public bPluginDataContainer()
		{
			Name = "DisplayActions";
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
		
		public override void OnActionExecute(SmartBot.Plugins.API.Actions.Action action) 
		{
			if(action is ResimulateAction)
			{
				Bot.Log("[PLUGIN] -> Execute : Resimulate action");
			}
			else if(action is AttackAction)
			{
				Bot.Log("[PLUGIN] -> Execute : Attack " + action.Actor.ToString() + " - " + action.Target.ToString());
			}
			else if(action is PushAction)
			{
				if(action.Target != null)
					Bot.Log("[PLUGIN] -> Execute : Attack " + action.Actor.ToString() + " - " + action.Target.ToString() + " Pos :" + action.Index.ToString());
				else 
					Bot.Log("[PLUGIN] -> Execute : Attack " + action.Actor.ToString() + " Pos :" + action.Index.ToString());
			}
			else if(action is ChoiceAction)
			{
				Bot.Log("[PLUGIN] -> Execute : Choice " + action.Choice.ToString());
			}
			else if(action is TargetAction)
			{
				Bot.Log("[PLUGIN] -> Execute : Target " + action.Target.ToString());
			}
			else if(action is ConcedeAction)
			{
				Bot.Log("[PLUGIN] -> Execute : Concede action");
			}
			else if(action is EndTurnAction)
			{
				Bot.Log("[PLUGIN] -> Execute : EndTurnAction action");
			}
			
		}
		
        public override void OnActionStackReceived(List<SmartBot.Plugins.API.Actions.Action> actions) 
		{
			/*Bot.Log("[PLUGIN] -> ActionStack : ");
						
			foreach(SmartBot.Plugins.API.Actions.Action a in actions)
				Bot.Log("[PLUGIN] -> " + a.GetActionStr());*/

		}
    }
}
