using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;

namespace SmartBot.Plugins
{
	[Serializable]
    public class bPluginDataContainer : PluginDataContainer
    {
		//Init vars
        public bPluginDataContainer()
		{
			Name = "DisplayInfos";
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
		
		
		/* --------------- DisplayBoardInfos Methods -------------- */
		
		private void Update()
		{
			if(Bot.CurrentBoard != null)
			{
				DisplayHerosInfo();
				DisplayWeaponsInfo();
				DisplayHeroPowersInfo();
				DisplayHandInfos();
				DisplayMinionFriendInfos();
				DisplayMinionEnemyInfos();
			}
		}
		
		private void DisplayHerosInfo()
		{
			string FriendlyClass = Bot.CurrentBoard.FriendClass.ToString();
			string EnemyClass = Bot.CurrentBoard.EnemyClass.ToString();
				
			string FriendHero = "("+ Bot.CurrentBoard.HeroFriend.CurrentHealth.ToString() +"/"+Bot.CurrentBoard.HeroFriend.CurrentArmor.ToString()+")";
			string EnemyHero = "("+ Bot.CurrentBoard.HeroEnemy.CurrentHealth.ToString() +"/"+Bot.CurrentBoard.HeroEnemy.CurrentArmor.ToString()+")";

			Bot.Log("[PLUGIN] -> Display infos : Heros ->" + FriendlyClass + FriendHero +" - " + EnemyClass + EnemyHero);
		}
		
		private void DisplayWeaponsInfo()
		{
			string FriendWeapon = string.Empty;
			string EnemyWeapon = string.Empty;

			if(Bot.CurrentBoard.WeaponFriend != null) //do we have weapon ?
			{
				FriendWeapon = Bot.CurrentBoard.WeaponFriend.CardId + " (" + Bot.CurrentBoard.WeaponFriend.CurrentAtk + "/" + Bot.CurrentBoard.WeaponFriend.CurrentDurability + ")"; 
			}
			if(Bot.CurrentBoard.WeaponEnemy != null) //do we have weapon ?
			{
				EnemyWeapon = Bot.CurrentBoard.WeaponEnemy.CardId + " (" + Bot.CurrentBoard.WeaponEnemy.CurrentAtk + "/" + Bot.CurrentBoard.WeaponEnemy.CurrentDurability + ")"; 
			}
			
			Bot.Log("[PLUGIN] -> Display infos : Weapons ->" + "Friend :" + FriendWeapon + " - Enemy :" + EnemyWeapon);
		}
		
		private void DisplayHeroPowersInfo()
		{
			string FriendHeroPower = Bot.CurrentBoard.Ability.CardId;
			string EnemyHeroPower = Bot.CurrentBoard.EnemyAbility.CardId;

			Bot.Log("[PLUGIN] -> Display infos : Hero powers ->" + "Friend :" + FriendHeroPower + " - Enemy :" + EnemyHeroPower);
		}
		
		private void DisplayHandInfos()
		{
			string Hand = string.Empty;
				
			for(int i = 0 ; i < Bot.CurrentBoard.Hand.Count ; i++)
			{
				Card c = Bot.CurrentBoard.Hand[i];
				if(c == null) continue;
					
				Hand += c.CardId + " (" + c.CurrentCost.ToString() + ") ";
					
				if(i < (int)((Bot.CurrentBoard.Hand.Count) - 1))
					Hand += " - ";
			}
				
			Bot.Log("[PLUGIN] -> Display infos : Hand ->" + Hand);
		}
		
		private void DisplayMinionFriendInfos()
		{
			string MinionFriend = string.Empty;
				
			for(int i = 0 ; i < Bot.CurrentBoard.MinionFriend.Count ; i++)
			{
				Card c = Bot.CurrentBoard.MinionFriend[i];
				if(c == null) continue;
					
				MinionFriend += c.CardId + " (" + c.CurrentAtk.ToString() + "/" + c.CurrentHealth + ")";
					
				if(c.IsTaunt)
					MinionFriend += "[T]";
				if(c.IsDivineShield)
					MinionFriend += "[D]";
				if(c.IsWindfury)
					MinionFriend += "[W]";
				if(c.IsCharge)
					MinionFriend += "[C]";
				if(c.IsStealth)
					MinionFriend += "[ST]";
				if(c.IsSilenced)
					MinionFriend += "[SLC]";
				if(c.HasPoison)
					MinionFriend += "[P]";
					
				if(i < (int)((Bot.CurrentBoard.MinionFriend.Count) - 1))
					MinionFriend += " - ";
			}
				
			Bot.Log("[PLUGIN] -> Display infos : MinionFriend ->" + MinionFriend);
		}

		private void DisplayMinionEnemyInfos()
		{
			string MinionEnemy = string.Empty;
				
			for(int i = 0 ; i < Bot.CurrentBoard.MinionEnemy.Count ; i++)
			{
				Card c = Bot.CurrentBoard.MinionEnemy[i];
				if(c == null) continue;
					
				MinionEnemy += c.CardId + " (" + c.CurrentAtk.ToString() + "/" + c.CurrentHealth + ")";
					
				if(c.IsTaunt)
					MinionEnemy += "[T]";
				if(c.IsDivineShield)
					MinionEnemy += "[D]";
				if(c.IsWindfury)
					MinionEnemy += "[W]";
				if(c.IsCharge)
					MinionEnemy += "[C]";
				if(c.IsStealth)
					MinionEnemy += "[ST]";
				if(c.IsSilenced)
					MinionEnemy += "[SLC]";
				if(c.HasPoison)
					MinionEnemy += "[P]";
					
				if(i < (int)((Bot.CurrentBoard.MinionEnemy.Count) - 1))
					MinionEnemy += " - ";
			}
				
			Bot.Log("[PLUGIN] -> Display infos : MinionEnemy ->" + MinionEnemy);
		}
    }
}
