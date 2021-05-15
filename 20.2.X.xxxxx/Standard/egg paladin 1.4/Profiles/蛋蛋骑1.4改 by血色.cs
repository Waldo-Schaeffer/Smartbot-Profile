using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
using SmartBotAPI.Plugins.API;
using SmartBotAPI.Battlegrounds;
using SmartBot.Plugins.API.Actions;


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
    public class standardeggPaladin  : Profile
    {
#region 英雄技能
        //幸运币
        private const Card.Cards TheCoin = Card.Cards.GAME_005;
        //战士
        private const Card.Cards ArmorUp = Card.Cards.HERO_01bp;
        //萨满
        private const Card.Cards TotemicCall = Card.Cards.HERO_02bp;
        //盗贼
        private const Card.Cards DaggerMastery = Card.Cards.HERO_03bp;
        //圣骑士
        private const Card.Cards Reinforce = Card.Cards.HERO_04bp;
        //猎人
        private const Card.Cards SteadyShot = Card.Cards.HERO_05bp;
        //德鲁伊
        private const Card.Cards Shapeshift = Card.Cards.HERO_06bp;
        //术士
        private const Card.Cards LifeTap = Card.Cards.HERO_07bp;
        //法师
        private const Card.Cards Fireblast = Card.Cards.HERO_08bp;
        //牧师
        private const Card.Cards LesserHeal = Card.Cards.HERO_09bp;
        #endregion

#region 英雄能力优先级
        private readonly Dictionary<Card.Cards, int> _heroPowersPriorityTable = new Dictionary<Card.Cards, int>
        {
            {SteadyShot, 9},//猎人
            {LifeTap, 8},//术士
            {DaggerMastery, 7},//盗贼
            {Reinforce, 5},//骑士
            {Fireblast, 4},//法师
            {Shapeshift, 3},//德鲁伊
            {LesserHeal, 2},//牧师
            {ArmorUp, 1},//战士
        };
        #endregion

#region 直伤卡牌 标准模式
        //直伤法术卡牌（必须是可打脸的伤害） 需要计算法强
        private static readonly Dictionary<Card.Cards, int> _spellDamagesTable = new Dictionary<Card.Cards, int>
        {
            //萨满
            {Card.Cards.CORE_EX1_238, 3},//闪电箭 Lightning Bolt ID：CORE_EX1_238
            {Card.Cards.DMF_701, 4},//深水炸弹 Dunk Tank ID：DMF_701
            {Card.Cards.DMF_701t, 4},//深水炸弹 Dunk Tank ID：DMF_701t
            {Card.Cards.BT_100, 3},//毒蛇神殿传送门 Serpentshrine Portal ID：BT_100 
            //德鲁伊

            //猎人
            {Card.Cards.BAR_801, 1},//击伤猎物 Wound Prey ID：BAR_801
            {Card.Cards.CORE_DS1_185, 2},//奥术射击 Arcane Shot ID：CORE_DS1_185
            {Card.Cards.CORE_BRM_013, 3},//快速射击 Quick Shot ID：CORE_BRM_013
            {Card.Cards.BT_205, 3},//废铁射击 Scrap Shot ID：BT_205 
            //法师
            {Card.Cards.BAR_541, 2},//符文宝珠 Runed Orb ID：BAR_541 
            {Card.Cards.CORE_CS2_029, 6},//火球术 Fireball ID：CORE_CS2_029
            {Card.Cards.BT_291, 5},//埃匹希斯冲击 Apexis Blast ID：BT_291 
            //骑士
            {Card.Cards.CORE_CS2_093, 2},//奉献 Consecration ID：CORE_CS2_093 
            //牧师
            //盗贼
            {Card.Cards.BAR_319, 2},//邪恶挥刺（等级1） Wicked Stab (Rank 1) ID：BAR_319
            {Card.Cards.BAR_319t, 4},//邪恶挥刺（等级2） Wicked Stab (Rank 2) ID：BAR_319t
            {Card.Cards.BAR_319t2, 6},//邪恶挥刺（等级3） Wicked Stab (Rank 3) ID：BAR_319t2 
            {Card.Cards.CORE_CS2_075, 3},//影袭 Sinister Strike ID：CORE_CS2_075
            //术士
            {Card.Cards.CORE_CS2_062, 3},//地狱烈焰 Hellfire ID：CORE_CS2_062
            //战士
            //中立
            {Card.Cards.DREAM_02, 5},//伊瑟拉苏醒 Ysera Awakens ID：DREAM_02
        };
        //直伤随从卡牌（必须可以打脸）
        private static readonly Dictionary<Card.Cards, int> _MinionDamagesTable = new Dictionary<Card.Cards, int>
        {
            //盗贼
            {Card.Cards.BAR_316, 2},//油田伏击者 Oil Rig Ambusher ID：BAR_316 
            //萨满
            {Card.Cards.CORE_CS2_042, 4},//火元素 Fire Elemental ID：CORE_CS2_042 
            //德鲁伊
            //术士
            {Card.Cards.CORE_CS2_064, 1},//恐惧地狱火 Dread Infernal ID：CORE_CS2_064 
            //中立
            {Card.Cards.CORE_CS2_189, 1},//精灵弓箭手 Elven Archer ID：CORE_CS2_189
            {Card.Cards.CS3_031, 8},//生命的缚誓者阿莱克丝塔萨 Alexstrasza the Life-Binder ID：CS3_031 
            {Card.Cards.DMF_174t, 4},//马戏团医师 Circus Medic ID：DMF_174t
            {Card.Cards.DMF_066, 2},//小刀商贩 Knife Vendor ID：DMF_066 
            {Card.Cards.SCH_199t2, 2},//转校生 Transfer Student ID：SCH_199t2 
            {Card.Cards.SCH_273, 1},//莱斯·霜语 Ras Frostwhisper ID：SCH_273
            {Card.Cards.BT_187, 3},//凯恩·日怒 Kayn Sunfury ID：BT_187
            {Card.Cards.BT_717, 2},//潜地蝎 Burrowing Scorpid ID：BT_717 
            {Card.Cards.CORE_EX1_249, 2},//迦顿男爵 Baron Geddon ID：CORE_EX1_249 
        };
        #endregion

#region 攻击模式和自定义 

//Update time 2021-05-02
//更新时间 2021-05-02


      public ProfileParameters GetParameters(Board board)
      {

            var p = new ProfileParameters(BaseProfile.Rush) { DiscoverSimulationValueThresholdPercent = -10 };           
            //Bot.Log("玩家信息: " + rank+"/n"+Legend);
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
            //////定义场攻  用法 myAttack <= 5 自己场攻大于小于5  enemyAttack  <= 5 对面场攻大于小于5  已计算武器伤害

            int myMinionHealth = 0;
            int enemyMinionHealth = 0;

            if (board.MinionFriend != null)
            {
                for (int x = 0; x < board.MinionFriend.Count; x++)
                {
                    myMinionHealth += board.MinionFriend[x].CurrentHealth;
                }
            }

            if (board.MinionEnemy != null)
            {
                for (int x = 0; x < board.MinionEnemy.Count; x++)
                {
                    enemyMinionHealth += board.MinionEnemy[x].CurrentHealth;
                }
            }


 #endregion

#region 攻击优先 卡牌威胁（通用） 血色 更新到贫瘠之地


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ULD_231))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_231, new Modifier(200));
            }//连环腿大师 Whirlkick Master ULD_231 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_237))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_237, new Modifier(200));
            }//狂欢报幕员 Carnival Barker DMF_237 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_217))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_217, new Modifier(200));
            }//越线的游客 Line Hopper DMF_217 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_120))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_120, new Modifier(200));
            }//纳兹曼尼织血者 Nazmani Bloodweaver DMF_120  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_707))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_707, new Modifier(200));
            }//鱼人魔术师 Magicfin DMF_707 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_709))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_709, new Modifier(200));
            }//巨型图腾埃索尔 Grand Totem Eys'or DMF_709

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_082))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_082, new Modifier(200));
            }//暗月雕像 Darkmoon Statue DMF_082 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_082t))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_082t, new Modifier(200));
            }//暗月雕像 Darkmoon Statue ID：DMF_082t 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_708))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_708, new Modifier(200));
            }//伊纳拉·碎雷 Inara Stormcrash DMF_708

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_102))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_102, new Modifier(200));
            }//游戏管理员 Game Master DMF_102

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_222))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_222, new Modifier(200));
            }//获救的流民 Redeemed Pariah DMF_222

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ULD_003))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_003, new Modifier(200));
            }//了不起的杰弗里斯 Zephrys the Great ULD_003

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.GVG_104))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GVG_104, new Modifier(200));
            }//大胖 Hobgoblin GVG_104

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.UNG_900))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.UNG_900, new Modifier(250));
            }//如果对面场上有灵魂歌者安布拉，提高攻击优先度


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ULD_240))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_240, new Modifier(250));
            }//如果对面场上有对空奥术法师 Arcane Flakmage ID：ULD_240，提高攻击优先度


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_022 && minion.IsTaunt == false))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_022, new Modifier(50));
            }//如果对面场上有空灵，降低攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_004))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(50));
            }//如果对面场上有疯狂的科学家，降低攻击优先度


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BRM_002))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BRM_002, new Modifier(300));
            }//如果对面场上有火妖，提高攻击优先度


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CFM_020))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CFM_020, new Modifier(0));
            }//如果对面场上有缚链者拉兹 Raza the Chained CFM_020，降低攻击优先度                     


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.EX1_608))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_608, new Modifier(250));
            }//如果对面场上有巫师学徒 Sorcerer's Apprentice ID：X1_608，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.VAN_EX1_608))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.VAN_EX1_608, new Modifier(250));
            }//如果对面场上有巫师学徒 Sorcerer's Apprentice ID：VAN_EX1_608，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BOT_447))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_447, new Modifier(-10));
            }//如果对面场上有晶化师，降低攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SCH_600t3))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_600t3, new Modifier(250));
            }//如果对面场上有加攻击的恶魔伙伴，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DRG_320))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_320, new Modifier(0));
            }//如果对面场上有新伊瑟拉，降低攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CS2_237))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS2_237, new Modifier(300));
            }//如果对面场上有饥饿的秃鹫 Starving Buzzard CS2_237，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.VAN_CS2_237))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.VAN_CS2_237, new Modifier(300));
            }//如果对面场上有饥饿的秃鹫 Starving Buzzard VAN_CS2_237，提高攻击优先度





            //核心系列和贫瘠之地

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.YOP_031))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_031, new Modifier(250));
            }//如果对面场上有螃蟹骑士 Crabrider ID：YOP_031，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_537))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_537, new Modifier(200));
            }//如果对面场上有钢鬃卫兵 Razormane Battleguard BAR_537，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_033))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_033, new Modifier(210));
            }//如果对面场上有勘探者车队 Prospector's Caravan BAR_033，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_035))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_035, new Modifier(200));
            }//如果对面场上有科卡尔驯犬者 Kolkar Pack Runner BAR_035，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_871))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_871, new Modifier(250));
            }//如果对面场上有士兵车队 Soldier's Caravan BAR_871 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_312))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_312, new Modifier(200));
            }//如果对面场上有占卜者车队 Soothsayer's Caravan BAR_312，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_043))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_043, new Modifier(250));
            }//如果对面场上有鱼人宝宝车队 Tinyfin's Caravan BAR_043 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_860))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_860, new Modifier(250));
            }//如果对面场上有火焰术士弗洛格尔 Firemancer Flurgl BAR_860 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_063))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_063, new Modifier(250));
            }//如果对面场上有甜水鱼人斥候 Lushwater Scout BAR_063，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_074))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_074, new Modifier(200));
            }//如果对面场上有前沿哨所 Far Watch Post BAR_074 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_720))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_720, new Modifier(230));
            }//如果对面场上有古夫·符文图腾 Guff Runetotem BAR_720 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_038))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_038, new Modifier(200));
            }//如果对面场上有塔维什·雷矛 Tavish Stormpike BAR_038 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_545))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_545, new Modifier(200));
            }//如果对面场上有奥术发光体 Arcane Luminary BAR_545，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_888))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_888, new Modifier(200));
            }//如果对面场上有霜舌半人马 Rimetongue BAR_888  ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_317))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_317, new Modifier(200));
            }//如果对面场上有原野联络人 Field Contact BAR_317，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_918))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_918, new Modifier(250));
            }//如果对面场上有塔姆辛·罗姆 Tamsin Roame BAR_918，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_076))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_076, new Modifier(200));
            }//如果对面场上有莫尔杉哨所 Mor'shan Watch Post BAR_076  ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_890))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_890, new Modifier(200));
            }//如果对面场上有十字路口大嘴巴 Crossroads Gossiper BAR_890 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_082))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_082, new Modifier(200));
            }//如果对面场上有贫瘠之地诱捕者 Barrens Trapper BAR_082，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_540))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_540, new Modifier(200));
            }//如果对面场上有腐烂的普雷莫尔 Plaguemaw the Rotting BAR_540 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_878))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_878, new Modifier(200));
            }//如果对面场上有战地医师老兵 Veteran Warmedic BAR_878，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_048))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_048, new Modifier(200));
            }//如果对面场上有布鲁坎 Bru'kan BAR_048，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_075))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_075, new Modifier(200));
            }//如果对面场上有十字路口哨所 Crossroads Watch Post BAR_075，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_744))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_744, new Modifier(200));
            }//如果对面场上有灵魂医者 Spirit Healer BAR_744 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.FP1_028))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_028, new Modifier(200));
            }//如果对面场上有送葬者 Undertaker FP1_028 ，提高攻击优先度 
            
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CS3_019))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_019, new Modifier(200));
            }//如果对面场上有考瓦斯·血棘 Kor'vas Bloodthorn ID：CS3_019 ，提高攻击优先度 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CORE_FP1_031))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_031, new Modifier(200));
            }//如果对面场上有瑞文戴尔男爵 Baron Rivendare ID：CORE_FP1_031 ，提高攻击优先度 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CS3_032))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_032, new Modifier(200));
            }//如果对面场上有龙巢之母奥妮克希亚 Onyxia the Broodmother ID：CS3_032 ，提高攻击优先度   

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SCH_317))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_317, new Modifier(200));
            }//如果对面场上有团伙核心 Playmaker ID：SCH_317 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_847))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_847, new Modifier(200));
            }//如果对面场上有洛卡拉 Rokara ID：BAR_847 ，提高攻击优先度  


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CS3_025))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_025, new Modifier(200));
            }//如果对面场上有伦萨克大王 Overlord Runthak ID：CS3_025 ，提高攻击优先度  


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.YOP_021))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_021, new Modifier(200));
            }//如果对面场上有被禁锢的凤凰 Imprisoned Phoenix ID：YOP_021  ，提高攻击优先度  


        //    if ((board.HeroEnemy.CurrentHealth + board.HeroEnemy.CurrentArmor)>= 20
        //      && board.MinionEnemy.Count(x=>x.IsLifeSteal==true && x.Template.Id==Card.Cards.CS3_031)>=1
        //    )
        //    {
        //        p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_031, new Modifier(200));
        //    }//如果对面场上有生命的缚誓者阿莱克丝塔萨 Alexstrasza the Life-Binder ID：CS3_031 有吸血属性，提高攻击优先度
        //    else
        //    {
        //        p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_031, new Modifier(0));
        //    }//如果对面场上有生命的缚誓者阿莱克丝塔萨 Alexstrasza the Life-Binder ID：CS3_031 没吸血属性，降低攻击优先度



            if ((board.HeroEnemy.CurrentHealth + board.HeroEnemy.CurrentArmor)>= 20
                && board.MinionEnemy.Count(x=>x.IsLifeSteal==true && x.Template.Id==Card.Cards.CS3_033)>=1
            )
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_033, new Modifier(200));
            }//如果对面场上有沉睡者伊瑟拉 Ysera the Dreamer ID：CS3_033 有吸血属性，提高攻击优先度
            else
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_033, new Modifier(0));
            }//如果对面场上有沉睡者伊瑟拉 Ysera the Dreamer ID：CS3_033 没吸血属性，降低攻击优先度

                                   
            if ((board.HeroEnemy.CurrentHealth + board.HeroEnemy.CurrentArmor)>= 20
              && board.MinionEnemy.Count(x=>x.IsLifeSteal==true && x.Template.Id==Card.Cards.CS3_034)>=1
            )
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_034, new Modifier(200));
            }//如果对面场上有织法者玛里苟斯 Malygos the Spellweaver ID：CS3_034 有吸血属性，提高攻击优先度
            else
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_034, new Modifier(0));
            }//如果对面场上有织法者玛里苟斯 Malygos the Spellweaver ID：CS3_034 没吸血属性，降低攻击优先度


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CORE_EX1_110))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_110, new Modifier(0));
            }//如果对面场上有凯恩·血蹄 Cairne Bloodhoof ID：CORE_EX1_110 ，降低攻击优先度   


            //对面如果是盗贼 巴罗夫拉出来的怪威胁值优先（主要防止战吼怪被回手重新使用）
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_072))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_072, new Modifier(0));
            }//如果对面场上有火刃侍僧 Burning Blade Acolyte ID：BAR_072 ，降低攻击优先度   

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SCH_351))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_351, new Modifier(200));
            }//如果对面场上有詹迪斯·巴罗夫 Jandice Barov ID：SCH_351 ，提高攻击优先度  


            #endregion


