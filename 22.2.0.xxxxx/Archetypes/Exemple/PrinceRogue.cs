using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{
  
    public class RogueArchetype : Archetype
    {
        public string ArchetypeName()
        {
            return "PrinceRogue";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.PrinceKeleseth,
                Cards.Backstab,
                Cards.FireFly,
                Cards.Shadowstep,
                Cards.ColdBlood,
                Cards.PatchesthePirate,
                Cards.SouthseaDeckhand,
                Cards.NagaCorsair,
                Cards.Swashburglar,
                Cards.EdwinVanCleef,
                Cards.PerditionsBlade,
                Cards.ShakutheCollector,
                Cards.SI7Agent,
                Cards.SouthseaCaptain,
                Cards.BittertideHydra,
                Cards.LeeroyJenkins,
                Cards.VilespineSlayer,
                Cards.Bonemare
            };
        }
    }
}