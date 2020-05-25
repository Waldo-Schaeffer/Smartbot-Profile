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
            return "SMK_Wild_Deathrattle_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Candleshot,
                Cards.PlayDead,
                Cards.Tracking,
                Cards.BearTrap,
                Cards.ExplosiveTrap,
                Cards.MadScientist,
				Cards.WanderingMonster,
				Cards.MindControlTech,
				Cards.NineLives,
				Cards.StitchedTracker,
				Cards.DireFrenzy,
				Cards.HoundmasterShaw,
				Cards.Loatheb,
				Cards.WitchwoodGrizzly,
				Cards.Zilliax,
				Cards.DeathstalkerRexxar,
				Cards.LesserEmeraldSpellstone,
				Cards.SylvanasWindrunner,
				Cards.AmaniWarBear,
				Cards.KathrenaWinterwisp,
				Cards.KingKrush
            };
        }
    }
}