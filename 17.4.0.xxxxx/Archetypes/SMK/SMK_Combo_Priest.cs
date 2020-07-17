using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class ComboPriestArchetype : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Combo_Priest";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.CircleofHealing,
                Cards.InnerFire,
                Cards.NorthshireCleric,
                Cards.PowerWordShield,
                Cards.DivineSpirit,
                Cards.ExtraArms,
                Cards.InjuredTolvir,
                Cards.WildPyromancer,
                Cards.AcolyteofPain,
                Cards.InjuredBlademaster,
                Cards.HighPriestAmet,
                Cards.MassDispel,
                Cards.Psychopomp,
                Cards.Silence,
                Cards.TopsyTurvy,
                Cards.Lightwarden,
                Cards.HolyRipple,
                Cards.NefersetRitualist,
                //GA
                //Cleric Of Scales
                Card.Cards.YOD_013,
                //Dark Prophecy
                Card.Cards.YOD_015,
                Cards.TwilightDrake,
                Cards.BigOlWhelp,
                Cards.EvasiveWyrm,
                Cards.MagicCarpet,
                Cards.PotionVendor,
            };
        }
    }
}
