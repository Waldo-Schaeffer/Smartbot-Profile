using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildOddDemonHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Odd_DemonHunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.BT_351,
                Card.Cards.BT_490,
	            Card.Cards.BT_480,
                Card.Cards.BT_753,
                Card.Cards.BT_142,
                Card.Cards.BT_407,
	            Card.Cards.BT_937,
                Card.Cards.BT_801,
				Card.Cards.DAL_090,
                Card.Cards.BT_352,
	            Card.Cards.BT_495,
                Card.Cards.BT_429,
                Card.Cards.BT_430,
                Card.Cards.BT_493,
	            Card.Cards.GIL_826,
            
            };
        }
    }
}