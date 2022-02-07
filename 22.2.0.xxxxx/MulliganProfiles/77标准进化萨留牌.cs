using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

// ### 进化萨
// # 职业：萨满祭司
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (0) 雷霆绽放 SCH_427
// # 2x (1) 异变轮转 DMF_700
// # 1x (1) 活化扫帚 SCH_311
// # 2x (1) 集合石 WC_028
// # 1x (2) 伯尔纳·锤喙 SW_115
// # 2x (2) 前沿哨所 BAR_074
// # 2x (2) 笼斗管理员 DMF_704
// # 2x (2) 魔杖工匠 SCH_160
// # 2x (3) 原初地下城历险家 WC_005
// # 1x (3) 导师火心 SCH_507
// # 1x (3) 死斗场管理者 DMF_703
// # 1x (3) 锈烂蝰蛇 SW_072
// # 2x (4) 深水炸弹 DMF_701
// # 2x (4) 运河慢步者 SW_033
// # 2x (5) 沼泽拳刺 BT_102
// # 2x (6) 小巧玩具 SW_034
// # 1x (10) 亚煞极，污染之源 DMF_188
// # 2x (10) 闪金镇豺狼人 SW_062
// # 
// AAECAZnDAwaVzQOczgOp3gP83gPQ+QOG+gMM3bgD/tED8NQDpt4Dp94Dqt4D9+gD4+4DpO8DwfYD4/YDxvkDAA==
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

            int flag1=0;//雷霆绽放 SCH_427
            int flag2=0;//集合石 WC_028
            int flag3=0;//伯尔纳·锤喙 SW_115
            int flag4=0;//前沿哨所 BAR_074
            int flag5=0;//笼斗管理员 DMF_704
            int flag6=0;//魔杖工匠 SCH_160
            int flag7=0;//原初地下城历险家 WC_005
            int flag8=0;//小巧玩具 SW_034
            int flag9=0;//异变轮转 DMF_700
            int flag10=0;//闪金镇豺狼人 SW_062

            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SCH_427//雷霆绽放 SCH_427 
                ){flag1+=1;}
                
                if(card==Card.Cards.WC_028//集合石 WC_028 
                ){flag2+=1;}
                
                if(card==Card.Cards.SW_115//伯尔纳·锤喙 SW_115
                ){flag3+=1;}
                
                if(card==Card.Cards.BAR_074 //前沿哨所 BAR_074
                ){flag4+=1;}
                
                if(card==Card.Cards.DMF_704//笼斗管理员 DMF_704
                ){flag5+=1;}

                if(card==Card.Cards.SCH_160//魔杖工匠 SCH_160
                ){flag6+=1;}

                if(card==Card.Cards.WC_005//原初地下城历险家 WC_005
                ){flag7+=1;}
              
                if(card==Card.Cards.SW_034//小巧玩具 SW_034
                ){flag8+=1;}
              
                if(card==Card.Cards.DMF_700//异变轮转 DMF_700
                ){flag9+=1;}
              
                if(card==Card.Cards.SW_062//闪金镇豺狼人 SW_062
                ){flag10+=1;}
                 
            }

            foreach (Card.Cards card in choices)
            {
                 if((card==Card.Cards.SCH_427//雷霆绽放 SCH_427
                )){
                    {
                       if(!CardsToKeep.Contains(Card.Cards.SCH_427) && (HasCoin==true||flag7>=1))
                    {
                        Keep(card,"雷霆绽放");
                    }
                    }   
                }
                 if((card==Card.Cards.SW_034//小巧玩具 SW_034
                )){
                    {
                       if(!CardsToKeep.Contains(Card.Cards.SW_034) && HasCoin==true)
                    {
                        Keep(card,"小巧玩具");
                    }
                    }   
                }
                 if((card==Card.Cards.SW_062//闪金镇豺狼人 SW_062
                )){
                    {
                       if(!CardsToKeep.Contains(Card.Cards.SW_062) && (HasCoin==true||flag2>=1))
                    {
                        Keep(card,"闪金镇豺狼人");
                    }
                    }   
                }

                if(card==Card.Cards.WC_028//集合石 WC_028 
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.WC_028))
                    {
                        Keep(card,"集合石");
                    }
                }
                if(card==Card.Cards.DMF_700//异变轮转 DMF_700
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.DMF_700))
                    {
                        Keep(card,"异变轮转");
                    }
                }
                if(card==Card.Cards.BAR_074//前沿哨所 BAR_074
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.BAR_074))
                    {
                        Keep(card,"前沿哨所");
                    }
                }
                if(card==Card.Cards.DMF_704//笼斗管理员 DMF_704
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.DMF_704))
                    {
                        Keep(card,"笼斗管理员");
                    }
                }
                if(card==Card.Cards.SCH_160//魔杖工匠 SCH_160
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.SCH_160))
                    {
                        Keep(card,"魔杖工匠");
                    }
                }
                if(card==Card.Cards.WC_005//原初地下城历险家 WC_005
                )
                {
                       if(!CardsToKeep.Contains(Card.Cards.WC_005))
                    {
                        Keep(card,"原初地下城历险家");
                    }
                }
                if(card==Card.Cards.SW_031//原初地下城历险家 SW_031
                )
                {
                  
                        Keep(card,"号令元素");
                    
                }
                if(card==Card.Cards.WC_020//原初地下城历险家 SW_031
                )
                {
                  
                        Keep(card,"火");
                    
                }
                // if(card==Card.Cards.SW_115//伯尔纳·锤喙 SW_115
                // )
                // {
                //         Keep(card,"伯尔纳·锤喙");
                // }
          
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