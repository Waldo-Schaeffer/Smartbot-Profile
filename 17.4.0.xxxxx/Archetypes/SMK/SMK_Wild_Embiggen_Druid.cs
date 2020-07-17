using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildEmbiggenDruid : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Embiggen_Druid";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Embiggen,
                Cards.BloodsailCorsair,
                Cards.DireMole,
                Cards.MarkoftheLotus,
                Cards.PatchesthePirate,
                Cards.SirFinleyMrrgglton,
                Cards.SouthseaDeckhand,
                Cards.DarnassusAspirant,
                Cards.EchoingOoze,
                Cards.NerubarWeblord,
                Cards.ParachuteBrigand,
                Cards.PoweroftheWild,
                Cards.ShipsCannon,
                Cards.SavageRoar,
                Cards.SouthseaCaptain,
                Cards.Jeeves,
                
            };
        }
    }
}