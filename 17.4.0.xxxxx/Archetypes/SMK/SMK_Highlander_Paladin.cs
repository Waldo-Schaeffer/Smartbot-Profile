using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_HighlanderPaladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Highlander_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.ArgentSquire,
                Cards.BeamingSidekick,
                Cards.BlessingofMight,
                Cards.BlessingofWisdom,
                Cards.BrazenZealot,
                Cards.Crystallizer,
                Cards.Crystology,
                Cards.GlowTron,
                Cards.JarDealer,
                Cards.Mecharoo,
                Cards.Murmy,
                Cards.MicroMummy,
                Cards.SandwaspQueen,
                Cards.SirFinleyoftheSands,
                Cards.ZephrystheGreat,
                Cards.HistoryBuff,
                Cards.MagicCarpet,
                Cards.SalhetsPride,
                Cards.AnnoyoModule,
                Cards.BlessingofKings,
                Cards.Consecration,
                Cards.ReplicatingMenace,
                Cards.Sandbinder,
                Cards.TruesilverChampion,
                Cards.LeeroyJenkins,
                Cards.Zilliax,
                Cards.Siamat,
                Cards.Octosari,
                Cards.TirionFordring
            };
        }
    }
}