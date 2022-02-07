using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 自定义 战士
// # 职业：战士
// # 模式：狂野模式
// #
// # 2x (1) 南海船工
// # 1x (1) 开进码头
// # 2x (1) 恩佐斯的副官
// # 2x (1) 海上威胁
// # 1x (1) 海盗帕奇斯
// # 2x (1) 空中悍匪
// # 2x (1) 血帆桨手
// # 2x (1) 血帆海盗
// # 2x (1) 蹩脚海盗
// # 2x (2) 港口匪徒
// # 2x (2) 空降歹徒
// # 2x (2) 船载火炮
// # 2x (2) 雾帆劫掠者
// # 2x (3) 南海船长
// # 1x (3) 海盗之锚
// # 2x (3) 迪菲亚炮手
// # 1x (6) 重拳先生
// # 
// AAEBAcGPBASRvALerQOY9gO/gAQN1AXlB/sPgrACoL0C3a0D6bAD/ucDlfYDl/YDnIEEpooEr6AEAA==
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
              
                 if((card==Card.Cards.SW_028//开进码头 SW_028 
                )){
                        Keep(card,"开进码头"); 
                }
                 if((card==Card.Cards.DRG_056//空降歹徒 DRG_056 
                )){
                    
                        Keep(card,"空降歹徒");
                    
                }

                if(card==Card.Cards.DRG_024// 空中悍匪 DRG_024
                ){
               
                        Keep(card,"空中悍匪");
                    
                }

                if(card==Card.Cards.CS3_008// 血帆桨手 CS3_008 
                ){
               
                        Keep(card,"血帆桨手");
                    
                }
                
                if(card==Card.Cards.OG_312// 恩佐斯的副官 OG_312 
                ){ if(!CardsToKeep.Contains(Card.Cards.OG_312))
                    {
                        Keep(card,"恩佐斯的副官");
                    }
                }
                if(card==Card.Cards.SW_029// 港口匪徒 SW_029  
                ){ if(!CardsToKeep.Contains(Card.Cards.SW_029))
                    {
                        Keep(card,"港口匪徒");
                    }
                }
                if(card==Card.Cards.GVG_075//船载火炮 GVG_075 
                ){ if(!CardsToKeep.Contains(Card.Cards.GVG_075))
                    {
                        Keep(card,"船载火炮");
                    }
                }
                // if(card==Card.Cards.CFM_325// 蹩脚海盗 CFM_325 
                // ){ 
                //         Keep(card,"蹩脚海盗");

                // }
                // if(card==Card.Cards.CS2_146// 南海船工 CS2_146
                // ){
                //         Keep(card,"南海船工");
                    
                // }
            
                if(card==Card.Cards.UNG_807// 葛拉卡爬行蟹 UNG_807 
                ){ if(!CardsToKeep.Contains(Card.Cards.UNG_807)&&WARRIOR>0)
                    {
                        Keep(card,"葛拉卡爬行蟹");
                    }
                }
            
                if(card==Card.Cards.SW_027// 海上威胁 SW_027 
                ){ if(!CardsToKeep.Contains(Card.Cards.SW_027)&&WARRIOR+WARLOCK>0)
                    {
                        Keep(card,"海上威胁");
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