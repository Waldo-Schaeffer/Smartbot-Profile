using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_MurlocPaladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Murloc_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.MurlocTidecaller,
                Cards.RighteousProtector,
                Cards.Hydrologist,
                Cards.KnifeJuggler,
                Cards.RockpoolHunter,
                Cards.DivineFavor,
                Cards.MurlocWarleader,
                Cards.NightmareAmalgam,
                Cards.CalltoArms,
                Cards.GentleMegasaur,
                Cards.Spellbreaker,
                Cards.SunkeeperTarim,
                Cards.Vinecleaver,
                Cards.LostintheJungle,
                Cards.NobleSacrifice,
                Cards.Redemption,
                Cards.ColdlightSeer,
                Cards.UnidentifiedMaul,
                Cards.BellringerSentry,
                Cards.BlessingofKings,
                Cards.TruesilverChampion,
                Cards.Fungalmancer,
            };
        }
    }
}