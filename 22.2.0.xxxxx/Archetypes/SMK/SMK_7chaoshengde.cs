using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
namespace SmartBotAPI.Plugins.API
{

    public class SMK_7chaoshengde : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_7chaoshengde";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				        Card.Cards.SW_422,//播种施肥  SW_422
                Card.Cards.SW_439 ,//活泼的松鼠  SW_439 
                Card.Cards.BT_722 ,//防护改装师 BT_722 
                Card.Cards.SCH_333,//自然研习  SCH_333 
                Card.Cards.SCH_617,//萌物来袭  SCH_617 
                Card.Cards.BAR_074,//前沿哨所  BAR_074 
                Card.Cards.SW_306,//劳累的驮骡 SW_306
                Card.Cards.SW_060,// 卖花女郎  SW_060 
                Card.Cards.BT_715,//噬骨殴斗者  BT_715
                Card.Cards.SW_437,//施肥  SW_437 
                Card.Cards.BAR_743,//狂野蟾蜍  BAR_743 
	              Card.Cards.BAR_537,//钢鬃卫兵  BAR_537 
                Card.Cards.SW_419,//艾露恩神谕者  SW_419 
                Card.Cards.SCH_142,//贪婪的书虫 SCH_142
                Card.Cards.SW_431,//花园猎豹   SW_431 
                Card.Cards.SCH_244,//教师的爱宠  SCH_244 
                Card.Cards.YOP_026,//树木生长 YOP_026
                Card.Cards.DMF_734,//格雷布  DMF_734
                Card.Cards.SW_080,//考内留斯·罗姆  SW_080 
                Card.Cards.CORE_GVG_085,//吵吵机器人  CORE_GVG_085 
                Card.Cards.SW_307,//旅行商人   SW_307 
                Card.Cards.BAR_075,//十字路口哨所 BAR_075
                Card.Cards.SW_319,//农夫  SW_319
                Card.Cards.SCH_231,//新生刺头   SCH_231
                Card.Cards.CORE_EX1_008,//银色侍从  CORE_EX1_008 
                Card.Cards.SCH_427,//雷霆绽放  SCH_427
                Card.Cards.SW_432,//科多兽坐骑  SW_432 
                Card.Cards.BAR_549,//尖壳印记 BAR_549 
            };
        }
    }
}