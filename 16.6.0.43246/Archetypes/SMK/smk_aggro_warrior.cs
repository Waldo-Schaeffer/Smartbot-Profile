using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_AggroWarrior : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Aggro_Warrior";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                //Uldum
                Cards.InnerRage,
                Cards.EterniumRover,
                Cards.TownCrier,
                Cards.CruelTaskmaster,
                Cards.TempleBerserker,
                Cards.BloodswornMercenary,
                Cards.FrothingBerserker,
                Cards.LivewireLance,
                Cards.KorkronElite,
                Cards.RestlessMummy,
                Cards.LeeroyJenkins,
                Cards.BattleRage,
                Cards.FrightenedFlunky,
                Cards.Rampage,
                Cards.MilitiaCommander,
                Cards.ArcaniteReaper,
                Cards.Zilliax,
                Cards.GrommashHellscream,
            };
        }
    }
}