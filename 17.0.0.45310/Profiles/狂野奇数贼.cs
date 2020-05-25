using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MidrangeRush : Profile
    {
        private const Card.Cards SteadyShot = Card.Cards.DS1h_292;
        private const Card.Cards Shapeshift = Card.Cards.CS2_017;
        private const Card.Cards LifeTap = Card.Cards.CS2_056;
        private const Card.Cards Fireblast = Card.Cards.CS2_034;
        private const Card.Cards Reinforce = Card.Cards.CS2_101;
        private const Card.Cards ArmorUp = Card.Cards.CS2_102;
        private const Card.Cards LesserHeal = Card.Cards.CS1h_001;
        private const Card.Cards DaggerMastery = Card.Cards.CS2_083b;

        private readonly Dictionary<Card.Cards, int> _heroPowersPriorityTable = new Dictionary<Card.Cards, int>
        {
            {SteadyShot, 8},
            {Shapeshift, 7},
            {LifeTap, 6},
            {Fireblast, 5},
            {Reinforce, 4},
            {ArmorUp, 3},
            {LesserHeal, 2},
            {DaggerMastery, 1}
        };

        public ProfileParameters GetParameters(Board board)
        {
		        
            var p = new ProfileParameters(BaseProfile.Rush) {DiscoverSimulationValueThresholdPercent = 10};


            int myAttack = 0;
            int enemyAttack = 0;

            if (board.MinionFriend != null)
            {
                for (int x = 0; x < board.MinionFriend.Count; x++)
                {
                    myAttack += board.MinionFriend[x].CurrentAtk;
                }
            }

            if (board.MinionEnemy != null)
            {
                for (int x = 0; x < board.MinionEnemy.Count; x++)
                {
                    enemyAttack += board.MinionEnemy[x].CurrentAtk;
                }
            }

            if (board.WeaponEnemy != null)
            {
                enemyAttack += board.WeaponEnemy.CurrentAtk;
            }

            if ((int)board.EnemyClass == 2 || (int)board.EnemyClass == 7 || (int)board.EnemyClass == 8)
            {
                enemyAttack += 1;
            }
            else if ((int)board.EnemyClass == 6)
            {
                enemyAttack += 2;
            }



            if ((board.EnemyClass == Card.CClass.PALADIN)|| (board.EnemyClass == Card.CClass.SHAMAN)) //如果敌人是骑士或萨满
            {
                p = new ProfileParameters(BaseProfile.Default);              //则切换为控场模式
            }
            if (board.EnemyClass == Card.CClass.PRIEST)      //如果敌人是牧师                  
            {
                p = new ProfileParameters(BaseProfile.Face);              //则切换为打脸模式
            }





            if (board.Hand.Exists(x => x.Template.Id == Cards.VilespineSlayer)  || board.Hand.Exists(x => x.Template.Id == Cards.EVILMiscreant));
                //如果你的手牌有怪盗恶霸或者邪脊刺杀者
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_035t, new Modifier(200));
            //血帆飞贼衍生物价值提高			


            if (board.MaxMana ==2 && (board.EnemyClass != Card.CClass.PALADIN))
			//如果你的水晶=2
			p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.AT_132_ROGUE, new Modifier(-60));
            //武器优先级=-60


		


            if ((board.MinionFriend.Count >= 4)
               && (board.ManaAvailable >= 5)
               && (board.Hand.Exists(x => x.Template.Id == Cards.Loatheb))
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Cards.Loatheb, new Modifier(-50));
            }
            //如果场上多过4个怪，而且手上有洛欧塞布，提高洛欧塞布的优先级


            if ((myAttack >= 5)
               && (board.ManaAvailable >= 6)
               && (board.Hand.Exists(x => x.Template.Id == Cards.Loatheb))
               && (board.MinionEnemy.Count <= 0)
               && (board.HeroEnemy.CurrentHealth - myAttack <= 12)
               && (board.EnemyClass == Card.CClass.PRIEST)
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Cards.Loatheb, new Modifier(-50));
            }
            //如果场攻大于5，而且对面是牧师，提高洛欧塞布的优先级


            if ((board.ManaAvailable >= 9)
               && (board.Hand.Exists(x => x.Template.Id == Cards.Loatheb))
               && (board.EnemyClass == Card.CClass.DRUID)
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Cards.Loatheb, new Modifier(-50));
            }
            //如果费用大于9，而且对面是德鲁伊，提高洛欧塞布的优先级


            if ((myAttack >= 2)
               && (board.ManaAvailable >= 5)
               && (board.Hand.Exists(x => x.Template.Id == Cards.Loatheb))
               && (board.HeroEnemy.CurrentHealth - myAttack <= 12)
               && (board.EnemyClass == Card.CClass.MAGE)
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Cards.Loatheb, new Modifier(-50));
            }
            //如果场攻大于2，而且对面是法师，提高洛欧塞布的优先级

            if ((myAttack >= 5)
               && (board.ManaAvailable >= 5)
               && (board.Hand.Exists(x => x.Template.Id == Cards.Loatheb))
               && (board.HeroEnemy.CurrentHealth - myAttack <= 12)
               && (board.EnemyClass == Card.CClass.WARLOCK)
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Cards.Loatheb, new Modifier(-50));
            }
            //如果场攻大于5，而且对面是术士，提高洛欧塞布的优先级
			
            if ((board.EnemyClass == Card.CClass.MAGE)
               && !(board.WeaponEnemy != null && board.WeaponEnemy.Template.Id == Cards.Aluneth)
               &&  board.HasCardInHand(Cards.HarrisonJones)
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Cards.HarrisonJones, new Modifier(500));
            }
            //打法师对面不下刀我方不下哈里森
			
            int ClanThugCount = board.Hand.Count(card => card.Template.Id == Card.Cards.GIL_534);
			
			//2费 刀是否砍脸
			if (board.MaxMana == 2 && ClanThugCount<2)
		    {
			p.GlobalWeaponsAttackModifier = 80;
			p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.AT_132_ROGUE, new Modifier(60));

			}
			
			if (board.MaxMana == 2 && (!(board.HasCardInHand(Card.Cards.AT_029)|| board.HasCardInHand(Card.Cards.DAL_415))))
			{
			p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(150));
		    }
		
		   
	
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_134, new Modifier(45));//军情7处特工可以不触发连击直接下



            return p;
        }
		

        public Card.Cards SirFinleyChoice(List<Card.Cards> choices)
        {
            var filteredTable = _heroPowersPriorityTable.Where(x => choices.Contains(x.Key)).ToList();
            return filteredTable.First(x => x.Value == filteredTable.Max(y => y.Value)).Key;
        }
		
		public Card.Cards KazakusChoice(List<Card.Cards> choices)
        {
            return choices[0];
        }
    }
}