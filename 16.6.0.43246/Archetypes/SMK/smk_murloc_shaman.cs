using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_MurlocShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Murloc_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.GrimscaleOracle,
                Cards.MurlocTidecaller,
                Cards.Murmy,
                Cards.SludgeSlurper,
                Cards.Toxfin,
                Cards.BluegillWarrior,
                Cards.Fishflinger,
                Cards.MurlocTidehunter,
                Cards.SouloftheMurloc,
                Cards.UnderbellyAngler,
                Cards.ColdlightSeer,
                Cards.MurlocWarleader,
                Cards.MurlocTastyfin,
                Cards.Bloodlust,
                Cards.NightmareAmalgam,
                Cards.Scargil,
                Cards.Siamat,
                Cards.Mutate,
                Cards.LeeroyJenkins,
                Cards.MoguFleshshaper
            };
        }
    }
}