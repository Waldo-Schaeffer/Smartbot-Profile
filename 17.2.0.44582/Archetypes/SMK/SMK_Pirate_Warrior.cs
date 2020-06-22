using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class PirateWarriorArchetype : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Pirate_Warrior";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.SkyRaider,
                Cards.SouthseaDeckhand,
                Cards.TownCrier,
                Cards.BloodsailRaider,
                Cards.ParachuteBrigand,
                Cards.Ancharrr,
                Cards.LivewireLance,
                Cards.Skybarge,
                Cards.DreadCorsair,
                Cards.HoardPillager,
                Cards.RestlessMummy,
                Cards.ArcaniteReaper,
                Cards.CaptainGreenskin,
                Cards.FacelessCorruptor,
                Cards.LeeroyJenkins,
                Cards.Upgrade,
                Cards.SharkfinFan,
                Cards.SouthseaCaptain,
                Cards.KorkronElite,
            };
        }
    }
}