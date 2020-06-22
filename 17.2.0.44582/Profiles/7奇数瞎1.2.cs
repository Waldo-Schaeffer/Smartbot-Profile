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
            {Card.Cards.BT_429p, 5},//恶魔冲击
            {Card.Cards.BT_429p2, 5},//恶魔冲击
            {Card.Cards.BT_495, 4},//刃缚精锐


        };

        
        //具体策略
        public ProfileParameters GetParameters(Board board)
        {

            Card z = board.Hand.Find(x => x.Template.Id > 0);
            Card y = board.Hand.FindLast(x => x.Template.Id > 0);
            int OutcastCards = board.Hand.Count(x => x.CurrentCost > 0 && BoardHelper.IsOutCastCard(x, board) == true);
                int GuldanOutcastCards= board.Hand.Count(x => x.CurrentCost > 0 && BoardHelper.IsGuldanOutCastCard(x, board) == true);


            Bot.Log("OutcastCards: " + (int)(OutcastCards+GuldanOutcastCards));
            //Bot.Log("玩家信息: " + rank+"/n"+Legend);

            var p = new ProfileParameters(BaseProfile.Rush) { DiscoverSimulationValueThresholdPercent = -10 };
            p.CastSpellsModifiers.AddOrUpdate(TheCoin, new Modifier(85));

            //自定义命名
            int a = (board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - BoardHelper.GetEnemyHealthAndArmor(board);//敌我血量差

            //攻击模式切换
            if (board.EnemyClass == Card.CClass.DEMONHUNTER
                || board.EnemyClass == Card.CClass.HUNTER
                || board.EnemyClass == Card.CClass.ROGUE
                || board.EnemyClass == Card.CClass.SHAMAN
                || board.EnemyClass == Card.CClass.PALADIN
                || board.EnemyClass == Card.CClass.WARRIOR)
            {
                p.GlobalAggroModifier = (int)(a * 0.625 + 96.5);
            }
            else
            {
                p.GlobalAggroModifier = (int)(a * 0.625 + 103.5);
            }
            Bot.Log("攻击性：" + p.GlobalAggroModifier.Value);


          
            //假如对面场上有奥秘并且剩余费用大于五，提高总督察的优先级		
            if (board.SecretEnemy &&
                board.ManaAvailable >= 5)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GIL_648, new Modifier(-200));
            }


            //假如费用大于5，手牌小于2，提高沉默优先级
            if (board.FriendDeckCount <= 2 &&
                board.ManaAvailable >= 5)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_490, new Modifier(-99));//吞噬魔法
            }
            //假如对面有科学家，提高沉默优先级
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_004))//疯狂的科学家
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_490, new Modifier(-99));//吞噬魔法
            }
            //假如对面有空灵，提高沉默优先级
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_022))//空灵召唤者
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_490, new Modifier(-99));//吞噬魔法
            }

            //费用大于等于3且耐久值为1，手上有战刃，提高棕红优先值

            if (board.ManaAvailable >= 3
                &&board.WeaponFriend != null
                && board.WeaponFriend.Template.Id == Card.Cards.BT_922//棕红之翼
                && board.WeaponFriend.CurrentDurability == 1
                && board.HasCardInHand(Card.Cards.BT_430))//埃辛诺斯战刃
            {
                p.GlobalWeaponsAttackModifier = 100;
            }

            //如果棕红之刃耐久值为1，费用=4手上有双刃斩击，提高棕红之刃攻击优先值

            if (board.ManaAvailable >= 3
                && board.WeaponFriend != null
                && board.WeaponFriend.Template.Id == Card.Cards.BT_922//棕红之翼
                && board.WeaponFriend.CurrentDurability == 1
                && board.HasCardInHand(Card.Cards.BT_430))//埃辛诺斯战刃
            {
                p.GlobalWeaponsAttackModifier = 100;
            }



            //如果费用等于3，手上没有硬币或者手上有双刃斩击，降低任务达人优先级
            if (board.ManaAvailable == 3
            &&board.HasCardInHand(Card.Cards.EX1_044) //任务达人
            &&!board.HasCardInHand(Card.Cards.GAME_005) //幸运币 
            ||!board.HasCardInHand(Card.Cards.BT_175)//双刃斩击
            ||!board.HasCardInHand(Card.Cards.BT_175t)//二次斩击
            
           )                 
            {
          p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_044, new Modifier(500));              //任务达人
            }
          //如果费用等于2，降低任务达人优先级
            if (board.ManaAvailable ==2
            &&board.HasCardInHand(Card.Cards.EX1_044) //任务达人
            &&board.HasCardInHand(Card.Cards.GAME_005) //幸运币 
            )                 
            {
          p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_044, new Modifier(9999));              //任务达人
            }

            //费用大于等于4，提高暴徒优先级
            if (board.ManaAvailable >=4
            && board.HasCardInHand(Card.Cards.GIL_534) //荆棘帮暴徒
            )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GIL_534, new Modifier(-200));   //荆棘帮暴徒
            }

            //费用大于等于5，提高洛欧塞布 优先级
            if (board.ManaAvailable >= 5
            && board.HasCardInHand(Card.Cards.FP1_030) //荆棘帮暴徒
            )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030, new Modifier(-200));   //洛欧塞布 
            }


            //费用等于5，手上有刃缚精锐和双刃斩击，提高双刃和二次斩击斩击优先级
            if (board.ManaAvailable == 5//费用等于5
                 && board.HasCardInHand(Card.Cards.BT_495)//刃缚精锐
                 && board.HasCardInHand(Card.Cards.BT_175)//双刃斩击
                 && board.HasCardInHand(Card.Cards.BT_175t)//二次斩击

                 )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_175, new Modifier(-300));//双刃斩击
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_175t, new Modifier(-300));//二次斩击
            }
           
           

           
             if (board.ManaAvailable >= 6 && board.Hand.Count <= 7
             && board.HasCardInHand(Card.Cards.BT_601))
            {
                p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BT_601, new Modifier(-70));
            }//如果当前剩余法力水晶大于等于6，手牌数量小于等于7并且手牌中有古尔丹头颅






            //技能卡
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_490, new Modifier(40));//吞噬魔法
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_753, new Modifier(120));//法力燃烧
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_491, new Modifier(50));//幽灵视觉
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_035, new Modifier(30));//混乱打击
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_601, new Modifier(30));//古尔丹的脑袋
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_187, new Modifier(200, board.HeroEnemy.Id));//日怒
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.HERO_10p_UP, new Modifier(50, board.HeroEnemy.Id));//恶魔之咬-奇数瞎


            //武器优先值
            //棕红之翼
            p.WeaponsAttackModifiers.AddOrUpdate(Cards.Umberwing, new Modifier(134));
            p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BT_921, new Modifier(120));//奥达奇
                                                                                  
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.TB_011, new Modifier(50));   //硬币
            //法术优先值		
            //恶魔变形
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_429, new Modifier(-35));
           
           //幽灵视觉
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_491, new Modifier(-5));
           
            //混乱打击
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_035, new Modifier(-10));
           
