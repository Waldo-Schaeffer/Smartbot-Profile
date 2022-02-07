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
namespace SmartBotAPI.Plugins.API
{

	public class SMK_77wildhaidaozhan : Archetype
	{
		public string ArchetypeName()
		{
			return "SMK_77wildhaidaozhan";
		}
// ### Warrior
// # 职业：战士
// # 模式：狂野模式
// #
//     升级
//     南海船工
//     开进码头
//     恩佐斯的副官
//     海上威胁
//     海盗帕奇斯
//     空中悍匪
//     血帆桨手
//     血帆海盗
//     蹩脚海盗
//     异教低阶牧师
//      港口匪徒
//      空降歹徒
//      船载火炮
//      雾帆劫掠者
//      南海船长
//      海盗之锚
// # 
// AAEBAQcEkbwC3q0Di9UDmPYDDf8D1AXlB/sPgrACoL0C3a0D6bAD/ucDlfYDl/YDpooEr6AEAA==
// # 
// # 想要使用这副套牌，请先复制到剪贴板，然后在游戏中点击“新套牌”进行粘贴。

		public List<Card.Cards> ArchetypeCardSet()
		{
			return new List<Card.Cards>()
			{
			//0费
				
			//1费
				Card.Cards.EX1_409,//升级 EX1_409
				Card.Cards.CS2_146,//南海船工 CS2_146
				Card.Cards.SW_028,//开进码头 SW_028 
				Card.Cards.OG_312,//恩佐斯的副官 OG_312 
				Card.Cards.SW_027,//海上威胁 SW_027 
				Card.Cards.CFM_637,//海盗帕奇斯 CFM_637  
				Card.Cards.DRG_024,//空中悍匪 DRG_024  
				Card.Cards.CS3_008,//血帆桨手 CS3_008 
				Card.Cards.CFM_325,//蹩脚海盗 CFM_325 
				Card.Cards.NEW1_025,//血帆海盗  NEW1_025 
			//2费
				Card.Cards.SCH_713,//异教低阶牧师 SCH_713 
				Card.Cards.SW_029,//港口匪徒 SW_029 
				Card.Cards.DRG_056,//空降歹徒 DRG_056 
				Card.Cards.GVG_075,//船载火炮 GVG_075 
				Card.Cards.CS3_022,//雾帆劫掠者 CS3_022 
				Card.Cards.UNG_807,//葛拉卡爬行蟹 UNG_807 

			//3费
				Card.Cards.CORE_NEW1_027,//南海船长 CORE_NEW1_027
				Card.Cards.DRG_025,//海盗之锚 DRG_025 
				Card.Cards.DRG_023,//空中炮艇 Skybarge DRG_023 
				Card.Cards.SCH_142,//贪婪的书虫 Voracious Reader      SCH_142 
				Card.Cards.OG_315,//血帆教徒 Bloodsail Cultist      OG_315 
				Card.Cards.SW_093,//暴风城海盗 Stormwind Freebooter SW_093 
			//4费
        Card.Cards.DMF_521,//吞剑艺人 Sword Eater      DMF_521 
        Card.Cards.NEW1_022,//恐怖海盗 Dread Corsair      NEW1_022
      //5费
				Card.Cards.SW_028t5,//船长洛卡拉 SW_028t5 
				Card.Cards.FP1_030,//洛欧塞布 Loatheb      FP1_030 

			//6费
			
      //9费  

			//10费
			
			};
		}
	}
}