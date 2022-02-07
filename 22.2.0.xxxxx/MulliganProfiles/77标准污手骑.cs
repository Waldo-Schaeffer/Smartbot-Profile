using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 污手骑
// # 职业：圣骑士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 圣礼骑士
// # 2x (1) 棱彩珠宝工具 SW_048 
// # 2x (1) 正义保护者 CORE_ICC_038
// 银色侍从  CORE_EX1_008  
// # 2x (2) 劳累的驮骡
// # 1x (2) 吵吵机器人 BOT_270t 
// # 2x (2) 定罪（等级1）
// # 1x (2) 莫戈尔·莫戈尔格
// # 2x (2) 阿达尔之手
// # 2x (3) 双盾优等生
// # 2x (3) 古墓卫士
// # 2x (3) 联盟旗手
// 防护长袍  SCH_146
// # 2x (4) 乌瑞恩首席剑士
// # 1x (4) 凯瑞尔·罗姆
// # 1x (4) 剑圣萨穆罗
// # 1x (5) 伦萨克大王
// # 1x (5) 泰兰·弗丁
// # 1x (6) 大领主弗塔根 SW_047
// # 1x (6) 战场军官
// # 1x (6) 考内留斯·罗姆 SW_080
// # 1x (8) 瓦里安，暴风城国王
// # 
// AAECAaToAgr8uAP76AOR7AOi+APH+QPY+QPZ+QOoigSqigTWoAQKysEDytEDzOsD4+sDh/QDiPQD8PYD8/YDqvgDyaAEAA==
// # 
// # 想要使用这副套牌，请先复制到剪贴板，然后在游戏中点击“新套牌”进行粘贴。



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
               if(card==Card.Cards.SW_048// 棱彩珠宝工具 SW_048  

                ){ if(!CardsToKeep.Contains(Card.Cards.SW_048))
                    {
                        Keep(card,"棱彩珠宝工具");
                    }
                }
                if(card==Card.Cards.SW_315// 联盟旗手 SW_315
                ){
                  if(!CardsToKeep.Contains(Card.Cards.SW_315))
                    {
                        Keep(card,"联盟旗手");
                    } 
                }

              
                if(card==Card.Cards.BT_019// 莫戈尔·莫戈尔格 BT_019 
                ){
                  Keep(card,"莫戈尔·莫戈尔格");  
                }
            
             
                
                if(card==Card.Cards.BAR_873// 圣礼骑士 BAR_873
                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_873))
                    {
                        Keep(card,"圣礼骑士");
                    }
                }
                
               
            
               
               
                if(card==Card.Cards.BOT_270t// 吵吵机器人 BOT_270t 

                ){ if(!CardsToKeep.Contains(Card.Cards.BOT_270t))
                    {
                        Keep(card,"吵吵机器人");
                    }
                }
                if(card==Card.Cards.CORE_ICC_038//  正义保护者 CORE_ICC_038  

                ){if(!CardsToKeep.Contains(Card.Cards.CORE_ICC_038))
                    {
                        Keep(card,"正义保护者");
                    
                }}
                if(card==Card.Cards.CORE_EX1_008//银色侍从  CORE_EX1_008  

                ){ if(!CardsToKeep.Contains(Card.Cards.CORE_EX1_008))
                    {
                        Keep(card,"银色侍从");
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