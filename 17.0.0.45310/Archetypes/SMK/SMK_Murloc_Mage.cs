using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_MurlocMage : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Murloc_Mage";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.GrimscaleOracle,
                Cards.ManaWyrm,
                Cards.MurlocTidecaller,
                Cards.ArchmageArugal,
                Cards.BluegillWarrior,
                Cards.BookofSpecters,
                Cards.Frostbolt,
                Cards.MurlocTidehunter,
                Cards.PrimordialGlyph,
                Cards.RockpoolHunter,
                Cards.ColdlightSeer,
                Cards.MurlocWarleader,
                Cards.NightmareAmalgam,
                Cards.Fireball,
                Cards.GentleMegasaur,
                Cards.DollmasterDorian,
                Cards.Aluneth,
            };
        }
    }
}