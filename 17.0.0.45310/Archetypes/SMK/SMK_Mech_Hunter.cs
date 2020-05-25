using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_MechHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Mech_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Tracking,
                Cards.FireworksTech,
                Cards.NineLives,
                Cards.SpiderBomb,
                Cards.Ursatron,
                Cards.Zilliax,
                Cards.Mecharoo,
                Cards.Springpaw,
                Cards.BombToss,
                Cards.Venomizer,
                Cards.AnimalCompanion,
                Cards.Explodinator,
                Cards.HoundmasterShaw,
                Cards.MarkedShot,
                Cards.ReplicatingMenace,
                Cards.Wargear,
                Cards.MechanicalWhelp,
                Cards.MissileLauncher,
                Cards.Oblivitron,
                Cards.UnleashtheBeast,
                Cards.BoommasterFlark,
                Cards.Zuljin,

            };
        }
    }
}