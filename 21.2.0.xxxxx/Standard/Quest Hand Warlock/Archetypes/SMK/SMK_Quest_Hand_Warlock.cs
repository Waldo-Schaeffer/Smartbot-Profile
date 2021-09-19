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

	public class SMK_Quest_Hand_Warlock : Archetype
	{
		public string ArchetypeName()
		{
			return "SMK_Quest_Hand_Warlock";
		}

		public List<Card.Cards> ArchetypeCardSet()
		{
			return new List<Card.Cards>()
			{
			//0费
				Card.Cards.SCH_514,//亡者复生 Raise Dead ID：SCH_514
			//1费
				Card.Cards.SW_091,//恶魔之种 The Demon Seed ID：SW_091
				Card.Cards.SCH_312,//巡游向导 Tour Guide ID：SCH_312
				Card.Cards.CORE_EX1_302,//死亡缠绕 Mortal Coil ID：CORE_EX1_302
				Card.Cards.SW_090,//纳斯雷兹姆之触 Touch of the Nathrezim ID：SW_090
			//2费
				Card.Cards.WC_021,//不稳定的暗影震爆 Unstable Shadow Blast ID：WC_021
				Card.Cards.CORE_ICC_055,//吸取灵魂 Drain Soul ID：CORE_ICC_055
				Card.Cards.SCH_713,//异教低阶牧师 Cult Neophyte ID：SCH_713
				Card.Cards.CORE_EX1_012,//血法师萨尔诺斯 Bloodmage Thalnos ID：CORE_EX1_012
				Card.Cards.BT_733,//莫尔葛工匠 Mo'arg Artificer ID：BT_733
			//3费
				Card.Cards.BAR_918,//塔姆辛·罗姆 Tamsin Roame ID：BAR_918
				Card.Cards.BAR_916,//血岩碎片刺背野猪人 Blood Shard Bristleback ID：BAR_916
				Card.Cards.YOP_033,//赛车回火 Backfire ID：YOP_033
			//4费
				Card.Cards.BAR_911,//灵魂撕裂 Soul Rend ID：BAR_911
				Card.Cards.CORE_CS2_062,//地狱烈焰 Hellfire ID：CORE_CS2_062
				Card.Cards.SW_056,//香料面包师 Spice Bread Baker ID：SW_056
			//6费
				Card.Cards.SW_092,//安纳塞隆 Anetheron ID：SW_092
				Card.Cards.SW_063,//战场军官 Battleground Battlemaster ID：SW_063
				Card.Cards.BAR_917,//贫瘠之地拾荒者 Barrens Scavenger ID：BAR_917
				Card.Cards.SW_089,//资深顾客 Entitled Customer ID：SW_089
			//10费
				Card.Cards.SCH_140,//血肉巨人 Flesh Giant ID：SCH_140
				Card.Cards.SW_062,//闪金镇豺狼人 Goldshire Gnoll ID：SW_062
			};
		}
	}
}