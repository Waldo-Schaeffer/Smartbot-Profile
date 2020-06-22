using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildSecretMage : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Secret_Mage";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.KabalLackey,				//暗金教侍从
                Cards.MedivhsValet,				//麦迪文的男仆
                Cards.AncientMysteries,			//远古谜团
                Cards.Arcanologist,				//秘法学家
                Cards.MadScientist,				//疯狂的科学家
                Cards.ArcaneFlakmage,			//对空奥术法师
                Cards.MirrorEntity,				//镜像实体
                Cards.KirinTorMage,				//肯瑞托法师
                Cards.ForgottenTorch,			//老旧的火把
                Cards.ExplosiveRunes,			//爆炸符文
                Cards.FlameWard,				//火焰结界
                Cards.Counterspell,				//法术反制
                Cards.IceBlock,					//寒冰屏障
                Cards.PolymorphBoar,			//变形术：野猪
                Cards.Fireball,					//火球术
                Cards.CloudPrince,				//云雾王子
                Cards.Aluneth,					//艾露尼斯
                Cards.KabalCrystalRunner,		//暗金教水晶侍女

                //其他
                Cards.Secretkeeper,				//奥秘守护者
				Cards.StargazerLuna,            //观星者露娜
                Cards.Duplicate,				//复制
                Cards.PotionofPolymorph,		//变羊药水
                Cards.ManaBind,					//法术共鸣
                Cards.Vaporize,					//蒸发
                Cards.Spellbender,				//扰咒
                Cards.SplittingImage,			//裂魂残像
                Cards.Effigy					//轮回
            };
        }
    }
}