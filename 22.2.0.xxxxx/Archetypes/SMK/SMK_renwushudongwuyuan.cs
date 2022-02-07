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
// # 2x (0) 亡者复生 Raise Dead      SCH_514 
// # 2x (1) 纳斯雷兹姆之触 Touch of the Nathrezim      SW_090 
// # 2x (1) 烈焰小鬼 Flame Imp      EX1_319 
// # 2x (1) 活化扫帚 Animated Broomstick      SCH_311 
// # 1x (1) 恶魔之种 The Demon Seed      SW_091
// # 2x (1) 巡游向导 Tour Guide      SCH_312 
// # 2x (2) 血缚小鬼 Bloodbound Imp      SW_084
// # 1x (2) 坎雷萨德·埃伯洛克 Kanrethad Ebonlocke      BT_309 
// # 2x (2) 吸取灵魂 Drain Soul      ICC_055 
// # 2x (2) 乱齿土狼 Hecklefang Hyena      BAR_745 
// # 2x (2) 不稳定的暗影震爆 Unstable Shadow Blast      WC_021 
// # 2x (3) 黑眼 Darkglare      BT_307 
// # 2x (3) 赛车回火 Backfire      YOP_033 
// # 2x (3) 摇滚堕落者 Man'ari Mosher      DMF_111
// # 2x (4) 小刀商贩 Knife Vendor      DMF_066 
// # 2x (9) 血肉巨人 Flesh Giant      SCH_140 
// #
// AAECAf0GAta5A4T7Aw7LuQOVzQObzQPXzgPB0QOT3gO04QOT5AOU7gOI7wP9+gOD+wOEoATnoAQA
// # 想要使用这副套牌，请先复制到剪贴板，再在游戏中创建新套牌。
// # 套牌详情请查看https://hsreplay.net/decks/nIp3gB8P6VKn1lK6vGEt0c/?utm_source=hdt&utm_medium=client&utm_campaign=mulliganguide

namespace SmartBotAPI.Plugins.API
{

    public class SMK_renwushudongwuyuan : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_renwushudongwuyuan";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				        Card.Cards.SCH_514,//亡者复生 Raise Dead      SCH_514 
                Card.Cards.SW_090,//纳斯雷兹姆之触 Touch of the Nathrezim      SW_090 
                Card.Cards.EX1_319,//烈焰小鬼 Flame Imp      EX1_319 
                Card.Cards.SCH_311,//活化扫帚 Animated Broomstick      SCH_311 
                Card.Cards.SW_091,//恶魔之种 The Demon Seed      SW_091 
                Card.Cards.SCH_312,//巡游向导 Tour Guide      SCH_312 
                Card.Cards.SW_084,//血缚小鬼 Bloodbound Imp      SW_084
                Card.Cards.BT_309,//坎雷萨德·埃伯洛克 Kanrethad Ebonlocke      BT_309 
                Card.Cards.ICC_055,//吸取灵魂 Drain Soul      ICC_055
                Card.Cards.BAR_745,//乱齿土狼 Hecklefang Hyena      BAR_745 
	            Card.Cards.WC_021,//不稳定的暗影震爆 Unstable Shadow Blast      WC_021  
                Card.Cards.BT_307,//黑眼 Darkglare      BT_307
                Card.Cards.YOP_033,//赛车回火 Backfire      YOP_033 
                Card.Cards.DMF_111,//摇滚堕落者 Man'ari Mosher      DMF_111
                Card.Cards.DMF_066,// 小刀商贩 Knife Vendor      DMF_066 
                Card.Cards.SCH_140,//血肉巨人 Flesh Giant      SCH_140 
            };
        }
    }
}