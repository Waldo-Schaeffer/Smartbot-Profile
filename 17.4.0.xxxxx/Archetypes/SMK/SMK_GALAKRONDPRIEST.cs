using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_GALAKRONDPRIEST : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_GALAKRONDPRIEST";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Card.Cards.BT_252,//复苏
                Card.Cards.BT_258,//被禁锢的矮劣魔
                Card.Cards.DRG_303,//迦拉克隆的信徒
                Card.Cards.EX1_339,//思维窃取
                Card.Cards.EX1_622,//暗言术：灭 
                Card.Cards.NEW1_020,//狂野炎术师
                Card.Cards.ULD_714,//苦修 
                Card.Cards.BT_733,//莫尔葛工匠
                Card.Cards.EX1_066,//酸性沼泽软泥怪
                Card.Cards.DAL_729,//拉祖尔女士
                Card.Cards.DRG_307,//永恒吐息
                Card.Cards.BT_257,//神圣化身
                Card.Cards.DRG_300,//命运编织者
                Card.Cards.CS1_112,//神圣新星
                Card.Cards.DRG_246,//时空裂痕 
                Card.Cards.DRG_242,//迦拉克隆之盾
                Card.Cards.DRG_099,//克罗斯·龙蹄
                Card.Cards.BT_198,//灵魂之镜
                Card.Cards.DRG_660,//讳言巨龙迦拉克隆
                Card.Cards.ULD_718,//死亡之灾祸
            };
        }
    }
}