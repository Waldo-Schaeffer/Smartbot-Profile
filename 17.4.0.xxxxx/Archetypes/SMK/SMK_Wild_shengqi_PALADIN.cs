using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_shengqi_PALADIN : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_shengqi_PALADIN";
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
            };
        }
    }
}