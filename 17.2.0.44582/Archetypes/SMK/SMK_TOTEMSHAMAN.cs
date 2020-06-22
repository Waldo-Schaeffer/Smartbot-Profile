using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_TOTEMSHAMAN : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_TOTEMSHAMAN";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Card.Cards.EX1_244,//图腾之力1
Card.Cards.ULD_171,//图腾潮涌2
Card.Cards.ULD_191,//欢快的同伴
Card.Cards.DAL_433,//淤泥吞食者3
Card.Cards.DRG_239,//灼光战斗法师
Card.Cards.DRG_216,//电涌风暴4
Card.Cards.BT_722,//防护改装师 
Card.Cards.DRG_215,//风暴之怒 
Card.Cards.ULD_276,//怪盗图腾
Card.Cards.BT_113,//图腾映像
Card.Cards.EX1_575,//法力之潮图腾
Card.Cards.EX1_565,//火舌图腾
Card.Cards.DAL_773,//魔法飞毯
Card.Cards.ULD_413,//分裂战斧 
Card.Cards.CS2_046,//嗜血
Card.Cards.EX1_238,//闪电箭
Card.Cards.ULD_158,//沙暴元素
Card.Cards.EX1_258,//无羁元素
Card.Cards.BT_100,//毒蛇神殿传送门
Card.Cards.BT_115,//沼泽之子
Card.Cards.EX1_241,//熔岩爆裂
Card.Cards.ULD_173,//维西纳
Card.Cards.ULD_177,//八爪巨怪




        };
    }
}
}