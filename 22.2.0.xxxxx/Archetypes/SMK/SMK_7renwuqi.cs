using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 任务骑
// # 职业：圣骑士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 迅疾救兵 Galloping Savior ID：BAR_550 
// # 1x (1) 甩笔侏儒 Pen Flinger ID：SCH_248 
// # 2x (1) 正义保护者 Righteous Protector ID：ICC_038 
// # 2x (1) 新生入学 First Day of School ID：SCH_247
// # 1x (1) 挺身而出 Rise to the Occasion ID：SW_313 
// # 2x (1) 巡游向导 Tour Guide ID：SCH_312 
// # 2x (1) 崇高牺牲 Noble Sacrifice ID：EX1_130 
// # 2x (1) 复仇 Avenge ID：FP1_020 
// # 2x (1) 圣礼骑士 Knight of Anointment ID：BAR_873 
// # 2x (1) 古神在上 Oh My Yogg! ID：DMF_236 
// # 2x (1) 受祝福的货物 Blessed Goods ID：SW_049 
// # 2x (2) 阿达尔之手 Hand of A'dal ID：BT_292 
// # 2x (2) 逝者之剑 Sword of the Fallen ID：BAR_875
// # 2x (2) 定罪（等级1） Conviction (Rank 1) ID：BAR_880 
// # 2x (3) 北卫军指挥官 Northwatch Commander ID：BAR_876 
// # 1x (5) 救赎者洛萨克森 Lothraxion the Redeemed ID：DMF_240 
// # 1x (6) 普瑞斯托女士 Lady Prestor ID：SW_078 
// #
// AAECAZ8FBJ/NA4feA7T2A9b5Aw3KwQObzQOezQOD3gPM6wPO6wPP6wPj6wPb7gOV+QPqnwTIoATJoAQA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/wHdB2hXpZhnANEKiwj1R0f/

namespace SmartBotAPI.Plugins.API
{

    public class SMK_77renwuqi : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_77renwuqi";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				        Card.Cards.BAR_550,
				        Card.Cards.SCH_248,
				        Card.Cards.ICC_038,
				        Card.Cards.SCH_247,
				        Card.Cards.SW_313,
				        Card.Cards.SCH_312,
				        Card.Cards.EX1_130,
				        Card.Cards.FP1_020,
				        Card.Cards.BAR_873,
				        Card.Cards.DMF_236,
				        Card.Cards.SW_049,
				        Card.Cards.BT_292,
				        Card.Cards.BAR_875,
				        Card.Cards.BAR_880,
				        Card.Cards.BAR_876,
				        Card.Cards.DMF_240,
				        Card.Cards.SW_078,
            };
        }
    }
}