#region sb不爱用的卡牌赋值

p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019, new Modifier(20));//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
p.CastMinionsModifiers.AddOrUpdate(Card.Cards.YOP_035, new Modifier(10));//月牙 Moonfang ID：YOP_035
p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(20));//食人魔巫术师 Ogremancer ID：SCH_710
p.PlayOrderModifiers.AddOrUpdate(Card.Cards.BAR_880, new Modifier(500));//1级定罪最先使用
p.PlayOrderModifiers.AddOrUpdate(Card.Cards.BT_018, new Modifier(500));//幽光鱼竿 Underlight Angling Rod ID：BT_018 最先使用


//buuf 优先给 螃蟹骑士 Crabrider ID：YOP_031 使用
//p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_CS2_092, new Modifier(-40, Card.Cards.YOP_031));//王者祝福 Blessing of Kings ID：CORE_CS2_092，螃蟹骑士 Crabrider ID：YOP_031
//p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_524, new Modifier(-40, Card.Cards.YOP_031));//荣誉护盾 Shield of Honor ID：SCH_524，螃蟹骑士 Crabrider ID：YOP_031
//p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_138, new Modifier(-40, Card.Cards.YOP_031));//威能祝福 Blessing of Authority ID：SCH_138，螃蟹骑士 Crabrider ID：YOP_031
//p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_025, new Modifier(-40, Card.Cards.YOP_031));//智慧圣契 Libram of Wisdom ID：BT_025，螃蟹骑士 Crabrider ID：YOP_031
//p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_292, new Modifier(-40, Card.Cards.YOP_031));//阿达尔之手 Hand of A'dal ID：BT_292，螃蟹骑士 Crabrider ID：YOP_031

