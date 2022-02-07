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
//  ### 暴风城：奇数走A猎
// # 职业：猎人
// # 模式：狂野模式
// #
// # 1x (1) 保卫矮人区 SW_322
// # 2x (1) 击伤猎物 BAR_801
// # 2x (1) 奥术射击 CORE_DS1_185
// # 1x (1) 奥秘图纸 BOT_402
// # 2x (1) 急速射击 DAL_373
// # 2x (1) 搜寻猎物 OG_061
// # 2x (1) 数量压制 SCH_604
// # 2x (1) 照明弹 EX1_544 
// # 2x (1) 猎人印记 CS2_084
// # 2x (1) 病毒增援 DRG_255
// # 2x (1) 追踪术 CORE_DS1_184
// # 1x (3) 剧毒魔蝎 BAR_065
// # 2x (3) 寒光智者 EX1_050 
// # 2x (3) 强风射击 AT_056
// # 2x (3) 杀戮命令 EX1_539
// # 2x (3) 瞄准射击 SW_321
// # 1x (9) 噬月者巴库 GIL_826 
// # 
// AAEBAairBASe+AKH+wKW6AP9+AMNjQGoAoAH+AfOFOmrAuyWA4WwA7nQA9vtA/f4A6mfBKqfBAA=
// # 
// # 想要使用这副套牌，请先复制到剪贴板，然后在游戏中点击“新套牌”进行粘贴。

namespace SmartBotAPI.Plugins.API
{

	public class SMK_wildjishulie : Archetype
	{
		public string ArchetypeName()
		{
			return "SMK_wildjishulie";
		}

		public List<Card.Cards> ArchetypeCardSet()
		{
			return new List<Card.Cards>()
			{
			//1费
				Card.Cards.SW_322,//保卫矮人区 SW_322
				Card.Cards.BAR_801,//击伤猎物 BAR_801
				Card.Cards.CORE_DS1_185,//奥术射击 CORE_DS1_185
				Card.Cards.BOT_402,//奥秘图纸 BOT_402
				Card.Cards.DAL_373,//急速射击 DAL_373
				Card.Cards.OG_061,//搜寻猎物 OG_061
				Card.Cards.SCH_604,//数量压制 SCH_604
				Card.Cards.EX1_544,//照明弹 EX1_544 
				Card.Cards.CS2_084,//猎人印记 CS2_084
				Card.Cards.DRG_255,//病毒增援 DRG_255
				Card.Cards.CORE_DS1_184,//追踪术 CORE_DS1_184
        // 3费
				Card.Cards.BAR_065,//剧毒魔蝎 BAR_065
				Card.Cards.EX1_050,//寒光智者 EX1_050 
				Card.Cards.AT_056,//强风射击 AT_056
				Card.Cards.EX1_539,//杀戮命令 EX1_539
				Card.Cards.SW_321,//瞄准射击 SW_321
        // 9费
				Card.Cards.GIL_826,//噬月者巴库 GIL_826
			};
		}
	}
}