using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildEvenShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Even_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
               Card.Cards.EX1_244,//图腾之力
               Card.Cards.ULD_171,//图腾潮涌
               Card.Cards.ICC_056,//低温静滞
               Card.Cards.AT_052,//图腾魔像 
               Card.Cards.KAR_073,//大漩涡传送门
               Card.Cards.ULD_276,//怪盗图腾 
               Card.Cards.CFM_696,//衰变 
               Card.Cards.GVG_038,//连环爆裂
               Card.Cards.GIL_530,//阴燃电鳗
               Card.Cards.ULD_413,//分裂战斧
               Card.Cards.AT_047,//德莱尼图腾师 
               Card.Cards.BT_737,//玛维·影歌
               Card.Cards.GIL_692,//吉恩·格雷迈恩
               Card.Cards.OG_028,//深渊魔物
               Card.Cards.BOT_245,//风暴聚合器
                Card.Cards.BOT_245,//风暴聚合器
                 Card.Cards.BT_230,//鱼斯拉 
                  Card.Cards.ULD_177,//八爪巨怪
                   Card.Cards.EX1_586,//海巨人 
            };
        }
    }
}