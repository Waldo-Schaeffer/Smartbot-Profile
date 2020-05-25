using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_AWEIDH : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_AWEIDH";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Card.Cards.BT_175,//双刃斩击
Card.Cards.BT_351,//战斗邪犬
Card.Cards.BT_480,//火**印奔行者
Card.Cards.BT_496,//暴怒的邪鳍
Card.Cards.BT_922,//棕红之翼
Card.Cards.BT_937,//流放者奥图里斯
Card.Cards.BT_801,//眼棱
Card.Cards.BT_352,//萨特监工
Card.Cards.BT_814,//伊利达雷邪刃武士
Card.Cards.BT_423,//灰舌将领
Card.Cards.BT_495,//刃缚精锐 
Card.Cards.BT_429,//恶魔变形
Card.Cards.BT_601,//古尔丹之颅
Card.Cards.BT_934,//被禁锢的安塔恩
Card.Cards.BT_493,//愤怒的女祭司
Card.Cards.BT_761,//盘牙督军
Card.Cards.BT_490,//吞噬魔法 
Card.Cards.ULD_191,//欢快的同伴
Card.Cards.BT_753,//法力燃烧
Card.Cards.BT_491,//幽灵视觉
Card.Cards.BT_035,//混乱打击
Card.Cards.EX1_044,//任务达人
Card.Cards.YOD_032,//狂暴邪翼蝠
Card.Cards.DRG_239,//灼光战斗法师



        };
    }
}
}