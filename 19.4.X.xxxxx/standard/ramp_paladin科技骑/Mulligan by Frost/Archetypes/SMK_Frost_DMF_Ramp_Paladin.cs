using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Frost_DMF_Ramp_Paladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Frost_DMF_Ramp_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.SphereofSapience,
				Cards.Crabrider,
				Cards.Wandmaker,
				Cards.MurlocWarleader,
				Cards.UnderlightAnglingRod,
				Cards.CircusAmalgam,
				Cards.FishyFlyer,
				Cards.NozdormutheTimeless,
				Cards.TiptheScales,
				Cards.MurgurMurgurgle,
				Cards.HighAbbessAlura,
				Cards.LordBarov,
				Cards.AnimatedBroomstick,
				//TransferStudent
				//Card.Cards.BT_128,
				//DMF
				Cards.RedscaleDragontamer,
				Cards.SilasDarkmoon,
				Cards.HammeroftheNaaru,
				Cards.CarnivalClown,
				Cards.DarkmoonRabbit,
				Cards.NZothGodoftheDeep,
				Cards.ScrapyardColossus,
				Cards.YShaarjtheDefiler,
            };
        }
    }
}