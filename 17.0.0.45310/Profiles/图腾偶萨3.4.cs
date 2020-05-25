using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
using SmartBotAPI.Plugins.API;

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
            var p = new ProfileParameters(BaseProfile.Rush) {DiscoverSimulationValueThresholdPercent = -10};			  
			if ((board.EnemyAbility.Template.Id == Card.Cards.AT_132_PALADIN
			  || board.EnemyClass == Card.CClass.MAGE
			  || board.EnemyClass == Card.CClass.DRUID)
			   && board.HeroEnemy.CurrentHealth >= 16)
            {
                p = new ProfileParameters(BaseProfile.Default) { DiscoverSimulationValueThresholdPercent = -10 };
            }
			if ((board.EnemyAbility.Template.Id == Card.Cards.AT_132_PALADIN
			  || board.EnemyClass == Card.CClass.MAGE
			  || board.EnemyClass == Card.CClass.DRUID)
			   && board.HeroEnemy.CurrentHealth < 16)
            {
                p = new ProfileParameters(BaseProfile.Rush) {DiscoverSimulationValueThresholdPercent = -10};
            }
        {
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

if ((board.EnemyAbility.Template.Id == Card.Cards.AT_132_PALADIN
			  || board.EnemyClass == Card.CClass.HUNTER
			  || board.EnemyClass == Card.CClass.MAGE
			  || board.EnemyClass == Card.CClass.DEMONHUNTER)
			   && board.HeroEnemy.CurrentHealth >= 16)
            {
                p = new ProfileParameters(BaseProfile.Default) { DiscoverSimulationValueThresholdPercent = -10 };
            }
			if ((board.EnemyAbility.Template.Id == Card.Cards.AT_132_PALADIN
			  || board.EnemyClass == Card.CClass.HUNTER
			  || board.EnemyClass == Card.CClass.MAGE
			  || board.EnemyClass == Card.CClass.DEMONHUNTER)
			   && board.HeroEnemy.CurrentHealth < 16)
            {
                p = new ProfileParameters(BaseProfile.Rush) {DiscoverSimulationValueThresholdPercent = -10};
            }

			if ((board.EnemyClass == Card.CClass.PRIEST
			  || board.EnemyClass == Card.CClass.WARLOCK)
			   && board.HeroEnemy.CurrentHealth <= 15)
            {
                p = new ProfileParameters(BaseProfile.Face) {DiscoverSimulationValueThresholdPercent = -10};
            }



            //遇到弟弟骑或者法师或者德鲁伊且对方血量大于17进入贪比模式

            /*if (board.ManaAvailable >= 7
			   && board.MinionFriend.Count <= 2
			   && board.Hand.Exists(x => x.Template.Id == Card.Cards.BOT_245)
			   )
			{
				p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_049, new Modifier(-9000));
				p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_245, new Modifier(9999));
			}
			*/

            //将硬币法术修正值设置为“Rush”配置文件中定义的基础法术值的50％，AI有更多机会玩这个法术
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.TB_011, new Modifier(50));

            if (
			    board.ManaAvailable >= 4
				&& board.Hand.Exists(x => x.Template.Id == Cards.DraeneiTotemcarver)
				&& !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.NEW1_009))
				&& !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_050))
				&& !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_051))
				&& !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_052))
				&& !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.AT_052))
                 && !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.ULD_276))//怪盗图腾
                )
				{
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.AT_047, new Modifier(100)); //德莱尼图腾师
                    p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_049, new Modifier(55));//如果场上无图腾，降低图腾师的优先级,优先摇图腾
				}
            if (
                    board.ManaAvailable >= 4
                    && board.Hand.Exists(x => x.Template.Id == Cards.SplittingAxe)
                    && !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.NEW1_009))
                    && !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_050))
                    && !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_051))
                    && !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_052))
                    && !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.AT_052))
                     && !(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.ULD_276))//怪盗图腾
                    )
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_413, new Modifier(100)); //德莱尼图腾师
                    p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_049, new Modifier(55));//如果场上无图腾，降低图腾战斧的优先级,优先摇图腾
                }

                if ((board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_004))//如果对面场上有疯狂的科学家
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_022))//如果对面场上有空灵召唤者
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.EX1_007))//如果对面场上有苦痛伺僧
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.YOD_024))//如果对面场上有炸弹牛仔
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DRG_071))//如果对面场上有厄运信天翁
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_197))//如果对面场上有灵魂之匣
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_028))//如果对面场上有星术师索兰莉安
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_019))//如果对面场上有莫戈尔·莫戈尔格
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_309))//如果对面场上有坎雷萨德·埃伯洛克
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_136))//如果对面场上有孢子首领姆希菲
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_210))//如果对面场上有顶级捕食者兹克索尔
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_713))//如果对面场上有阿卡玛
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_109))//如果对面场上有瓦丝琪女士
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_123))//如果对面场上有卡加斯·刃拳
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.LOOT_161))//如果对面场上有食肉魔块
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ICC_214))//如果对面场上有黑曜石雕像
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.LOOT_368))//如果对面场上有虚空领主
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DAL_039))//如果对面场上有无面渗透者
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DAL_558))//如果对面场上有大法师瓦格斯
        || (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BT_255))//如果对面场上有凯尔萨斯·逐日者
       )
                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CFM_696, new Modifier(200));//提高衰变优先级
                }


                if (!board.SecretEnemy)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_254, new Modifier(300));
            }//对面没有奥秘，不下张杰
            if (board.MinionFriend.Any(card => card.Race == Card.CRace.TOTEM)   //场上有怪？怪类型是Totemic？
                && board.MinionFriend.Count >= 2   //场上怪数量大于等于2
                && board.Hand.Exists(x => x.Template.Id == Cards.TotemicMight)//手上有图腾之力
               )
            {
                p.CastSpellsModifiers.AddOrUpdate(Cards.TotemicMight, new Modifier(20));//提高图腾之力优先级
            }

            if ((board.EnemyClass == Card.CClass.PALADIN)|| (board.EnemyClass == Card.CClass.SHAMAN)) //如果敌人是骑士或萨满
            {
                p = new ProfileParameters(BaseProfile.Default);              //则切换为控场模式
            }
            if (board.EnemyClass == Card.CClass.PRIEST)      //如果敌人是牧师                  
            {
                p = new ProfileParameters(BaseProfile.Face);              //则切换为打脸模式
            }
			if (board.EnemyClass == Card.CClass.DEMONHUNTER)      //如果敌人是DH                  
            {
                p = new ProfileParameters(BaseProfile.Default);              //则切换为控场模式
            }

           

            if (!(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_052))
                && board.ManaAvailable >= 2
                && board.Hand.Exists(x => x.Template.Id == Cards.Crackle)
               )
            {
                p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_049, new Modifier(55));//手上有连环爆裂，场上无加法术图腾，提高摇图腾
                                                                                          //p.CastSpellsModifiers.AddOrUpdate(Cards.Crackle, new Modifier(999));
            }

           

            if (!(board.MinionFriend.Exists(minion => minion.Template.Id == Card.Cards.CS2_052))
				&& board.ManaAvailable >= 4
				&& board.Hand.Exists(x => x.Template.Id == Cards.JadeLightning)
			   )
			{
				p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_049, new Modifier(55));//手上有青玉闪电，场上无加法术图腾，提高摇图腾
			    //p.CastSpellsModifiers.AddOrUpdate(Cards.JadeLightning, new Modifier(999));
			}

            /*if (
				 board.ManaAvailable >= 6
				&& (board.EnemyClass == Card.CClass.MAGE)
				&& board.Hand.Exists(x => x.Template.Id == Card.Cards.ICC_701)
			   )
			{ 
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ICC_701, new Modifier(300));//6费，对面是法师，出游荡恶鬼
			}*/

            /*if (
				 board.ManaAvailable >= 6
				&& (board.EnemyClass == Card.CClass.MAGE
				|| board.EnemyClass == Card.CClass.PRIEST
				|| board.EnemyClass == Card.CClass.WARRIOR
				)
				&& board.Hand.Exists(x => x.Template.Id == Card.Cards.ICC_706)
				&& board.Hand.Exists(x => x.Template.Id == Card.Cards.GIL_692)
			   )
			{ 
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ICC_706, new Modifier(-9999));
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GIL_692, new Modifier(9999));//6费，魔蛛提高优先级
			}*/
           
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ULD_240))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_240, new Modifier(9999));
            }//如果对面场上有232，提高攻击优先度
			
			if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_022))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_022, new Modifier(-999));
            }//如果对面场上有空灵，降低攻击优先度
			
			if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_022))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CFM_696, new Modifier(-2500));
            } //如果对面场上有空灵，提高退化优先度
			
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BRM_002))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BRM_002, new Modifier(9999));
            }//如果对面场上有火妖，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.EX1_608))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_608, new Modifier(250));
            }//如果对面场上有哀绿，提高攻击优先度

            if ((board.Hand.Exists(x => x.Template.Id == Card.Cards.TRL_352)
                || (board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.TRL_352) )
                && board.HeroEnemy.CurrentHealth <= 16)
            {
                p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.TRL_352, new Modifier(-1000));
            }//如果对面血量小于等于16，提高武器攻击优先度
            if (board.ManaAvailable <= 4)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.AT_053, new Modifier(9999));
            }//如果费用小于等于4费，不打先祖知识
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.BOT_407, new Modifier(120));//修饰雷云元素
                if (board.OverloadedMana > 0 
                    && board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.GIL_530) 
                    && (board.Hand.Exists(x => x.Template.Id == Card.Cards.TRL_352)
                    || (board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.TRL_352))){
                        p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GIL_530, new Modifier(500));
                    }
                if ((board.Hand.Exists(x => x.Template.Id == Card.Cards.BOT_407)
                    || board.Hand.Exists(x => x.Template.Id == Card.Cards.AT_047) 
                    || board.Hand.Exists(x => x.Template.Id == Card.Cards.OG_024) 
                    || board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_173))
                    && (board.ManaAvailable == 3)
                    && (board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_158))
                    && (board.MinionEnemy.Count == 0))
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_158, new Modifier(9999));
                }//如果3费，不下222
                p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.TRL_352, new Modifier(200));//修饰舔舔魔杖；
                if (board.OverloadedMana > 0)
                {
                    p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.TRL_352, new Modifier(-1000));//舔舔魔杖
                }
                if (board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.ULD_173)//维西纳
                    && (board.OverloadedMana == 0)
                    && (board.Hand.Exists(x => x.Template.Id == Card.Cards.BRM_012))
                    && (board.ManaAvailable >= 4))
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BRM_012, new Modifier(150));//火焰驱逐者
                }
                else if (board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.ULD_173)
                    && board.OverloadedMana == 0
                    && board.Hand.Exists(x => x.Template.Id == Card.Cards.GIL_600))//静电震击
                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_600, new Modifier(200));////静电震击
                }
                else if (board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.ULD_173)
                    && board.OverloadedMana == 0
                    && board.Hand.Exists(x => x.Template.Id == Card.Cards.AT_053))
                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.AT_053, new Modifier(200));
                }
                else if (board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.ULD_173)
                    && board.OverloadedMana == 0
                    && board.Hand.Exists(x => x.Template.Id == Card.Cards.GVG_038)
                    && board.ManaAvailable >= 2)
                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GVG_038, new Modifier(200));
                }
             else if (board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.ULD_173)
                    && board.OverloadedMana == 0
                    && board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_158)
                    && board.ManaAvailable >= 2)
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_158, new Modifier(200));
                }
                else if (board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.ULD_173)
                    && board.OverloadedMana == 0
                    && board.Hand.Exists(x => x.Template.Id == Card.Cards.AT_052)
                    && board.ManaAvailable >= 2)
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.AT_052, new Modifier(150));
                }
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.ULD_276, new Modifier(150));//修饰0/2图腾
                if (board.MinionEnemy.Count == 0)
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_276, new Modifier(-1000));
                }
				if(board.ManaAvailable >= 8  && board.HasCardInHand(Cards.Octosari))
				{
				    p.CastMinionsModifiers.AddOrUpdate(Cards.Octosari, new Modifier(-500));
				}// 八爪巨怪

            if ((board.Hand.Exists(x => x.Template.Id == Card.Cards.TRL_352)
                || (board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.TRL_352) )
                && board.HeroEnemy.CurrentHealth <= 16)
            {
                p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.TRL_352, new Modifier(-1000));
            }//如果对面血量小于等于16，提高武器攻击优先度


			
            if (board.MinionFriend.Any(card => card.Race == Card.CRace.TOTEM)   //如果场上有图腾种族随从
                && board.MinionFriend.Count >= 2   //场上随从数量大于等于2
                && board.Hand.Exists(x => x.Template.Id == Cards.TotemicMight)//手上有法术“图腾之力”
               )
            {
                p.CastSpellsModifiers.AddOrUpdate(Cards.TotemicMight, new Modifier(55));//提高图腾之力优先级
            }

            if (board.MinionFriend.Count >= 4   //场上随从数量大于等于4
                && myAttack <= 5)//场攻小于等于5
            {
                p.CastSpellsModifiers.AddOrUpdate(Cards.TheStormBringer, new Modifier(5));//提高风暴聚合器优先级
            }


                //敌方
                
                //提高流放者奥图里斯威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_937, new Modifier(200));





                return p;	
        }

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