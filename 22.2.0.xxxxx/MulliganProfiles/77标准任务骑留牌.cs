using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ### 任务骑
// # 职业：圣骑士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 迅疾救兵 Galloping Savior ID：BAR_550 
// # 1x (1) 甩笔侏儒 Pen Flinger ID：SCH_248 
// # 2x (1) 正义保护者 Righteous Protector ID：ICC_038 
// # 2x (1) 新生入学 First Day of School ID：SCH_247
// # 1x (1) 挺身而出 Rise to the Occasion ID：SW_313 
// # 2x (1) 巡游向导 Tour Guide ID：SCH_312 
// # 2x (1) 崇高牺牲 Noble Sacrifice ID：EX1_130 
// # 2x (1) 复仇 Avenge ID：FP1_020 
// # 2x (1) 圣礼骑士 Knight of Anointment ID：BAR_873 
// # 2x (1) 古神在上 Oh My Yogg! ID：DMF_236 
// # 2x (1) 受祝福的货物 Blessed Goods ID：SW_049 
// # 2x (2) 阿达尔之手 Hand of A'dal ID：BT_292 
// # 2x (2) 逝者之剑 Sword of the Fallen ID：BAR_875
// # 2x (2) 定罪（等级1） Conviction (Rank 1) ID：BAR_880 
// # 2x (3) 北卫军指挥官 Northwatch Commander ID：BAR_876 
// # 1x (5) 救赎者洛萨克森 Lothraxion the Redeemed ID：DMF_240 
// # 1x (6) 普瑞斯托女士 Lady Prestor ID：SW_078 
// #
// AAECAZ8FBJ/NA4feA7T2A9b5Aw3KwQObzQOezQOD3gPM6wPO6wPP6wPj6wPb7gOV+QPqnwTIoATJoAQA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/wHdB2hXpZhnANEKiwj1R0f/


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

            int flag1=0;// 挺身而出 Rise to the Occasion ID：SW_313 
            int flag2=0;//圣礼骑士 Knight of Anointment ID：BAR_873
            int flag3=0;//新生入学 First Day of School ID：SCH_247
            int flag4=0;//受祝福的货物 Blessed Goods ID：SW_049
            int flag5=0;//正义保护者 Righteous Protector ID：ICC_038
            int flag6=0;//逝者之剑 Sword of the Fallen ID：BAR_875
            int flag7=0;//古神在上 Oh My Yogg! ID：DMF_236 
            int flag8=0;//巡游向导 Tour Guide ID：SCH_312 
            int flag9=0;//雷霆绽放 Lightning Bloom ID：SCH_427 
            int flag10=0;//魔法乌鸦 Enchanted Raven ID：SW_319

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SW_313  
                ){flag1+=1;}
                
                if(card==Card.Cards.BAR_873
                ){flag2+=1;}
                
                if(card==Card.Cards.SCH_247
                ){flag3+=1;}
                
                if(card==Card.Cards.SW_049 
                ){flag4+=1;}
                
                if(card==Card.Cards.ICC_038
                ){flag5+=1;}

                if(card==Card.Cards.BAR_875
                ){flag6+=1;}

                if(card==Card.Cards.DMF_236
                ){flag7+=1;}
                 
                if(card==Card.Cards.SCH_312
                ){flag8+=1;}

                // if(card==Card.Cards.SCH_427//雷霆绽放 Lightning Bloom ID：SCH_427 
                // ){flag9+=1;}
                // if(card==Card.Cards.SW_319//魔法乌鸦 Enchanted Raven ID：SW_319
                // ){flag10+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                 if((card==Card.Cards.SW_313 
                )){
                    {
                       if(!CardsToKeep.Contains(Card.Cards.SW_313))
                    {
                        Keep(card,"挺身而出");
                    }
                    }   
                }

                if(card==Card.Cards.BAR_873 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.BAR_873))
                    {
                        Keep(card,"圣礼骑士");
                    }   
                }
                if(card==Card.Cards.SCH_247 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.SCH_247))
                    {
                        Keep(card,"新生入学");
                    }   
                }
              
                if(card==Card.Cards.SW_049 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.SW_049))
                    {
                        Keep(card,"受祝福的货物");
                    }   
                }
              
                if(card==Card.Cards.ICC_038 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.ICC_038))
                    {
                        Keep(card,"正义保护者");
                    }   
                }
              
                if(card==Card.Cards.BAR_875 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.BAR_875))
                    {
                        Keep(card,"逝者之剑");
                    }   
                }
          
              
                if(card==Card.Cards.SCH_312 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.SCH_312))
                    {
                        Keep(card,"巡游向导");
                    }   
                }
              

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