using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_BombWarrior : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Bomb_Warrior";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Card.Cards.EX1_607,//怒火中烧
                Card.Cards.YOD_022,//冒进的艇长
                Card.Cards.EX1_402,//铸甲师
                Card.Cards.DRG_024,//空中悍匪
                Card.Cards.EX1_392,//战斗怒火
                Card.Cards.BT_124,//海盗藏品
                Card.Cards.BT_156,//被禁锢的邪犬
                Card.Cards.ULD_271,//受伤的托维尔人
                Card.Cards.CS2_104,//狂暴
                Card.Cards.DRG_025,//海盗之锚
                Card.Cards.ULD_720,//血誓雇佣兵
                Card.Cards.YOD_024,//炸弹牛仔
                Card.Cards.NEW1_011,//库卡隆精英卫士
                Card.Cards.DAL_063,//圣剑扳手
                Card.Cards.NEW1_024,//绿皮船长
                Card.Cards.DAL_064,//爆破之王砰砰
                Card.Cards.BT_138,//沸血蛮兵
            };
        }
    }
}