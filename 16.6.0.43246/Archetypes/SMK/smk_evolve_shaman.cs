using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class EvolveShamanArchetype : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Evolve_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Mutate,
                Cards.Zap,
                Cards.Evolve,
                Cards.SludgeSlurper,
                Cards.VoltaicBurst,
                Cards.Likkim,
                Cards.DesertHare,
                Cards.Thunderhead,
                Cards.Vessina,
                Cards.Bloodlust,
                Cards.MoguFleshshaper,
                Cards.SeaGiant,
                Cards.LightningBolt,
                Cards.EVILTotem,
                Cards.SouloftheMurloc,
                Cards.UnderbellyAngler,
                Cards.SpiritoftheFrog,
            };
        }
    }
}