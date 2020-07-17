using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_dage_SHAMAN : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_dage_SHAMAN";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				Card.Cards.FP1_025,//转生
                Card.Cards.AT_053,//先祖知识 Ancestral Knowledge  ID：AT_053
                Card.Cards.CFM_696,//衰变
                Card.Cards.CS2_038,//先祖之魂 
                 Card.Cards.AT_048,//治疗波 Healing Wave  ID：AT_048
                  Card.Cards.KAR_073,//大漩涡传送门
                  Card.Cards.BT_100,//毒蛇神殿传送门
                 Card.Cards.CS2_053,//视界术
                Card.Cards.LOOT_373,//治疗之雨
                Card.Cards.BT_101,//鲜活孢子
                Card.Cards.GVG_029,//先祖召唤
                Card.Cards.YOD_042,//莱登之拳
	            Card.Cards.DAL_009,//哈加莎的阴谋
                Card.Cards.DAL_052,//泥沼变形怪 
                Card.Cards.BOT_099,//我找到了
                Card.Cards.DAL_047,//活动喷泉 
                Card.Cards.ICC_314,//巫妖王 The Lich King  ID：ICC_314
                Card.Cards.LOOT_064,//小型法术蓝宝石 Lesser Sapphire Spellstone  ID：LOOT_064
                Card.Cards.OG_042,//亚煞极
                Card.Cards.ULD_721,//月亮巨人守护者
                Card.Cards.BT_155,//废料场巨像
            };
        }
    }
}