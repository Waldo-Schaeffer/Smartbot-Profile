using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_erwangzi_ROGUE : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_erwangzi_ROGUE ";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.EX1_144,//暗影步
                Card.Cards.CS2_146,//南海船工 
                Card.Cards.WE1_029,//吹嘘海盗 
                Card.Cards.CFM_637,//海盗帕奇斯
                Card.Cards.DRG_030,//赞美迦拉克隆
                Card.Cards.BT_701,//间谍女郎
                Card.Cards.ICC_851,//凯雷塞斯王子
                Card.Cards.EX1_134,//军情七处特工
	            Card.Cards.DRG_247,//封印命运
                Card.Cards.DAL_415,//怪盗恶霸 
                Card.Cards.LOOT_211,//精灵咏唱者
                Card.Cards.DRG_050,//虔信狂徒 
                Card.Cards.FP1_030,//洛欧塞布
                Card.Cards.EX1_116,//火车王里诺艾
                Card.Cards.DRG_242,//迦拉克隆之盾 
                 Card.Cards.UNG_064,//邪脊吞噬者
	            Card.Cards.BRM_008,//黑铁潜藏者
                Card.Cards.DRG_099,//克罗斯·龙蹄
                 Card.Cards.DRG_610,//梦魇巨龙迦拉克隆
            
            };
        }
    }
}