using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

//
//NGA精英玩家俱乐部一键生成工具
//https://hs.fbigame.com/
//

//version 0003 for profile 1.1+

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

    public class SMK_7shengqi : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_7shengqi";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
               Card.Cards.BT_020,//奥尔多侍从 Aldor Attendant  ID：BT_020
               Card.Cards.ICC_038,//正义保护者 Righteous Protector  ID：ICC_038
               Card.Cards.BOT_909,//水晶学 Crystology  ID：BOT_909
               Card.Cards.GVG_058,//护盾机器人 Shielded Minibot  ID：GVG_058
               Card.Cards.BT_025,//智慧圣契 Libram of Wisdom  ID：BT_025
               Card.Cards.LOOT_088,//英勇药水 Potion of Heroism  ID：LOOT_088
               Card.Cards.BT_292,//阿达尔之手 Hand of A'dal  ID：BT_292
               Card.Cards.GVG_061,//作战动员 Muster for Battle  ID：GVG_061
               Card.Cards.DRG_232,//光铸狂热者 Lightforged Zealot  ID：DRG_232
               Card.Cards.CS2_093,//奉献 Consecration  ID：CS2_093
               Card.Cards.BT_026,//奥尔多真理追寻者 Aldor Truthseeker  ID：BT_026
               Card.Cards.BT_011,//正义圣契 Libram of Justice  ID：BT_011
               Card.Cards.UNG_952,//剑龙骑术 Spikeridged Steed  ID：UNG_952
               Card.Cards.UNG_015,//守日者塔林姆 Sunkeeper Tarim  ID：UNG_015
               Card.Cards.BT_334,//女伯爵莉亚德琳 Lady Liadrin  ID：BT_334
               Card.Cards.LOOT_216,//莱妮莎·炎伤 Lynessa Sunsorrow  ID：LOOT_216
               Card.Cards.BT_024,//希望圣契 Libram of Hope  ID：BT_024
               Card.Cards.SCH_247,//新生入学 First Day of School  ID：SCH_247
               Card.Cards.BT_019,//莫戈尔·莫戈尔格 Murgur Murgurgle  ID：BT_019
               Card.Cards.YOD_010,//炮火机甲 Shotbot  ID：YOD_010
               Card.Cards.SCH_149,//银色自大狂 Argent Braggart  ID：SCH_149
               Card.Cards.SCH_532,//双盾优等生 Goody Two-Shields  ID：SCH_532
               Card.Cards.SCH_141,//高阶修士奥露拉 High Abbess Alura  ID：SCH_141
               Card.Cards.SCH_138,//威能祝福 Blessing of Authority  ID：SCH_138
               Card.Cards.ULD_143,//法老祝福 Pharaoh's Blessing  ID：ULD_143
               Card.Cards.SCH_139,//虔诚的学徒 Devout Pupil  ID：SCH_139
               Card.Cards.DRG_231,//光铸远征军 Lightforged Crusader  ID：DRG_231
               Card.Cards.BT_018,//幽光鱼竿 Underlight Angling Rod  ID：BT_018
               Card.Cards.CS2_092,//王者祝福 Blessing of Kings  ID：CS2_092
               Card.Cards.DMF_238,//纳鲁之锤 Hammer of the Naaru ID：DMF_238
            };
        }
    }
}