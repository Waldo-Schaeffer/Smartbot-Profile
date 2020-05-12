using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_OddPaladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Odd_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.AcherusVeteran,
                Cards.ArgentSquire,
                Cards.LostintheJungle,
                Cards.RighteousProtector,
                Cards.DivineFavor,
                Cards.UnidentifiedMaul,
                Cards.Fungalmancer,
                Cards.LevelUp,
                Cards.Vinecleaver,
                Cards.BakutheMooneater,
                Cards.BlessingofMight,
                Cards.DireMole,
                Cards.FireFly,
                Cards.GlacialShard,
                Cards.LightsJustice,
                Cards.IronbeakOwl,
                Cards.RaidLeader,
                Cards.StonehillDefender,
                Cards.CorridorCreeper,
                Cards.StormwindChampion,
                Cards.NightmareAmalgam,

                //The Boomsday Project
                Cards.KangorsEndlessArmy, 
                Cards.GlowTron,
                Cards.Zilliax,
                Cards.Wargear,
                Cards.GigglingInventor,
                Cards.BronzeGatekeeper,
                Cards.MechanoEgg, 

                //Wild update (16.08.19)
                Cards.CompetitiveSpirit,
                Cards.NeverSurrender,
                Cards.PatchesthePirate,
                Cards.SouthseaDeckhand,
                Cards.MusterforBattle,
                Cards.RallyingBlade,
                Cards.StewardofDarkshire,
                Cards.WarhorseTrainer,
                Cards.Loatheb,
                Cards.Quartermaster,
            };
        }
    }
}