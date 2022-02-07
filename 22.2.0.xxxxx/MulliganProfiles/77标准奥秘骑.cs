using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 奥秘骑
// # 职业：圣骑士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 迅疾救兵 BAR_550
// # 2x (1) 正义保护者
// # 2x (1) 圣礼骑士
// # 2x (1) 古神在上 DMF_236
// # 2x (2) 阿达尔之手
// # 2x (2) 逝者之剑 BAR_875 
// # 1x (2) 莫戈尔·莫戈尔格
// # 2x (2) 定罪（等级1）
// # 2x (2) 劳累的驮骡
// # 2x (3) 联盟旗手
// # 2x (3) 双盾优等生
// # 2x (3) 北卫军指挥官 BAR_876
// # 1x (4) 凯瑞尔·罗姆
// # 1x (5) 伦萨克大王 CS3_025 
// # 2x (6) 纳鲁之锤
// # 2x (6) 战场军官
// # 1x (8) 瓦里安，暴风城国王
// #
// AAECAZ8FBPy4A5HsA9n5A6qKBA3KwQPK0QOD3gOF3gPM6wPO6wPP6wPj6wPb7gOI9APw9gPH+QPJoAQA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/MiDVg8iRPID1Z6LrQTY3ke/


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

            int flag1=0;//奥尔多侍从 BT_020 
            int flag2=0;//BAR_875逝者之剑 BAR_875 
            
            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.BT_020//奥尔多侍从 BT_020 
                ){flag1+=1;}
                if(card==Card.Cards.BAR_875//逝者之剑逝者之剑 BAR_875 
                ){flag2+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                 if((card==Card.Cards.BAR_873//圣礼骑士 BAR_873
                )){
                    
                        Keep(card,"圣礼骑士");
                    
                }

              
                if(card==Card.Cards.BT_019// 莫戈尔·莫戈尔格 BT_019 
                ){
               
                        Keep(card,"莫戈尔·莫戈尔格");
                    
                }
            
             
                if(card==Card.Cards.BAR_875// 逝者之剑 BAR_875 
                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_875))
                    {
                        Keep(card,"逝者之剑");
                    }
                }
                
                if(card==Card.Cards.SW_315// 联盟旗手 SW_315
                ){
                        Keep(card,"联盟旗手");
                    
                }
                if(card==Card.Cards.BAR_876// 北卫军指挥官 BAR_876 

                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_876)&&flag2>0)
                    {
                        Keep(card,"北卫军指挥官");
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