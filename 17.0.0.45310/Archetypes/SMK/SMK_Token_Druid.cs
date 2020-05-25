using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_TokenDruid : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Token_Druid";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Cards.StrengthinNumbers,
Cards.BreathofDreams,
Cards.Wrath,
Cards.Swipe,
Cards.AnubisathDefender,
Cards.EmeraldExplorer,
Cards.ExoticMountseller,
Cards.Overflow,
Cards.WingedGuardian,
Cards.TwinTyrant,
Cards.Alexstrasza,
Cards.Ysera,
Cards.YseraUnleashed,
Card.Cards.BT_135,
Card.Cards.BT_132,
Card.Cards.BT_127,
Card.Cards.BT_134,
Card.Cards.BT_130,

            };
        }
    }
}