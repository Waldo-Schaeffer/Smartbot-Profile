using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_AggroOverloadShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Aggro_Overload_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
                Cards.Zap,
                Cards.FireFly,
                Cards.LightningBolt,
                Cards.VoltaicBurst,
                Cards.EarthenMight,
                Cards.FlametongueTotem,
                Cards.Likkim,
                Cards.RockbiterWeapon,
                Cards.ElectraStormsurge,
                Cards.LavaBurst,
                Cards.SpiritoftheFrog,
                Cards.UnboundElemental,
                Cards.Thunderhead,
                Cards.Doomhammer,
                Cards.TotemicSmash,
                Cards.KnifeJuggler,
                Cards.FeralSpirit,
                Cards.KragwatheFrog,
            };
        }
    }
}