#endregion

#region 对面没用过超凡之盟(正常游戏逻辑)
if (!board.EnemyGraveyard.Contains(Card.Cards.BAR_539))//超凡之盟 Celestial Alignment ID：BAR_539 
{
    
#region 食人魔巫术师


    if (board.HasCardOnBoard(Card.Cards.SCH_710)  //修饰食人魔巫术师 Ogremancer ID：SCH_710，数值越高越保守，就是不会拿去交换随从
    )
    {
        p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(200));
    }//修饰食人魔巫术师 Ogremancer ID：SCH_710，数值越高越保守，就是不会拿去交换随从 

        if(board.MaxMana >=5
            && board.Hand.Count(x=>x.CurrentCost>=5 && x.Template.Id==Card.Cards.SCH_710)>=1//食人魔巫术师 Ogremancer ID：SCH_710
            && myAttack >= enemyMinionHealth
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(-1000));
            Bot.Log("场攻大于对面小怪血量用食人魔");
        }

        if(board.MaxMana >=5
            && board.Hand.Count(x=>x.CurrentCost>=5 && x.Template.Id==Card.Cards.SCH_710)>=1//食人魔巫术师 Ogremancer ID：SCH_710
            && board.MinionEnemy.Count == 0
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(-1000));
            Bot.Log("对面没怪用食人魔");
        }

        if(board.MaxMana >=5
            && board.Hand.Count(x=>x.CurrentCost>=5 && x.Template.Id==Card.Cards.SCH_710)>=1//食人魔巫术师 Ogremancer ID：SCH_710
            && (board.EnemyClass == Card.CClass.MAGE
            || board.EnemyClass == Card.CClass.DRUID
            )
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(-500));
            Bot.Log("对面法德用食人魔");
        }   

        if(board.MaxMana >=5
            && board.Hand.Count(x=>x.CurrentCost>=5 && x.Template.Id==Card.Cards.SCH_710)>=1//食人魔巫术师 Ogremancer ID：SCH_710
            && board.MinionEnemy.Count == 0
            && (board.EnemyClass == Card.CClass.MAGE
            || board.EnemyClass == Card.CClass.DRUID
            )
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(-1000));
            Bot.Log("对面法德用食人魔");
        }        

        if(board.MaxMana ==4
            && board.Hand.Count(x=>x.CurrentCost==5 && x.Template.Id==Card.Cards.SCH_710)>=1//食人魔巫术师 Ogremancer ID：SCH_710
            && board.MinionEnemy.Count == 0
            && (board.HasCardInHand(Card.Cards.GAME_005)//幸运币 The Coin ID：GAME_005
            || board.HasCardInHand(Card.Cards.BAR_COIN1)//幸运币 The Coin ID：BAR_COIN1
            || board.HasCardInHand(Card.Cards.BAR_COIN2)//幸运币 The Coin ID：BAR_COIN2
            || board.HasCardInHand(Card.Cards.BAR_COIN3)//幸运币 The Coin ID：BAR_COIN3
            || board.HasCardInHand(Card.Cards.DMF_COIN1)//幸运币 The Coin ID：DMF_COIN1
            || board.HasCardInHand(Card.Cards.DMF_COIN2)//幸运币 The Coin ID：DMF_COIN2
            )
            && (board.EnemyClass == Card.CClass.MAGE
            || board.EnemyClass == Card.CClass.DRUID
            )
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(-1000));
            Bot.Log("4费对面法德没怪硬币食人魔");
        }                         

        if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_035)//科卡尔驯犬者 Kolkar Pack Runner BAR_035
        && myAttack <=2 
        && board.Hand.Count(x=>x.CurrentCost==5 && x.Template.Id==Card.Cards.SCH_710)>=1//食人魔巫术师 Ogremancer ID：SCH_710
        )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_710, new Modifier(-1000));
                Bot.Log("对面场上有科卡尔驯犬者解不掉，就用食人魔");
            }

    #endregion

