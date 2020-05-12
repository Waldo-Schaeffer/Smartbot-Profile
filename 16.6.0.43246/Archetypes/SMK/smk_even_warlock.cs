using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{
    public class SMK_EvenWarlock : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Even_Warlock";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.AcidicSwampOoze,
                Cards.Defile,
                Cards.Doomsayer,
                Cards.PlatedBeetle,
                Cards.SunfuryProtector,
                Cards.VulgarHomunculus,
                Cards.Hellfire,
                Cards.HookedReaver,
                Cards.LesserAmethystSpellstone,
                Cards.ShroomBrewer,
                Cards.Spellbreaker,
                Cards.TwilightDrake,
                Cards.DreadInfernal,
                Cards.GennGreymane,
                Cards.TheLichKing,
                Cards.BloodreaverGuldan,
                Cards.MountainGiant,
            };
        }
    }
}