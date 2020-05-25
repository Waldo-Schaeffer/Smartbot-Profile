using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_RESURRECTPRIEST : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_RESURRECT PRIEST";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Card.Cards.BT_252,//复苏
Card.Cards.ULD_714,//苦修
Card.Cards.DRG_071,//厄运信天翁
Card.Cards.DRG_307,//永恒吐息
Card.Cards.DRG_302,//墓地符文 
Card.Cards.DAL_558,//大法师瓦格斯 
Card.Cards.ULD_268,//接引冥神
Card.Cards.ULD_275,//白骨怨灵
Card.Cards.CS1_112,//神圣新星
Card.Cards.DAL_039,//无面渗透者
Card.Cards.ULD_208,//卡塔图防御者
Card.Cards.BT_198,//灵魂之镜
Card.Cards.DRG_660,//讳言巨龙迦拉克隆
Card.Cards.BT_341,//骸骨巨龙
Card.Cards.DAL_721,//亡者卡特琳娜
Card.Cards.ULD_718,//死亡之灾祸
Card.Cards.DAL_724,//群体复活


        };
    }
}
}