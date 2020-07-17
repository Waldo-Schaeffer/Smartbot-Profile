using System;
using System.Collections.Generic;
using SmartBot.Database;
using SmartBot.Plugins.API;

/* Explanation on profiles :
 * 
 * All the values defined in profiles are percentage modifiers, it means that it will affect base profile's default values.
 * 
 * Modifiers values can be set within the range [-10000 ; 10000]  (negative modifier has the opposite effect)
 * You can specify targets for the non-global modifiers, these target specific modifers will be added on top of global modifier + modifier for the card (without target)
 * 
 * Total modifier applied =  Global Modifier + Targetless modifier + Target specific modifier
 *
 *
 * GlobalDrawModifier          ---> Modifier applied to card draw value
 * GlobalWeaponsAttackModifier ---> Modifier applied to the value of weapons attacks, the higher it ism the less likely the AI will be to attack with weapons
 * 
 * GlobalCastSpellsModifier    ---> Modifier applied to all spells no matter what they are. The higher is the modifier, the less likely the AI will be to play spells
 * GlobalCastMinionsModifier   ---> Modifier applied to all minions no matter what they are. The higher is the modifier, the less likely the AI will be to play minions
 * 
 * GlobalAggroModifier         ---> Modifier applied to enemy's health value, the higher it is, the more aggressive will be the AI
 * GlobalDefenseModifier       ---> Modifier applied to friendly's health value, the higher it is, the more hp conservative will be the AI
 * 
 * CastSpellsModifiers         ---> You can set individual modifiers to each spells, the higher is the modifier, the less likely the AI will be to play the spell
 * CastMinionsModifiers        ---> You can set individual modifiers to each minions, the higher is the modifier, the less likely the AI will be to play the minion
 * CastHeroPowerModifier       ---> Modifier applied to the heropower, the higher is the modifier, the less likely the AI will be to play it
 *
 * WeaponsAttackModifiers      ---> Modifier applied to the weapons attacks, the higher is the modifier, the less likely the AI will be to attack with it
 *
 * OnBoardFriendlyMinionsValuesModifiers 	---> Modifier applied to friendly minions on board. The higher is the modifier, the more the AI will be conservative with it.
 * OnBoardBoardEnemyMinionsModifiers 		---> Modifier applied to enemy minions on board. The higher is the modifier, the more the AI will consider it as a priority target.
 *
 */


namespace SmartBotProfiles
{
    [Serializable]
    public class SovietArena : Profile
    {
        private double baseProfileEdge = 3.3;

        //幸运币
        private const Card.Cards TheCoin = Card.Cards.GAME_005;

        //猎人
        private const Card.Cards SteadyShot = Card.Cards.HERO_05bp;
        //德鲁伊
        private const Card.Cards Shapeshift = Card.Cards.HERO_06bp;
        //术士
        private const Card.Cards LifeTap = Card.Cards.HERO_07bp;
        //法师
        private const Card.Cards Fireblast = Card.Cards.HERO_08bp;
        //圣骑士
        private const Card.Cards Reinforce = Card.Cards.HERO_04bp;
        //战士
        private const Card.Cards ArmorUp = Card.Cards.HERO_01bp;
        //牧师
        private const Card.Cards LesserHeal = Card.Cards.HERO_09bp;
        //潜行者
        private const Card.Cards DaggerMastery = Card.Cards.HERO_03bp;
        //DH
        private const Card.Cards DemonsBite = Card.Cards.HERO_10bp;
        private const Card.Cards DemonsbiteUp = Card.Cards.HERO_10bp2;

        public ProfileParameters GetParameters(Board board)
        {
            var totalCost = 0;
            var dropCount = new[] {0, 0, 0, 0, 0};
            foreach (var card in board.Deck)
            {
                var cardTemplate = CardTemplate.LoadFromId(card);
                totalCost += cardTemplate.Cost;
                if (cardTemplate.Cost < 5)
                {
                    dropCount[cardTemplate.Cost] += 1;
                }
            }

            var totalEarly = dropCount[0] + dropCount[1] + dropCount[2] + dropCount[3];

            var averageCost = (double) totalCost / 30;
            var rush = (averageCost < baseProfileEdge) || (totalEarly > 20);

            var p = rush ? new ProfileParameters(BaseProfile.Rush) : new ProfileParameters(BaseProfile.Default);
            Bot.Log(String.Format("Average cost: {0:0.00}", averageCost));
            Bot.Log(String.Format("Drops: {0}-{1}-{2}-{3}-{4}", dropCount[0], dropCount[1], dropCount[2], dropCount[3], dropCount[4]));
            Bot.Log("Profile: " + (rush ? "Rush" : "Default"));

            p.DiscoverSimulationValueThresholdPercent = 10;            

            return p;
        }

        public Card.Cards SirFinleyChoice(List<Card.Cards> choices)
        {
            return choices[0];
        }

        public Card.Cards KazakusChoice(List<Card.Cards> choices)
        {
            return choices[0];
        }
    }
}