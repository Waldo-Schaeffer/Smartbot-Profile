using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ### 任务动物园
// # 职业：术士
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 亡者复生 Raise Dead ID：SCH_514 
// # 2x (1) 纳斯雷兹姆之触 Touch of the Nathrezim ID：SW_090 
// # 2x (1) 烈焰小鬼 Flame Imp ID：CORE_EX1_319 
// # 2x (1) 活化扫帚 Animated Broomstick ID：SCH_311 
// # 1x (1) 恶魔之种 The Demon Seed ID：SW_091
// # 2x (1) 巡游向导 Tour Guide ID：SCH_312 
// # 2x (2) 血缚小鬼 Bloodbound Imp ID：SW_084
// # 1x (2) 坎雷萨德·埃伯洛克 Kanrethad Ebonlocke ID：BT_309 
// # 2x (2) 吸取灵魂 Drain Soul ID：ICC_055 
// # 2x (2) 乱齿土狼 Hecklefang Hyena ID：BAR_745 
// # 2x (2) 不稳定的暗影震爆 Unstable Shadow Blast ID：WC_021 
// # 2x (3) 黑眼 Darkglare ID：BT_307 
// # 2x (3) 赛车回火 Backfire ID：YOP_033 
// # 2x (3) 摇滚堕落者 Man'ari Mosher ID：DMF_111
// # 2x (4) 小刀商贩 Knife Vendor ID：DMF_066 
// # 2x (9) 血肉巨人 Flesh Giant ID：SCH_140 
// #
// AAECAf0GAta5A4T7Aw7LuQOVzQObzQPXzgPB0QOT3gO04QOT5AOU7gOI7wP9+gOD+wOEoATnoAQA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/nIp3gB8P6VKn1lK6vGEt0c/?utm_source=hdt&utm_medium=client&utm_campaign=mulliganguide

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

            int flag1=0;//坎雷萨德·埃伯洛克 Kanrethad Ebonlocke ID：BT_309 
            int flag2=0;//巡游向导 Tour Guide ID：SCH_312 
            int flag3=0;//黑眼 Darkglare ID：BT_307
            int flag4=0;//烈焰小鬼 Flame Imp ID：CORE_EX1_319 
            int flag5=0;//血缚小鬼 Bloodbound Imp ID：SW_084
            int flag6=0;//恶魔之种 The Demon Seed ID：SW_091
            int flag7=0;//亡者复生 Raise Dead ID：SCH_514
            int flag8=0;//精魂狱卒 Spirit Jailer ID：SCH_700 
            int flag9=0;//癫狂的游客 Midway Maniac ID：DMF_114 
            int flag10=0;//癫狂的游客 Midway Maniac ID：DMF_114 


            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SW_091//恶魔之种 The Demon Seed ID：SW_091  
                ){flag1+=1;}
                
                if(card==Card.Cards.BT_309//坎雷萨德·埃伯洛克 Kanrethad Ebonlocke ID：BT_309  
                ){flag2+=1;}
                
                if(card==Card.Cards.SCH_312//巡游向导 Tour Guide ID：SCH_312 
                ){flag3+=1;}
                
                if(card==Card.Cards.BT_307 //黑眼 Darkglare ID：BT_307   
                ){flag4+=1;}
                
                if(card==Card.Cards.CORE_EX1_319//烈焰小鬼 Flame Imp ID：CORE_EX1_319
                ){flag5+=1;}

                if(card==Card.Cards.SW_084//血缚小鬼 Bloodbound Imp ID：SW_084 
                ){flag6+=1;}
                if(card==Card.Cards.SCH_514//亡者复生 Raise Dead ID：SCH_514
                ){flag7+=1;}
                if(card==Card.Cards.SCH_700//精魂狱卒 Spirit Jailer ID：SCH_700 
                ){flag8+=1;}
                if(card==Card.Cards.DMF_114
                ){flag9+=1;}
                if(card==Card.Cards.BAR_745//乱齿土狼 He才开了房H也拿IDB乱齿土狼 Hecklefang Hyena ID：BAR_745 
                ){flag10+=1;}
            }

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SCH_312//巡游向导 Tour Guide ID：SCH_312 
                )
                {
                    Keep(card,"巡游向导 ");
                }
                if(card==Card.Cards.BT_309//坎雷萨德·埃伯洛克 Kanrethad Ebonlocke ID：BT_309 
                )
                {
                    Keep(card,"坎雷萨德·埃伯洛克 ");
                }
                if(card==Card.Cards.SW_091//恶魔之种 The Demon Seed ID：SW_091  
                )
                {
                     Keep(card,"恶魔之种 ");
                }
                if(card==Card.Cards.BT_307//黑眼 Darkglare ID：BT_307 
                )
                {
                    if(!CardsToKeep.Contains(Card.Cards.BT_307)&&flag1+flag2+flag4+flag7+flag8+flag9+flag10>=2)
                    {
                        Keep(card,"黑眼 ");
                    }   
                }
                if(card==Card.Cards.CORE_EX1_319//烈焰小鬼 Flame Imp ID：CORE_EX1_319
                )
                {
                    Keep(card,"烈焰小鬼 ");
                }
                if(card==Card.Cards.SCH_700//精魂狱卒 Spirit Jailer ID：SCH_700 
                )
                {
                    Keep(card,"精魂狱卒 ");
                }
              
                if(card==Card.Cards.SW_084//血缚小鬼 Bloodbound Imp ID：SW_084 
                )
                {
                   if(!CardsToKeep.Contains(Card.Cards.SW_084))
                    {
                        Keep(card,"血缚小鬼");
                    }
                }
                if(card==Card.Cards.BAR_745//血缚小鬼 Bloodbound Imp ID：BAR_745 
                )
                {
                   if(!CardsToKeep.Contains(Card.Cards.BAR_745))
                    {
                        Keep(card,"土狼");
                    }
                }
                if(card==Card.Cards.DMF_114//血缚小鬼 Bloodbound Imp ID：DMF_114 
                )
                {
                   if(!CardsToKeep.Contains(Card.Cards.DMF_114))
                    {
                        Keep(card,"游客");
                    }
                }
                if(card==Card.Cards.YOP_033//血缚小鬼 Bloodbound Imp ID：DMF_114回火 Backfire ID：YOP_033  
                )
                {
                   if(!CardsToKeep.Contains(Card.Cards.YOP_033))
                    {
                        Keep(card,"赛车回火");
                    }
                }
                // if(card==Card.Cards.WC_005//原初地下城历险家 Primal Dungeoneer ID：WC_005 
                // )
                // {
                //     if(!CardsToKeep.Contains(Card.Cards.WC_005)&&flag1+flag2>=1)
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

                // if(card==Card.Cards.CORE_EX1_319//笼斗管理员 Cagematch Custodian ID：CORE_EX1_319 
                // ){
                //     if(!CardsToKeep.Contains(Card.Cards.CORE_EX1_319)
                //        && HasCoin==true)
                //     {
                //         Keep(card,"后手留一张莫戈尔·莫戈尔格 Murgur Murgurgle");
                //     }
                //     else
                //     {
                //         if(!CardsToKeep.Contains(Card.Cards.CORE_EX1_319)
                //         && flag1+flag2+flag3>=1
                //         )
                //         {
                //         Keep(card,"先手有1费留一张莫戈尔·莫戈尔格 Murgur Murgurgle");
                //         } 
                //     }  //笼斗管理员 Cagematch Custodian ID：CORE_EX1_319 
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