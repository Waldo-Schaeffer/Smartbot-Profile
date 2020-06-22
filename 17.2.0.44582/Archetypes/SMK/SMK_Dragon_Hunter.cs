using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_DragonHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Dragon_Hunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.BlazingBattlemage,
                Cards.DwarvenSharpshooter,
                Cards.Tracking,
                Cards.CorrosiveBreath,
                Cards.FaerieDragon,
                Cards.PhaseStalker,
                Cards.SnakeTrap,
                Cards.PrimordialExplorer,
                Cards.Scalerider,
                Cards.Stormhammer,
                Cards.Dragonbane,
                Cards.EvasiveFeywing,
               // Cards.FrenziedFelwing,
                Cards.Lifedrinker,
                Cards.LeeroyJenkins,
                //Cards.RotnestDrake,
                

            };
        }
    }
}