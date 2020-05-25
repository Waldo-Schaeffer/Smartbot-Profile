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
Cards.SludgeSlurper,
Cards.EVILTotem,
Cards.InvocationofFrost,
Cards.DesertHare,
Cards.DevotedManiac,
Cards.DreadCorsair,
Cards.DragonsPack,
Cards.ShieldofGalakrond,
Cards.CorruptElementalist,
Cards.KronxDragonhoof,
Cards.GalakrondtheTempest,
Cards.MoguFleshshaper,
Card.Cards.BT_102,
Card.Cards.BT_100,
Card.Cards.BT_106,

            };
        }
    }
}