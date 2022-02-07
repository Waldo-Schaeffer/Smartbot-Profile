using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ; ### 萨满祭司
// ; # 职业：萨满祭司
// ; # 模式：标准模式
// ; # 狮鹫年
// ; #
// ; # 2x (1) 哀嚎蒸汽
// ; # 2x (1) 火光元素
// ; # 2x (2) 凶恶的雨云
// ; # 2x (2) 拍卖行木槌
// ; # 2x (2) 笼斗管理员
// ; # 2x (3) 原初地下城历险家
// ; # 1x (3) 导师火心
// ; # 2x (3) 敲狼锤
// ; # 2x (3) 旋岩虫
// ; # 2x (3) 旱地风暴
// ; # 2x (3) 毒蛇神殿传送门
// ; # 2x (4) 花岗岩熔铸体
// ; # 2x (4) 运河慢步者
// ; # 2x (5) 荷塘潜伏者
// ; # 2x (6) 火元素
// ; # 1x (8) 风领主奥拉基尔
// ; # 
// ; AAECAZnDAwKczgOAoAQO27gDqt4Dq94DjOED4OwD4ewDre4Dr+4D4+4D1vUDwPYDwfYDjJ8E46AEAA==
// ; # 
// ; # 想要使用这副套牌，请先复制到剪贴板，然后在游戏中点击“新套牌”进行粘贴。

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

            int flag1=0;//哀嚎蒸汽 Wailing Vapor ID：WC_042 
            int flag2=0;//火光元素 Kindling Elemental ID：BAR_854 
            int flag3=0;//花岗岩熔铸体 Granite Forgeborn ID：SW_032
            int flag4=0;//敲狼锤 Whack-A-Gnoll Hammer ID：DMF_705 
            int flag5=0;//笼斗管理员 Cagematch Custodian ID：DMF_704
            int flag6=0;//旱地风暴 Arid Stormer ID：BAR_045 
            int flag7=0;//凶恶的雨云 Menacing Nimbus ID：CORE_BOT_533
            int flag8=0;//拍卖行木槌 Auctionhouse Gavel ID：SW_025 
            int flag9=0;//前沿哨所 Far Watch Post ID：BAR_074  
            int flag10=0;//莫尔杉哨所 Mor'shan Watch Post ID：BAR_076 

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.WC_042//哀嚎蒸汽 Wailing Vapor ID：WC_042  
                ){flag1+=1;}
                
                if(card==Card.Cards.BAR_854//火光元素 Kindling Elemental ID：BAR_854  
                ){flag2+=1;}
                
                if(card==Card.Cards.SW_032//花岗岩熔铸体 Granite Forgeborn ID：SW_032 ID：SW_032 
                    || card==Card.Cards.SW_032//花岗岩熔铸体 Granite Forgeborn ID：SW_032 ID：SW_032
                ){flag3+=1;}
                
                if(card==Card.Cards.DMF_705 //敲狼锤 Whack-A-Gnoll Hammer ID：DMF_705   
                ){flag4+=1;}
                
                if(card==Card.Cards.DMF_704//笼斗管理员 Cagematch Custodian ID：DMF_704  
                ){flag5+=1;}

                if(card==Card.Cards.BAR_045//旱地风暴 Arid Stormer ID：BAR_045 
                ){flag6+=1;}

                if(card==Card.Cards.CORE_BOT_533//凶恶的雨云 Menacing Nimbus ID：CORE_BOT_533 
                ){flag7+=1;}
                 
                if(card==Card.Cards.SW_025//拍卖行木槌 Auctionhouse Gavel ID：SW_025 
                ){flag8+=1;}

                if(card==Card.Cards.BAR_074//前沿哨所 Far Watch Post ID：BAR_074  
                ){flag9+=1;}
                if(card==Card.Cards.BAR_076//莫尔杉哨所 Mor'shan Watch Post ID：BAR_076 
                ){flag10+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                if((card==Card.Cards.SW_032//花岗岩熔铸体 Granite Forgeborn ID：SW_032 ID：SW_032 
                )){
                    {
                       if(!CardsToKeep.Contains(Card.Cards.SW_032))
                    {
                        Keep(card,"留1花岗岩熔铸体");
                    }
                    }   
                }//留一张花岗岩熔铸体


                if(card==Card.Cards.BAR_854//火光元素 Kindling Elemental ID：BAR_854 
                )
                {
                    Keep(card,"留2火光元素 ");
                }
                if(card==Card.Cards.WC_042//哀嚎蒸汽 Wailing Vapor ID：WC_042  
                )
                {
                     Keep(card,"留2哀嚎蒸汽 ");
                }
                if(card==Card.Cards.DMF_705//敲狼锤 Whack-A-Gnoll Hammer ID：DMF_705 
                )
                {
                    if(!CardsToKeep.Contains(Card.Cards.DMF_705)&&flag6>=1)
                    {
                        Keep(card,"留1敲狼锤 ");
                    }   
                }
                if(card==Card.Cards.DMF_704//笼斗管理员 Cagematch Custodian ID：DMF_704
                )
                {
                    Keep(card,"留2笼斗管理员 ");
                }
                if(card==Card.Cards.BAR_074//前沿哨所 Far Watch Post ID：BAR_074 
                )
                {
                    Keep(card,"留2前沿哨所 ");
                }
                if(card==Card.Cards.BAR_045//旱地风暴 Arid Stormer ID：BAR_045 
                )
                {
                    if(!CardsToKeep.Contains(Card.Cards.BAR_045)&&flag1+flag2>=1)
                    {
                        Keep(card,"留1旱地风暴 ");
                    }   
                }
                if(card==Card.Cards.BAR_076//莫尔杉哨所 Mor'shan Watch Post ID：BAR_076
                )
                {
                    if(!CardsToKeep.Contains(Card.Cards.BAR_076)&&flag1+flag2>=1)
                    {
                        Keep(card,"留1莫尔杉哨所 ");
                    }   
                }
                // if(card==Card.Cards.WC_005//原初地下城历险家 Primal Dungeoneer ID：WC_005 
                // )
                // {
                //     if(!CardsToKeep.Contains(Card.Cards.WC_005))
                //     {
                //         Keep(card,"留1原初地下城历险家");
                //     }   
                // }
                if(card==Card.Cards.CORE_BOT_533//凶恶的雨云 Menacing Nimbus ID：CORE_BOT_533 
                )
                {
                     Keep(card,"留2凶恶的雨云 ");
                }
                if(card==Card.Cards.SW_025//拍卖行木槌 Auctionhouse Gavel ID：SW_025 
                )
                {
                     Keep(card,"留2拍卖行木槌 ");
                }

                // if(card==Card.Cards.BT_292//阿达尔之手 Hand of A'dal ID：BT_292  
                // ){
                //     if(!CardsToKeep.Contains(Card.Cards.BT_292)
                //     )
                //     {
                //         Keep(card,"留一张阿达尔之手");
                //     }
                // }                

                // if(card==Card.Cards.YOP_031//螃蟹骑士 Crabrider ID：YOP_031 
                // ){
                //     if(!CardsToKeep.Contains(Card.Cards.YOP_031)
                //        && HasCoin==true)
                //     {
                //         Keep(card,"后手留一张螃蟹骑士");
                //     }
                // }

                // if(card==Card.Cards.DMF_194//赤鳞驯龙者 Redscale Dragontamer ID：DMF_194
                // ){
                //     if(!CardsToKeep.Contains(Card.Cards.DMF_194)
                //        && HasCoin==true)
                //     {
                //         Keep(card,"后手留一张赤鳞驯龙者 Redscale Dragontamer");
                //     }
                // }


                // if(card==Card.Cards.CORE_FP1_007//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007 
                // ){
                //     {
                //         if(!CardsToKeep.Contains(Card.Cards.CORE_FP1_007)
                //         )
                //         {
                //         Keep(card,"留一张蛛魔之卵 Nerubian Egg");
                //         } 
                //     }  //蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
                // }

                // if(card==Card.Cards.DMF_704//笼斗管理员 Cagematch Custodian ID：DMF_704 
                // ){
                //     if(!CardsToKeep.Contains(Card.Cards.DMF_704)
                //        && HasCoin==true)
                //     {
                //         Keep(card,"后手留一张莫戈尔·莫戈尔格 Murgur Murgurgle");
                //     }
                //     else
                //     {
                //         if(!CardsToKeep.Contains(Card.Cards.DMF_704)
                //         && flag1+flag2+flag3>=1
                //         )
                //         {
                //         Keep(card,"先手有1费留一张莫戈尔·莫戈尔格 Murgur Murgurgle");
                //         } 
                //     }  //笼斗管理员 Cagematch Custodian ID：DMF_704 
                // }

                // //留第一张逝者之剑 Sword of the Fallen ID：BAR_875
                // if(card==Card.Cards.BAR_875)
                // {
                //     if(!CardsToKeep.Contains(Card.Cards.BAR_875))
                //     {
                //         Keep(card,"留第一张逝者之剑 Sword of the Fallen");
                //     }   
                // }
				
				
				// //有逝者之剑 Sword of the Fallen ID：BAR_875留北卫军指挥官 Northwatch Commander ID：BAR_876
        //         if(card==Card.Cards.BAR_876 && flag7>=1
        //         && HasCoin==true
        //          )
				// {Keep(card,"后手有逝者之剑 Sword of the Fallen留北卫军指挥官 Northwatch Commander");}


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