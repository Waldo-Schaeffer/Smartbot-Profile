using System;
using System.Collections.Generic;
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
    public class MidrangeDefault : Profile
    {
        public ProfileParameters GetParameters(Board board)
        {
			var profileparams = new ProfileParameters(BaseProfile.Default);
			profileparams.DiscoverSimulationValueThresholdPercent = 10;
            return profileparams;
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