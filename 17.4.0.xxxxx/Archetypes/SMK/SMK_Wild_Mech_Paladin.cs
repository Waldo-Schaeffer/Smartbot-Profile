using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_Mech_Paladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Mech_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Cards.Crystology,//水晶学
Cards.GlowTron,//格洛顿
Cards.Mecharoo,//机械袋鼠
Cards.Galvanizer,//通电机器人
Cards.MicroMummy,//微型木乃伊
Cards.GoboglideTech,//地精滑翔技师
Cards.SkyClaw,//空中飞爪
Cards.SN1PSN4P,//大铡蟹
Cards.AnnoyoModule,//吵吵模块
Cards.BlessingofKings,//王者祝福
Cards.Explodinator,//投弹机器人
Cards.ReplicatingMenace,//量产型恐吓机
Cards.BlessingofWisdom,//智慧祝福
Cards.Wargear,//战争机兵
Cards.Zilliax,//奇利亚斯
Cards.LeeroyJenkins,//火车王里诺艾
Cards.HotAirBalloon,//热气球
Cards.TruesilverChampion,//真银圣剑
Cards.BlessingofWisdom,//智慧祝福
Cards.FacelessCorruptor,//无面腐蚀者

        };
    }
}
}