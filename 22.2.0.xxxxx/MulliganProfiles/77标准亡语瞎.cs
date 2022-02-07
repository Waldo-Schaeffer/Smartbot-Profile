using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
// ### 亡语瞎
// # 职业：恶魔猎手
// # 模式：标准模式
// # 狮鹫年
// #
// # 2x (1) 獠牙锥刃 BAR_330
// # 2x (1) 伊利达雷研习 YOP_001
// # 2x (2) 异教低阶牧师 SCH_713
// # 2x (2) 前沿哨所 BAR_074 
// # 2x (2) 剃刀野猪 BAR_325
// # 1x (3) 魔钢处决者 DMF_248 
// # 1x (3) 邪能响尾蛇 WC_701 
// # 2x (3) 吞噬软浆怪 WC_027 
// # 2x (3) 剃刀沼泽兽王 BAR_326
// # 2x (3) 亡首教徒
// # 2x (4) 知名表演者
// # 1x (4) 暴怒邪吼者
// # 2x (4) 固执的商贩
// # 1x (4) 凯恩·日怒 BT_187
// # 1x (5) 泰兰·弗丁
// # 2x (6) 古尔丹之颅
// # 1x (7) 亡语者布莱克松 BAR_329
// # 2x (8) 伊利达雷审判官
// #
// AAECAea5AwbaxgPX3gO/7QOv7wOoigTUnwQM2cYDi9UDyd0D8+MD9+gDmOoDu+0DvO0D/e0DqO8Di/cDgIUEAA==
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/QKgPAiCJInBxpWPTf8Gq2/


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

            int flag1=0;//棱彩珠宝工具 SW_048 
           
            
            foreach (Card.Cards card in choices)
            {
                if(card==Card.Cards.SW_048//棱彩珠宝工具 SW_048 
                ){flag1+=1;}
                if(card==Card.Cards.SW_315//联盟旗手 SW_315 
                ){flag1+=1;}
               
            }

            foreach (Card.Cards card in choices)
            {
               if(card==Card.Cards.SW_450// 巫师的计策 SW_450 

                ){ 
                        Keep(card,"巫师的计策");
                    
                }
                if(card==Card.Cards.BAR_330// 獠牙锥刃 BAR_330
                ){
                  if(!CardsToKeep.Contains(Card.Cards.BAR_330))
                    {
                        Keep(card,"獠牙锥刃");
                    } 
                }
                if(card==Card.Cards.YOP_001// 伊利达雷研习 YOP_001
                ){
                  if(!CardsToKeep.Contains(Card.Cards.YOP_001))
                    {
                        Keep(card,"伊利达雷研习");
                    } 
                }
                if(card==Card.Cards.SCH_713// 异教低阶牧师 SCH_713
                ){
                  if(!CardsToKeep.Contains(Card.Cards.SCH_713))
                    {
                        Keep(card,"异教低阶牧师");
                    } 
                }
                if(card==Card.Cards.BAR_074// 前沿哨所 BAR_074 
                ){
                  if(!CardsToKeep.Contains(Card.Cards.BAR_074))
                    {
                        Keep(card,"前沿哨所");
                    } 
                }
                if(card==Card.Cards.BAR_325//剃刀野猪 BAR_325
                ){
                  if(!CardsToKeep.Contains(Card.Cards.BAR_325))
                    {
                        Keep(card,"剃刀野猪");
                    } 
                }
                if(card==Card.Cards.WC_701//邪能响尾蛇 WC_701
                ){
                  if(!CardsToKeep.Contains(Card.Cards.WC_701))
                    {
                        Keep(card,"邪能响尾蛇");
                    } 
                }
                if(card==Card.Cards.WC_027//吞噬软浆怪 WC_027
                ){
                  if(!CardsToKeep.Contains(Card.Cards.WC_027))
                    {
                        Keep(card,"吞噬软浆怪");
                    } 
                }
                if(card==Card.Cards.BAR_326//剃刀沼泽兽王 BAR_326
                ){
                  if(!CardsToKeep.Contains(Card.Cards.BAR_326))
                    {
                        Keep(card,"剃刀沼泽兽王");
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