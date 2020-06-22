using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{
    public class SMK_GiantLock : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_GiantLock";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.SacrificialPact,
                Cards.KoboldLibrarian,
                Cards.MortalCoil,
                Cards.Darkbomb,
                Cards.Defile,
                Cards.Doomsayer,
                Cards.Hellfire,
                Cards.LesserAmethystSpellstone,
                Cards.Shadowflame,
                Cards.Voidcaller,
                Cards.Loatheb,
                Cards.NagaSeaWitch,
                Cards.MalGanis,
                Cards.Voidlord,
                Cards.BloodreaverGuldan,
                Cards.SeaGiant,
                Cards.ClockworkGiant,
                Cards.MountainGiant,
                Cards.MoltenGiant,
                Cards.Shadowflame,
                Cards.MistressofMixtures,
                Cards.PossessedLackey,
                Cards.SenseDemons,
                Cards.PrinceTaldaram
            };
        }
    }
}