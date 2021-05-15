using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

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

            int flag1=0;//新生入学 First Day of School ID：SCH_247 
            int flag2=0;//圣礼骑士 Knight of Anointment ID：BAR_873 
            int flag3=0;//正义保护者 Righteous Protector
            int flag4=0;//异教低阶牧师 Cult Neophyte ID：SCH_713
            int flag5=0;//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019
            int flag6=0;//阿达尔之手 Hand of A'dal ID：BT_292 
            int flag7=0;//逝者之剑 Sword of the Fallen ID：BAR_875 
            int flag8=0;//螃蟹骑士 Crabrider ID：YOP_031

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SCH_247//新生入学 First Day of School ID：SCH_247  
                ){flag1+=1;}
                
                if(card==Card.Cards.BAR_873//圣礼骑士 Knight of Anointment ID：BAR_873  
                ){flag2+=1;}
                
                if(card==Card.Cards.CORE_ICC_038//正义保护者 Righteous Protector ID：CORE_ICC_038 
                    || card==Card.Cards.ICC_038//正义保护者 Righteous Protector ID：ICC_038
                ){flag3+=1;}
                
                if(card==Card.Cards.SCH_713//异教低阶牧师 Cult Neophyte ID：SCH_713  
                ){flag4+=1;}
                
                if(card==Card.Cards.BT_019//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
                ){flag5+=1;}

                if(card==Card.Cards.BT_292//阿达尔之手 Hand of A'dal ID：BT_292    
                ){flag6+=1;}

                if(card==Card.Cards.BAR_875//逝者之剑 Sword of the Fallen ID：BAR_875 
                ){flag7+=1;}
                 
                if(card==Card.Cards.YOP_031//螃蟹骑士 Crabrider ID：YOP_031   
                ){flag8+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                if((card==Card.Cards.CORE_ICC_038//正义保护者 Righteous Protector ID：CORE_ICC_038 
                    || card==Card.Cards.ICC_038//正义保护者 Righteous Protector ID：ICC_038
                )){
                    {
                        if(!CardsToKeep.Contains(Card.Cards.CORE_ICC_038)//正义保护者 Righteous Protector ID：CORE_ICC_038 
                        && !CardsToKeep.Contains(Card.Cards.ICC_038)//正义保护者 Righteous Protector ID：ICC_038
                        )
                        {
                        Keep(card,"留一张正义保护者 Righteous Protecto");
                        } 
                    }   
                }//留一张正义保护者



                if(card==Card.Cards.SCH_247//新生入学 First Day of School ID：SCH_247 
                ){  
                        if(!CardsToKeep.Contains(Card.Cards.SCH_247))
                        {
                            Keep(card,"留一张新生入学 First Day of School");
                        }                     
                }//留一张新生入学

                if(card==Card.Cards.BAR_873//圣礼骑士 Knight of Anointment ID：BAR_873 
                )
                {
                    if(!CardsToKeep.Contains(Card.Cards.BAR_873))
                    {
                        Keep(card,"留一张圣礼骑士 Knight of Anointment");
                    }   
                }//留一张圣礼骑士

                if(card==Card.Cards.BT_292//阿达尔之手 Hand of A'dal ID：BT_292  
                ){
                    if(!CardsToKeep.Contains(Card.Cards.BT_292)
                    )
                    {
                        Keep(card,"留一张阿达尔之手 Hand of A'dal");
                    }
                }                

                if(card==Card.Cards.YOP_031//螃蟹骑士 Crabrider ID：YOP_031 
                ){
                    if(!CardsToKeep.Contains(Card.Cards.YOP_031)
                       && HasCoin==true)
                    {
                        Keep(card,"后手留一张螃蟹骑士 Crabrider");
                    }
                }

                if(card==Card.Cards.DMF_194//赤鳞驯龙者 Redscale Dragontamer ID：DMF_194
                ){
                    if(!CardsToKeep.Contains(Card.Cards.DMF_194)
                       && HasCoin==true)
                    {
                        Keep(card,"后手留一张赤鳞驯龙者 Redscale Dragontamer");
                    }
                }


                if(card==Card.Cards.CORE_FP1_007//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007 
                ){
                    {
                        if(!CardsToKeep.Contains(Card.Cards.CORE_FP1_007)
                        )
                        {
                        Keep(card,"留一张蛛魔之卵 Nerubian Egg");
                        } 
                    }  //蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
                }

                if(card==Card.Cards.BT_019//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019 
                ){
                    if(!CardsToKeep.Contains(Card.Cards.BT_019)
                       && HasCoin==true)
                    {
                        Keep(card,"后手留一张莫戈尔·莫戈尔格 Murgur Murgurgle");
                    }
                    else
                    {
                        if(!CardsToKeep.Contains(Card.Cards.BT_019)
                        && flag1+flag2+flag3>=1
                        )
                        {
                        Keep(card,"先手有1费留一张莫戈尔·莫戈尔格 Murgur Murgurgle");
                        } 
                    }  //莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019 
                }

                //留第一张逝者之剑 Sword of the Fallen ID：BAR_875
                if(card==Card.Cards.BAR_875)
                {
                    if(!CardsToKeep.Contains(Card.Cards.BAR_875))
                    {
                        Keep(card,"留第一张逝者之剑 Sword of the Fallen");
                    }   
                }
				
				
				//有逝者之剑 Sword of the Fallen ID：BAR_875留北卫军指挥官 Northwatch Commander ID：BAR_876
                if(card==Card.Cards.BAR_876 && flag7>=1
                && HasCoin==true
                 )
				{Keep(card,"后手有逝者之剑 Sword of the Fallen留北卫军指挥官 Northwatch Commander");}


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