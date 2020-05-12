using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_EmbiggenDruid : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Embiggen_Druid";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Embiggen,
                Cards.StrengthinNumbers,
                Cards.BreathofDreams,
                Cards.FaerieDragon,
                Cards.EvasiveFeywing,
                Cards.Swipe,
                Cards.BigOlWhelp,
                Cards.LeeroyJenkins,
                Cards.OasisSurger,
                Cards.Zilliax,
                Cards.EmeraldExplorer,
                Cards.EvasiveWyrm,
                //Winged Guardian
                Card.Cards.YOD_003,
                Cards.InjuredTolvir,
                Cards.Scalerider,
                Cards.WitchingHour,
                Cards.FrizzKindleroost
            };
        }
    }
}