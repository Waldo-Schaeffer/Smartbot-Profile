using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Dragon_Rogue : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Dragon_Rogue";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
              Cards.Preparation,
              Cards.CounterfeitCoin,
              Cards.Backstab,
              Cards.Sap,
              Cards.Eviscerate,
              Cards.NecriumBlade,
              Cards.NecriumApothecary,
              Cards.WitchwoodPiper,
              Cards.AnkatheBuried,
              Cards.CandleBreath,
              Cards.EvasiveWyrm,
              Cards.EvasiveDrakonid,
              Cards.Chromaggus,
              Cards.PrimordialDrake,
              Cards.Alexstrasza,
              Cards.Nozdormu,
              Cards.SleepyDragon,
              Cards.Ysera,
              Cards.Deathwing
            };
        }
    }
}