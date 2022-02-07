using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 进化萨
// # 职业：萨满祭司
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 雷霆绽放 SCH_427
// # 2x (1) 异变轮转 DMF_700
// # 1x (1) 活化扫帚 SCH_311
// # 2x (1) 集合石 WC_028
// # 1x (2) 伯尔纳·锤喙 SW_115
// # 2x (2) 前沿哨所 BAR_074
// # 2x (2) 笼斗管理员 DMF_704
// 异教低阶牧师 Cult Neophyte ID：SCH_713
// # 2x (2) 魔杖工匠 SCH_160
// # 2x (3) 原初地下城历险家 WC_005
// # 1x (3) 导师火心 SCH_507
// # 1x (3) 死斗场管理者 DMF_703
// # 1x (3) 锈烂蝰蛇 SW_072
// # 2x (4) 深水炸弹 DMF_701
// # 2x (4) 运河慢步者 SW_033
// # 2x (5) 沼泽拳刺 BT_102
// # 2x (6) 小巧玩具 SW_034
// # 1x (10) 亚煞极，污染之源 DMF_188
// # 2x (10) 闪金镇豺狼人 SW_062
// # 
// AAECAZnDAwaVzQOczgOp3gP83gPQ+QOG+gMM3bgD/tED8NQDpt4Dp94Dqt4D9+gD4+4DpO8DwfYD4/YDxvkDAA==
// # 
// # 想要使用这副套牌，请先复制到剪贴板，然后在游戏中点击“新套牌”进行粘贴。


namespace SmartBotAPI.Plugins.API
{

    public class SMK_7jinhuasa : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_7jinhuasa";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				        Card.Cards.SCH_427,
				        Card.Cards.DMF_700,
				        Card.Cards.SCH_311,
				        Card.Cards.WC_028,
				        Card.Cards.SW_115,
				        Card.Cards.BAR_074,
				        Card.Cards.DMF_704,
				        Card.Cards.SCH_160,
				        Card.Cards.WC_005,
				        Card.Cards.SCH_507,
				        Card.Cards.DMF_703,
				        Card.Cards.SW_072,
				        Card.Cards.DMF_701,
				        Card.Cards.SW_033,
				        Card.Cards.BT_102,
				        Card.Cards.SW_034,
				        Card.Cards.DMF_188,
				        Card.Cards.SW_062,
            };
        }
    }
}