using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_HistoryWarlock : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_History_Warlock";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.AbusiveSergeant,   //叫嚣的中士
                Cards.BeamingSidekick,   //欢快的同伴
                Cards.BlazingBattlemage, //灼光战斗法师
                //Cards.FiendishServant,   //邪魔仆从
                Cards.FlameImp,          //烈焰小鬼
                Cards.HotAirBalloon,     //热气球
                Cards.Mecharoo,          //机械袋鼠
                Cards.TheSoularium,      //莫瑞干的灵界
                Cards.Voidwalker,        //虚空行者
                Cards.DireWolfAlpha,     //恐狼前锋
                Cards.EVILGenius,        //怪盗天才
                Cards.HistoryBuff,       //历史爱好者
                Cards.MagicCarpet,       //魔法飞毯
                Cards.SN1PSN4P,          //大闸蟹
                //Cards.FrenziedFelwing,   //狂暴邪翼蝠
                Cards.FacelessCorruptor, //无面腐蚀者
                Cards.Zilliax,           //奇利亚斯
                Cards.SeaGiant           //海巨人
            };
        }
    }
}