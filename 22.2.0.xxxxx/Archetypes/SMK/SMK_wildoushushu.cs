using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

//
//NGA精英玩家俱乐部一键生成工具
//https://hs.fbigame.com/
//

//version 0001 for profile 0.9+
//德：DRUID 猎：HUNTER 法：MAGE 骑：PALADIN 牧：PRIEST 贼：ROGUE 萨：SHAMAN 术：WARLOCK 战：WARRIOR 瞎：DEMONHUNTER

/* 
 * 
 * https://github.com/Waldo-Schaeffer
 * https://gitee.com/m586
 * Copyleft 2016 - 2021 SunGuanqi. All Rights Reserved
 * Attribution 4.0 International (Attribution 4.0 International (CC-BY-SA 4.0)
 * 使用时请遵守知识共享署名 4.0 国际许可协议，且不可删除本版权信息
 * (2021/09/17)
 * 
 */
 
namespace SmartBotAPI.Plugins.API
{

	public class SMK_wildoushushu : Archetype
	{
		public string ArchetypeName()
		{
			return "SMK_wildoushushu";
		}

		public List<Card.Cards> ArchetypeCardSet()
		{
			return new List<Card.Cards>()
			{
			//0费
				Card.Cards.SCH_514,//亡者复生 Raise Dead     SCH_514
			//2费
				Card.Cards.CORE_ICC_055,//吸取灵魂 Drain Soul     CORE_ICC_055
				Card.Cards.SCH_713,//异教低阶牧师 Cult Neophyte     SCH_713
				Card.Cards.SW_306,//劳累的驮骡 Encumbered Pack Mule     SW_306
				Card.Cards.ICC_041,//亵渎 Defile     ICC_041 
				Card.Cards.CFM_790,//卑劣的脏鼠 Dirty Rat     CFM_790 
				Card.Cards.EX1_058,//日怒保卫者 Sunfury Protector     EX1_058
				Card.Cards.UNG_807,//葛拉卡爬行蟹 Golakka Crawler     UNG_807 
			//4费
				Card.Cards.CORE_EX1_005,//王牌猎人 Big Game Hunter     CORE_EX1_005
				Card.Cards.SW_056,//香料面包师 Spice Bread Baker     SW_056
				Card.Cards.SW_085,//暗巷契约 Dark Alley Pact     SW_085
				Card.Cards.BAR_911,//灵魂撕裂 Soul Rend BAR_911 
			//6费
				Card.Cards.SW_092,//安纳塞隆 Anetheron     SW_092
				Card.Cards.SW_063,//战场军官 Battleground Battlemaster     SW_063
				Card.Cards.GIL_692,//吉恩·格雷迈恩 Genn Greymane     GIL_692 
				Card.Cards.ICC_706,//蛛魔拆解者 Nerubian Unraveler     ICC_706
				Card.Cards.BAR_917,//贫瘠之地拾荒者 Barrens Scavenger     BAR_917
			//10费
				Card.Cards.EX1_105,//山岭巨人 Mountain Giant  EX1_105 
				Card.Cards.SW_062,//闪金镇豺狼人 Goldshire Gnoll     SW_062
				Card.Cards.EX1_620,//熔核巨人 Molten Giant     EX1_620
			};
		}
	}
}