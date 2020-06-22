using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_GalakrondWarrior : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Galakrond_Warrior";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.EterniumRover,
                Cards.TownCrier,
                Cards.RitualChopper,
                Cards.AcolyteofPain,
                Cards.Awaken,
                Cards.KorkronElite,
                Cards.TempleBerserker,
                Cards.DragonBreeder,
                Cards.ScionofRuin,
                Cards.DevotedManiac,
                Cards.LeeroyJenkins,
                Cards.ShieldofGalakrond,
                Cards.KronxDragonhoof,
                Cards.GalakrondtheUnbreakable,
                Cards.InnerRage,
                Cards.Whirlwind,
                Cards.Armorsmith,
                Cards.BattleRage,
                Cards.BloodswornMercenary,
                Cards.EVILQuartermaster,
                Cards.LivewireLance,
                Cards.PlagueofWrath,
                Cards.Zilliax,
            };
        }
    }
}