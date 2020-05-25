using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildOddRogue : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Odd_Rogue";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Buccaneer,
                Cards.PraiseGalakrond,
                Cards.GalakrondtheNightmare,
                Cards.BloodsailFlybooter,
                Cards.PharaohCat,
                Cards.SouthseaDeckhand,
                Cards.PatchesthePirate,
                Cards.Swashburglar,
                Cards.SouthseaDeckhand,
                Cards.EVILMiscreant,
                Cards.IronbeakOwl,
                Cards.FacelessCorruptor,
                Cards.SouthseaCaptain,
                Cards.DarkIronSkulker,
                Cards.Fungalmancer,
                Cards.LeeroyJenkins,
                Cards.Loatheb,
                Cards.VilespineSlayer,
                Cards.BakutheMooneater,
	Cards.BadLuckAlbatross,
	Cards.DrBoom,
	Cards.ChiefInspector,
			
            };
        }
    }
}