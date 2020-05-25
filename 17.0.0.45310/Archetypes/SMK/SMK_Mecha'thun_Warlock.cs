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
            return "SMK_Mecha'thun_Warlock";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.KoboldLibrarian,
				Cards.MortalCoil,
				Cards.TheSoularium,
				Cards.Bloodbloom,
				Cards.BloodmageThalnos,
				Cards.Defile,
				Cards.Galvanizer,
				Cards.Cataclysm,
				Cards.LesserAmethystSpellstone,
				Cards.Zilliax,
				Cards.Mechathun,
				Cards.DarkPact,
				Cards.GlacialShard,
				Cards.WaxElemental,
				Cards.CheatyAnklebiter,
				Cards.LootHoarder,
				Cards.NoviceEngineer,
				Cards.PlatedBeetle,
				Cards.AcolyteofPain,
				Cards.TarCreeper,
				Cards.VoodooDoll,
				Cards.Corpsetaker,
				Cards.SkulloftheManari,
				Cards.LordGodfrey,
				Cards.TwistingNether,
				Cards.Voidlord,
				Cards.BloodreaverGuldan
            };
        }
    }
}