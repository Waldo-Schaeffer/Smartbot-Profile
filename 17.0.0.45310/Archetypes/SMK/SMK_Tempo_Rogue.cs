using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_TempoRogue : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Tempo_Rogue";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Backstab,
                Cards.Shadowstep,
                Cards.ColdBlood,
                Cards.FireFly,
                Cards.BlinkFox,
                Cards.EdwinVanCleef,
                Cards.HenchClanThug,
                Cards.ElvenMinstrel,
                Cards.SaroniteChainGang,
                Cards.LeeroyJenkins,
                Cards.VilespineSlayer,
                Cards.Hallucination,
                Cards.SouthseaDeckhand,
                Cards.PrinceKeleseth,
                Cards.FaceCollector,
                Cards.TarCreeper,
                Cards.Spellbreaker,
                Cards.CobaltScalebane,
                Cards.TessGreymane,

                Cards.TheLichKing,
                Cards.GlacialShard,
                Cards.SonyaShadowdancer,
                Cards.SI7Agent,
                
                //Miracle
                Cards.Preparation,
                Cards.BloodmageThalnos,
                Cards.Eviscerate,
                Cards.Sap,
                Cards.FanofKnives,
                Cards.Sprint,

				Cards.Backstab,
				
				Cards.Sap,
				Cards.FanofKnives,
				Cards.FaldoreiStrider,
				Cards.SpiritoftheShark,
				Cards.Fungalmancer,
				Cards.GraltheShark,
				Cards.Zilliax,
				Cards.MojomasterZihi,

                //Wild
                Cards.DrBoom,
                Cards.Loatheb,
                Cards.DarkIronSkulker,
                Cards.Buccaneer
            };
        }
    }
}