//1费逻辑

#region 1费下牌逻辑


        if(board.MaxMana ==1
        && board.HasCardInHand(Card.Cards.CS3_007)//电击学徒 Novice Zapper ID：CS3_007
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS3_007, new Modifier(200));
        } //1费不用电击学徒


        if (board.MaxMana == 1
    && !board.HasCardInHand(Card.Cards.SCH_247)//新生入学 First Day of School ID：SCH_247
    && !board.HasCardInHand(Card.Cards.GAME_005)//幸运币 The Coin ID：GAME_005
    && !board.HasCardInHand(Card.Cards.BAR_COIN1)//幸运币 The Coin ID：BAR_COIN1
    && !board.HasCardInHand(Card.Cards.BAR_COIN2)//幸运币 The Coin ID：BAR_COIN2
    && !board.HasCardInHand(Card.Cards.BAR_COIN3)//幸运币 The Coin ID：BAR_COIN3
    && !board.HasCardInHand(Card.Cards.DMF_COIN1)//幸运币 The Coin ID：DMF_COIN1
    && !board.HasCardInHand(Card.Cards.DMF_COIN2)//幸运币 The Coin ID：DMF_COIN2
    && (board.HasCardInHand(Card.Cards.BAR_550)//迅疾救兵 Galloping Savior ID：BAR_550
    || board.HasCardInHand(Card.Cards.CS3_016)//清算 Reckoning ID：CS3_016
    || board.HasCardInHand(Card.Cards.CORE_EX1_130)//崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
    || board.HasCardInHand(Card.Cards.CORE_FP1_020)//复仇 Avenge ID：CORE_FP1_020
    || board.HasCardInHand(Card.Cards.DMF_236)//古神在上 Oh My Yogg! ID：DMF_236
    )
    && board.Hand.Count(x=>x.CurrentCost==1)==1 //只有1张1费
    )
    {
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_550, new Modifier(0)); //迅疾救兵 Galloping Savior ID：BAR_550
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS3_016, new Modifier(0)); //清算 Reckoning ID：CS3_016
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_130, new Modifier(0)); //崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_020, new Modifier(0)); //复仇 Avenge ID：CORE_FP1_020
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_236, new Modifier(0)); //古神在上 Oh My Yogg! ID：DMF_236 
    } //先手只有1张1费而且是奥秘  用奥秘 (防止1费空过)

        if (board.MaxMana == 1
    && (board.HasCardInHand(Card.Cards.SCH_247)//新生入学 First Day of School ID：SCH_247
    || board.Hand.Count(minion => minion.CurrentCost == 1)>=1 //1费怪大于等于1
    )
    && board.MinionEnemy.Count == 0  //对面场上没怪
    && (board.HasCardInHand(Card.Cards.GAME_005)//幸运币 The Coin ID：GAME_005
    || board.HasCardInHand(Card.Cards.BAR_COIN1)//幸运币 The Coin ID：BAR_COIN1
    || board.HasCardInHand(Card.Cards.BAR_COIN2)//幸运币 The Coin ID：BAR_COIN2
    || board.HasCardInHand(Card.Cards.BAR_COIN3)//幸运币 The Coin ID：BAR_COIN3
    || board.HasCardInHand(Card.Cards.DMF_COIN1)//幸运币 The Coin ID：DMF_COIN1
    || board.HasCardInHand(Card.Cards.DMF_COIN2)//幸运币 The Coin ID：DMF_COIN2
    )
    )
    {
p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(200));//幸运币 The Coin ID：GAME_005
p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_COIN1, new Modifier(200));//幸运币 The Coin ID：BAR_COIN1
p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_COIN2, new Modifier(200));//幸运币 The Coin ID：BAR_COIN2
p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_COIN3, new Modifier(200));//幸运币 The Coin ID：BAR_COIN3
p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_COIN1, new Modifier(200));//幸运币 The Coin ID：DMF_COIN1
p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_COIN2, new Modifier(200));//幸运币 The Coin ID：DMF_COIN2
    } //手上有新生入学或者1费怪对面场上没怪  禁止跳币


        if (board.MaxMana == 1
    && !board.HasCardInHand(Card.Cards.SCH_247)//新生入学 First Day of School ID：SCH_247
    && !board.HasCardInHand(Card.Cards.GAME_005)//幸运币 The Coin ID：GAME_005
    && !board.HasCardInHand(Card.Cards.BAR_COIN1)//幸运币 The Coin ID：BAR_COIN1
    && !board.HasCardInHand(Card.Cards.BAR_COIN2)//幸运币 The Coin ID：BAR_COIN2
    && !board.HasCardInHand(Card.Cards.BAR_COIN3)//幸运币 The Coin ID：BAR_COIN3
    && !board.HasCardInHand(Card.Cards.DMF_COIN1)//幸运币 The Coin ID：DMF_COIN1
    && !board.HasCardInHand(Card.Cards.DMF_COIN2)//幸运币 The Coin ID：DMF_COIN2
    && board.HasCardInHand(Card.Cards.BAR_880)//定罪（等级1） Conviction (Rank 1) ID：BAR_880
    && (board.HasCardInHand(Card.Cards.BAR_550)//迅疾救兵 Galloping Savior ID：BAR_550
    || board.HasCardInHand(Card.Cards.CS3_016)//清算 Reckoning ID：CS3_016
    || board.HasCardInHand(Card.Cards.CORE_EX1_130)//崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
    || board.HasCardInHand(Card.Cards.CORE_FP1_020)//复仇 Avenge ID：CORE_FP1_020
    || board.HasCardInHand(Card.Cards.DMF_236)//古神在上 Oh My Yogg! ID：DMF_236
    )
    && board.Hand.Count(x=>x.CurrentCost==1)==2 //只有2张1费
    )
    {
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_550, new Modifier(0)); //迅疾救兵 Galloping Savior ID：BAR_550
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS3_016, new Modifier(0)); //清算 Reckoning ID：CS3_016
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_130, new Modifier(0)); //崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_020, new Modifier(0)); //复仇 Avenge ID：CORE_FP1_020
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_236, new Modifier(0)); //古神在上 Oh My Yogg! ID：DMF_236 
    } //先手只有2张1费而且是奥秘和定罪  用奥秘  (防止1费空过)


        if (board.MaxMana == 1
    && board.HasCardInHand(Card.Cards.BAR_873)//圣礼骑士 Knight of Anointment ID：BAR_873
    && (board.HasCardInHand(Card.Cards.BAR_550)//迅疾救兵 Galloping Savior ID：BAR_550
    || board.HasCardInHand(Card.Cards.CS3_016)//清算 Reckoning ID：CS3_016
    || board.HasCardInHand(Card.Cards.CORE_EX1_130)//崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
    || board.HasCardInHand(Card.Cards.CORE_FP1_020)//复仇 Avenge ID：CORE_FP1_020
    || board.HasCardInHand(Card.Cards.DMF_236)//古神在上 Oh My Yogg! ID：DMF_236
    )
    )
    {
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_550, new Modifier(200)); //迅疾救兵 Galloping Savior ID：BAR_550
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS3_016, new Modifier(200)); //清算 Reckoning ID：CS3_016
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_130, new Modifier(200)); //崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_020, new Modifier(200)); //复仇 Avenge ID：CORE_FP1_020
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_236, new Modifier(200)); //古神在上 Oh My Yogg! ID：DMF_236 
    } //1费有奥秘和圣礼骑士  不用奥秘  

        if (board.MaxMana == 1
    && board.EnemyClass == Card.CClass.PALADIN
    && (board.HasCardInHand(Card.Cards.GAME_005)//幸运币 The Coin ID：GAME_005
    || board.HasCardInHand(Card.Cards.BAR_COIN1)//幸运币 The Coin ID：BAR_COIN1
    || board.HasCardInHand(Card.Cards.BAR_COIN2)//幸运币 The Coin ID：BAR_COIN2
    || board.HasCardInHand(Card.Cards.BAR_COIN3)//幸运币 The Coin ID：BAR_COIN3
    || board.HasCardInHand(Card.Cards.DMF_COIN1)//幸运币 The Coin ID：DMF_COIN1
    || board.HasCardInHand(Card.Cards.DMF_COIN2)//幸运币 The Coin ID：DMF_COIN2
    )
    && board.HasCardInHand(Card.Cards.BAR_873)//圣礼骑士 Knight of Anointment ID：BAR_873
    && board.HasCardInHand(Card.Cards.BT_019)//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
    && board.MinionEnemy.Count >= 1 
    )
    {
       p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019, new Modifier(-1000));//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
    } //1费后手对面场上有怪顶鱼人 

        if (board.MaxMana == 1
    && (board.HasCardInHand(Card.Cards.GAME_005)//幸运币 The Coin ID：GAME_005
    || board.HasCardInHand(Card.Cards.BAR_COIN1)//幸运币 The Coin ID：BAR_COIN1
    || board.HasCardInHand(Card.Cards.BAR_COIN2)//幸运币 The Coin ID：BAR_COIN2
    || board.HasCardInHand(Card.Cards.BAR_COIN3)//幸运币 The Coin ID：BAR_COIN3
    || board.HasCardInHand(Card.Cards.DMF_COIN1)//幸运币 The Coin ID：DMF_COIN1
    || board.HasCardInHand(Card.Cards.DMF_COIN2)//幸运币 The Coin ID：DMF_COIN2
    )
    && !board.HasCardInHand(Card.Cards.SCH_247)//新生入学 First Day of School ID：SCH_247  
    && !board.HasCardInHand(Card.Cards.BAR_873)//圣礼骑士 Knight of Anointment ID：BAR_873
    && board.HasCardInHand(Card.Cards.BAR_875)//逝者之剑 BAR_875 Sword of The Fallen
    && (board.HasCardInHand(Card.Cards.BT_019)//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
    || board.HasCardInHand(Card.Cards.BT_019)//螃蟹骑士 Crabrider ID：YOP_031
    || board.HasCardInHand(Card.Cards.BT_019)//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
    ))
    {
       p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BAR_875, new Modifier(500));//逝者之剑 BAR_875 Sword of The Fallen
       p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019, new Modifier(-50));//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
       p.CastMinionsModifiers.AddOrUpdate(Card.Cards.YOP_031, new Modifier(-200));//螃蟹骑士 Crabrider ID：YOP_031
       p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_007, new Modifier(0));//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
    } //1费后手没有1费怪可以用  不硬币刀  硬币下怪

        if (board.MaxMana == 1
    && (board.HasCardInHand(Card.Cards.GAME_005)//幸运币 The Coin ID：GAME_005
    || board.HasCardInHand(Card.Cards.BAR_COIN1)//幸运币 The Coin ID：BAR_COIN1
    || board.HasCardInHand(Card.Cards.BAR_COIN2)//幸运币 The Coin ID：BAR_COIN2
    || board.HasCardInHand(Card.Cards.BAR_COIN3)//幸运币 The Coin ID：BAR_COIN3
    || board.HasCardInHand(Card.Cards.DMF_COIN1)//幸运币 The Coin ID：DMF_COIN1
    || board.HasCardInHand(Card.Cards.DMF_COIN2)//幸运币 The Coin ID：DMF_COIN2
    )
    && !board.HasCardInHand(Card.Cards.SCH_247)//新生入学 First Day of School ID：SCH_247  
    && !board.HasCardInHand(Card.Cards.BAR_873)//圣礼骑士 Knight of Anointment ID：BAR_873
    && board.HasCardInHand(Card.Cards.BAR_875)//逝者之剑 BAR_875 Sword of The Fallen
    && !board.HasCardInHand(Card.Cards.BT_019)//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
    && !board.HasCardInHand(Card.Cards.BT_019)//螃蟹骑士 Crabrider ID：YOP_031
    && !board.HasCardInHand(Card.Cards.BT_019)//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
    )
    {
       p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BAR_875, new Modifier(-200));//逝者之剑 BAR_875 Sword of The Fallen
    } //1费后手什么都没  硬币刀
    

     #endregion

