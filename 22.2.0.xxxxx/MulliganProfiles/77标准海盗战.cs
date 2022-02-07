using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ### 任务战
// # 职业：战士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 血帆桨手 CS3_008 
// # 2x (1) 海上威胁 SW_027 
// # 1x (1) 开进码头 SW_028
// # 2x (1) 体能研习 SCH_237
// # 2x (2) 雾帆劫掠者 CS3_022
// # 2x (2) 血帆袭击者 NEW1_018
// # 2x (2) 港口匪徒 SW_029
// # 1x (2) 氮素药膏 YOP_015 
// # 2x (3) 货物保镖 SW_030 
// # 1x (3) 胁迫 SCH_521
// # 2x (3) 暴风城海盗 SW_093
// # 2x (3) 南海船长 CORE_NEW1_027
// # 2x (4) 收割之镰 SCH_238
// # 2x (4) 吞剑艺人 DMF_521 
// # 2x (5) 石槌掌锚手 BAR_896 
// # 2x (5) 劈砍课程 SCH_623 
// # 1x (8) 问题学生 SCH_337
// 厚重板甲  SW_094 
// 萨鲁法尔大王 BAR_334 
// 雷区挑战  DMF_522 
// 克雷什，群龟之王  WC_026
// #
// AAECAQcEp84DitADgeQDmPYDDeLMA+PMA/fUA7XeA/7nA9XxA5X2A5b2A5f2A8/7A6aKBK2gBK+gBAA=
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/j2cRZnUAKRPxKW1jMcGDOe/


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

            int flag1=0;//港口匪徒 SW_029
            int flag2=0;//血帆桨手 CS3_008
            
            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SW_029//港口匪徒 SW_029
                ){flag1+=1;}
                if(card==Card.Cards.CS3_008//血帆桨手 CS3_008
                ){flag2+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                 if((card==Card.Cards.SW_028//开进码头 SW_028
                )){
                    
                        Keep(card,"开进码头");
                    
                }

                if(card==Card.Cards.SW_029// 港口匪徒 SW_029
                ){
               
                        Keep(card,"港口匪徒");
                    
                }

                if(card==Card.Cards.CS3_008// 血帆桨手 CS3_008
                ){
               
                        Keep(card,"血帆桨手");
                    
                }
                if(card==Card.Cards.NEW1_018// 血帆袭击者 NEW1_018
                ){ if(!CardsToKeep.Contains(Card.Cards.NEW1_018))
                    {
                        Keep(card,"血帆袭击者");
                    }
                }

              if(card==Card.Cards.SW_030&&flag1+flag2>=1)//货物保镖 SW_030 
                {
                     if(!CardsToKeep.Contains(Card.Cards.SW_030))
                    {
                        Keep(card,"货物保镖");
                    }
                }
              if(card==Card.Cards.SW_093&&flag1+flag2>=1)//暴风城海盗 SW_093
                {
                     if(!CardsToKeep.Contains(Card.Cards.SW_093))
                    {
                        Keep(card,"暴风城海盗");
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