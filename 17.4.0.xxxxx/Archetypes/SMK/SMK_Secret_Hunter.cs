using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMKSecretHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Secret_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Candleshot,
                Cards.DireMole,
                Cards.Springpaw,
                Cards.ExplosiveTrap,
                Cards.FreezingTrap,
                Cards.SnakeTrap,
                Cards.WanderingMonster,
                Cards.AnimalCompanion,
                Cards.VenomstrikeTrap,
                Cards.FlankingStrike,
                Cards.EaglehornBow,
                Cards.DireFrenzy,
                Cards.KillCommand,
                Cards.LesserEmeraldSpellstone,
                Cards.DeathstalkerRexxar,
                Cards.MastersCall,
                Cards.TundraRhino,
                Cards.Snipe,
		Cards.CracklingRazormaw,
		Cards.ScavengingHyena,
		Cards.Subject9
            };
        }
    }
}