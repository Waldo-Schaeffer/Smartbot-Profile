using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 任务法
// # 职业：法师
// # 模式：标准模式
// # 狮鹫年
// #
// # 1x (1) 巫师的计策 SW_450
// 冰霜法术
// # 1x (0) 冰风暴（等级1）BAR_305
// # 1x (0) 冰风暴（等级1）BAR_305t
// # 1x (0) 冰风暴（等级1）BAR_305t2
// # 2x (1) 冰冷智慧 SCH_509
// # 2x (3) 寒冰护体 CORE_EX1_289

// 奥术
// # 2x (1) 始生研习 SCH_270 
// # 2x (2) 考前刷夜 SCH_353 
// # 2x (2) 符文宝珠 BAR_541 
// # 2x (3) 奥术智慧 CS2_023 
// # 2x (3) 咒术洪流 BT_002

// 火焰
// # 2x (1) 初始之火 SW_108
// # 2x (2) 点燃 SW_110 
// # 2x (4) 火球术 CORE_CS2_029
// # 2x (4) 火热促销 SW_107
// # 2x (4) 燃烧 SCH_348 

// # 2x (4) 套圈圈 DMF_105
// # 2x (5) 清凉的泉水 BAR_542
// # 2x (5) 埃匹希斯冲击 BT_291
// #
// AAECAf0EAtTqA+j3Aw7BuAOBvwPHzgPNzgP30QPr3gPQ7APR7AOn9wOu9wOy9wP8ngT9ngTonwQA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/yTC2CWh4SMPSXr3YtLbm0b/


namespace SmartBot.Mulligan
{
    [Serializable]
    public class DefaultMulliganProfile : MulliganProfile
    {
        List<Card.Cards> CardsToKeep = new List<Card.Cards>();

        private readonly List<Card.Cards> WorthySpells = new List<Card.Cards>
        {
            
        };

        public List<Card.Cards> HandleMulligan(List<Card.Cards> choices, Card.CClass opponentClass,
            Card.CClass ownClass)
        {
            bool HasCoin = choices.Count >= 4;

            int flag1=0;//棱彩珠宝工具 SW_048 
           
            
            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SW_048//棱彩珠宝工具 SW_048 
                ){flag1+=1;}
                if(card==Card.Cards.SW_315//联盟旗手 SW_315 
                ){flag1+=1;}
               
            }

            foreach (Card.Cards card in choices)
            {
               if(card==Card.Cards.SW_450// 巫师的计策 SW_450 

                ){ 
                        Keep(card,"巫师的计策");
                    
                }
                if(card==Card.Cards.SCH_270// 始生研习 SCH_270
                ){
                  if(!CardsToKeep.Contains(Card.Cards.SCH_270))
                    {
                        Keep(card,"始生研习");
                    } 
                }
                if(card==Card.Cards.SW_108// 初始之火 SW_108
                ){
                  if(!CardsToKeep.Contains(Card.Cards.SW_108))
                    {
                        Keep(card,"初始之火");
                    } 
                }
                if(card==Card.Cards.SCH_509// 冰冷智慧 SCH_509
                ){
                  if(!CardsToKeep.Contains(Card.Cards.SCH_509))
                    {
                        Keep(card,"冰冷智慧");
                    } 
                }
                if(card==Card.Cards.BAR_541// 符文宝珠 BAR_541
                ){
                  if(!CardsToKeep.Contains(Card.Cards.BAR_541))
                    {
                        Keep(card,"符文宝珠");
                    } 
                }
                if(card==Card.Cards.SW_110//点燃 SW_110
                ){
                  if(!CardsToKeep.Contains(Card.Cards.SW_110))
                    {
                        Keep(card,"符文宝珠");
                    } 
                }
                if(card==Card.Cards.CORE_EX1_289//寒冰护体 CORE_EX1_289
                ){
                  if(!CardsToKeep.Contains(Card.Cards.CORE_EX1_289))
                    {
                        Keep(card,"寒冰护体");
                    } 
                }
                if(card==Card.Cards.BT_002//咒术洪流 BT_002
                ){
                  if(!CardsToKeep.Contains(Card.Cards.BT_002))
                    {
                        Keep(card,"咒术洪流");
                    } 
                }

              
            }
            return CardsToKeep;
        }

        private void Keep(Card.Cards id, string log = "")
        {
            CardsToKeep.Add(id);
            if(log != "")
                Bot.Log(log);
        }

    }
}//德：DRUID 猎：HUNTER 法：MAGE 骑：PALADIN 牧：PRIEST 贼：ROGUE 萨：SHAMAN 术：WARLOCK 战：WARRIOR 瞎：DEMONHUNTER