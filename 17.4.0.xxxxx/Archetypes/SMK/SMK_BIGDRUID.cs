using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_BIGDRUID : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_BIGDRUID";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Card.Cards.FB_Champs_EX1_169,//激活
Card.Cards.DRG_051,//人多势众
Card.Cards.DAL_350,//水晶之力
Card.Cards.EX1_154,//愤怒
Card.Cards.DRG_318,//梦境吐息 
Card.Cards.BT_132,//铁木树皮
Card.Cards.BT_134,//沼泽射线
Card.Cards.BT_127,//被禁锢的萨特
Card.Cards.BT_130,//过度生长
Card.Cards.DRG_313,//翡翠龙探险者
Card.Cards.DAL_774,//特殊坐骑商人
Card.Cards.DRG_310,//辟法龙人
Card.Cards.YOD_003,//飞翼守护者
Card.Cards.DRG_213,//双头暴虐龙
Card.Cards.EX1_561,//阿莱克丝塔萨 
Card.Cards.BT_155,//废料场巨像

        };
    }
}
}