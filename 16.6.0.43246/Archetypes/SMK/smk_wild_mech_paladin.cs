using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildMechPaladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Mech_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Crystology,
                Cards.GlowTron,
                Cards.Skaterbot,
                Cards.SmugglersRun,
                Cards.AnnoyoTron,
                Cards.Galvanizer,
                Cards.GrimestreetOutfitter,
                Cards.Mechwarper,
                Cards.ShieldedMinibot,
                Cards.DivineFavor,
                Cards.FlyingMachine,
                Cards.SN1PSN4P,
                Cards.AnnoyoModule,
                Cards.ReplicatingMenace,
                Cards.Wargear,
                Cards.Zilliax
            };
        }
    }
}