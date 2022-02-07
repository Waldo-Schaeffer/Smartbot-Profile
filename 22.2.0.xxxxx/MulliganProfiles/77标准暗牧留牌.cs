using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ### 快攻暗牧
// # 职业：牧师
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 亡者复生 Raise Dead ID：SCH_514 
// # 2x (1) 被禁锢的矮劣魔 Imprisoned Homunculus ID：BT_258 
// # 2x (1) 被禁锢的矮劣魔 Imprisoned Homunculus ID：BT_258 
// # 2x (1) 疲倦的大一新生 Frazzled Freshman ID：SCH_137 
// # 2x (1) 巡游向导 Tour Guide ID：SCH_312 
// # 2x (2) 食魔影豹 Manafeeder Panthara ID：SCH_283 
// # 2x (2) 蠕动的恐魔 Wriggling Horror ID：DMF_091 
// # 2x (2) 暮光欺诈者 Twilight Deceptor ID：SW_444 
// # 1x (2) 暗中生长 Thrive in the Shadows ID：CS3_028 
// # 2x (2) 异教低阶牧师 Cult Neophyte ID：SCH_713
// # 2x (3) 龙喉监工 Dragonmaw Overseer ID：BT_256 
// # 1x (3) 锈烂蝰蛇 Rustrot Viper ID：SW_072 
// # 1x (3) 曼科里克 Mankrik ID：BAR_721 
// # 2x (3) 暗影之灵 Shadowed Spirit ID：CS3_013 
// # 2x (4) 虚空碎片 Void Shard ID：SW_442 
// # 2x (4) 小刀商贩 Knife Vendor ID：DMF_066 
// # 1x (5) 黑暗主教本尼迪塔斯 Darkbishop Benedictus ID：SW_448
// #
// AAECAa0GBOfwA7v3A9D5A62KBA2wugPpugObzQPXzgP7zgO70QOL1QO04QPK4wOh6AOI9wOj9wOt9wMA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/7CRB3G82MqlPtD112CB04c/

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

            int flag1=0;// 被禁锢的矮劣魔 Imprisoned Homunculus ID：BT_258 
            int flag2=0;//异教低阶牧师 Cult Neophyte ID：SCH_713
            int flag3=0;// 疲倦的大一新生 Frazzled Freshman ID：SCH_137 
            int flag4=0;//虚触侍从 Voidtouched Attendant ID：SW_446
            int flag5=0;//龙喉监工 Dragonmaw Overseer ID：BT_256
            // int flag6=0;//逝者之剑 Sword of the Fallen ID：BAR_875
            // int flag7=0;//古神在上 Oh My Yogg! ID：DMF_236 
            // int flag8=0;//巡游向导 Tour Guide ID：SCH_312 
            // int flag9=0;//雷霆绽放 Lightning Bloom ID：SCH_427 
            // int flag10=0;//魔法乌鸦 Enchanted Raven ID：SW_319

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.BT_258  
                ){flag1+=1;}
                
                if(card==Card.Cards.SCH_713
                ){flag2+=1;}
                
                if(card==Card.Cards.SCH_137
                ){flag3+=1;}
                
                if(card==Card.Cards.SW_446 
                ){flag4+=1;}
                
                if(card==Card.Cards.BT_256
                ){flag5+=1;}

                // if(card==Card.Cards.BAR_875
                // ){flag6+=1;}

                // if(card==Card.Cards.DMF_236
                // ){flag7+=1;}
                 
                // if(card==Card.Cards.SCH_312
                // ){flag8+=1;}

                // if(card==Card.Cards.SCH_427//雷霆绽放 Lightning Bloom ID：SCH_427 
                // ){flag9+=1;}
                // if(card==Card.Cards.SW_319//魔法乌鸦 Enchanted Raven ID：SW_319
                // ){flag10+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                 if((card==Card.Cards.BT_258 
                )){
                    {
                       if(!CardsToKeep.Contains(Card.Cards.BT_258))
                    {
                        Keep(card,"挺身而出");
                    }
                    }   
                }

                if(card==Card.Cards.BT_258 
                )
                {
                      
                        Keep(card,"被禁锢的矮劣魔");
                      
                }
                if(card==Card.Cards.SCH_713 
                )
                {
                      
                        Keep(card,"异教低阶牧师");
                      
                }
                if(card==Card.Cards.SCH_137 
                )
                {
                      
                        Keep(card,"疲倦的大一新生");
                      
                }
                if(card==Card.Cards.SW_446 
                )
                {
                      
                        Keep(card,"虚触侍从");
                      
                }
                if(card==Card.Cards.SCH_126 //教导主任加丁 Disciplinarian Gandling ID：SCH_126 
                )
                {
                      
                        Keep(card,"教导主任加丁");
                      
                }
                if(card==Card.Cards.EX1_008 //银色侍从 Argent Squire ID：EX1_008 
                )
                {
                      
                        Keep(card,"银色侍从");
                      
                }
                if(card==Card.Cards.BT_256 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.BT_256))
                    {
                        Keep(card,"龙喉监工");
                    }   
                }
                if(card==Card.Cards.DMF_091 //蠕动的恐魔 Wriggling Horror ID：DMF_091 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.DMF_091))
                    {
                        Keep(card,"蠕动的恐魔");
                    }   
                }
              
              
              
                // if(card==Card.Cards.SW_049 
                // )
                // {
                //        if(!CardsToKeep.Contains(Card.Cards.SW_049))
                //     {
                //         Keep(card,"受祝福的货物");
                //     }   
                // }
              
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