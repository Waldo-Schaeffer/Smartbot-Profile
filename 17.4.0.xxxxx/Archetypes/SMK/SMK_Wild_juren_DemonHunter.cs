using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_juren_DemonHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_juren_DemonHunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.AT_082,//低阶侍从
                Card.Cards.BT_407,//乌祖尔恐魔
                Card.Cards.BT_490,//吞噬魔法
                Card.Cards.BT_351,//战斗邪犬
                Card.Cards.BT_753,//法力燃烧
                Card.Cards.ICC_851,//凯雷塞斯王子
                Card.Cards.DRG_403,//喷灯破坏者
                Card.Cards.BT_801,//眼棱
				Card.Cards.GIL_534,//荆棘帮暴徒
                Card.Cards.BT_352,//萨特监工
                Card.Cards.BT_187,//凯恩·日怒
                 Card.Cards.OG_254,//奥秘吞噬者
                 Card.Cards.BT_416,//暴怒邪吼者
                  Card.Cards.BT_937,//流放者奥图里斯
	            Card.Cards.BT_495,//刃缚精锐
                Card.Cards.BT_430,//埃辛诺斯战刃
                Card.Cards.BT_429,//恶魔变形
                Card.Cards.BT_601,//古尔丹之颅
                Card.Cards.FP1_030,//洛欧塞布
	            Card.Cards.EX1_116,//火车王里诺艾
                   Card.Cards.BT_493,//愤怒的女祭司
                Card.Cards.BT_934,//被禁锢的安塔恩
                Card.Cards.AT_120,//冰霜巨人
            };
        }
    }
}