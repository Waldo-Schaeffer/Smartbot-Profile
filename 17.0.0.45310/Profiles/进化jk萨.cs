using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
using SmartBotAPI.Plugins.API;

/* Explanation on profiles :
 * 
 * 配置文件中定义的所有值都是百分比修饰符，这意味着它将影响基本配置文件的默认值。
 * 
 * 修饰符值可以在[-10000;范围内设置。 10000]（负修饰符有相反的效果）
 * 您可以为非全局修改器指定目标，这些目标特定修改器将添加到卡的全局修改器+修改器之上（无目标）
 * 
 * 应用的总修改器=全局修改器+无目标修改器+目标特定修改器
 * 
 * GlobalDrawModifier --->修改器应用于卡片绘制值
 * GlobalWeaponsAttackModifier --->修改器适用于武器攻击的价值，它越高，人工智能攻击武器的可能性就越小
 * 
 * GlobalCastSpellsModifier --->修改器适用于所有法术，无论它们是什么。修饰符越高，AI玩法术的可能性就越小
 * GlobalCastMinionsModifier --->修改器适用于所有仆从，无论它们是什么。修饰符越高，AI玩仆从的可能性就越小
 * 
 * GlobalAggroModifier --->修改器适用于敌人的健康值，越高越好，人工智能就越激进
 * GlobalDefenseModifier --->修饰符应用于友方的健康值，越高，hp保守的将是AI
 * 
 * CastSpellsModifiers --->你可以为每个法术设置个别修饰符，修饰符越高，AI玩法术的可能性越小
 * CastMinionsModifiers --->你可以为每个小兵设置单独的修饰符，修饰符越高，AI玩仆从的可能性越小
 * CastHeroPowerModifier --->修饰符应用于heropower，修饰符越高，AI玩它的可能性就越小
 * 
 * WeaponsAttackModifiers --->适用于武器攻击的修饰符，修饰符越高，AI攻击它的可能性越小
 * 
 * OnBoardFriendlyMinionsValuesModifiers --->修改器适用于船上友好的奴才。修饰语越高，AI就越保守。
 * OnBoardBoardEnemyMinionsModifiers --->修改器适用于船上的敌人。修饰符越高，AI就越会将其视为优先目标。
 *
 */

namespace SmartBotProfiles
{
    [Serializable]
    public class WildSecretMage : Profile
    {
        //幸运币
        private const Card.Cards TheCoin = Card.Cards.GAME_005;

        //猎人
        private const Card.Cards SteadyShot = Card.Cards.DS1h_292;
        //德鲁伊
        private const Card.Cards Shapeshift = Card.Cards.CS2_017;
        //术士
        private const Card.Cards LifeTap = Card.Cards.CS2_056;
        //法师
        private const Card.Cards Fireblast = Card.Cards.CS2_034;
        //圣骑士
        private const Card.Cards Reinforce = Card.Cards.CS2_101;
        //战士
        private const Card.Cards ArmorUp = Card.Cards.CS2_102;
        //牧师
        private const Card.Cards LesserHeal = Card.Cards.CS1h_001;
        //潜行者
        private const Card.Cards DaggerMastery = Card.Cards.CS2_083b;
		private Board _board;
        private bool _coin;

        //英雄能力优先级
        private readonly Dictionary<Card.Cards, int> _heroPowersPriorityTable = new Dictionary<Card.Cards, int>
        {
            {SteadyShot, 8},
            {LifeTap, 7},
            {DaggerMastery, 6},
            {Reinforce, 5},
            {Shapeshift, 4},
            {Fireblast, 3},
            {ArmorUp, 2},
            {LesserHeal, 1}
        };
		
		
		
        //直伤法术伤害表
        private static readonly Dictionary<Card.Cards, int> _spellDamagesTable = new Dictionary<Card.Cards, int>
        {
            {Card.Cards.LOE_002, 3},
            {Card.Cards.LOE_002t, 6},
            {Card.Cards.CS2_029, 6},
        };

        //具体策略
        public ProfileParameters GetParameters(Board board)
        {
			
        var p = new ProfileParameters(BaseProfile.Rush);
        p.DiscoverSimulationValueThresholdPercent = 10;
		
		
		//进化兔子
		if (!board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_169))
		{	
		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DAL_071, new Modifier(60, Card.Cards.ULD_719));
		}
		
