using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMKOddHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Odd_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.AcherusVeteran,
                Cards.ArcaneShot,
                Cards.Candleshot,
                Cards.DireMole,
                Cards.EmeraldReaver,
                Cards.GlacialShard,
                Cards.Tracking,
                Cards.AnimalCompanion,
                Cards.EaglehornBow,
                Cards.IronbeakOwl,
                Cards.KillCommand,
                Cards.UnleashtheHounds,
                Cards.LeeroyJenkins,
                Cards.BakutheMooneater,
                Cards.FireFly,
                Cards.HuntersMark,
                Cards.WorgenInfiltrator,
                Cards.Bearshark,
                Cards.Wolfrider
            };
        }
    }
}