//2费逻辑


#region 2费下怪逻辑

       if(board.MaxMana ==2
            && board.HasCardInHand(Card.Cards.CORE_FP1_007)//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
            && (board.HasCardInHand(Card.Cards.BT_292)//阿达尔之手 Hand of A'dal ID：BT_292
            || board.HasCardInHand(Card.Cards.CORE_CS2_092)//王者祝福 Blessing of Kings ID：CORE_CS2_092
            || board.HasCardInHand(Card.Cards.SCH_138)//威能祝福 Blessing of Authority ID：SCH_138 
            || board.HasCardInHand(Card.Cards.BAR_880)//定罪（等级1） Conviction (Rank 1) ID：BAR_880
            || board.HasCardInHand(Card.Cards.BAR_880t)//定罪（等级2） Conviction (Rank 2) ID：BAR_880t
            || board.HasCardInHand(Card.Cards.BAR_880t2)//定罪（等级3） Conviction (Rank 3) ID：BAR_880t2
            || board.HasCardInHand(Card.Cards.CORE_CS2_188)//叫嚣的中士 Abusive Sergeant ID：CORE_CS2_188
        ))
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_007, new Modifier(-200));
        }//等于2费有buff  下蛋

        if(board.MaxMana >=3
            && board.HasCardInHand(Card.Cards.CORE_FP1_007)//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
            && (board.MinionEnemy.Count == 0
            || myAttack >= enemyMinionHealth)
            && (board.HasCardInHand(Card.Cards.BT_292)//阿达尔之手 Hand of A'dal ID：BT_292
            || board.HasCardInHand(Card.Cards.CORE_CS2_092)//王者祝福 Blessing of Kings ID：CORE_CS2_092
            || board.HasCardInHand(Card.Cards.SCH_138)//威能祝福 Blessing of Authority ID：SCH_138 
            || board.HasCardInHand(Card.Cards.BAR_880)//定罪（等级1） Conviction (Rank 1) ID：BAR_880
            || board.HasCardInHand(Card.Cards.BAR_880t)//定罪（等级2） Conviction (Rank 2) ID：BAR_880t
            || board.HasCardInHand(Card.Cards.BAR_880t2)//定罪（等级3） Conviction (Rank 3) ID：BAR_880t2
            || board.HasCardInHand(Card.Cards.CORE_CS2_188)//叫嚣的中士 Abusive Sergeant ID：CORE_CS2_188
        ))
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_007, new Modifier(-200));
        }//大于2费有buff 对面没怪或者自己场攻大于等于对面怪的血量 下蛋


        if(board.MaxMana ==2
            && board.HasCardInHand(Card.Cards.BAR_875)//逝者之剑 BAR_875 Sword of The Fallen
            && board.HasCardInHand(Card.Cards.BAR_876)//北卫军指挥官 BAR_876
        )
        {
            p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BAR_875, new Modifier(-200));
        }//手上有奥秘刀和334 用奥秘刀

        if(board.MaxMana == 3
            && board.Secret.Count == 0     
            && board.HasCardInHand(Card.Cards.BAR_876)//北卫军指挥官 BAR_876
            && (board.HasCardInHand(Card.Cards.BAR_875)//逝者之剑 BAR_875 Sword of The Fallen
            || board.HasCardInHand(Card.Cards.BAR_550)//迅疾救兵 Galloping Savior ID：BAR_550
            || board.HasCardInHand(Card.Cards.CS3_016)//清算 Reckoning ID：CS3_016
            || board.HasCardInHand(Card.Cards.CORE_EX1_130)//崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
            || board.HasCardInHand(Card.Cards.CORE_FP1_020)//复仇 Avenge ID：CORE_FP1_020
            || board.HasCardInHand(Card.Cards.DMF_236)//古神在上 Oh My Yogg! ID：DMF_236
            )
            )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_876, new Modifier(200));
        }//3费头上没奥秘  手上有奥秘或者奥秘刀  不用334

        if (board.MaxMana == 2
    && (board.HasCardInHand(Card.Cards.BT_019)//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019
    || board.HasCardInHand(Card.Cards.YOP_031)//螃蟹骑士 Crabrider ID：YOP_031
    || board.HasCardInHand(Card.Cards.CORE_FP1_007)//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
    || board.HasCardInHand(Card.Cards.BAR_875)//逝者之剑 BAR_875 Sword of The Fallen
    || board.HasCardInHand(Card.Cards.CORE_FP1_007)//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
    )
    )
    {
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_550, new Modifier(200)); //迅疾救兵 Galloping Savior ID：BAR_550
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS3_016, new Modifier(200)); //清算 Reckoning ID：CS3_016
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_130, new Modifier(200)); //崇高牺牲 Noble Sacrifice ID：CORE_EX1_130
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_020, new Modifier(200)); //复仇 Avenge ID：CORE_FP1_020
         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_236, new Modifier(200)); //古神在上 Oh My Yogg! ID：DMF_236 
    } //2费不用奥秘 下怪 或者刀

        if (board.MaxMana == 2
    && board.HasCardInHand(Card.Cards.BAR_875)//逝者之剑 BAR_875 Sword of The Fallen
    && !board.HasCardInHand(Card.Cards.BAR_876)//北卫军指挥官 BAR_876
    && (board.HasCardInHand(Card.Cards.BT_019)//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019
    || board.HasCardInHand(Card.Cards.YOP_031)//螃蟹骑士 Crabrider ID：YOP_031
    || board.HasCardInHand(Card.Cards.CORE_FP1_007)//蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
    )
    )
    {
       p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.BAR_875, new Modifier(200));
    } //2费有刀和其他2费没334  不用刀

        if(board.MaxMana ==2
            && !board.HasCardInHand(Card.Cards.GAME_005)//硬币 GAME_005
            && board.MinionFriend.Count == 0
            && board.MinionEnemy.Count >= 1
            && (board.HasCardInHand(Card.Cards.BT_019)//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019
            || board.HasCardInHand(Card.Cards.YOP_031)//螃蟹骑士 Crabrider ID：YOP_031
        ))
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019, new Modifier(-200));//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019, new Modifier(-200));//螃蟹骑士 Crabrider ID：YOP_031
        }//2费对面场上有怪自己场上没怪没硬币 顶鱼


        if (board.MaxMana == 2
    && board.EnemyClass == Card.CClass.PALADIN
    && board.HasCardInHand(Card.Cards.BAR_873)//圣礼骑士 Knight of Anointment ID：BAR_873
    && board.HasCardInHand(Card.Cards.BT_019)//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
    && board.MinionEnemy.Count >= 1 
    )
    {
       p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019, new Modifier(-1000));//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019  
    } //2费场上有怪顶鱼人 

