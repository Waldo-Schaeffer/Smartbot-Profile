using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_ElementalShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Elemental_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Zap,
                Cards.FireFly,
                Cards.LightningBolt,
                Cards.VoltaicBurst,
                Cards.EarthenMight,
                Cards.ElementaryReaction,
                Cards.FlametongueTotem,
                Cards.MenacingNimbus,
                Cards.StormforgedAxe,
                Cards.ElectraStormsurge,
                Cards.UnboundElemental,
                Cards.StormChaser,
                Cards.Thunderhead,
                Cards.Bloodlust,
                Cards.FireElemental,
                Cards.TheStormBringer,
                Cards.KalimosPrimalLord,
            };
        }
    }
}