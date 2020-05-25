using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_GalakrondZooWarlock : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Galakrond_Zoo_Warlock";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.BeamingSidekick,
                Cards.BlazingBattlemage,
                Cards.FlameImp,
                Cards.GrimRally,
                Cards.Voidwalker,
                Cards.EVILGenius,
                Cards.KnifeJuggler,
                Cards.ScarabEgg,
                Cards.DragonblightCultist,
                Cards.FiendishRites,
                Cards.VeiledWorshipper,
                Cards.ShieldofGalakrond,
                Cards.KronxDragonhoof,
                Cards.GalakrondtheWretched,
                Cards.SeaGiant,
                Cards.DevotedManiac,
                Cards.FacelessCorruptor,
            };
        }
    }
}