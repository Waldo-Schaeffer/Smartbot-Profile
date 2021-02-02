using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

//
//NGA精英玩家俱乐部一键生成工具
//https://hs.fbigame.com/
//

//version 0008 for profile 2.0+

/* 
 * 
 * https://github.com/Waldo-Schaeffer
 * https://gitee.com/m586
 * Copyleft 2016 - 2021 SunGuanqi. All Rights Reserved
 * Attribution 4.0 International (Attribution 4.0 International (CC-BY-SA 4.0)
 * 使用时请遵守知识共享署名 4.0 国际许可协议，且不可删除本版权信息
 * 
 */

namespace SmartBotAPI.Plugins.API
{

    public class SMK_YOP_Ramp_Paladin : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_YOP_Ramp_Paladin";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				//核心key卡
				//魔杖工匠、时光巨龙诺兹多姆、高阶修士奥露拉、鱼人为王
				Card.Cards.ULD_716,//鱼人为王 Tip the Scales  ID：ULD_716(法术%)
				Card.Cards.SCH_160,//魔杖工匠 Wandmaker ID：SCH_160(法术%)
				Card.Cards.DRG_309,//时光巨龙诺兹多姆 Nozdormu the Timeless ID：DRG_309(龙!)
				Card.Cards.SCH_141,//高阶修士奥露拉 High Abbess Alura ID：SCH_141(法术%)
				
				//鱼人!卡
				Card.Cards.BT_019,//莫戈尔·莫戈尔格 Murgur Murgurgle  ID：BT_019(鱼人!)
				Card.Cards.YOP_031,//螃蟹骑士 Crabrider ID：YOP_031(鱼人!)
				Card.Cards.DMF_194,//赤鳞驯龙者 Redscale Dragontamer ID：DMF_194(鱼人!)
				Card.Cards.EX1_507,//鱼人领军 Murloc Warleader ID：EX1_507(鱼人!)
				Card.Cards.SCH_707,//鱼人飞骑 Fishy Flyer ID：SCH_707(鱼人!)
				
				Card.Cards.BT_018,//幽光鱼竿 Underlight Angling Rod  ID：BT_018
				
				//恩佐斯卡(元素!、野兽!、龙!、机械!、图腾×、海盗!、恶魔×、鱼人!)
				Card.Cards.BT_155,//废料场巨像 Scrapyard Colossus ID：BT_155(元素!)
				Card.Cards.DMF_070,//暗月兔子 Darkmoon Rabbit ID：DMF_070(野兽!)
				Card.Cards.DMF_073,//暗月飞船 Darkmoon Dirigible ID：DMF_073(机械!腐蚀#)
				Card.Cards.DMF_064,//旋转木马 Carousel Gryphon ID：DMF_064(机械!腐蚀#)
				Card.Cards.DRG_055,//藏宝匪贼 Hoard Pillager  ID：DRG_055(海盗!)
				
				Card.Cards.DMF_532,//马戏团融合怪 Circus Amalgam ID：DMF_532(融合怪!!)
				
				Card.Cards.DMF_002,//恩佐斯，深渊之神 N'Zoth, God of the Deep ID：DMF_002
				
				
				//亚煞极卡(腐蚀#)
				Card.Cards.DMF_163,//狂欢小丑 Carnival Clown ID：DMF_163(腐蚀#)
				Card.Cards.DMF_078,//大力士 Strongman ID：DMF_078(腐蚀#)
				
				Card.Cards.DMF_188,//亚煞极，污染之源 Y'Shaarj, the Defiler ID：DMF_188
				
				//不推荐的卡牌
				Card.Cards.SCH_247,//新生入学 First Day of School ID：SCH_247(法术%)
				
				//工具人
				//希拉斯·暗月、巴罗夫领主、贪婪的书虫、活化扫帚
				Card.Cards.SCH_142,//贪婪的书虫 Voracious Reader  ID：SCH_142
				Card.Cards.SCH_311,//活化扫帚 Animated Broomstick ID：SCH_311
				Card.Cards.SCH_526,//巴罗夫领主 Lord Barov ID：SCH_526
				Card.Cards.DMF_074,//希拉斯·暗月 Silas Darkmoon ID：DMF_074
            };
        }
    }
}