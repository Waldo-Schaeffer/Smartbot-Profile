using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_SpitefulDruid : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Spiteful_Druid";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.FireFly,
                Cards.GlacialShard,
                Cards.PrinceKeleseth,
                Cards.DruidoftheScythe,
                Cards.GreedySprite,
                Cards.MindControlTech,
                Cards.TarCreeper,
                Cards.Spellbreaker,
                Cards.CobaltScalebane,
                Cards.SpitefulSummoner,
                Cards.MalfurionthePestilent,
                Cards.GrandArchivist,
                Cards.UltimateInfestation,
                Cards.CryptLord,
                Cards.Mindbreaker,
                Cards.SaroniteChainGang,
                Cards.Scaleworm,
                Cards.TwilightDrake,
                Cards.Fungalmancer,
                Cards.LeeroyJenkins,
                Cards.Wyrmguard,
                Cards.PrimordialDrake,
            };
        }
    }
}