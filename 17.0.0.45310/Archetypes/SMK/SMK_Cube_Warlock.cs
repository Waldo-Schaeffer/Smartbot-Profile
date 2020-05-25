using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{
    public class SMK_Cube_Warlock : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Cube_Warlock";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.DarkPact,
                Cards.KoboldLibrarian,
                Cards.Defile,
                Cards.Hellfire,
                Cards.LesserAmethystSpellstone,
                Cards.CarnivorousCube,
                Cards.Doomguard,
                Cards.PossessedLackey,
                Cards.SkulloftheManari,
                Cards.Voidlord,
                Cards.BloodreaverGuldan,
                Cards.MountainGiant,
                Cards.MortalCoil,
                Cards.Doomsayer,
                Cards.PrinceTaldaram,
                Cards.TarCreeper,
                Cards.VoodooDoll,
                Cards.SpiritsingerUmbra,
                Cards.FacelessManipulator,
                Cards.LordGodfrey,

                Cards.SenseDemons,
                Cards.Voidcaller,
                Cards.TwistingNether,
                Cards.MalGanis,
                Cards.DespicableDreadlord,
                Cards.MistressofMixtures,
                Cards.DollmasterDorian,
                Cards.NZoththeCorruptor
            };
        }
    }
}