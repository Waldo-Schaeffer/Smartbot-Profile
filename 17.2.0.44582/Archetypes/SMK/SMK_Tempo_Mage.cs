using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_TempoMage : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Tempo_Mage";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.ArcaneMissiles,
                Cards.ManaWyrm,
                Cards.Arcanologist,
                Cards.Frostbolt,
                Cards.PrimordialGlyph,
                Cards.SorcerersApprentice,
                Cards.ArcaneIntellect,
                Cards.Counterspell,
                Cards.ExplosiveRunes,
                Cards.KirinTorMage,
                Cards.Fireball,
                Cards.Aluneth,
                Cards.Pyroblast,
                Cards.BreathofSindragosa,
                Cards.MirrorImage,
                Cards.Cinderstorm,
                Cards.VexCrow,
                Cards.ArchmageAntonidas,
            };
        }
    }
}