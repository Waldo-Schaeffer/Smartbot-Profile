using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WeaponRogue : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_WeaponRogue";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Preparation,
                Cards.DeadlyPoison,
                Cards.Doomerang,
                Cards.Kingsbane,
                Cards.CavernShinyfinder,
                Cards.LeechingPoison,
                Cards.Sap,
                Cards.Toxicologist,
                Cards.CutthroatBuccaneer,
                Cards.FanofKnives,
                Cards.RaidingParty,
                Cards.BladeFlurry,
                Cards.CaptainGreenskin,
                Cards.Vanish,
                Cards.Sprint,
                Cards.ValeeratheHollow,
                Cards.DreadCorsair,
                Cards.WalkthePlank,
            };
        }
    }
}