//随从优先值
            //降低玛维优先级
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_737, new Modifier(80));
            //任务达人
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_044, new Modifier(-40));  
             //盲眼监视者
           p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_323, new Modifier(-30));
            //愤怒的女祭司
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_493, new Modifier(-70));
            //暴怒邪吼者
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_416, new Modifier(-80));
            //焦油爬行者
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_928, new Modifier(-40));
            //萨特监工
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_352, new Modifier(-20));
            //荆棘帮暴徒
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GIL_534, new Modifier(-60));
            //喷灯破坏者
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_403, new Modifier(-80));
            //异种群居蝎 
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_034, new Modifier(-40));
            //南海船工
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_146, new Modifier(-90));
            //南海船长
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_027, new Modifier(-80));



            //随从优先解
            //提高战斗邪犬威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_351, new Modifier(200));
            //螃蟹应该解
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_700, new Modifier(200));
            //炸弹牛仔应该解
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_024, new Modifier(200));
            //提高相位威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_252, new Modifier(200));
            //提高热气球威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_057, new Modifier(200));
            //提高空中私掠者威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_016, new Modifier(200));
            //提高搬水工威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_270, new Modifier(200));
            //提高凶恶的废钢猎犬威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_759, new Modifier(200));
            //提高神殿狂战士威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_185, new Modifier(200));
            //大法师瓦格斯
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_558, new Modifier(200));
            //提高厄运信天翁
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_071, new Modifier(200));
            //提高高级跟班厄尔克
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_035, new Modifier(200));
            //提高狂奔的魔刃豹
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_006, new Modifier(200));
            //提高卡雷苟斯
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_609, new Modifier(200));
            //提高希亚玛特
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_178, new Modifier(200));
            //提高齐恩瓦拉
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_104, new Modifier(200));
            //提高机械袋鼠
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_445, new Modifier(200));
            //提高机械袋鼠宝宝
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_445t, new Modifier(300));
            //提高雏龙
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116t, new Modifier(200));
            //提高铸甲师
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_402, new Modifier(200));
            //提高无面渗透者威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_039, new Modifier(200));
            //提高鲨鱼之灵威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.TRL_092, new Modifier(200));
            //提高灭龙弩炮威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_256, new Modifier(200));
            //提高飞刺威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_037, new Modifier(200));
            //提高奇利亚斯威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_548, new Modifier(200));
            //提高流放者奥图里斯威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_937, new Modifier(200));
            //提高暗影珠宝师汉纳尔威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_188, new Modifier(200));
            //提高萨特监工威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_352, new Modifier(200));
            //提高怒鳞纳迦威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_355, new Modifier(200));
            //提高特殊坐骑商人威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_774, new Modifier(200));
             //降低龙裔小鬼威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_238t12t2, new Modifier(-20));
            //降低空灵召唤者威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_022, new Modifier(-20));



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
            public static bool  IsOutcast(Card card,Board board)
            {
                if(IsOutCastCard(card,board) || IsGuldanOutCastCard(card,board))
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
