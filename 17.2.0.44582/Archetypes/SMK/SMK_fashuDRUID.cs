using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_fashuDRUID : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_fashuDRUID";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Card.Cards.CS2_008,//月火术
Card.Cards.FB_Champs_EX1_169,//激活 
Card.Cards.DAL_350,//水晶之力
Card.Cards.EX1_154,//愤怒
Card.Cards.BT_128,//真菌宝藏 
Card.Cards.EX1_160,//野性之力
Card.Cards.BT_132,//铁木树皮
Card.Cards.BT_134,//沼泽射线
Card.Cards.CS2_011,//野蛮咆哮
Card.Cards.EX1_158,//丛林之魂
Card.Cards.BT_130,//过度生长
Card.Cards.BT_135,//萤火成群
Card.Cards.BT_255,//凯尔萨斯·逐日者
Card.Cards.ULD_273,//溢流
Card.Cards.DAL_774,//特殊坐骑商人
Card.Cards.DAL_256,//森林的援助
Card.Cards.EX1_183,//野性赐福



        };
    }
}
}