using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 任务贼
// # 职业：潜行者
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 暗影步
// # 2x (1) 军情七处的要挟 SW_412
// # 1x (1) 探查内鬼 SW_052
// # 2x (1) 秘密通道
// # 2x (1) 间谍女郎 BT_701
// # 1x (2) “赤烟”腾武
// # 2x (2) 军情七处潜伏者 SW_418 
// # 2x (2) 狐人老千
// # 2x (2) 行骗
// # 2x (2) 邪恶挥刺（等级1）
// # 2x (3) 军情七处探员 SW_413 
// # 2x (3) 军情七处特工 EX1_134 
// # 2x (3) 暗心贤者
// # 2x (4) 军情七处线人 SW_411 
// # 1x (4) 斯卡布斯·刀油
// # 1x (6) 战场军官
// # 2x (7) 军情七处刺客
// # 
// AAECAZurBATD4QOd8AOm+QPH+QMNubgDz7kDqssD390D590DqusDn/QDofQDovQDo/UDpvUD9Z8E9p8EAA==
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

            int flag1=0;//奥尔多侍从 BT_020 
            
            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.BT_020//奥尔多侍从 BT_020 
                ){flag1+=1;}
              
            }

            foreach (Card.Cards card in choices)
            {
                 if((card==Card.Cards.SW_052//探查内鬼 SW_052
                )){
                    
                        Keep(card,"探查内鬼");
                    
                }

                if(card==Card.Cards.BT_701// 间谍女郎 BT_701
                ){
               
                        Keep(card,"间谍女郎");
                    
                }

                if(card==Card.Cards.SW_418// 军情七处潜伏者 SW_418 
                ){
               
                        Keep(card,"军情七处潜伏者");
                    
                }
                if(card==Card.Cards.SW_413// 军情七处探员 SW_413 
                ){
               
                        Keep(card,"军情七处探员");
                    
                }
                if(card==Card.Cards.SW_413// 军情七处探员 SW_413
                ){ if(!CardsToKeep.Contains(Card.Cards.SW_413))
                    {
                        Keep(card,"军情七处探员");
                    }
                }
                if(card==Card.Cards.EX1_134// 军情七处特工 EX1_134 
                ){ if(!CardsToKeep.Contains(Card.Cards.EX1_134))
                    {
                        Keep(card,"军情七处特工");
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