using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

//
//NGA精英玩家俱乐部一键生成工具
//https://hs.fbigame.com/
//

//version 0004 for profile 2.0+

/* 
 * 
 * https://github.com/Waldo-Schaeffer
 * https://gitee.com/m586
 * Copyleft 2016 - 2021 SunGuanqi. All Rights Reserved
 * Attribution 4.0 International (Attribution 4.0 International (CC-BY-SA 4.0)
 * 使用时请遵守知识共享署名 4.0 国际许可协议，且不可删除本版权信息
 * 
 */
namespace SmartBotAPI.Plugins.API
{

	public class SMK_Broom_Paladin : Archetype
	{
		public string ArchetypeName()
		{
			return "SMK_Broom_Paladin";
		}

		public List<Card.Cards> ArchetypeCardSet()
		{
			return new List<Card.Cards>()
			{
			//0费
				Card.Cards.SCH_247,//新生入学 First Day of School ID：SCH_247
			//1费
				Card.Cards.BT_020,//奥尔多侍从 Aldor Attendant  ID：BT_020
				Card.Cards.SCH_311,//活化扫帚 Animated Broomstick ID：SCH_311
				Card.Cards.SCH_248,//甩笔侏儒 Pen Flinger ID：SCH_248
			//2费
				Card.Cards.EX1_096,//战利品贮藏者 Loot Hoarder ID：EX1_096
				Card.Cards.BT_025,//智慧圣契 Libram of Wisdom  ID：BT_025
				Card.Cards.BT_292,//阿达尔之手 Hand of A'dal  ID：BT_292
				Card.Cards.SCH_149,//银色自大狂 Argent Braggart  ID：SCH_149
				Card.Cards.YOD_010,//炮火机甲 Shotbot  ID：YOD_010
				Card.Cards.BT_019,//莫戈尔·莫戈尔格 Murgur Murgurgle  ID：BT_019
				Card.Cards.EX1_015,//工程师学徒 Novice Engineer ID：EX1_015
			//3费
				Card.Cards.SCH_526,//巴罗夫领主 Lord Barov ID：SCH_526
				Card.Cards.ULD_438,//萨赫特的傲狮 Salhet's Pride ID：ULD_438
			//4费
				Card.Cards.CS2_093,//奉献 Consecration ID：CS2_093
			//5费	
				Card.Cards.BT_026,//奥尔多真理追寻者 Aldor Truthseeker  ID：BT_026
				Card.Cards.BT_011,//正义圣契 Libram of Justice  ID：BT_011
			//6费
				Card.Cards.SCH_139,//虔诚的学徒 Devout Pupil ID：SCH_139
				Card.Cards.DMF_238,//纳鲁之锤 Hammer of the Naaru ID：DMF_238
			//7费
				Card.Cards.BT_334,//女伯爵莉亚德琳 Lady Liadrin  ID：BT_334
				Card.Cards.DMF_074,//希拉斯·暗月 Silas Darkmoon ID：DMF_074
			//9费
				Card.Cards.BT_024,//希望圣契 Libram of Hope  ID：BT_024
			};
		}
	}
}