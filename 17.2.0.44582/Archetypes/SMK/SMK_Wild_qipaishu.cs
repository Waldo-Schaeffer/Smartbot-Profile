using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_qipaishu : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_qipaishu";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.EX1_308,//灵魂之火 
                Card.Cards.KAR_089,//玛克扎尔的小鬼
                Card.Cards.BOT_568,//莫瑞甘的灵界 
                 Card.Cards.OG_109,//夜色镇图书管理员
                  Card.Cards.AT_021,//小鬼骑士
                  Card.Cards.BT_309,//坎雷萨德·埃伯洛克
                 Card.Cards.UNG_836,//萨瓦丝女王
                Card.Cards.BT_305,//被禁锢的拾荒小鬼
                Card.Cards.ULD_163,//过期货物专卖商
                Card.Cards.EX1_306,//魔犬
	            Card.Cards.KAR_205,//镀银魔像
                Card.Cards.AT_022,//加拉克苏斯之拳
                Card.Cards.BT_301,//夜影主母
                Card.Cards.LOOT_417,//大灾变
                Card.Cards.UNG_833,//拉卡利地狱犬
                Card.Cards.TRL_252,//高阶祭司耶克里克
                 Card.Cards.EX1_310,//末日守卫
	            Card.Cards.BT_300,//古尔丹之手
            };
        }
    }
}