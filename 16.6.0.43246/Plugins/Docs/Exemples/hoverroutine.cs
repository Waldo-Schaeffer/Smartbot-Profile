using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;

namespace SmartBot.Plugins
{
	[Serializable]
    public class bPluginDataContainer : PluginDataContainer
    {
		public int DurationMin { get; set; }//in milliseconds
		public int DurationMax { get; set; }//in milliseconds
		public int IntervalMin { get; set; }//in milliseconds
		public int IntervalMax { get; set; }//in milliseconds

		//Init vars
        public bPluginDataContainer()
		{
			Name = "HoverRoutine";
			DurationMin = 1500;
			DurationMax = 3000;
			IntervalMin = 1000;
			IntervalMax = 10000;
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
			Update();
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
			Update();
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
		
		
		/* --------------- HoverRoutine Methods -------------- */
		
		private DateTime _lastHover = DateTime.MinValue;
		private int _lastInterval = 0;
		private static Random _rd = new Random();
		private int RandomInt(int max ,int min = 0)
		{
			 if (_rd == null)
                _rd = new Random();
            return _rd.Next(min, max);
		}
		
		private void Update()
		{
			if(Bot.CurrentBoard != null && !Bot.CurrentBoard.IsOwnTurn)
			{
				if(_lastHover.AddMilliseconds(_lastInterval) < DateTime.Now)
				{
					bool HasFriendlyMinion = Bot.CurrentBoard.MinionFriend.Count > 0;
					bool HasEnemyMinion = Bot.CurrentBoard.MinionEnemy.Count > 0;
					bool HasCardsInHand = Bot.CurrentBoard.Hand.Count > 0;
					
					List<int> RandPool = new List<int>();
					
					if(HasFriendlyMinion) RandPool.Add(0);
					if(HasEnemyMinion) RandPool.Add(1);
					if(HasCardsInHand) RandPool.Add(2);
					
					if(RandPool.Count == 0) return;
					
					int Duration = RandomInt(((bPluginDataContainer)DataContainer).DurationMax,((bPluginDataContainer)DataContainer).DurationMin);
					int Interval = RandomInt(((bPluginDataContainer)DataContainer).IntervalMax,((bPluginDataContainer)DataContainer).IntervalMin);

					switch(RandPool[RandomInt(RandPool.Count)])
					{
						case 0:
							Bot.SendRandomHoverOnFriendlyMinions(Duration);
							break;
							
						case 1:
							Bot.SendRandomHoverOnEnemyMinions(Duration);
							break;
							
						case 2:
							Bot.SendRandomHoverOnHand(Duration);
							break;
					}
					_lastHover = DateTime.Now;
					_lastInterval = Interval;
				}
			}
			else
			{
				_lastHover = DateTime.MinValue;
				_lastInterval = 0;
			}
		}
    }
}
