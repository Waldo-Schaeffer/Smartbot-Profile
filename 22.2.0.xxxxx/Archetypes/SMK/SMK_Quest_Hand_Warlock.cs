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
				Card.Cards.SCH_514,//亡者复生 Raise Dead     SCH_514
			//1费
				Card.Cards.SW_091,//恶魔之种 The Demon Seed     SW_091
				Card.Cards.SCH_312,//巡游向导 Tour Guide     SCH_312
				Card.Cards.CORE_EX1_302,//死亡缠绕 Mortal Coil     CORE_EX1_302
				Card.Cards.SW_090,//纳斯雷兹姆之触 Touch of the Nathrezim     SW_090
				Card.Cards.BAR_913,//火焰祭坛 Altar of Fire  BAR_913
				Card.Cards.EX1_319,//烈焰小鬼      EX1_319
				Card.Cards.SCH_311,//活化扫帚      SCH_311 
			//2费
				Card.Cards.WC_021,//不稳定的暗影震爆 Unstable Shadow Blast     WC_021
				Card.Cards.CORE_ICC_055,//吸取灵魂 Drain Soul     CORE_ICC_055
				Card.Cards.SCH_713,//异教低阶牧师 Cult Neophyte     SCH_713
				Card.Cards.CORE_EX1_012,//血法师萨尔诺斯 Bloodmage Thalnos     CORE_EX1_012
				Card.Cards.BT_733,//莫尔葛工匠 Mo'arg Artificer     BT_733
				Card.Cards.SCH_283,//食魔影豹 Manafeeder Panthara  SCH_283 
				Card.Cards.SW_084,//血缚小鬼 Bloodbound Imp      SW_084
				Card.Cards.BT_309,//坎雷萨德·埃伯洛克 Kanrethad Ebonlocke      BT_309 
				Card.Cards.BAR_745,//乱齿土狼 Hecklefang Hyena      BAR_745
			//3费
				Card.Cards.BAR_918,//塔姆辛·罗姆 Tamsin Roame     BAR_918
				Card.Cards.BAR_916,//血岩碎片刺背野猪人 Blood Shard Bristleback     BAR_916
				Card.Cards.YOP_033,//赛车回火 Backfire     YOP_033
				Card.Cards.SW_003,//符文秘银杖 Runed Mithril Rod  SW_003
				Card.Cards.SW_072,//锈烂蝰蛇 Rustrot Viper  SW_072
				Card.Cards.SW_079,//飞行管理员杜加尔 Flightmaster Dungar  SW_079 
				Card.Cards.BT_307,//黑眼 Darkglare      BT_307
				Card.Cards.DMF_111,//摇滚堕落者 Man'ari Mosher      DMF_111
			//4费
				Card.Cards.BAR_911,//灵魂撕裂 Soul Rend     BAR_911
				Card.Cards.CORE_CS2_062,//地狱烈焰 Hellfire     CORE_CS2_062
				Card.Cards.SW_056,//香料面包师 Spice Bread Baker     SW_056
				Card.Cards.SW_066,//王室图书管理员 Royal Librarian  SW_066 
				Card.Cards.BT_301,//夜影主母 Nightshade Matron BT_301 
				Card.Cards.DMF_066,//小刀商贩 Knife Vendor      DMF_066 
				Card.Cards.SW_066,//王室图书管理员 SW_066 
      //5费
        Card.Cards.CS3_024,//泰兰·弗丁 Taelan Fordring   CS3_024 
        Card.Cards.BAR_919,// 尼尔鲁·火刃 Neeru Fireblade  BAR_919 
			//6费
				Card.Cards.SW_092,//安纳塞隆 Anetheron     SW_092
				Card.Cards.SW_063,//战场军官 Battleground Battlemaster     SW_063
				Card.Cards.BAR_917,//贫瘠之地拾荒者 Barrens Scavenger     BAR_917
				Card.Cards.SW_089,//资深顾客 Entitled Customer     SW_089
				Card.Cards.BT_300,//古尔丹之手 Hand of Gul'dan  BT_300
      //9费  
				Card.Cards.CS3_031,//生命的缚誓者阿莱克丝塔萨  CS3_031 

			//10费
				Card.Cards.SCH_140,//血肉巨人 Flesh Giant     SCH_140
				Card.Cards.SW_062,//闪金镇豺狼人 Goldshire Gnoll     SW_062
			};
		}
	}
}