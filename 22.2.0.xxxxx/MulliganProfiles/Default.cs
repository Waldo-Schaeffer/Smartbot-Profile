using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBot.Mulligan
{
    [Serializable]
    public class DefaultMulliganProfile : MulliganProfile
    {
        List<Card.Cards> CardsToKeep = new List<Card.Cards>();

        private readonly List<Card.Cards> WorthySpells = new List<Card.Cards>
        {
            /* --Hunter-- */
            Cards.AnimalCompanion,

            /* --Warrior-- */

            /* --Priest-- */
            Cards.ShadowWordPain,

            /* --Mage-- */
            Cards.Frostbolt,
            Cards.Flamecannon,
            Cards.UnstablePortal,

            /* --Warlock-- */
            Cards.Darkbomb,

            /* --Rogue-- */
            Cards.Backstab,
            Cards.DeadlyPoison,

            /* --Shaman-- */
            Cards.EarthShock,
            Cards.RockbiterWeapon,
            Cards.FeralSpirit,

            /* --Druid-- */
            Cards.LivingRoots,
            Cards.Innervate,
            Cards.PoweroftheWild,
            Cards.Wrath,
            Cards.WildGrowth,

            /* --Paladin-- */
            Cards.DivineStrength,
            Cards.MusterforBattle
             
        };

        public List<Card.Cards> HandleMulligan(List<Card.Cards> choices, Card.CClass opponentClass,
            Card.CClass ownClass)
        {
            var HasCoin = choices.Count >= 4;

            var WorthyOneDrop = choices.Count(IsWorthyOneDrop);
            var WorthyTwoDrop = choices.Count(IsWorthyTwoDrop);
            var WorthyThreeDrop = choices.Count(IsWorthyThreeDrop);

            var MaxOneDrop = HasCoin && WorthyOneDrop == 2 && WorthyTwoDrop >= 1 && WorthyThreeDrop >= 1 ? 2 : 1;

            var MaxTwoDrop = (HasCoin && WorthyOneDrop == 0 && WorthyTwoDrop >= 2) ||
                             (WorthyTwoDrop >= 2 && WorthyThreeDrop == 0 && WorthyOneDrop == 0 && HasCoin)
                ? 2
                : 1;

            var MaxThreeDrop = WorthyTwoDrop > 0 ? 1 : 0;

            var MaxFourDrop = HasCoin && WorthyTwoDrop == 1 && WorthyThreeDrop == 0 ? 1 : 0;

            Log("---Default mulligan logic-----");
            Log(string.Format("Choices : {0}  --  Limits : 1:{1} - 2:{2} - 3:{3} - 4:{4}", string.Join(",",choices),MaxOneDrop,MaxTwoDrop,MaxThreeDrop,MaxFourDrop));

            choices.ForEach(delegate(Card.Cards cardid)
            {
                if (IsWorthyOneDrop(cardid) && CardsToKeep.Count(IsWorthyOneDrop) < MaxOneDrop)
                    Keep(cardid, string.Format("Kept 1 mana cost : {0}", Load(cardid).Name));
                if (IsWorthyTwoDrop(cardid) && CardsToKeep.Count(IsWorthyTwoDrop) < MaxTwoDrop)
                    Keep(cardid, string.Format("Kept 2 mana cost : {0}", Load(cardid).Name));
                if (IsWorthyThreeDrop(cardid) && CardsToKeep.Count(IsWorthyThreeDrop) < MaxThreeDrop)
                    Keep(cardid, string.Format("Kept 3 mana cost : {0}", Load(cardid).Name));
                if (IsWorthyFourDrop(cardid) && CardsToKeep.Count(IsWorthyFourDrop) < MaxFourDrop)
                    Keep(cardid, string.Format("Kept 4 mana cost : {0}", Load(cardid).Name));
            });

            return CardsToKeep;
        }

        private void Keep(Card.Cards id, string log = "")
        {
            CardsToKeep.Add(id);
            if(log != "")
                Log(log);
        }

        private void Log(string log)
        {
            Bot.Log("-- " + log);
        }

        private bool IsWorthyOneDrop(Card.Cards id)
        {
			if (id == Card.Cards.CFM_637)return false;
            if (Load(id).Cost == 1 && WorthySpells.Contains(id)) return true;
            return Load(id).Cost == 1 && HasWorthyStats(id);
        }

        private bool IsWorthyTwoDrop(Card.Cards id)
        {
            if (Load(id).Cost == 2 && WorthySpells.Contains(id)) return true;
            return Load(id).Cost == 2 &&
                   ((Load(id).Type == Card.CType.MINION && (HasWorthyAbility(id) || HasWorthyStats(id))) ||
                    (Load(id).Type == Card.CType.WEAPON));
        }

        private bool IsWorthyThreeDrop(Card.Cards id)
        {
            if (Load(id).Cost == 3 && WorthySpells.Contains(id)) return true;
            return Load(id).Cost == 3 &&
                   ((Load(id).Type == Card.CType.MINION && (HasWorthyAbility(id) || HasWorthyStats(id))) ||
                    (Load(id).Type == Card.CType.WEAPON));
        }

        private bool IsWorthyFourDrop(Card.Cards id)
        {
            return Load(id).Cost == 4 && Load(id).Type == Card.CType.MINION;
        }

        private bool HasWorthyStats(Card.Cards id)
        {
            switch (Load(id).Cost)
            {
                case 1:
                    return Load(id).Atk >= 1;

                case 2:
                    return Load(id).Atk + Load(id).Health >= 4 && Load(id).Health > 1 && Load(id).Atk > 1;

                case 3:
                    return Load(id).Atk + Load(id).Health >= 6 && Load(id).Health > 1 && Load(id).Atk > 1;

                default:
                    return false;
            }
        }

        private bool HasWorthyAbility(Card.Cards id)
        {
            switch (Load(id).Cost)
            {
                case 1:
                    return Load(id).Stealth || Load(id).HasBattlecry || Load(id).HasDeathrattle ||
                           Load(id).Divineshield ||
                           Load(id).Poison;

                case 2:
                    return (Load(id).Stealth || Load(id).HasBattlecry || Load(id).HasDeathrattle ||
                            Load(id).Divineshield ||
                            Load(id).Poison || Load(id).Windfury || Load(id).Charge) && Load(id).Atk > 1 && Load(id).Health > 1;

                case 3:
                    return (Load(id).Stealth || Load(id).HasBattlecry || Load(id).HasDeathrattle ||
                            Load(id).Divineshield ||
                            Load(id).Poison || Load(id).Windfury) &&
                           Load(id).Atk > 1 && Load(id).Health > 1;
                    ;

                default:
                    return false;
            }
        }

        private CardTemplate Load(Card.Cards id)
        {
            return CardTemplate.LoadFromId(id);
        }
    }
}