#endregion



//特定卡牌逻辑

#region 复仇

if (board.HasCardInHand(Card.Cards.CORE_FP1_020)//复仇 Avenge ID：CORE_FP1_020
    && (board.HasCardOnBoard(Card.Cards.CORE_FP1_007)//自己场上有蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
    || board.MinionFriend.Count >= 2 //或者自己场上的怪大于等于2
    )
    )
 {
    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_020, new Modifier(20));//稍微提高复仇
 }
#endregion

#region 维克图斯

if (!board.FriendGraveyard.Contains(Card.Cards.BT_019) //莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019
&& !board.FriendGraveyard.Contains(Card.Cards.CORE_FP1_007) //蛛魔之卵 Nerubian Egg ID：CORE_FP1_007
&& !board.FriendGraveyard.Contains(Card.Cards.CS3_024) //泰兰·弗丁 Taelan Fordring ID：CS3_024
&& !board.FriendGraveyard.Contains(Card.Cards.DMF_069) //娃娃机 Claw Machine ID：DMF_069
)
{
    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_162, new Modifier(500));//维克图斯 Vectus ID：SCH_162
}//墓地没亡语怪  禁止使用维克图斯

p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_162t, new Modifier(0));//降低魔药龙崽的保守 Plagued Hatchling ID：SCH_162t 优先拿去送

