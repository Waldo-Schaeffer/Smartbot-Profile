using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_SecretHighlanderHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Secret_Highlander_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.DwarvenSharpshooter,
                Cards.Secretkeeper,
                Cards.Springpaw,
                Cards.ExplosiveTrap,
                Cards.FreezingTrap,
                Cards.PhaseStalker,
                Cards.RatTrap,
                Cards.SnakeTrap,
                Cards.Snipe,
                Cards.ZephrystheGreat,
                Cards.AnimalCompanion,
                Cards.DeadlyShot,
                Cards.DesertSpear,
                Cards.EaglehornBow,
                Cards.KillCommand,
                Cards.PrimordialExplorer,
                Cards.UnleashtheHounds,
                Cards.Ursatron,
                Cards.HyenaAlpha,
                Cards.MarkedShot,
                Cards.Zilliax,
                Cards.UnleashtheBeast,
                Cards.DinotamerBrann,
                Cards.Siamat,
                Cards.DragonqueenAlexstrasza,
                Cards.Zuljin,
                Cards.DragonmawPoacher,
                Cards.Sandbinder,
                Cards.Veranus,
            };
        }
    }
}