		//龙族跟班
		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_052, new Modifier(80));//
		
		p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_255, new Modifier(180));
		
		//龙虾人
		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_106, new Modifier(130));//
		
		//wq 待调整
		p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.BT_102, new Modifier(1050));
		
		//恐怖海盗
		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_022, new Modifier(70));//
		
		if (board.MinionFriend.Count > 5)
		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_719, new Modifier(170));
		
		//跳图腾
		if (board.MaxMana == 1&& board.MinionEnemy.Count == 0)
		{
		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_276, new Modifier(-580));//翡翠龙
		}
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		

		
		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DS1_184, new Modifier(135));//追踪术
 		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_203, new Modifier(65));//集群战术
 		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_213, new Modifier(-15));//集群战术+
		


 
  
			

		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_313, new Modifier(40));//翡翠龙
		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DAL_774, new Modifier(-50));//坐骑商人
		p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_310, new Modifier(140));//魔免龙
		
		
 		

		
		

		
		if (board.MaxMana == 1)
		{
		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DRG_318, new Modifier(1220));//翡翠梦境
        } 
		
		if (board.MaxMana >= 3 &&board.MaxMana <=7)
		{
		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_130, new Modifier(-30));//过度生长
        } 
		
		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_273, new Modifier(50));//激流
		
		p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_132, new Modifier(75));//铁木树皮
		
		
		
            return p;
        }
		
		//防止法术被使用
		private void PreventSpellFromBeingPlayedOnMinions(ref ProfileParameters parameters, Board board)
		{
			//遍历场上不是嘲讽的敌方随从
			foreach (var card in board.MinionEnemy.FindAll(x => !x.IsTaunt))
			{
                //修改火球的修正值为“Rush”配置文件中定义的基值的500％
                parameters.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_029, new Modifier(1000, card.Id));

                //修改炽热的火把的修正值为“Rush”配置文件中定义的基值的500％
                parameters.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002t, new Modifier(500, card.Id));				
			}
		}

        //芬利·莫格顿爵士技能选择
        public Card.Cards SirFinleyChoice(List<Card.Cards> choices)
        {
            var filteredTable = _heroPowersPriorityTable.Where(x => choices.Contains(x.Key)).ToList();
            return filteredTable.First(x => x.Value == filteredTable.Max(y => y.Value)).Key;
        }
		


        //卡扎库斯选择
        public Card.Cards KazakusChoice(List<Card.Cards> choices)
        {
            return choices[0];
        }
		
		//甲板工具类
        public static class BoardHelper
		{
			//得到敌方的血量和护甲之和
            public static int GetEnemyHealthAndArmor(Board board)
            {
                return board.HeroEnemy.CurrentHealth + board.HeroEnemy.CurrentArmor;
            }
			
			//得到自己的法强
            public static int GetSpellPower(Board board)
            {
				//计算没有被沉默的随从的法术强度之和
                return board.MinionFriend.FindAll(x => x.IsSilenced == false).Sum(x => x.SpellPower);
            }
			
			//获得第二轮斩杀血线
            public static int GetSecondTurnLethalRange(Board board)
			{
				//敌方英雄的生命值和护甲之和减去可释放法术的伤害总和
                return GetEnemyHealthAndArmor(board) - GetPlayableSpellSequenceDamages(board);
			}
			
			//下一轮是否可以斩杀敌方英雄
            public static bool HasPotentialLethalNextTurn(Board board)
			{
				//如果敌方随从没有嘲讽并且造成伤害
				//(敌方生命值和护甲的总和 减去 下回合能生存下来的当前场上随从的总伤害 减去 下回合能攻击的可使用随从伤害总和)
				//后的血量小于总法术伤害
				if(!board.MinionEnemy.Any(x => x.IsTaunt) && 
					(GetEnemyHealthAndArmor(board)-GetPotentialMinionDamages(board)-GetPlayableMinionSequenceDamages(GetPlayableMinionSequence(board),board))
						<= GetTotalBlastDamagesInHand(board))
				{
					return true;
				}
				//法术释放过敌方英雄的血量是否大于等于第二轮斩杀血线
				return GetRemainingBlastDamagesAfterSequence(board) >= GetSecondTurnLethalRange(board);
			}
			
			//获得下回合能生存下来的当前场上随从的总伤害
            public static int GetPotentialMinionDamages(Board board)
            {
                return GetPotentialMinionAttacker(board).Sum(x => x.CurrentAtk);
            }
			
			//获得下回合能生存下来的当前场上随从集合
            public static List<Card> GetPotentialMinionAttacker(Board board)
			{
				//下回合能生存下来的当前场上随从集合
				var minionscopy = board.MinionFriend.ToArray().ToList();
				
				//遍历 以敌方随从攻击力 降序排序 的 场上敌方随从集合
                foreach (var mi in board.MinionEnemy.OrderByDescending(x => x.CurrentAtk))
				{
					//以友方随从攻击力 降序排序 的 场上的所有友方随从集合，如果该集合存在生命值大于与敌方随从攻击力
					if(board.MinionFriend.OrderByDescending(x => x.CurrentAtk).Any(x => x.CurrentHealth <= mi.CurrentAtk))
					{
						//以友方随从攻击力 降序排序 的 场上的所有友方随从集合,找出该集合中友方随从的生命值小于等于敌方随从的攻击力的随从
						var tar = board.MinionFriend.OrderByDescending(x => x.CurrentAtk).FirstOrDefault(x => x.CurrentHealth <= mi.CurrentAtk);
						//将该随从移除掉
						minionscopy.Remove(tar);
					}
				}
				
				return minionscopy;
			}
			
			//获取可以使用的随从集合
            public static List<Card.Cards> GetPlayableMinionSequence(Board board)
			{
				//卡片集合
                var ret = new List<Card.Cards>();
				
				//当前剩余的法力水晶
                var manaAvailable = board.ManaAvailable;
				
				//遍历以手牌中费用降序排序的集合
                foreach (var card in board.Hand.OrderByDescending(x => x.CurrentCost))
				{
					//如果当前卡牌不为随从，继续执行
                    if (card.Type != Card.CType.MINION) continue;
					
					//当前法力值小于卡牌的费用，继续执行
                    if (manaAvailable < card.CurrentCost) continue;
					
					//添加到容器里
                    ret.Add(card.Template.Id);
					
					//修改当前使用随从后的法力水晶
                    manaAvailable -= card.CurrentCost;
				}
				
				return ret;
			}			
			
			//获取下回合能攻击的可使用随从伤害总和
            public static int GetPlayableMinionSequenceDamages(List<Card.Cards> minions, Board board)
			{
				//下回合能攻击的可使用随从集合攻击力相加
                return GetPlayableMinionSequenceAttacker(minions, board).Sum(x => CardTemplate.LoadFromId(x).Atk);
			}
			
			//获取下回合能攻击的可使用随从集合
            public static List<Card.Cards> GetPlayableMinionSequenceAttacker(List<Card.Cards> minions, Board board)
			{
				//未处理的下回合能攻击的可使用随从集合
                var minionscopy = minions.ToArray().ToList();
				
				//遍历 以敌方随从攻击力 降序排序 的 场上敌方随从集合
                foreach (var mi in board.MinionEnemy.OrderByDescending(x => x.CurrentAtk))
				{
					//以友方随从攻击力 降序排序 的 场上的所有友方随从集合，如果该集合存在生命值大于与敌方随从攻击力
					if(minions.OrderByDescending(x => CardTemplate.LoadFromId(x).Atk).Any(x => CardTemplate.LoadFromId(x).Health <= mi.CurrentAtk))
					{
						//以友方随从攻击力 降序排序 的 场上的所有友方随从集合,找出该集合中友方随从的生命值小于等于敌方随从的攻击力的随从
						var tar = minions.OrderByDescending(x => CardTemplate.LoadFromId(x).Atk).FirstOrDefault(x => CardTemplate.LoadFromId(x).Health <= mi.CurrentAtk);
						//将该随从移除掉
						minionscopy.Remove(tar);
					}
				}
				
				return minionscopy;
			}
			
			//获取当前回合手牌中的总法术伤害
            public static int GetTotalBlastDamagesInHand(Board board)
            {
				//从手牌中找出法术伤害表存在的法术的伤害总和(包括法强)
                return
                    board.Hand.FindAll(x => _spellDamagesTable.ContainsKey(x.Template.Id))
                        .Sum(x => _spellDamagesTable[x.Template.Id] + GetSpellPower(board));
            }

			//获取可以使用的法术集合
            public static List<Card.Cards> GetPlayableSpellSequence(Board board)
            {
				//卡片集合
                var ret = new List<Card.Cards>();
				
				//当前剩余的法力水晶
                var manaAvailable = board.ManaAvailable;
				
				//遍历以手牌中费用降序排序的集合
                foreach (var card in board.Hand.OrderBy(x => x.CurrentCost))
                {
					//如果手牌中又不在法术序列的法术牌，继续执行
                    if (_spellDamagesTable.ContainsKey(card.Template.Id) == false) continue;
					
					//当前法力值小于卡牌的费用，继续执行
                    if (manaAvailable < card.CurrentCost) continue;
					
					//添加到容器里
                    ret.Add(card.Template.Id);
					
					//修改当前使用随从后的法力水晶
                    manaAvailable -= card.CurrentCost;
                }

                return ret;
            }
			
			//获取存在于法术列表中的法术集合的伤害总和(包括法强)
            public static int GetSpellSequenceDamages(List<Card.Cards> sequence, Board board)
            {
                return
                    sequence.FindAll(x => _spellDamagesTable.ContainsKey(x))
                        .Sum(x => _spellDamagesTable[x] + GetSpellPower(board));
            }
			
			//得到可释放法术的伤害总和
            public static int GetPlayableSpellSequenceDamages(Board board)
            {
                return GetSpellSequenceDamages(GetPlayableSpellSequence(board), board);
            }
			
			//计算在法术释放过敌方英雄的血量
            public static int GetRemainingBlastDamagesAfterSequence(Board board)
			{
				//当前回合总法术伤害减去可释放法术的伤害总和
				return GetTotalBlastDamagesInHand(board) - GetPlayableSpellSequenceDamages(board);
			}

			//在没有法术的情况下有潜在致命的下一轮
            public static bool HasPotentialLethalNextTurnWithoutSpells(Board board)
			{
				if(!board.MinionEnemy.Any(x => x.IsTaunt) &&
					(GetEnemyHealthAndArmor(board) -
                     GetPotentialMinionDamages(board) -
                     GetPlayableMinionSequenceDamages(GetPlayableMinionSequence(board), board) <=
                     0))
				{
					return true;
				}
				return false;
			}
		}
    }
}
