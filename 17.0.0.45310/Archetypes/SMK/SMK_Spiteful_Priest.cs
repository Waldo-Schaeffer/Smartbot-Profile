using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_SpitefulPriest : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Spiteful_Priest";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.NorthshireCleric,
                Cards.TarCreeper,
                Cards.Duskbreaker,
                Cards.CobaltScalebane,
                Cards.SpitefulSummoner,
                Cards.FreeFromAmber,
                Cards.MindControl,
                Cards.Chameleos,
                Cards.FaerieDragon,
                Cards.PrinceKeleseth,
                Cards.ShadowAscendant,
                Cards.CuriousGlimmerroot,
                Cards.NightmareAmalgam,
                Cards.StonehillDefender,
                Cards.TwilightAcolyte,
                Cards.Scaleworm,
                Cards.Spellbreaker,
                Cards.TortollanShellraiser,
                Cards.TwilightDrake,
                Cards.CabalShadowPriest,
                Cards.LadyinWhite,
                Cards.NightscaleMatriarch,
                Cards.Wyrmguard,
                Cards.GrandArchivist,

                Cards.FireFly,
                Cards.AcolyteofPain,
                Cards.HarrisonJones,
                Cards.DefenderofArgus,
            };
        }
    }
}