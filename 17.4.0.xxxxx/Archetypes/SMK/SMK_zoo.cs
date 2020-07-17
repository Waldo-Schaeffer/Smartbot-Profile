using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_zoo : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_zoo";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.EX1_405,//持盾卫士
                Card.Cards.ULD_191,//欢快的同伴
	            Card.Cards.EX1_308,//灵魂之火
                Card.Cards.DRG_239,//灼光战斗法师
                Card.Cards.EX1_319,//烈焰小鬼
                Card.Cards.CS2_171,//石牙野猪
	            Card.Cards.CS2_065,//虚空行者
                Card.Cards.EX1_008,//银色侍从
				Card.Cards.BT_722,//防护改装师
                Card.Cards.BT_305,//被禁锢的拾荒小鬼
	            Card.Cards.ULD_163,//过期货物专卖商
                Card.Cards.DAL_773,//魔法飞毯
                Card.Cards.BT_301,//夜影主母
                Card.Cards.DRG_076,//无面腐蚀者
	            Card.Cards.BT_300,//古尔丹之手
            };
        }
    }
}