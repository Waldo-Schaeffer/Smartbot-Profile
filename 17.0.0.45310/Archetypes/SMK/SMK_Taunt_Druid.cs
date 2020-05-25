using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_TauntDruid : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Taunt_Druid";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.LesserJasperSpellstone,
                Cards.Naturalize,
                Cards.WildGrowth,
                Cards.WitchingHour,
                Cards.BranchingPaths,
                Cards.IronwoodGolem,
                Cards.OakenSummons,
                Cards.Swipe,
                Cards.CarnivorousCube,
                Cards.Nourish,
                Cards.RottenApplebaum,
                Cards.PrimordialDrake,
                Cards.TheLichKing,
                Cards.Hadronox,
                Cards.UltimateInfestation,
                Cards.Wrath,
                Cards.FerociousHowl,
                Cards.TarCreeper,
                Cards.HarrisonJones,
            };
        }
    }
}