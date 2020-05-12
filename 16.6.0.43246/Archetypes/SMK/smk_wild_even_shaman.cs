using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildEvenShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Even_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Crackle,
                Cards.Devolve,
                Cards.FlametongueTotem,
                Cards.JadeClaws,
                Cards.MaelstromPortal,
                Cards.MurksparkEel,
                Cards.TotemGolem,
                Cards.FlamewreathedFaceless,
                Cards.JadeLightning,
                Cards.ThingfromBelow,
                Cards.DireWolfAlpha,
                Cards.KnifeJuggler,
                Cards.PilotedShredder,
                Cards.Spellbreaker,
                Cards.AyaBlackpaw,
                Cards.GennGreymane,
                Cards.SeaGiant,

                Cards.BloodmageThalnos,
                Cards.ArgentCommander,
                Cards.WhirlingZapomatic,
                Cards.AncestralKnowledge,
                Cards.HagathatheWitch,
                Cards.HauntedCreeper,
                Cards.SaroniteChainGang,
                Cards.FireElemental,
                Cards.AlAkirtheWindlord,
                Cards.FireguardDestroyer
            };
        }
    }
}