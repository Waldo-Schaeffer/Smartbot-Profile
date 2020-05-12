using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class FaceHunterArchetype : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Face_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.ArcaneShot,
                Cards.DwarvenSharpshooter,
                Cards.RapidFire,
                Cards.TimberWolf,
                Cards.ToxicReinforcements,
                Cards.Tracking,
                Cards.ExplosiveTrap,
                Cards.FreezingTrap,
                Cards.PhaseStalker,
                Cards.AnimalCompanion,
                Cards.EaglehornBow,
                Cards.KillCommand,
                Cards.UnleashtheHounds,
                Cards.Lifedrinker,
                Cards.Misdirection,
                Cards.Spellzerker,
                Cards.LeeroyJenkins,
            };
        }
    }
}