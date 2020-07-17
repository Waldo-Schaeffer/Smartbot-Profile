using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_Wild_haidao_WARRIOR : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_haidao_WARRIOR";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Card.Cards.EX1_409,//升级 Upgrade!  ID：EX1_409
				Card.Cards.CS2_146,//南海船工 Southsea Deckhand  ID：CS2_146
                Card.Cards.OG_312,//恩佐斯的副官 N'Zoth's First Mate  ID：OG_312
                Card.Cards.CFM_637,//海盗帕奇斯 Patches the Pirate  ID：CFM_637
                Card.Cards.DRG_024,//空中悍匪 Sky Raider  ID：DRG_024
                Card.Cards.LOE_076,//芬利·莫格顿爵士 Sir Finley Mrrgglton  ID：LOE_076
                Card.Cards.BT_124,//海盗藏品 Corsair Cache  ID：BT_124
                Card.Cards.DRG_056,//空降歹徒 Parachute Brigand  ID：DRG_056
                Card.Cards.GVG_075,//船载火炮
                Card.Cards.TRL_507,//鲨鳍后援
                Card.Cards.NEW1_018,//血帆袭击者 Bloodsail Raider  ID：NEW1_018
                Card.Cards.NEW1_022,//恐怖海盗
                Card.Cards.NEW1_027,//南海船长 
                Card.Cards.DRG_025,//海盗之锚 Ancharrr  ID：DRG_025
                Card.Cards.DRG_023,//空中炮艇 Skybarge  ID：DRG_023
                Card.Cards.OG_315,//血帆教徒 Bloodsail Cultist  ID：OG_315
                Card.Cards.DAL_063,//圣剑扳手 Wrenchcalibur  ID：DAL_063
                Card.Cards.NEW1_011,//库卡隆精英卫士 Kor'kron Elite  ID：NEW1_011
	            Card.Cards.NEW1_022,//恐怖海盗 Dread Corsair  ID：NEW1_022
                Card.Cards.EX1_116,//火车王里诺艾 Leeroy Jenkins  ID：EX1_116
                Card.Cards.BT_737,//玛维·影歌 Maiev Shadowsong  ID：BT_737
                Card.Cards.ULD_177,//八爪巨怪 Octosari  ID：ULD_177
                Card.Cards.DRG_055,//藏宝匪贼 Hoard Pillager  ID：DRG_055
                Card.Cards.BOT_511,//爆盐投弹手 Seaforium Bomber  ID：BOT_511
                Card.Cards.DAL_060,//发条地精 Clockwork Goblin  ID：DAL_060

            };
        }
    }
}