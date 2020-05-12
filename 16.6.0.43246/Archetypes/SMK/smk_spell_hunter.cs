using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_SpellHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Spell_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Candleshot,
                Cards.HuntersMark,
                Cards.SecretPlan,
                Cards.ExplosiveTrap,
                Cards.FreezingTrap,
                Cards.WanderingMonster,
                Cards.AnimalCompanion,
                Cards.KillCommand,
                Cards.FlankingStrike,
                Cards.BaitedArrow,
                Cards.LesserEmeraldSpellstone,
                Cards.DeathstalkerRexxar,
                Cards.ToMySide,
                Cards.Rhokdelar,
                Cards.Zuljin,
                Cards.Tracking,
                Cards.DeadlyShot,
                Cards.EaglehornBow,
                Cards.UnleashtheHounds,
                Cards.CrushingWalls,
            };
        }
    }
}