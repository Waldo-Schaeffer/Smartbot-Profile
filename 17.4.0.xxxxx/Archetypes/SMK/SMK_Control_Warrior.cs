using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_ControlWarrior : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Control_Warrior";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.OmegaAssembly, //Omega Assembly
                Cards.ShieldSlam,
                Cards.DeadMansHand,
                Cards.DrywhiskerArmorer,
                Cards.Execute,
                Cards.Warpath,
                Cards.WeaponsProject, //Weapons Project
                Cards.AcolyteofPain,
                Cards.BloodRazor,
                Cards.Brawl,
                Cards.HarrisonJones,
                Cards.DrBoomMadGenius, //Dr. Boom, Mad Genius
                Cards.CorneredSentry,
                Cards.Slam,
                Cards.ShieldBlock,
                Cards.GigglingInventor, //Giggling Inventor
                Cards.AzalinaSoulthief,
                Cards.GeosculptorYip,
                Cards.GrommashHellscream,
                Cards.ScourgelordGarrosh,
                Cards.TheLichKing,
                Cards.TheBoomship, //The Boomship

                //Odd one
                Cards.BakutheMooneater,
                Cards.GluttonousOoze,
                Cards.IronbeakOwl,
                Cards.RecklessFlurry,
                Cards.TinkmasterOverspark,
                Cards.ZolatheGorgon,
                Cards.BigGameHunter,
                Cards.CarnivorousCube,
                Cards.DirehornHatchling,
                Cards.Dynomatic,
                Cards.FacelessManipulator,
                Cards.Supercollider,
                Cards.Zilliax,
                Cards.AzalinaSoulthief,

                //Quest Warrior
                Cards.FirePlumesHeart,
                Cards.PhantomMilitia,
                Cards.StonehillDefender,
                Cards.TarCreeper,
                Cards.SaroniteChainGang,
            };
        }
    }
}