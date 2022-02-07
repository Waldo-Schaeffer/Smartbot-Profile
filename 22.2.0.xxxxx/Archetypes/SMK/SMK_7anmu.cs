using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 快攻暗牧
// # 职业：牧师
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 亡者复生 Raise Dead ID：SCH_514 
// # 2x (1) 被禁锢的矮劣魔 Imprisoned Homunculus ID：BT_258 
// # 2x (1) 虚触侍从 Voidtouched Attendant ID：SW_446
// # 2x (1) 疲倦的大一新生 Frazzled Freshman ID：SCH_137 
// # 2x (1) 巡游向导 Tour Guide ID：SCH_312 
// # 2x (2) 食魔影豹 Manafeeder Panthara ID：SCH_283 
// # 2x (2) 蠕动的恐魔 Wriggling Horror ID：DMF_091 
// # 2x (2) 暮光欺诈者 Twilight Deceptor ID：SW_444 
// # 1x (2) 暗中生长 Thrive in the Shadows ID：CS3_028 
// # 2x (2) 异教低阶牧师 Cult Neophyte ID：SCH_713
// # 2x (3) 龙喉监工 Dragonmaw Overseer ID：BT_256 
// # 1x (3) 锈烂蝰蛇 Rustrot Viper ID：SW_072 
// # 1x (3) 曼科里克 Mankrik ID：BAR_721 
// # 2x (3) 暗影之灵 Shadowed Spirit ID：CS3_013 
// # 2x (4) 虚空碎片 Void Shard ID：SW_442 
// # 2x (4) 小刀商贩 Knife Vendor ID：DMF_066 
// # 1x (5) 黑暗主教本尼迪塔斯 Darkbishop Benedictus ID：SW_448
// #
// AAECAa0GBOfwA7v3A9D5A62KBA2wugPpugObzQPXzgP7zgO70QOL1QO04QPK4wOh6AOI9wOj9wOt9wMA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/7CRB3G82MqlPtD112CB04c/

namespace SmartBotAPI.Plugins.API
{

    public class SMK_77anmu : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_77anmu";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				        Card.Cards.BT_258,
				        Card.Cards.SCH_514,
				        Card.Cards.BT_258,
				        Card.Cards.SCH_137,
				        Card.Cards.SCH_312,
				        Card.Cards.SCH_283,
				        Card.Cards.DMF_091,
				        Card.Cards.SW_444,
				        Card.Cards.CS3_028,
				        Card.Cards.SCH_713,
				        Card.Cards.BT_256,
				        Card.Cards.SW_072,
				        Card.Cards.BAR_721,
				        Card.Cards.CS3_013,
				        Card.Cards.SW_442,
				        Card.Cards.DMF_066,
				        Card.Cards.SW_448,
            };
        }
    }
}