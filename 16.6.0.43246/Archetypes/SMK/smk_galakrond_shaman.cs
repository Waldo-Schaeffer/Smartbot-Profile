using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_GalakrondShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Galakrond_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Mutate,
                Cards.InvocationofFrost,
                Cards.SludgeSlurper,
                Cards.FarSight,
                Cards.ManaTideTotem,
                Cards.DevotedManiac,
                Cards.CorruptElementalist,
                Cards.DragonsPack,
                Cards.FacelessCorruptor,
                Cards.ShieldofGalakrond,
                Cards.KronxDragonhoof,
                Cards.GalakrondtheTempest,
                Cards.MoguFleshshaper,
                Cards.Shudderwock,
                Cards.EVILTotem,
                Cards.NoviceEngineer,
                Cards.ElectraStormsurge,
                Cards.Lifedrinker,
                Cards.BaristaLynchen,
            };
        }
    }
}