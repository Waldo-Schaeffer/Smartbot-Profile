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
    public class WildDemonHunter : Profile
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
        //DH
        private const Card.Cards DemonsBite = Card.Cards.HERO_10p;
        private const Card.Cards DemonsbiteUp = Card.Cards.HERO_10p_UP;

  
        //英雄能力优先级
        private readonly Dictionary<Card.Cards, int> _heroPowersPriorityTable = new Dictionary<Card.Cards, int>
        {
            {SteadyShot, 9},
            {DemonsbiteUp, 8},
            {LifeTap, 7},
            {DaggerMastery, 6},
            {Reinforce, 5},
            {Shapeshift, 4},
            {DemonsBite, 4},
            {Fireblast, 3},
            {ArmorUp, 2},
            {LesserHeal, 1}
        };

        //直伤卡牌
        private static readonly Dictionary<Card.Cards, int> _spellDamagesTable = new Dictionary<Card.Cards, int>
        {
            {Card.Cards.LOE_002, 3},//火把
            {Card.Cards.LOE_002t, 6},//炽热的火把
            {Card.Cards.CS2_029, 6},//火球
            {Card.Cards.BT_187, 3},//日怒

        };

        
        //具体策略
        public ProfileParameters GetParameters(Board board)
        {
            Card z = board.Hand.Find(x => x.Template.Id > 0);
            Card y = board.Hand.FindLast(x => x.Template.Id > 0);
            int 流放牌数 = board.Hand.Count(x => x.CurrentCost > 0 && BoardHelper.IsOutCastCard(x, board) == true);
                int 古尔丹流放牌数= board.Hand.Count(x => x.CurrentCost > 0 && BoardHelper.IsGuldanOutCastCard(x, board) == true);

            Bot.Log("流放牌数 = " + (int)(流放牌数+古尔丹流放牌数));
            //Bot.Log("右:是否流放: " + BoardHelper.IsOutCastCard(y, board));


            #region 自定义变量
            int twoHpMinionsCount = board.MinionEnemy.Count(minion => minion.CurrentHealth <= 2);//对面小于等于2血的随从数量
            int threeHpMinionsCount = board.MinionEnemy.Count(minion => minion.CurrentHealth <= 3) - board.MinionFriend.FindAll(x => x.CurrentAtk >= 3).Count;//对面小于等于3血的随从数量
            int fiveHpMinionsCount = board.MinionEnemy.Count(minion => minion.CurrentHealth <= 5) - board.MinionFriend.FindAll(x => x.CurrentAtk >= 5).Count;//对面小于等于5血的随从数量
            #endregion

            #region 攻击模式


            var p = new ProfileParameters(BaseProfile.Rush) { DiscoverSimulationValueThresholdPercent = -10 };


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
                p = new ProfileParameters(BaseProfile.Rush) { DiscoverSimulationValueThresholdPercent = -10 };
            }
            #endregion

            #region 权重初始化
            //技能卡
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_490, new Modifier(40));//吞噬魔法
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_753, new Modifier(120));//法力燃烧
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_491, new Modifier(50));//幽灵视觉
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_035, new Modifier(30));//混乱打击
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_601, new Modifier(30));//古尔丹的脑袋
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_187, new Modifier(200, board.HeroEnemy.Id));//日怒
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.HERO_10p_UP, new Modifier(50, board.HeroEnemy.Id));//恶魔之咬-奇数瞎
            //随从卡
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_323, new Modifier(120));//盲眼监视者
            //武器卡
            p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BT_922, new Modifier(50));//棕红之翼
            p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BT_921, new Modifier(120));//奥达奇

            //硬币
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.TB_011, new Modifier(50));
            #endregion

            #region 随从权重

            if (board.ManaAvailable >= 4
              && (board.HasCardInHand(Card.Cards.BT_187))
              && board.FriendDeckCount >= 2
              ||board.EnemyClass==Card.CClass.DEMONHUNTER)//DH内战，日怒直接下
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_187, new Modifier(100));
            }
            else
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_187, new Modifier(180));
            }//日怒权重

            if (board.ManaAvailable <= 3
                && (board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_351) 
                || board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_323)
                || board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_922)
                || board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_066)
                || board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_352))
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_035))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_035, new Modifier(400));
            }//2费卡牌权重

            if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.BT_490) == 1
                && (board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_351) || board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_480))
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_753))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_490, new Modifier(400));
            }//1费卡牌权重

            if (board.ManaAvailable <= 2
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_323)
                && board.Hand.Count(card => card.Template.Id==Card.Cards.BT_601) == 0)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_323, new Modifier(40));
            }//2费之前没古尔丹脑袋，下盲眼监视者

            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_323)
                && BoardHelper.GetPlayableSpellSequence(board).Count == 0
                && board.EnemyClass == Card.CClass.WARLOCK)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_323, new Modifier(40));
            }//没可以打的法术，下盲眼监视者

            if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_351)
                && board.MinionEnemy.Count == 0)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(40));
            }//1费对面没有随从，下战斗邪犬
            else if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.BT_323) == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_922))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_323, new Modifier(2000));
            }//1费手里有棕红和盲眼监视者，不跳费下盲眼监视者

            else if (board.ManaAvailable >= 4
                && !board.Hand.Exists(x => x.Template.Race == Card.CRace.DEMON))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_416, new Modifier(250));
            }//手牌没恶魔，降低暴怒权重

            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_937)
                && board.Hand.Count < 2)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_612, new Modifier(300));
            }//手里没有能打的流放牌，降低流放者权重（作者还没加卡牌是否流放判断）

            if (board.ManaAvailable == 2
            && board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_066)
            && !board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
            && board.Hand.Exists(x => x.Template.Cost == 2))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_066, new Modifier(600));
            }//2费手里有其他2费随从，不打软泥怪

            if (board.Hand.Count >= 7)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_601, new Modifier(300));
            }//手牌太多，降低过牌权重

            if(BoardHelper.GetPlayableMinionSequence(board).Count < 2
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_215))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_352, new Modifier(40));
            }//场上随从少于2个，铺场

            if (board.WeaponEnemy != null && board.EnemyClass == Card.CClass.DEMONHUNTER)//软泥怪解武器
            {
                if (board.WeaponEnemy.Template.Id == Card.Cards.BT_921)
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_066, new Modifier(50));
                }
                else
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_066, new Modifier(600));
                }

            }
            else {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_066, new Modifier(50));
            }

            #endregion

            #region 技能权重
            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_514)
                && twoHpMinionsCount <= 2
                && board.MinionEnemy.Sum(x => x.CurrentAtk) <= 4)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_514, new Modifier(200));
            }//对面随从少，降低献祭光环权重

            if (twoHpMinionsCount >= 3)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_514, new Modifier(190 - 40 * twoHpMinionsCount));
            }//对面怪多，提高献祭光环

            /*
            if (board.MinionFriend.Exists(x => x.Template.Id==Card.Cards.BT_002)
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726)
                && board.Hand.FindAll(x => x.Template.Id == Card.Cards.CFM_760).Min(x => x.CurrentCost) <= board.ManaAvailable
                && board.Hand.FindAll(x => x.Template.Id == Card.Cards.CFM_760).Min(x => x.CurrentCost) > board.ManaAvailable - 2
                && (!board.Secret.Contains(Cards.Counterspell) || board.FriendGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.EX1_287))
                && (!board.Secret.Contains(Cards.ExplosiveRunes)) || board.FriendGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.LOOT_101))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_726, new Modifier(-700));
            }//场上有流放随从，释放流放牌(暂时未知流放如何判断)
            */
            if (board.ManaAvailable == 4
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.BT_514) >=1
                && twoHpMinionsCount >= 3
                && board.MinionFriend.Count <= 1)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_801, new Modifier(200));

            }//4费对面小于2血的怪多，不打眼棱


            if ((board.FriendDeckCount <= 6 && board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_035)))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_035, new Modifier(20));
            }//牌库卡不多，提高过牌


            if (board.MinionEnemy.Count == 1
                && board.MinionEnemy.Exists(x => x.Template.Id == Card.Cards.NEW1_021)
                && board.MinionFriend.Count == 0
                && BoardHelper.GetPlayableSpellSequence(board).Count > 0)
            {
                foreach (var card1 in BoardHelper.GetPlayableSpellSequence(board))
                {
                    p.CastSpellsModifiers.AddOrUpdate(card1, new Modifier(30));
                }
            }//对面有末日，提高过牌

            if (board.ManaAvailable >= 6 && board.Hand.Count <= 5 && board.HasCardInHand(Card.Cards.BT_601))
            {
                p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BT_601, new Modifier(50));
            }//如果当前剩余法力水晶大于等于6，手牌数量小于等于5并且手牌中有古尔丹头颅

            if(board.EnemyClass==Card.CClass.MAGE
                || board.EnemyClass == Card.CClass.PRIEST
                || board.EnemyClass == Card.CClass.WARLOCK)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_753, new Modifier(50));//法力燃烧
            }
            #endregion

            #region 敌方权重
            //针对一些随从优先解掉
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BRM_002))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BRM_002, new Modifier(400));
            }//如果对面场上有火妖，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ULD_276))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_276, new Modifier(110));
            }//如果对面场上有202，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.GVG_069))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GVG_069, new Modifier(150));
            }//如果对面场上有533，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.GVG_084))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GVG_084, new Modifier(300));
            }//如果对面场上有314，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BOT_103))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_103, new Modifier(120));
            }//如果对面场上有露娜，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BOT_407))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_407, new Modifier(120));
            }//如果对面场上有雷云元素，提高攻击优先度

            if (!BoardHelper.HasPotentialLethalNextTurn(board)
                && board.MinionEnemy.Count(minion => minion.Template.Id == Card.Cards.GVG_045t) >= 3
                && board.EnemyClass == Card.CClass.WARLOCK)
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GVG_045t, new Modifier(150));
            }//如果对面场上有1-1小鬼，提高攻击优先度
            #endregion

            #region 武器权重
            if ((board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_271)
                || (board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.BT_271) )
                && board.HeroEnemy.CurrentHealth <= 16)
            {
                p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.BT_271, new Modifier(-1000));
            }//如果对面血量小于等于16，提高武器攻击优先度
            #endregion


            return p;
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

        //计算类
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
                if (!board.MinionEnemy.Any(x => x.IsTaunt) &&
                    (GetEnemyHealthAndArmor(board) - GetPotentialMinionDamages(board) - GetPlayableMinionSequenceDamages(GetPlayableMinionSequence(board), board))
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
                    if (board.MinionFriend.OrderByDescending(x => x.CurrentAtk).Any(x => x.CurrentHealth <= mi.CurrentAtk))
                    {
                        //以友方随从攻击力 降序排序 的 场上的所有友方随从集合,找出该集合中友方随从的生命值小于等于敌方随从的攻击力的随从
                        var tar = board.MinionFriend.OrderByDescending(x => x.CurrentAtk).FirstOrDefault(x => x.CurrentHealth <= mi.CurrentAtk);
                        //将该随从移除掉
                        minionscopy.Remove(tar);
                    }
                }

                return minionscopy;
            }

            //获得下回合能生存下来的对面随从集合
            public static List<Card> GetSurvivalMinionEnemy(Board board)
            {
                //下回合能生存下来的当前对面场上随从集合
                var minionscopy = board.MinionEnemy.ToArray().ToList();

                //遍历 以友方随从攻击力 降序排序 的 场上友方可攻击随从集合
                foreach (var mi in board.MinionFriend.FindAll(x => x.CanAttack).OrderByDescending(x => x.CurrentAtk))
                {
                    //如果存在友方随从攻击力大于等于敌方随从血量
                    if (board.MinionEnemy.OrderByDescending(x => x.CurrentHealth).Any(x => x.CurrentHealth <= mi.CurrentAtk))
                    {
                        //以敌方随从血量降序排序的所有敌方随从集合，找出敌方生命值小于等于友方随从攻击力的随从
                        var tar = board.MinionEnemy.OrderByDescending(x => x.CurrentHealth).FirstOrDefault(x => x.CurrentHealth <= mi.CurrentAtk);
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

            //获取可以使用的奥秘集合
            public static List<Card.Cards> GetPlayableSecret(Board board)
            {
                //卡片集合
                var ret = new List<Card.Cards>();

                //遍历手牌中所有奥秘集合
                foreach (var card1 in board.Hand.FindAll(card => card.Template.IsSecret))
                {
                    if (board.Secret.Count > 0)
                    {
                        //遍历头上奥秘集合
                        foreach (var card2 in board.Secret.FindAll(card => CardTemplate.LoadFromId(card).IsSecret))
                        {

                            //如果手里奥秘和头上奥秘不相等
                            if (card1.Template.Id != card2)
                            {
                                //添加到容器里
                                ret.Add(card1.Template.Id);
                            }
                        }
                    }
                    else
                    { ret.Add(card1.Template.Id); }
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
                    if (minions.OrderByDescending(x => CardTemplate.LoadFromId(x).Atk).Any(x => CardTemplate.LoadFromId(x).Health <= mi.CurrentAtk))
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

                if (board.Secret.Count > 0)
                {
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
                }
                else if (board.Secret.Count == 0)
                {
                    //遍历以手牌中费用降序排序的集合
                    foreach (var card in board.Hand.FindAll(x => x.Type == Card.CType.SPELL).OrderBy(x => x.CurrentCost))
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

            public static bool IsOutCastCard(Card card, Board board)
            {
                var OutcastLeft = board.Hand.Find(x => x.CurrentCost >= 0);
                var OutcastRight = board.Hand.FindLast(x => x.CurrentCost >= 0);
                if (card.Template.Id == OutcastLeft.Template.Id
                    || card.Template.Id == OutcastRight.Template.Id)
                {
                    return true;
                }
                return false;
            }
            public static bool IsGuldanOutCastCard(Card card, Board board)
            {
                if ((board.FriendGraveyard.Exists(x => CardTemplate.LoadFromId(x).Id == Card.Cards.BT_601)
                    && card.Template.Cost - card.CurrentCost == 3))
                {
                    return true;
                }
                return false;
            }

            //在没有法术的情况下有潜在致命的下一轮
            public static bool HasPotentialLethalNextTurnWithoutSpells(Board board)
            {
                if (!board.MinionEnemy.Any(x => x.IsTaunt) &&
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
