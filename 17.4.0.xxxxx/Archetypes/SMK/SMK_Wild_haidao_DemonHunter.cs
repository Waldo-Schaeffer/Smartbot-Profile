using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_haidao_DemonHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_haidao_DemonHunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                    Card.Cards.BT_175,//双刃斩击
				Card.Cards.CS2_146,//南海船工
                Card.Cards.BT_490,//吞噬魔法
                Card.Cards.NEW1_025,//血帆海盗
                Card.Cards.BT_753,//法力燃烧
                Card.Cards.CFM_637,//海盗帕奇斯
                 Card.Cards.BT_491,//幽灵视觉
                  Card.Cards.BT_035,//混乱打击
                  Card.Cards.BT_922,//棕红之翼
                    Card.Cards.DRG_056,//空降歹徒
                        Card.Cards.GVG_075,//船载火炮
                        Card.Cards.BT_921,//奥达奇战刃
                         Card.Cards.TRL_507,//鲨鳍后援
                         Card.Cards.NEW1_022,//恐怖海盗
                           Card.Cards.NEW1_027,//南海船长 
                                  Card.Cards.GIL_681,//梦魇融合怪
				Card.Cards.GIL_534,//荆棘帮暴徒
                Card.Cards.BT_937,//流放者奥图里斯
                 Card.Cards.DRG_055,//藏宝匪贼
                Card.Cards.BT_352,//萨特监工
                Card.Cards.BT_187,//凯恩·日怒
	            Card.Cards.BT_495,//刃缚精锐
                Card.Cards.BT_430,//埃辛诺斯战刃
                Card.Cards.BT_429,//恶魔变形
                Card.Cards.BT_601,//古尔丹之颅
	            Card.Cards.EX1_116,//火车王里诺艾
            };
        }
    }
}