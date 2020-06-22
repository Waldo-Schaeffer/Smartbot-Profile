using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_OddRogue : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Odd_Rogue";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.ArgentSquire,
                Cards.ColdBlood,
                Cards.DeadlyPoison,
                Cards.DireMole,
                Cards.FireFly,
                Cards.SouthseaDeckhand,
                Cards.HenchClanThug,
                Cards.ViciousFledgling,
                Cards.Fungalmancer,
                Cards.GigglingInventor,
                Cards.LeeroyJenkins,
                Cards.VilespineSlayer,
                Cards.BakutheMooneater,
                Cards.BlinkFox,
                Cards.BloodKnight,
                Cards.EdwinVanCleef,
                Cards.IronbeakOwl,
                Cards.VoidRipper,
                Cards.CobaltScalebane,
                Cards.MyrasUnstableElement,
            };
        }
    }
}