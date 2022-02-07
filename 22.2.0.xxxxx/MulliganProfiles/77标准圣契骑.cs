using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 自定义 圣骑士
// # 职业：圣骑士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 圣礼骑士 BAR_873
// # 2x (1) 奥尔多侍从 BT_020 
// # 1x (1) 活化扫帚 SCH_311
// # 2x (2) 城建税 SW_046
// # 2x (2) 智慧圣契 BT_025
// # 2x (2) 神圣坐骑 SW_316
// # 1x (2) 莫戈尔·莫戈尔格 BT_019 
// # 2x (2) 阿达尔之手 BT_292
// # 1x (3) 巴罗夫领主 SCH_526 
// # 2x (3) 联盟旗手 SW_315
// # 1x (4) 凯瑞尔·罗姆
// # 1x (4) 剑圣萨穆罗 BAR_078 
// # 2x (5) 奥尔多真理追寻者 BT_026 
// # 2x (5) 正义圣契 BT_011
// # 2x (6) 虔诚的学徒
// # 1x (7) 女伯爵莉亚德琳 BT_334
// # 1x (7) 审判圣契 YOP_011
// # 1x (8) 瓦里安，暴风城国王 SW_081 
// # 2x (9) 希望圣契 BT_024 
// # 前沿哨所 BAR_074
// # 战场军官  SW_063
// # 逝者之剑 BAR_875 
// # 十字路口哨所 BAR_075 
// AAECAcHbBAj8uAOEwQOVzQOT0AP94wP76AOR7APZ+QMLyrgD/bgD6rkD67kD7LkDysEDwNEDzOsD8PYDi/gDjfgDAA==
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
            int flag2=0;//BAR_875逝者之剑 BAR_875 
            int flag3=0;//古神在上 DMF_236 
            int DRUID=0;
            int HUNTER=0;
            int MAGE=0;
            int PALADIN=0;
            int PRIEST=0;
            int ROGUE=0;
            int SHAMAN=0;
            int WARLOCK=0;
            int WARRIOR=0;
            int DEMONHUNTER=0;
            
            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.BT_020//奥尔多侍从 BT_020 
                ){flag1+=1;}
                if(card==Card.Cards.BAR_873//圣礼骑士 BAR_873
                ){flag1+=1;}
                if(card==Card.Cards.BAR_875//逝者之剑逝者之剑 BAR_875 
                ){flag2+=1;}
                if(card==Card.Cards.DMF_236//古神在上 DMF_236 
                ){flag3+=1;}
            }
            // foreach (Card.Cards EnemyClass in opponentClass)
            // {
            //     if(EnemyClass == Card.CClass.PALADIN//奥尔多侍从 BT_020 
            //     ){flag1+=1;}
            //     if(card==Card.Cards.BAR_873//圣礼骑士 BAR_873
            //     ){flag1+=1;}
            //     if(card==Card.Cards.BAR_875//逝者之剑逝者之剑 BAR_875 
            //     ){flag2+=1;}
            //     if(card==Card.Cards.DMF_236//古神在上 DMF_236 
            //     ){flag3+=1;}
            // }
            Bot.Log("对阵职业"+opponentClass);

            if(opponentClass==Card.CClass.PALADIN){
            PALADIN+=1;
            }
            if(opponentClass==Card.CClass.DRUID){
            DRUID+=1;
            }
            if(opponentClass==Card.CClass.HUNTER){
            HUNTER+=1;
            }
            if(opponentClass==Card.CClass.MAGE){
            MAGE+=1;
            }
            if(opponentClass==Card.CClass.PRIEST){
            PRIEST+=1;
            }
            if(opponentClass==Card.CClass.ROGUE){
            ROGUE+=1;
            }
            if(opponentClass==Card.CClass.SHAMAN){
            SHAMAN+=1;
            }
            if(opponentClass==Card.CClass.WARLOCK){
            WARLOCK+=1;
            }
            if(opponentClass==Card.CClass.WARRIOR){
            WARRIOR+=1;
            }
            if(opponentClass==Card.CClass.DEMONHUNTER){
            DEMONHUNTER+=1;
            }

            foreach (Card.Cards card in choices)
            {
              
                 if((card==Card.Cards.BAR_873//圣礼骑士 BAR_873
                )){
                    
                        Keep(card,"圣礼骑士");
                    
                }
                 if((card==Card.Cards.BT_019//莫戈尔·莫戈尔格 BT_019
                )){
                    
                        Keep(card,"莫戈尔·莫戈尔格");
                    
                }

                if(card==Card.Cards.BT_020// 奥尔多侍从 BT_020 
                ){
               
                        Keep(card,"奥尔多侍从");
                    
                }

                if(card==Card.Cards.BT_019// 莫戈尔·莫戈尔格 BT_019 
                ){
               
                        Keep(card,"莫戈尔·莫戈尔格");
                    
                }
                
                if(card==Card.Cards.BT_292// 阿达尔之手 BT_292
                ){ if(!CardsToKeep.Contains(Card.Cards.BT_292)&&flag1>=1)
                    {
                        Keep(card,"阿达尔之手");
                    }
                }
                if(card==Card.Cards.SCH_247// 新生入学 SCH_247 
                ){ if(!CardsToKeep.Contains(Card.Cards.SCH_247))
                    {
                        Keep(card,"新生入学");
                    }
                }
                if(card==Card.Cards.DMF_236//古神在上 DMF_236
                ){ if(!CardsToKeep.Contains(Card.Cards.DMF_236))
                    {
                        Keep(card,"古神在上");
                    }
                }
                if(card==Card.Cards.BAR_875// 逝者之剑 BAR_875 
                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_875))
                    {
                        Keep(card,"逝者之剑");
                    }
                }
                if(card==Card.Cards.BT_025// 智慧圣契 BT_025
                ){ if(!CardsToKeep.Contains(Card.Cards.BT_025))
                    {
                        Keep(card,"智慧圣契");
                    }
                }
                if(card==Card.Cards.BAR_074// 前沿哨所 BAR_074
                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_074))
                    {
                        Keep(card,"前沿哨所");
                    }
                }
                if(card==Card.Cards.SW_315// 联盟旗手 SW_315
                ){ if(!CardsToKeep.Contains(Card.Cards.SW_315))
                    {
                        Keep(card,"联盟旗手");
                    }
                }
                if(card==Card.Cards.BAR_876// 北卫军指挥官 BAR_876 

                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_876)&&flag2+flag3>0)
                    {
                        Keep(card,"北卫军指挥官");
                    }
                }
                if(card==Card.Cards.BT_026// 奥尔多真理追寻者 BT_026 
                ){ if(!CardsToKeep.Contains(Card.Cards.BT_026))
                    {
                        Keep(card,"奥尔多真理追寻者");
                    }
                }
                if(card==Card.Cards.BAR_075// 十字路口哨所 BAR_075 
                ){ if(!CardsToKeep.Contains(Card.Cards.BAR_075)&&MAGE+ROGUE>0)
                    {
                        Keep(card,"十字路口哨所");
                    }
                }
                if(card==Card.Cards.SCH_311// 活化扫帚 SCH_311
                ){ if(!CardsToKeep.Contains(Card.Cards.SCH_311)&&DRUID>0)
                    {
                        Keep(card,"活化扫帚");
                    }
                }
                if(card==Card.Cards.BAR_078&&HUNTER+SHAMAN+DRUID>0// 剑圣萨穆罗 BAR_078  
                ){              
                        Keep(card,"剑圣萨穆罗");
                }
                if(card==Card.Cards.BT_024&&HUNTER+SHAMAN>0&&flag1>0// 希望圣契 BT_024   
                ){              
                        Keep(card,"希望圣契");
                }
                if(card==Card.Cards.SCH_526&&DRUID>0// 巴罗夫领主 SCH_526   
                ){              
                        Keep(card,"巴罗夫领主");
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