using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_EggPaladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_EggPaladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.BlessingofMight,
                Cards.LostintheJungle,
                Cards.RighteousProtector,
                Cards.Equality,
                Cards.HauntedCreeper,
                Cards.KnifeJuggler,
                Cards.NerubianEgg,
                Cards.ShieldedMinibot,
                Cards.DivineFavor,
                Cards.MusterforBattle,
                Cards.RallyingBlade,
                Cards.CalltoArms,
                Cards.Consecration,
                Cards.KeeperofUldaman,
                Cards.Spellbreaker,
                Cards.Fungalmancer,
                Cards.SunkeeperTarim,
                Cards.SirFinleyMrrgglton,
                Cards.Vinecleaver,

                //2019
                Cards.ArgentSquire,
                Cards.ScarabEgg,
                Cards.DireWolfAlpha,
                Cards.DevilsaurEgg,
                Cards.UnidentifiedMaul,
                Cards.SeaGiant,
                Cards.RighteousProtector,
                Cards.BlessingofKings
            };
        }
    }
}