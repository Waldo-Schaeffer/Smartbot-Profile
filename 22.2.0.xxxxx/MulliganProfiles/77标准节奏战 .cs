using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ### 暴风城：节奏战
// # 职业：战士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 体能研习
// # 1x (1) 战歌大使 BAR_843 
// # 2x (1) 被禁锢的甘尔葛 BT_121
// # 2x (1) 防护改装师
// # 2x (2) 严酷的监工
// # 2x (2) 前沿哨所 BAR_074 
// # 2x (2) 港口匪徒 SW_029
// # 2x (3) 噬骨骑兵
// # 2x (3) 太阳之井新兵 BAR_025
// # 2x (3) 暴风城海盗
// # 1x (3) 洛卡拉
// # 2x (3) 防护长袍 SCH_146
// # 2x (4) 十字路口哨所
// # 2x (4) 吞剑艺人
// # 2x (5) 石槌掌锚手 BAR_896
// # 1x (7) 萨鲁法尔大王
// # 1x (8) 格罗玛什·地狱咆哮
// # 
// AAECAePjAwSN7QOV7QOq7gOLoAQNvLkDnrsD3r4D4swD0dEDtd4D9+gD+OgD8ukD1fEDlfYDz/sDjKAEAA==
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
                 if((card==Card.Cards.BAR_843//战歌大使 BAR_843
                )){
                    
                        Keep(card,"战歌大使");
                    
                }

                if(card==Card.Cards.SW_029// 港口匪徒 SW_029
                ){
               
                        Keep(card,"港口匪徒");
                    
                }

                if(card==Card.Cards.CS3_008// 血帆桨手 CS3_008
                ){
               
                        Keep(card,"血帆桨手");
                    
                }
                if(card==Card.Cards.BT_121// 被禁锢的甘尔葛 BT_121
                ){ if(!CardsToKeep.Contains(Card.Cards.BT_121))
                    {
                        Keep(card,"被禁锢的甘尔葛");
                    }
                }
                if(card==Card.Cards.BAR_074// 前沿哨所 BAR_074
                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_074))
                    {
                        Keep(card,"前沿哨所");
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