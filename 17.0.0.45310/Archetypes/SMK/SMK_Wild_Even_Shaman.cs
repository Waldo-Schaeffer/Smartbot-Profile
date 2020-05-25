using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildEvenShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Even_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.TotemicSurge,//图腾潮涌
                Cards.Zap,//静电震击
                Cards.Crackle,//连环爆裂
                Cards.Devolve,//衰变
                Cards.EVILTotem,//怪盗图腾
                Cards.Likkim,//舔舔魔杖
                Cards.SandstormElemental,//沙暴元素
                Cards.Thunderhead,//雷云元素
                Cards.Vessina,//维西纳
                Cards.Squallhunter,//猎风巨龙
                Cards.JadeClaws,//青玉之爪
                Cards.MaelstromPortal,//大漩涡传送门
                Cards.MurksparkEel,//阴燃电鳗
                Cards.TotemGolem,//图腾魔像
                Cards.FlamewreathedFaceless,//无面投火者
                Cards.JadeLightning,//青玉闪电
                Cards.ThingfromBelow,//深渊魔物
                Cards.DireWolfAlpha,//恐狼前锋
                Cards.KnifeJuggler,//飞刀杂耍者
                Cards.PilotedShredder,//载人收割机
                Cards.Spellbreaker,//破法者
                Cards.AyaBlackpaw,//艾雅·黑掌
                Cards.GennGreymane,//吉恩·格雷迈恩
                Cards.SeaGiant,//海巨人
                Cards.GolakkaCrawler,//格卡拉爬行蟹
                Cards.EaterofSecrets,//奥秘吞噬者
                Cards.BloodmageThalnos,//血法师萨尔诺斯
                Cards.ArgentCommander,//银色指挥官
                Cards.WhirlingZapomatic,
                Cards.AncestralKnowledge,//先祖知识
                Cards.HagathatheWitch,
                Cards.SaroniteChainGang,//萨隆苦囚
                Cards.FireElemental,//火元素
                Cards.AlAkirtheWindlord,
                Cards.FireguardDestroyer,//火焰驱逐者
                Cards.EarthenMight,//大地之力
                 Cards.SerpentWard,//毒蛇守卫
                 Cards.Octosari,//八爪巨怪

            };
        }
    }
}