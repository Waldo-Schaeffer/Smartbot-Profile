using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_EvenPaladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Even_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.DrygulchJailor,
                Cards.Equality,
                Cards.KnifeJuggler,
                Cards.CalltoArms,
                Cards.LightfusedStegodon,
                Cards.TruesilverChampion,
                Cards.GennGreymane,
                Cards.SunkeeperTarim,
                Cards.SilverSword,
                Cards.DarkConviction,
                Cards.DireWolfAlpha,
                Cards.LootHoarder,
                Cards.PlatedBeetle,
                Cards.ViciousScalehide,
                Cards.BlessingofKings,
                Cards.Consecration,
                Cards.SaroniteChainGang,
                Cards.Spellbreaker,
                Cards.TheGlassKnight,
                Cards.CrystalLion,
                Cards.SpikeridgedSteed,
                Cards.TheLichKing,
                Cards.TirionFordring,
            };
        }
    }
}