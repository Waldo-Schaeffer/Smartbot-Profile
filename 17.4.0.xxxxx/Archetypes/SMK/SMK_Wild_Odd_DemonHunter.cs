using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildOddDemonHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Odd_DemonHunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.AT_082,//低阶侍从
                Card.Cards.BT_490,//吞噬魔法
                Card.Cards.UNG_205,//冰川裂片
                 Card.Cards.CS2_146,//南海船工
                  Card.Cards.UNG_809,//火羽精灵
                 Card.Cards.CFM_637,//海盗帕奇斯
                Card.Cards.BT_142,//影蹄杀手
                Card.Cards.BT_480,//火**印奔行者
                Card.Cards.BT_722,//防护改装师
                Card.Cards.BT_407,//乌祖尔恐魔
	            Card.Cards.BT_351,//战斗邪犬
                Card.Cards.ULD_191,//欢快的同伴
                Card.Cards.BT_753,//法力燃烧
                Card.Cards.DRG_403,//喷灯破坏者
                Card.Cards.ULD_209,//狐人恶棍
                Card.Cards.NEW1_027,//南海船长
                Card.Cards.BT_714,//冰霜织影者
                 Card.Cards.UNG_928,//焦油爬行者
	            Card.Cards.OG_034,//异种群居蝎 
                Card.Cards.BT_801,//眼棱
                 Card.Cards.BT_921,//奥达奇战刃
                  Card.Cards.ULD_209,//狐人恶棍
				Card.Cards.GIL_534,//荆棘帮暴徒
                Card.Cards.BT_352,//萨特监工
	            Card.Cards.BT_495,//刃缚精锐
                Card.Cards.BT_430,//埃辛诺斯战刃
                Card.Cards.BT_429,//恶魔变形
                Card.Cards.FP1_030,//洛欧塞布
                Card.Cards.BT_735,//奥
                Card.Cards.GIL_648,//总督察
	            Card.Cards.EX1_116,//火车王里诺艾
                Card.Cards.BT_493,//愤怒的女祭司
                Card.Cards.GIL_826,//噬月者巴库
                Card.Cards.BT_481,//奈瑟兰达姆斯
            
            };
        }
    }
}