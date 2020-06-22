using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMKDeathrattleHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Deathrattle_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Candleshot,
                Cards.HuntersMark,
                Cards.PlayDead,
                Cards.Tracking,
                Cards.WildPyromancer,
                Cards.AcolyteofPain,
                Cards.DevilsaurEgg,
                Cards.StitchedTracker,
                Cards.TerrorscaleStalker,
                Cards.FlankingStrike,
                Cards.HoundmasterShaw,
                Cards.WingBlast,
                Cards.CarnivorousCube,
                Cards.WitchwoodGrizzly,
                Cards.DeathstalkerRexxar,
                Cards.SavannahHighmane,
                Cards.KathrenaWinterwisp,
                Cards.KingKrush
            };
        }
    }
}