p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_040, new Modifier(200, Card.Cards.SCH_162t));//南海岸酋长 South Coast Chieftain ID：BAR_040，魔药龙崽 Plagued Hatchling ID：SCH_162t
//但是禁止使用打2打自己的怪
     #endregion

#region 定罪

        if(board.MaxMana <=4
            && board.HasCardInHand(Card.Cards.BAR_880)//定罪（等级1） Conviction (Rank 1) ID：BAR_880
            && (board.MinionEnemy.Count == 0
            || myAttack >= enemyMinionHealth)
        )
        {
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BAR_880, new Modifier(5000));
        } //4费前对面没怪或者自己场攻大于等于对面怪的血量 不用1级定罪



     #endregion

#region 新生入学防奥秘

if (board.SecretEnemy
&& (board.EnemyClass == Card.CClass.PALADIN
|| board.EnemyClass == Card.CClass.MAGE
)
)
{
    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_247, new Modifier(-1000));//提高新生入学优先
    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(-1000));//提高硬币优先
    p.PlayOrderModifiers.AddOrUpdate(Card.Cards.SCH_247, new Modifier(2000)); //最先用新生入学
    p.PlayOrderModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(1500)); //硬币 GAME_005
}


#endregion

#region 大型魔像

        if(board.ManaAvailable >= 5
            && board.HasCardInHand(Card.Cards.BAR_079_m2)//大型魔像 Greater Golem ID：BAR_079_m2
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_079_m2, new Modifier(-200));
        } //剩余5费提高用大型魔像优先

#endregion

}
#endregion

#region 对面用过超凡之盟(专用逻辑)

if (board.EnemyGraveyard.Contains(Card.Cards.BAR_539))//超凡之盟 Celestial Alignment ID：BAR_539 
{

}

#endregion

//德：DRUID 猎：HUNTER 法：MAGE 骑：PALADIN 牧：PRIEST 贼：ROGUE 萨：SHAMAN 术：WARLOCK 战：WARRIOR 瞎：DEMONHUNTER
            return p;
        }}
        
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
                    //以友方随从攻击力 降序排序 的 场上的所有友方随���集合，如果该集合存在生命值大于与敌方随从攻击力
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