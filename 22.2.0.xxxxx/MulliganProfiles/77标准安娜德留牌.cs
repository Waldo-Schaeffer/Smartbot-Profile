using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ### 自定义 德鲁伊4
// # 职业：德鲁伊
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 激活 Innervate ID：CORE_EX1_169  
// # 2x (0) 雷霆绽放 Lightning Bloom ID：SCH_427 
// # 2x (1) 自然研习 Nature Studies ID：SCH_333 
// # 2x (2) 日蚀 Solar Eclipse ID：DMF_058 
// # 2x (2) 月蚀 Lunar Eclipse ID：DMF_057 
// # 2x (2) 猜重量 Guess the Weight ID：DMF_075 
// # 2x (3) 真菌宝藏 Fungal Fortunes ID：BT_128 
// # 1x (4) 丛林之魂 Soul of the Forest ID：CORE_EX1_158
// # 2x (4) 萌芽分裂 Germination ID：BT_129 
// # 2x (4) 过度生长 Overgrowth ID：BT_130 
// # 2x (5) 树木生长 Arbor Up ID：YOP_026 
// # 2x (5) 萤火成群 Glowfly Swarm ID：BT_135 
// # 1x (6) 安娜科德拉 Lady Anacondra ID：WC_006
// # 2x (6) 滋养 Nourish ID：CORE_EX1_164 
// # 2x (6) 紧壳商品 Best in Shell ID：SW_429 
// # 2x (8) 塞纳里奥结界 Cenarion Ward ID：DMF_732 
// # 
// AAECAbSKAwLk7gPXnwQO5boD5roD6LoD77oDm84D8NQDieADiuADpOED0eEDjOQDpPYDrp8E2p8EAA==
// # 
// # 想要使用这副套牌，请先复制到剪贴板，然后在游戏中点击“新套牌”进行粘贴。
// ### 自定义 德鲁伊5
// # 职业：德鲁伊
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 激活
// # 2x (0) 雷霆绽放
// # 2x (1) 自然研习
// # 2x (2) 日蚀
// # 2x (2) 月蚀
// # 2x (2) 猜重量
// # 2x (3) 真菌宝藏
// # 2x (4) 萌芽分裂
// # 2x (4) 过度生长
// # 2x (5) 树木生长
// # 2x (5) 萤火成群
// # 1x (6) 安娜科德拉
// # 2x (6) 滋养
// # 1x (6) 紧壳商品
// # 2x (7) 超凡之盟 Celestial Alignment ID：BAR_539 
// # 1x (8) 塞纳里奥结界
// # 1x (10) 优胜劣汰
// # 
// AAECAbSKAwS60AOk4QPk7gOk9gMN5boD5roD6LoD77oDm84D8NQDieADiuAD0eEDjOQDwOwDrp8E2p8EAA==
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

            int flag1=0;// 过度生长 Overgrowth ID：BT_130
            int flag2=0;//真菌宝藏 Fungal Fortunes ID：BT_128
            int flag3=0;// 安娜科德拉 Lady Anacondra ID：WC_006
            int flag4=0;//萤火成群 Glowfly Swarm ID：BT_135
            int flag5=0;//雷霆绽放 Lightning Bloom ID：SCH_427
            int flag6=0;//激活 Innervate ID：CORE_EX1_169 
            int flag7=0;//超凡之盟 Celestial Alignment ID：BAR_539 

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.BT_130  
                ){flag1+=1;}
                
                if(card==Card.Cards.BT_128
                ){flag2+=1;}
                
                if(card==Card.Cards.WC_006
                ){flag3+=1;}
                
                if(card==Card.Cards.BT_135 
                ){flag4+=1;}
                
                if(card==Card.Cards.SCH_427
                ){flag5+=1;}

                if(card==Card.Cards.CORE_EX1_169 
                ){flag6+=1;}

                if(card==Card.Cards.BAR_539
                ){flag7+=1;}
                 
                // if(card==Card.Cards.SCH_312
                // ){flag8+=1;}

                // if(card==Card.Cards.SCH_427//雷霆绽放 Lightning Bloom ID：SCH_427 
                // ){flag9+=1;}
                // if(card==Card.Cards.SW_319//魔法乌鸦 Enchanted Raven ID：SW_319
                // ){flag10+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.BT_130 
                )
                {
                  Keep(card,"过度生长");     
                }
                if(card==Card.Cards.BT_128 
                )
                {
                  Keep(card,"真菌宝藏");    
                }
                if(card==Card.Cards.WC_006 
                )
                {
                  Keep(card,"安娜");     
                }
                if(card==Card.Cards.BT_135 
                )
                {
                  Keep(card,"萤火成群");    
                } 
                if(card==Card.Cards.SCH_427 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.SCH_427)&&flag1+flag2+flag3+flag4+flag7>0)
                    {
                        Keep(card,"雷霆绽放");
                    }   
                }
                if(card==Card.Cards.CORE_EX1_169  
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.CORE_EX1_169 )&&flag1+flag2+flag3+flag4+flag7>0)
                    {
                        Keep(card,"激活");
                    }   
                }
                if(card==Card.Cards.BAR_539  
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.BAR_539 ))
                    {
                        Keep(card,"超凡之盟");
                    }   
                }
              
                // if(card==Card.Cards.ICC_038 
                // )
                // {
                //        if(!CardsToKeep.Contains(Card.Cards.ICC_038))
                //     {
                //         Keep(card,"正义保护者");
                //     }   
                // }
              
                // if(card==Card.Cards.BAR_875 
                // )
                // {
                //        if(!CardsToKeep.Contains(Card.Cards.BAR_875))
                //     {
                //         Keep(card,"逝者之剑");
                //     }   
                // }
              
                // if(card==Card.Cards.DMF_236 
                // )
                // {
                //        if(!CardsToKeep.Contains(Card.Cards.DMF_236))
                //     {
                //         Keep(card,"古神在上");
                //     }   
                // }
              
                // if(card==Card.Cards.SCH_312 
                // )
                // {
                //        if(!CardsToKeep.Contains(Card.Cards.SCH_312))
                //     {
                //         Keep(card,"巡游向导");
                //     }   
                // }
              

                // if(card==Card.Cards.SCH_247&&flag2>=1)
                // {
                //      Keep(card,"2噬骨殴斗者 ");
                // }
                // if(card==Card.Cards.BAR_743&&flag2>=1)//狂野蟾蜍 Toad of the Wilds ID：BAR_743 
                // {
                //         Keep(card,"2狂野蟾蜍 ");   
                // }
                // if(card==Card.Cards.BOT_270t&&flag2>=1)//吵吵机器人 Annoy-o-Tron ID：BOT_270t 
                // {
                //         Keep(card,"吵吵机器人 ");   
                // }
                // if(card==Card.Cards.SW_422//播种施肥 Sow the Soil ID：SW_422
                // )
                // {
                //     Keep(card,"播种施肥 ");
                // }
                // if(card==Card.Cards.SW_439//活泼的松鼠 Vibrant Squirrel ID：SW_439 
                // )
                // {
                //     Keep(card,"活泼的松鼠 ");
                // }
                // if(card==Card.Cards.SW_319//魔法乌鸦 Enchanted Raven ID：SW_319
                // )
                // {
                //     Keep(card,"魔法乌鸦 ");
                // }
                // if(card==Card.Cards.SCH_427//雷霆绽放 Lightning Bloom ID：SCH_427
                // )
                // {
                //     if(!CardsToKeep.Contains(Card.Cards.SCH_427))
                //     {
                //         Keep(card,"雷霆绽放 ");
                //     }   
                // }
                // if(card==Card.Cards.BAR_076//莫尔杉哨所 Mor'shan Watch Post ID：BAR_076
                // )
                // {
                //     if(!CardsToKeep.Contains(Card.Cards.BAR_076)&&flag1+flag2>=1)
                //     {
                //         Keep(card,"留1莫尔杉哨所 ");
                //     }   
                // }
                // if(card==Card.Cards.WC_005//原初地下城历险家 Primal Dungeoneer ID：WC_005 
                // )
                // {
                //     if(!CardsToKeep.Contains(Card.Cards.WC_005))
                //     {
                //         Keep(card,"留1原初地下城历险家");
                //     }   
                // }
                // if(card==Card.Cards.CORE_BOT_533//凶恶的雨云 Menacing Nimbus ID：CORE_BOT_533 
                // )
                // {
                //      Keep(card,"留2凶恶的雨云 ");
                // }
                // if(card==Card.Cards.SW_025//拍卖行木槌 Auctionhouse Gavel ID：SW_025 
                // )
                // {
                //      Keep(card,"留2拍卖行木槌 ");
                // }

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

                //留第一张逝者之剑 Sword of the Fallen ID：BAR_875
                // if(card==Card.Cards.BAR_875)
                // {
                //     if(!CardsToKeep.Contains(Card.Cards.BAR_875))
                //     {
                //         Keep(card,"留第一张逝者之剑 Sword of the Fallen");
                //     }   
                // }
				
				
				//有逝者之剑 Sword of the Fallen ID：BAR_875留北卫军指挥官 Northwatch Commander ID：BAR_876
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