using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_GalakrondlonggeWarrior : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_GalakrondlonggeWarrior";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.EterniumRover,//恒金巡游者
                Cards.TownCrier,//城镇公告员
                Cards.RitualChopper,//仪式斩斧
                Cards.AcolyteofPain,//苦痛侍僧
                Cards.Awaken,//祈求觉醒
                Cards.KorkronElite,//库卡隆精英卫士
                Cards.TempleBerserker,//神殿狂战士
                Cards.DragonBreeder,//幼龙饲养员
                Cards.ScionofRuin,//废墟之子
                Cards.DevotedManiac,//虔信狂徒
                Cards.LeeroyJenkins,//火车王里诺艾
                Cards.ShieldofGalakrond,//迦拉克隆之盾
                Cards.KronxDragonhoof,//克罗斯·龙蹄
                Cards.GalakrondtheUnbreakable,//无敌巨龙迦拉克隆
                Cards.InnerRage,//怒火中烧
                Cards.Whirlwind,//旋风斩
                Cards.Armorsmith,//铸甲师
                Cards.BattleRage,//战斗怒火
                Cards.BloodswornMercenary,//血誓雇佣兵
                Cards.EVILQuartermaster,//怪盗军需官
                Cards.LivewireLance,//电缆长枪
                Cards.PlagueofWrath,//愤怒之灾祸
                Cards.Zilliax,//奇利亚斯
            };
        }
    }
}