using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_MechHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_MechHunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Cards.Cogmaster,
Cards.FaithfulLumi,
Cards.Mecharoo,
Cards.AnnoyoTron,
Cards.Galvanizer,
Cards.Mechwarper,
Cards.UpgradeableFramebot,
Cards.Venomizer,
Cards.MetaltoothLeaper,
Cards.SpiderBomb,
Cards.Explodinator,
Cards.Jeeves,
Cards.ReplicatingMenace,
Cards.Wargear,
Cards.Zilliax,


            };
        }
    }
}