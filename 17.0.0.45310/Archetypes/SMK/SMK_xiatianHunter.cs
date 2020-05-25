using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_xiatianHunter : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_xiatianHunter";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {

Cards.Springpaw,//魔泉山猫
Cards.BlazingBattlemage,//灼光战斗法师
Cards.DwarvenSharpshooter,//矮人神射手
Cards.FaerieDragon,//精灵龙
Cards.FiretreeWitchdoctor,//火树巫医
Cards.CorrosiveBreath,//腐蚀吐息
Cards.TastyFlyfish,//美味飞鱼
Cards.Stormhammer,//风暴之锤
Cards.PrimordialExplorer,//始生龙探险者
Cards.Scalerider,//锐鳞骑士
Cards.Dragonbane,//灭龙弩炮
Cards.EvasiveFeywing,//辟法灵龙
Cards.LeeroyJenkins,//火车王里诺艾
Cards.BigOlWhelp,//雏龙巨婴
Cards.Veranus,//维拉努斯
Cards.RatTrap,//捕鼠陷阱
Cards.ExplosiveTrap,//爆炸陷阱
Cards.PhaseStalker,//相位追猎者
Cards.UnleashtheHounds,//关门放狗
Cards.Lifedrinker,//吸血蚊
Cards.Crystallizer,//晶化师
Cards.AnimalCompanion,//动物伙伴?
Cards.FrizzKindleroost,//弗瑞兹·光巢?
Cards.EvasiveWyrm,//辟法巨龙?
Cards.EvasiveDrakonid,//辟法龙人?





        };
    }
}
}