using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_TokenDruid : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Token_Druid";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Acornbearer,
                Cards.CrystalsongPortal,
                Cards.DreamwayGuardians,
                Cards.KeeperStalladris,
                Cards.PoweroftheWild,
                Cards.Wrath,
                Cards.BlessingoftheAncients,
                Cards.Landscaping,
                Cards.SavageRoar,
                Cards.ArchmageVargoth,
                Cards.SouloftheForest,
                Cards.Swipe,
                Cards.WisperingWoods,
                Cards.TheForestsAid,
                Cards.EVILCableRat,
                Cards.MarkoftheLoa,

                Cards.ArgentSquire,
                Cards.DireWolfAlpha,
                Cards.HenchClanHogsteed,
                Cards.MicrotechController,
                Cards.EccentricScribe,
                Cards.SeaGiant
            };
        }
    }
}