using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_QuestHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Quest_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.CleartheWay,
                Cards.Springpaw,
                Cards.UnsealtheVault,
                Cards.HenchClanHogsteed,
                Cards.QuestingExplorer,
                Cards.DesertSpear,
                Cards.DivingGryphon,
                Cards.UnleashtheHounds,
                Cards.FacelessCorruptor,
                Cards.LeeroyJenkins,
                Cards.Zilliax,
                Cards.SwarmofLocusts,
                Cards.Shuma,
                Cards.SeaGiant,
                Cards.Tracking,
                Cards.RevengeoftheWild,
                Cards.ScavengingHyena,
                Cards.BoneWraith,
                Cards.CultMaster,
                Cards.HalazzitheLynx,
                Cards.Veranus,
            };
        }
    }
}