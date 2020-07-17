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
            {Card.Cards.LOE_002, 3},//火把
            {Card.Cards.LOE_002t, 6},//炽热的火把
            {Card.Cards.CS2_029, 6},//火球
            {Card.Cards.KAR_092, 3},//男仆
            {Card.Cards.ULD_293, 6},//云雾王子
        };


        //具体策略
        public ProfileParameters GetParameters(Board board)
        {
            var p = new ProfileParameters(BaseProfile.Rush);
            p.DiscoverSimulationValueThresholdPercent = 10;

            //卡牌优先度调整
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_295, new Modifier(92));//降低冰箱优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_287, new Modifier(40));//提高法反优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_726, new Modifier(200));//降低远古谜团优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_023, new Modifier(30));//提高奥术智慧优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.AT_005, new Modifier(280));//降低变猪优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_239, new Modifier(120));//降低火焰结界优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_029, new Modifier(80, board.HeroEnemy.Id));//提高火球打脸优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002t, new Modifier(60, board.HeroEnemy.Id));//提高火把打脸优先度
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002, new Modifier(50, board.HeroEnemy.Id));//提高火把打脸优先度
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_020, new Modifier(120));//降低秘法学家优先度
            //p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_034, new Modifier(120));//降低火铳优先度
            p.CastWeaponsModifiers.AddOrUpdate(Cards.Aluneth, new Modifier(120));//降低提刀优先度
                                                                                 //p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_034, new Modifier(9999,board.HeroFriend.Id));
                                                                                 //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.KAR_092, new Modifier(9999,board.HeroFriend.Id));



            //自定义命名
            int twoHpMinionsCount = board.MinionEnemy.Count(minion => minion.CurrentHealth <= 2);//对面小于等于2血的随从数量
            int LibraryResidueSecret = 8 - (board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).IsSecret) + board.Hand.Count(card => card.Template.IsSecret) + board.Secret.Count);//牌库剩余奥秘
            int threeHpMinionsCount = board.MinionEnemy.Count(minion => minion.CurrentHealth <= 3) - board.MinionFriend.FindAll(x => x.CurrentAtk >= 3).Count;//对面小于等于3血的随从数量
            int fiveHpMinionsCount = board.MinionEnemy.Count(minion => minion.CurrentHealth <= 5) - board.MinionFriend.FindAll(x => x.CurrentAtk >= 5).Count;//对面小于等于5血的随从数量

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

            //攻击模式切换
            if ((((board.MinionEnemy.FindAll(x => x.IsTaunt && !x.IsSilenced).Sum(x => x.CurrentHealth) + 9 * board.MinionEnemy.Count(x => x.Template.Id == Card.Cards.LOOT_368) - board.MinionFriend.Sum(x => x.CurrentAtk)) >= 8
                && BoardHelper.GetEnemyHealthAndArmor(board) <= 15)
                || BoardHelper.GetSecondTurnLethalRange(board) <= 7)
                && ((board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - board.MinionEnemy.Sum(x => x.CurrentAtk)) > 7)
            {
                //p = new ProfileParameters(BaseProfile.Face);
                //p.DiscoverSimulationValueThresholdPercent = 10;
                p.GlobalAggroModifier.Value = 130;
                //p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_034, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_029, new Modifier(-40, board.HeroEnemy.Id));//提高火球打脸优先度
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002t, new Modifier(-40, board.HeroEnemy.Id));//提高火把打脸优先度
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002, new Modifier(-40, board.HeroEnemy.Id));//提高火把打脸优先度
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_293, new Modifier(-40, board.HeroEnemy.Id));//提高云雾王子打脸优先度
                p.CastSpellsModifiers.AddOrUpdate(TheCoin, new Modifier(0));
            }//对面嘲讽太多，抢脸，降低火铳优先度
            else if (BoardHelper.GetEnemyHealthAndArmor(board) <= 15
                && ((board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - board.MinionEnemy.Sum(x => x.CurrentAtk)) > 7)
            {
                //p = new ProfileParameters(BaseProfile.Face);
                //p.DiscoverSimulationValueThresholdPercent = 10;
                p.GlobalAggroModifier.Value = 130;
                //p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_034, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_029, new Modifier(30, board.HeroEnemy.Id));//提高火球打脸优先度
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002t, new Modifier(20, board.HeroEnemy.Id));//提高火把打脸优先度
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002, new Modifier(20, board.HeroEnemy.Id));//提高火把打脸优先度
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_293, new Modifier(-40, board.HeroEnemy.Id));//提高云雾王子打脸优先度
                p.CastSpellsModifiers.AddOrUpdate(TheCoin, new Modifier(0));
            }//对面少于15血，抢脸
            else if (board.ManaAvailable >= 3)
            {
                p.CastSpellsModifiers.AddOrUpdate(TheCoin, new Modifier(30));
            }//提高硬币优先度
            else if (board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.LOOT_108)
            {
                p.GlobalAggroModifier.Value = 120;
            }//有武器，攻击性提高
            else if (board.ManaAvailable < 6)
            {
                p.GlobalAggroModifier.Value = 100;
            }//6费前攻击性调整
            else if (board.ManaAvailable >= 6)
            {
                p.GlobalAggroModifier.Value = 110;
            }//6费后攻击性调整

            if (board.Secret.Contains(Cards.IceBlock)
                && BoardHelper.HasPotentialLethalNextTurn(board)
                && !(board.EnemyClass == Card.CClass.MAGE && board.SecretEnemy && !board.EnemyGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.EX1_295)))
            {
                p.GlobalAggroModifier.Value = 1000;
            }//如果有冰箱，下回合斩杀，攻击性提高
            else if (BoardHelper.HasPotentialLethalNextTurn(board)
                && ((board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - board.MinionEnemy.Sum(x => x.CurrentAtk)) > 7)
            {
                p.GlobalAggroModifier.Value = 200;
            }//下回合斩杀，攻击性提高

            if (BoardHelper.HasPotentialLethalNextTurn(board)
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_295))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_295, new Modifier(50));
            }//下回合斩杀，提高冰箱优先度

            if (BoardHelper.GetSurvivalMinionEnemy(board).Sum(x => x.CurrentHealth) > 18
                && BoardHelper.GetSecondTurnLethalRange(board) <= 7
                && board.Secret.Contains(Cards.IceBlock)
                && !(board.EnemyClass == Card.CClass.MAGE && board.SecretEnemy && !board.EnemyGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.EX1_295)))
            {
                p.GlobalAggroModifier.Value = 1000;
            }//无法挽救场面，第二轮斩杀血线小于等于7，头上有冰箱，抢脸
            else if (BoardHelper.GetSurvivalMinionEnemy(board).Sum(x => x.CurrentHealth) > 23
                && BoardHelper.GetSecondTurnLethalRange(board) <= 13
                && board.Secret.Contains(Cards.IceBlock)
                && !(board.EnemyClass == Card.CClass.MAGE && board.SecretEnemy && !board.EnemyGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.EX1_295)))
            {
                p.GlobalAggroModifier.Value = 1000;
            }//无法挽救场面，第二轮斩杀血线小于等于12，头上有冰箱，抢脸
            else if (BoardHelper.GetSurvivalMinionEnemy(board).Sum(x => x.CurrentHealth) > 23)
            {
                p.GlobalAggroModifier.Value = 500;
            }//无法挽救场面,打脸

            //武器调整
            if (board.ManaAvailable >= 6 && board.Hand.Count <= 5 && board.HasCardInHand(Cards.Aluneth))
            {
                p.CastWeaponsModifiers.AddOrUpdate(Cards.Aluneth, new Modifier(50));
            }//如果当前剩余法力水晶大于等于6，手牌数量小于等于5并且手牌中有艾露尼斯，提高艾露尼斯的优先级	




            //随从调整
            if (board.ManaAvailable >= 4
              && (board.HasCardInHand(Card.Cards.ULD_293))
              && board.Secret.Count > 0)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_293, new Modifier(100));
            }
            else
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_293, new Modifier(180));
            }//增加云雾王子的优先级

            if (board.ManaAvailable <= 3
                && (board.Hand.Exists(x => x.Template.Id == Card.Cards.FP1_004) || board.Hand.Exists(x => x.Template.Id == Card.Cards.UNG_020))
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_726, new Modifier(400));
            }//2费下科学家或者秘法学家，不打远古谜团

            if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.UNG_020) == 1
                && (board.Hand.Exists(x => x.Template.Id == Card.Cards.KAR_092) || board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_240))
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_020, new Modifier(400));
            }//1费手里有硬币、麦迪文男仆、秘法学家，不打秘法学家

            if (board.EnemyClass == Card.CClass.MAGE
                && board.SecretEnemy
                && board.EnemyGraveyard.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.LOOT_101) < 2
                && (board.EnemyGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.EX1_612) || board.EnemyGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.UNG_020) || board.EnemyGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.ULD_293))
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_066))
                foreach (var card in board.Hand.FindAll(x => 6 - x.CurrentHealth >= (board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor)))
                {
                    p.CastMinionsModifiers.AddOrUpdate(card.Template.Id, new Modifier(5000));
                }//对阵奥秘法，对面可能有爆炸，防止自己被炸死

            if (LibraryResidueSecret > 0
                && board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.FP1_004))
            {
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(40));
            }//牌库有奥秘，提高送掉科学家的优先度

            if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_066)
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.FP1_018)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_287)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.LOOT_101)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_295)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_239)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_620))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CFM_066, new Modifier(500));
            }//1费手里有复制，不下暗金教侍从

            if (board.ManaAvailable <= 2
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_066)
                && board.Hand.Count(card => card.Template.IsSecret) == 0)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CFM_066, new Modifier(200));
            }//2费之前手里没奥秘，不打暗金教侍从

            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_066)
                && BoardHelper.GetPlayableSecret(board).Count == 0
                && board.EnemyClass == Card.CClass.WARLOCK)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CFM_066, new Modifier(200));
            }//没可以打的奥秘，对面是术士，不打暗金教侍从

            if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.KAR_092)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.FP1_004) == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.UNG_020)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726)
                && board.MinionEnemy.Count == 0)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(500));
            }//1费对面没有随从，不下科学家
            else if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.FP1_004) == 1
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.UNG_020)
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_240))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(2000));
            }//1费手里有科学家和对空奥术师，不跳费下科学家
            else if (board.ManaAvailable == 1
                && board.Hand.Count(x => x.Template.Id == Card.Cards.FP1_004) == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.KAR_092)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.UNG_020)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(300));
            }//1费有硬币，不下科学家

            if (LibraryResidueSecret == 0)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_726, new Modifier(6000));
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_020, new Modifier(5000));
            }//牌库没奥秘，不打远古谜团，降低秘法学家优先度
            else if (board.ManaAvailable >= 3
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_612))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_020, new Modifier(250));
            }//降低秘法学家优先度

            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_612)
                && BoardHelper.GetPlayableSecret(board).Count == 0
                && board.Hand.Count >= 5
                && (board.Hand.Count(x => x.Type == Card.CType.MINION) - board.Hand.Count(x => x.Template.Id == Card.Cards.EX1_612) != board.Hand.Count(x => x.Template.Id == Card.Cards.ULD_240))
                && (board.Hand.Count(x => x.Type == Card.CType.MINION) - board.Hand.Count(x => x.Template.Id == Card.Cards.EX1_612) != 0))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_612, new Modifier(300));
            }//手里没有能打的奥秘，降低肯瑞托优先度

            if (board.ManaAvailable == 2
            && board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_066)
            && !board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
            && (board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726) || board.Hand.Exists(x => x.Template.Id == Card.Cards.UNG_020) || board.Hand.Exists(x => x.Template.Id == Card.Cards.FP1_004)))
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CFM_066, new Modifier(600));
            }//2费手里有暗金教侍从，秘法学家或者科学家或者远古谜团，不打暗金教侍从

            if (board.Hand.Count >= 7)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_023, new Modifier(300));
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_020, new Modifier(400));
            }//手牌太多，降低奥术智慧、秘法学家优先度




            //法术调整
            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_239)
                && threeHpMinionsCount <= 2
                && board.MinionEnemy.Sum(x => x.CurrentAtk) <= 4)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_239, new Modifier(200));
            }//对面随从给少，降低火焰结界优先度

            if (threeHpMinionsCount >= 3
                || (fiveHpMinionsCount >= 3 && board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.ULD_240)))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_239, new Modifier(190 - 40 * threeHpMinionsCount));
            }//对面3血以下怪多，提高火焰结界优先度

            if (board.ManaAvailable <= 2
                && board.MinionEnemy.Sum(x => x.CurrentAtk) == 1
                && board.MinionFriend.Count == 0)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_239, new Modifier(1000));
            }//远古谜团拿到火焰结界对面场功太低，不打

            if ((board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.LOOT_108)
                && board.Hand.Count >= 6
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.FP1_018))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.FP1_018, new Modifier(150));
            }//有刀，手牌过多，降低复制优先度

            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_760)
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.ULD_726)
                && board.Hand.FindAll(x => x.Template.Id == Card.Cards.CFM_760).Min(x => x.CurrentCost) <= board.ManaAvailable
                && board.Hand.FindAll(x => x.Template.Id == Card.Cards.CFM_760).Min(x => x.CurrentCost) > board.ManaAvailable - 2
                && (LibraryResidueSecret > 0)
                && (!board.Secret.Contains(Cards.Counterspell) || board.FriendGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.EX1_287))
                && (!board.Secret.Contains(Cards.ExplosiveRunes)) || board.FriendGraveyard.Exists(card => CardTemplate.LoadFromId(card).Id == Card.Cards.LOOT_101))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ULD_726, new Modifier(-700));
            }//先打远古谜团再打水晶侍女

            if (board.ManaAvailable == 4
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.ULD_240) == 2
                && board.Hand.Count(card => card.Template.IsSecret) == 1
                && twoHpMinionsCount >= 3
                && board.MinionFriend.Count <= 1)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_287, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_101, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_295, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.FP1_018, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CFM_620, new Modifier(200));
            }//4费手里只有一张奥秘和2张对空奥术师，对面小于2血的怪多，不打奥秘

            else if (board.ManaAvailable == 4
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.ULD_240) == 1
                && board.Hand.Count(card => card.Template.IsSecret) == 1
                && twoHpMinionsCount >= 3
                && board.MinionFriend.Count <= 1)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_240, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_287, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_101, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_295, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.FP1_018, new Modifier(200));
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CFM_620, new Modifier(200));
            }//4费手里只有一个对空奥术师，对面小于2血的怪多，不下对空奥术师,不打奥秘

            else if (board.ManaAvailable == 4
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Count(x => x.Template.Id == Card.Cards.ULD_240) == 1
                && board.Hand.Count(card => card.Template.IsSecret) >= 2
                && twoHpMinionsCount >= 3
                && board.MinionFriend.Count <= 1)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_240, new Modifier(200));
            }//4费手里只有一个对空奥术师，对面小于2血的怪多，不下对空奥术师

            if (board.ManaAvailable == 4
                && board.EnemyAbility.Template.Id == Card.Cards.HERO_04bp
                && board.Hand.Count(x => x.Template.Id == Card.Cards.ULD_240) == 1
                && board.Hand.Count(card => card.Template.IsSecret) >= 1)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_240, new Modifier(200));
            }//4费对面奇数骑，只有一个对空奥术师，手里有奥秘，不下对空奥术师

            if (board.ManaAvailable == 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.CFM_066)
                && !board.Hand.Exists(x => x.Template.Id == Card.Cards.GAME_005)
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_287)
                && board.Hand.Count(card => card.Template.IsSecret) > 1)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_287, new Modifier(120));
            }//1费对面有硬币，奥秘大于1个，降低法反优先度

            if (board.ManaAvailable <= 4
                && board.MinionEnemy.Count == 0
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_287)
                && board.EnemyClass == Card.CClass.DRUID)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_287, new Modifier(-10));
            }//对面德鲁伊，4费前没随从，提高法反优先度

            if (board.Hand.Count <= 6
                   && board.Hand.Exists(x => x.Template.Id == Card.Cards.LOE_002))
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOE_002, new Modifier(20));
            }//牌库卡不多，提高打火把优先度

            if(board.EnemyClass == Card.CClass.WARLOCK
                && board.Hand.Count(card => card.Template.IsSecret) > 1
                && board.Hand.Exists(x => x.Template.Id == Card.Cards.LOOT_101)
                && board.ManaAvailable <= 6
                && board.MinionEnemy.Sum(x => x.CurrentAtk) <= 4)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_101, new Modifier(200));
            }//对阵术士，降低爆炸优先度

            if(board.MinionEnemy.Count == 1
                && board.MinionEnemy.Exists(x => x.Template.Id == Card.Cards.NEW1_021)
                && board.MinionFriend.Count == 0
                && BoardHelper.GetPlayableSecret(board).Count > 0)
            {
                foreach (var card1 in BoardHelper.GetPlayableSecret(board))
                {
                    p.CastSpellsModifiers.AddOrUpdate(card1, new Modifier(30));
                }
            }//对面有末日，提高奥秘优先度
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
