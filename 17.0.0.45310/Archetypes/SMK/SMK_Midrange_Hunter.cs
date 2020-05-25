using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMKMidrangeHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Midrange_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.DireMole,
                Cards.Springpaw,
                Cards.CracklingRazormaw,
                Cards.ScavengingHyena,
                Cards.AnimalCompanion,
                Cards.KillCommand,
                Cards.UnleashtheHounds,
                Cards.FlankingStrike,
                Cards.Candleshot,
                Cards.HuntersMark,
                Cards.JeweledMacaw,
                Cards.TimberWolf,
                Cards.Tracking,
                Cards.HeadhuntersHatchet,
                Cards.Bearshark,
                Cards.MastersCall,
                Cards.DireFrenzy,
                Cards.Houndmaster,
                Cards.BaitedArrow,
                Cards.TundraRhino,
                Cards.DeathstalkerRexxar,
                Cards.SavannahHighmane
            };
        }
    }
}