using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_ZooWarlock : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Zoo_Warlock";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.FlameImp,
                Cards.KoboldLibrarian,
                Cards.Soulfire,
                Cards.Voidwalker,
                Cards.PrinceKeleseth,
                Cards.TarCreeper,
                Cards.SaroniteChainGang,
                Cards.DespicableDreadlord,
                Cards.Doomguard,
                Cards.AcherusVeteran,
                Cards.FireFly,
                Cards.MortalCoil,
                Cards.Duskbat,
                Cards.ViciousFledgling,
                Cards.HookedReaver,
                Cards.Spellbreaker,
                Cards.Fungalmancer,
                Cards.BloodreaverGuldan,
                Cards.SeaGiant,

                Cards.Lightwarden,
                Cards.VoodooDoctor,
                Cards.FungalEnchanter,
                Cards.HappyGhoul,
                Cards.VoidRipper,
                Cards.Lifedrinker,
                Cards.LeeroyJenkins,

                Cards.TheSoularium,
                Cards.SoulInfusion,
                Cards.DoublingImp,

                Cards.AbusiveSergeant,
                Cards.ArgentSquire,
                Cards.GrimRally,
                Cards.Mecharoo,
                Cards.DireWolfAlpha,
                Cards.EVILGenius,
                Cards.KnifeJuggler,
                Cards.ScarabEgg,
                Cards.SaroniteTaskmaster,
                Cards.MagicCarpet,
                Cards.ArchVillainRafaam,
				
				Cards.NefersetThrasher,
                Cards.Crystallizer,
                Cards.SinisterDeal,
                Cards.EVILCableRat,
                Cards.SerpentEgg,
                Cards.DiseasedVulture,
                Cards.DarkPharaohTekahn,
            };
        }
    }
}