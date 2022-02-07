using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 自定义 牧师
// # 职业：牧师
// # 模式：狂野模式
// #
// # 2x (0) 亡者复生 SCH_514 
// # 2x (1) 南海船工 CS2_146 
// # 2x (1) 暗影投弹手 GVG_009
// # 1x (1) 海盗帕奇斯 CFM_637 
// # 2x (1) 虚触侍从 SW_446 
// # 2x (2) 心灵震爆 DS1_233 
// # 2x (2) 暗中生长 CS3_028 
// # 2x (2) 暗影布缝针 SW_012 
// # 2x (2) 暗影视界 UNG_029
// # 2x (2) 暮光欺诈者 SW_444 
// # 2x (2) 空降歹徒  DRG_056 
// # 2x (2) 船载火炮 GVG_075
// # 2x (2) 葛拉卡爬行蟹 UNG_807 
// # 2x (2) 迪菲亚麻风侏儒 DED_513 
// # 2x (4) 狂暴邪翼蝠 YOD_032
// # 1x (5) 黑暗主教本尼迪塔斯 SW_448 
// # 1x (5) 血帆海盗 NEW1_025 
// # 
// AAEBAZ/HAgKRvAK79wMOoQTUBZEP+w/RwQLkwgLpsAO6tgPXzgP08QOj9wOt9wONgQStigQA
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

            int flag1=0;//狂暴邪翼蝠 YOD_032
            int flag2=0;//暗影投弹手 GVG_009
            int flag3=0;//心灵震爆 DS1_233 
            int flag4=0;//迪菲亚麻风侏儒 DED_513
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
                if(card==Card.Cards.YOD_032//狂暴邪翼蝠 YOD_032
                ){flag1+=1;}
                if(card==Card.Cards.GVG_009//暗影投弹手 GVG_009
                ){flag2+=1;}
                if(card==Card.Cards.DS1_233//心灵震爆 DS1_233 
                ){flag2+=1;}
                if(card==Card.Cards.DED_513//迪菲亚麻风侏儒 DED_513
                ){flag4+=1;}
                if(card==Card.Cards.SCH_514//亡者复生 SCH_514
                ){flag3+=1;}
                if(card==Card.Cards.DS1_233//心灵震爆 DS1_233
                ){flag3+=1;}
                if(card==Card.Cards.CS3_028//暗中生长 CS3_028
                ){flag3+=1;}
                if(card==Card.Cards.UNG_029//暗影视界 UNG_029
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
              
                 if((card==Card.Cards.SW_446//虚触侍从 SW_446 
                )){
                        Keep(card,"虚触侍从"); 
                }
                 if((card==Card.Cards.SW_446&&flag1>=1//暗影投弹手 GVG_009
                )){
                        Keep(card,"暗影投弹手");
                    
                }
                 if((card==Card.Cards.YOD_032&&flag2>=1//狂暴邪翼蝠 YOD_032
                )){
                        Keep(card,"狂暴邪翼蝠");
                    
                }
                //  if((card==Card.Cards.DS1_233&&flag1>=1//心灵震爆 DS1_233
                // )){
                //        if(!CardsToKeep.Contains(Card.Cards.DS1_233))
                //     {
                //         Keep(card,"心灵震爆");
                //     }
                // }
                 if((card==Card.Cards.DRG_056//空降歹徒 DRG_056 
                )){
                    
                        Keep(card,"空降歹徒");
                    
                }
                 if((card==Card.Cards.NEW1_025//血帆海盗 NEW1_025
                )){
                    
                        Keep(card,"血帆海盗");
                    
                }
                if(card==Card.Cards.DED_513// 迪菲亚麻风侏儒 DED_513  
                ){ if(!CardsToKeep.Contains(Card.Cards.DED_513))
                    {
                        Keep(card,"迪菲亚麻风侏儒");
                    }
                }
                if(card==Card.Cards.SCH_514// 亡者复生 SCH_514
                ){ if(!CardsToKeep.Contains(Card.Cards.SCH_514)&&flag4>=1&&flag3<2)
                    {
                        Keep(card,"亡者复生");
                    }
                }
                // if(card==Card.Cards.DS1_233// 心灵震爆 DS1_233
                // ){ if(!CardsToKeep.Contains(Card.Cards.DS1_233)&&flag4>=1&&flag3<2)
                //     {
                //         Keep(card,"心灵震爆");
                //     }
                // }
                if(card==Card.Cards.CS3_028// 暗中生长 CS3_028
                ){ if(!CardsToKeep.Contains(Card.Cards.CS3_028)&&flag4>=1&&flag3<2)
                    {
                        Keep(card,"暗中生长");
                    }
                }
                if(card==Card.Cards.UNG_029// 暗影视界 UNG_029
                ){ if(!CardsToKeep.Contains(Card.Cards.UNG_029)&&flag4>=1&&flag3<2)
                    {
                        Keep(card,"暗影视界");
                    }
                }
                if(card==Card.Cards.GVG_075//船载火炮 GVG_075
                && HasCoin==true 
                ){ if(!CardsToKeep.Contains(Card.Cards.GVG_075))
                    {
                        Keep(card,"船载火炮");
                    }
                }
                if(card==Card.Cards.CS2_146// 南海船工 CS2_146
                ){
                        Keep(card,"南海船工");
                    
                }
            
                if(card==Card.Cards.UNG_807// 葛拉卡爬行蟹 UNG_807 
                ){ if(!CardsToKeep.Contains(Card.Cards.UNG_807)&&WARRIOR+PRIEST>0)
                    {
                        Keep(card,"葛拉卡爬行蟹");
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