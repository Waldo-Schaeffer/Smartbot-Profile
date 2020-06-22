using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_T7 : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_T7";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.BT_202,//地狱野猪
                Card.Cards.DRG_253,//矮人神射手
	            Card.Cards.DS1_184,//追踪术
                Card.Cards.BT_213,//拾荒者的智慧
                Card.Cards.YOD_005,//新鲜气息
                Card.Cards.DRG_252,//相位追猎者
	            Card.Cards.BT_203,//集群战术
                Card.Cards.BT_201,//强能箭猪
				Card.Cards.EX1_539,//杀戮命令
                Card.Cards.BT_210,//顶级捕食者兹克索尔
	            Card.Cards.DRG_071,//厄运信天翁
                Card.Cards.BT_205,//废铁射击
                Card.Cards.DRG_256,//灭龙弩炮
                Card.Cards.BT_737,//玛维·影歌
	            Card.Cards.BT_212,//莫克纳萨将狮
                Card.Cards.DS1_070,//驯兽师
            
            };
        }
    }
}