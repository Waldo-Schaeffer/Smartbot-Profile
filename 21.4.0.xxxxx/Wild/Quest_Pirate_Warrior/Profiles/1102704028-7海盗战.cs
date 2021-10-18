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
 
 /* 
 * 
 * https://github.com/Waldo-Schaeffer
 * https://gitee.com/m586
 * Copyleft 2016 - 2020 SunGuanqi. All Rights Reserved
 * Attribution 4.0 International (Attribution 4.0 International (CC BY 4.0)
 * 使用时请遵守知识共享署名 4.0 国际许可协议，且不可删除本版权信息
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
            {SteadyShot, 9},//稳固射击 Steady Shot
            {DemonsbiteUp, 9},
            {LifeTap,9},//生命分流 Life Tap
            {DaggerMastery,4},//匕首精通 Dagger Mastery
            {Reinforce, 7},//援军 Reinforce
            {Shapeshift, 6},//变形 Shapeshift
            {DemonsBite, 8},
            {Fireblast, 5},//火焰冲击 Fireblast
            {ArmorUp, 3},//全副武装” "Armor Up"
            {LesserHeal, 1},//次级治疗术 Lesser Heal
        };

        //直伤卡牌
        private static readonly Dictionary<Card.Cards, int> _spellDamagesTable = new Dictionary<Card.Cards, int>
        {
            
          
            {Card.Cards.BT_100, 3},//毒蛇神殿传送门 Serpentshrine Portal  ID：BT_100
            {Card.Cards.TRL_012, 2},//图腾重击 Totemic Smash  ID：TRL_012
            {Card.Cards.CFM_707, 4},//青玉闪电 Jade Lightning  ID：CFM_707
            {Card.Cards.BRM_011, 2},//熔岩震击 Lava Shock  ID：BRM_011
            {Card.Cards.EX1_238, 3},//闪电箭 Lightning Bolt  ID：EX1_238
            {Card.Cards.EX1_241, 5},//熔岩爆裂 Lava Burst  ID：EX1_241
            {Card.Cards.CS2_037, 1},//冰霜震击 Frost Shock  ID：CS2_037
            {Card.Cards.DAL_614, 2},//狗头人跟班 Kobold Lackey  ID：DAL_614
            {Card.Cards.GIL_530, 2},//阴燃电鳗 Murkspark Eel  ID：GIL_530
            {Card.Cards.GVG_038, 6},//连环爆裂 Crackle ID：GVG_038
            {Card.Cards.EX1_116, 6},//火车王里诺艾 Leeroy Jenkins  ID：EX1_116
            {Card.Cards.CS2_087, 3},//力量祝福 Blessing of Might  ID：CS2_087
			//sm血色 偷你妈的头

        };


        //攻击模式
        


        public ProfileParameters GetParameters(Board board)
        {

		//version 2.4.1 for 19.0HS

           

            var p = new ProfileParameters(BaseProfile.Rush) { DiscoverSimulationValueThresholdPercent = -10 };
            p.CastSpellsModifiers.AddOrUpdate(TheCoin, new Modifier(85));

            //自定义命名
            int a = (board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - BoardHelper.GetEnemyHealthAndArmor(board);//敌我血量差



 //攻击模式切换
            if (board.EnemyClass == Card.CClass.DEMONHUNTER
                || board.EnemyClass == Card.CClass.HUNTER
                || board.EnemyClass == Card.CClass.ROGUE
                || board.EnemyClass == Card.CClass.SHAMAN
                || board.EnemyClass == Card.CClass.DRUID
                || board.EnemyClass == Card.CClass.PALADIN
                || board.EnemyClass == Card.CClass.WARRIOR)
            {
                p.GlobalAggroModifier = (int)(a * 0.625 + 110.5);
            }
            else
            {
                p.GlobalAggroModifier = (int)(a * 0.625 + 110.5);
            }
            Bot.Log("攻击性：" + p.GlobalAggroModifier.Value);

            if (!board.MinionEnemy.Any(x => x.IsTaunt) &&
                   (BoardHelper.GetEnemyHealthAndArmor(board) -
                  BoardHelper.GetPotentialMinionDamages(board) -
                BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <=
                BoardHelper.GetTotalBlastDamagesInHand(board)))
            {
                p.GlobalAggroModifier = 450;
            }//如果下一轮可以斩杀对面，攻击性提高

			//判定是否过载
			// bool guozai;
			// if (board.LockedMana == 0
			// 	&& board.OverloadedMana == 0){
			// 	guozai = false;
			// }else{
			// 	guozai = true;
			// }
			
			//卡片集合
			var ret = new List<Card.Cards>();

			//当前剩余的法力水晶
			var manaAvailable = board.ManaAvailable;
			
			//计算可用水晶数
			//如果手上有雷霆绽放，手动计算费用（Wirmate偷懒把雷霆绽放和硬币当成一个东西了）
			if (board.HasCardInHand(Card.Cards.SCH_427)){
				manaAvailable = manaAvailable + (board.Hand.Count(x => x.Template.Id == Card.Cards.SCH_427) * 2);
			}
			
			//如果手上有硬币，手动计算费用（Wirmate偷懒把雷霆绽放和硬币当成一个东西了）
			if (board.HasCardInHand(Card.Cards.GAME_005)){
				manaAvailable = manaAvailable + (board.Hand.Count(x => x.Template.Id == Card.Cards.GAME_005) * 1);
			}
			
			//日志输出
			//我方本回合可用水晶
			Bot.Log("我方本回合可用水晶:" + board.ManaAvailable);
			//我方最大法力水晶
			Bot.Log("我方最大法力水晶:" + board.MaxMana);
			//法术提供的可用总水晶上限
			Bot.Log("法术提供的可用总水晶上限:" + manaAvailable);
			
			//如果对面是奇数骑，优先解掉白银之手新兵
			// if (board.EnemyAbility.Template.Id == Card.Cards.HERO_04bp2){
			// 	p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS2_101t, new Modifier(200));
			// }
            //如果对面是德，随从大于等于4，优先解场
			if (board.EnemyAbility.Template.Id == Card.Cards.HERO_06bp
               && board.MinionEnemy.Count >= 4
            )
            {
				p.GlobalAggroModifier = 40;
			}
            
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
				







                //如果下一轮可以斩杀对面提高洛欧塞布 Loatheb  ID：FP1_030
                if (board.HasCardInHand(Card.Cards.FP1_030)//洛欧塞布 Loatheb  ID：FP1_030
                && !board.MinionEnemy.Any(x => x.IsTaunt)
                && (BoardHelper.GetEnemyHealthAndArmor(board) - BoardHelper.GetPotentialMinionDamages(board) - BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <= BoardHelper.GetTotalBlastDamagesInHand(board))
                || myAttack >= (board.HeroEnemy.CurrentHealth) - 5
                ){
					p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030, new Modifier(-500));//洛欧塞布 Loatheb  ID：FP1_030
                }
                // //如果下一轮可以斩杀对面提高洛欧塞布 Loatheb  ID：FP1_030
                // if (board.SecretEnemy == 0 //对手有奥秘
                // && !board.MinionEnemy.Any(x => x.IsTaunt)
                // && (BoardHelper.GetEnemyHealthAndArmor(board) - BoardHelper.GetPotentialMinionDamages(board) - BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <= BoardHelper.GetTotalBlastDamagesInHand(board))
                // ){
				// 	p.GlobalAggroModifier = 450;
                // }

            
                //具体策略

             


      
            //如果场上多过4个怪，而且手上有洛欧塞布，提高洛欧塞布的优先级
			if ((board.MinionFriend.Count >= 4)
               && (board.ManaAvailable >= 5)
               && (board.Hand.Exists(x => x.Template.Id == Card.Cards.FP1_030 ))
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030 , new Modifier(-50));
            }
            else
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030 , new Modifier(250));
            }
			


            //如果对方费用大于5，而且对面是牧师，提高洛欧塞布和古神的优先级
            if ((board.EnemyMaxMana >= 5)
               && (board.EnemyClass == Card.CClass.PRIEST)
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030 , new Modifier(-50));
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_236 , new Modifier(-50));
            }
			
			//对面是牧师，火车王随便下
			if (board.EnemyClass == Card.CClass.PRIEST)
			{
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116, new Modifier(-50));//修改火车王的优先级
                    //提高雏龙威胁值雏龙 Whelp ID：EX1_116t 
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116t, new Modifier(100));
			}
			//对面是牧师，随便下
			if (board.EnemyClass == Card.CClass.PRIEST)
			{
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_926, new Modifier(-999));//身陷绝境的哨卫 Cornered Sentry ID：UNG_926 
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.UNG_076t1, new Modifier(100));//迅猛龙 Raptor ID：UNG_076t1 
			}
			//对面是牧师，厄运信天翁 Bad Luck Albatross ID：DRG_071随便下
			if (board.EnemyClass == Card.CClass.PRIEST
            && board.EnemyClass == Card.CClass.WARLOCK
            )
			{
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_071, new Modifier(-999));//厄运信天翁 Bad Luck Albatross ID：DRG_071
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_071t, new Modifier(100));//信天翁 Albatross ID：DRG_071t 

			}

			//对面是牧师，厄运信天翁 Bad Luck Albatross ID：DRG_071随便下
			if (board.EnemyClass == Card.CClass.MAGE)
			{
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_071, new Modifier(-999));//厄运信天翁 Bad Luck Albatross ID：DRG_071
			// p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_071t, new Modifier(200));//信天翁 Albatross ID：DRG_071t 

			}

    

            //对面没随从,降低扫帚和巴罗夫优先级
			if (board.MinionEnemy.Count <=1
            && board.HasCardInHand(Card.Cards.SCH_311)//活化扫帚 Animated Broomstick  ID：SCH_311
            || board.HasCardInHand(Card.Cards.SCH_526)//巴罗夫领主 Lord Barov  ID：SCH_526
            )
			{
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_311, new Modifier(150)); //活化扫帚 Animated Broomstick  ID：SCH_311
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_526, new Modifier(200)); //巴罗夫领主 Lord Barov  ID：SCH_526
			}
            //对面随从大于3,提高巴罗夫
            if (board.MinionEnemy.Count>board.MinionFriend.Count
            &&  board.HasCardInHand(Card.Cards.SCH_526)//巴罗夫领主 Lord Barov  ID：SCH_526
            )
			{
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_526, new Modifier(-20)); //巴罗夫领主 Lord Barov  ID：SCH_526
			}
//海盗战策略
  //当手上有圣剑扳手 Wrenchcalibur  ID：DAL_063时，提高升级和334优先级
            if (    board.WeaponFriend != null 
                && (board.WeaponFriend.Template.Id == Card.Cards.DAL_063)//圣剑扳手 Wrenchcalibur ID：DAL_063
                && board.HasCardInHand(Card.Cards.EX1_409)//升级 Upgrade!  ID：EX1_409
                || board.HasCardInHand(Card.Cards.OG_315)//血帆教徒 Bloodsail Cultist  ID：OG_315
                || board.HasCardInHand(Card.Cards.NEW1_024)//绿皮船长 Captain Greenskin  ID：NEW1_024
                ) ;
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_409, new Modifier(-999));//升级 Upgrade!  ID：EX1_409
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_315, new Modifier(-999));//血帆教徒 Bloodsail Cultist  ID：OG_315
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_024, new Modifier(-999));//绿皮船长 Captain Greenskin  ID：NEW1_024
            }
  //当手上有海盗之锚 Ancharrr  ID：DRG_025时，提高升级和334优先级
            if (board.WeaponFriend != null 
                && (board.WeaponFriend.Template.Id == Card.Cards.DRG_025)
                && board.HasCardInHand(Card.Cards.EX1_409)//升级 Upgrade!  ID：EX1_409
                || board.HasCardInHand(Card.Cards.OG_315)//血帆教徒 Bloodsail Cultist  ID：OG_315
                || board.HasCardInHand(Card.Cards.NEW1_024)//绿皮船长 Captain Greenskin  ID：NEW1_024
                ) ;
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_409, new Modifier(-9999));//升级 Upgrade!  ID：EX1_409
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_315, new Modifier(-9999));//血帆教徒 Bloodsail Cultist  ID：OG_315
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_024, new Modifier(-999));//绿皮船长 Captain Greenskin  ID：NEW1_024
            }

           


            //当手上有海盗之锚 Ancharrr  ID：DRG_025时，降低炸弹刀优先值，降低波力优先值
            if (   board.WeaponFriend != null 
                && (board.WeaponFriend.Template.Id == Card.Cards.DRG_025)//海盗之锚 Ancharrr  ID：DRG_025
                && board.WeaponFriend.CurrentDurability >= 1
                && board.HasCardInHand(Card.Cards.OG_312)// Card.Cards.OG_312,//恩佐斯的副官
                || board.HasCardInHand(Card.Cards.DAL_063)//Card.Cards.DAL_063,//圣剑扳手
                || board.HasCardInHand(Card.Cards.DRG_055)//藏宝匪贼 Hoard Pillager  ID：DRG_055
                || board.HasCardInHand(Card.Cards.DMF_521)//吞剑艺人 Sword Eater ID：DMF_521 
                ) ;
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_312, new Modifier(999));//Card.Cards.OG_312,//恩佐斯的副官
                p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DAL_063, new Modifier(999));//Card.Cards.DAL_063,//圣剑扳手
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_055, new Modifier(999));//藏宝匪贼 Hoard Pillager  ID：DRG_055
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_521, new Modifier(999));//吞剑艺人 Sword Eater ID：DMF_521 
            }


            //当手上有Card.Cards.DAL_063,//圣剑扳手时，降低海盗刀优先值，降低波力优先值
            if (   board.WeaponFriend != null 
                && (board.WeaponFriend.Template.Id == Card.Cards.DAL_063)//Card.Cards.DAL_063,//圣剑扳手
                && board.WeaponFriend.CurrentDurability >= 1
                && board.HasCardInHand(Card.Cards.OG_312)// Card.Cards.OG_312,//恩佐斯的副官
                || board.HasCardInHand(Card.Cards.DRG_025)//海盗之锚 Ancharrr  ID：DRG_025
                || board.HasCardInHand(Card.Cards.DRG_055)//藏宝匪贼 Hoard Pillager  ID：DRG_055
                || board.HasCardInHand(Card.Cards.DMF_521)//吞剑艺人 Sword Eater ID：DMF_521 
                ) ;
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_312, new Modifier(999));//Card.Cards.OG_312,//恩佐斯的副官
                p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(999));//海盗之锚 Ancharrr  ID：DRG_025
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_055, new Modifier(999));//藏宝匪贼 Hoard Pillager  ID：DRG_055
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_521, new Modifier(999));//吞剑艺人 Sword Eater ID：DMF_521 
            }
            //当手上有断颚之刃 Jawbreaker ID：DMF_521t 时，降低海盗刀优先值，降低波力优先值
            if (board.WeaponFriend != null 
                && (board.WeaponFriend.Template.Id == Card.Cards.DMF_521t)
                && board.WeaponFriend.CurrentDurability >= 1
                && board.HasCardInHand(Card.Cards.OG_312)// Card.Cards.OG_312,//恩佐斯的副官
                || board.HasCardInHand(Card.Cards.DRG_025)//海盗之锚 Ancharrr  ID：DRG_025
                || board.HasCardInHand(Card.Cards.DRG_055)//藏宝匪贼 Hoard Pillager  ID：DRG_055
                || board.HasCardInHand(Card.Cards.DMF_521)//吞剑艺人 Sword Eater ID：DMF_521 
                || board.HasCardInHand(Card.Cards.DAL_063)//Card.Cards.DAL_063,//圣剑扳手
                ) ;
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_312, new Modifier(999));//Card.Cards.OG_312,//恩佐斯的副官
                p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(999));//海盗之锚 Ancharrr  ID：DRG_025
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_055, new Modifier(999));//藏宝匪贼 Hoard Pillager  ID：DRG_055
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_521, new Modifier(999));//吞剑艺人 Sword Eater ID：DMF_521 
                p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DAL_063, new Modifier(999));//Card.Cards.DAL_063,//圣剑扳手
            }

            //当手上没有有海盗之锚 Ancharrr  ID：DRG_025时，降低升级优先值，降低波力优先值
            if (board.HasCardInHand(Card.Cards.EX1_409)//升级 Upgrade!  ID：EX1_409
                 && !(board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.DRG_025)//海盗之锚 Ancharrr  ID：DRG_025
                || !(board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.DAL_063)//Card.Cards.DAL_063,//圣剑扳手
                ) ;
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_409, new Modifier(300));//升级 Upgrade!  ID：EX1_409
            }





                //当手上有锈蚀铁钩 Rusty Hook  ID：OG_058时，降低升级和334优先级
                if ((board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.OG_058)//锈蚀铁钩 Rusty Hook  ID：OG_058
                && board.HasCardInHand(Card.Cards.EX1_409)//升级 Upgrade!  ID：EX1_409
                || board.HasCardInHand(Card.Cards.OG_315)//血帆教徒 Bloodsail Cultist  ID：OG_315
                || board.HasCardInHand(Card.Cards.NEW1_024)//绿皮船长 Captain Greenskin  ID：NEW1_024
                ) ;
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_409, new Modifier(300));//升级 Upgrade!  ID：EX1_409
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_315, new Modifier(300));//血帆教徒 Bloodsail Cultist  ID：OG_315
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_024, new Modifier(300));//绿皮船长 Captain Greenskin  ID：NEW1_024
            }



            //当手上有重斧 Heavy Axe  ID：EX1_409t时，降低升级和334优先级
            if ((board.WeaponFriend != null && board.WeaponFriend.Template.Id == Card.Cards.EX1_409t)//重斧 Heavy Axe  ID：EX1_409t
                && board.HasCardInHand(Card.Cards.EX1_409)//升级 Upgrade!  ID：EX1_409
                || board.HasCardInHand(Card.Cards.OG_315)//血帆教徒 Bloodsail Cultist  ID：OG_315
                || board.HasCardInHand(Card.Cards.NEW1_024)//绿皮船长 Captain Greenskin  ID：NEW1_024
                ) ;
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_409, new Modifier(300));//升级 Upgrade!  ID：EX1_409
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_315, new Modifier(300));//血帆教徒 Bloodsail Cultist  ID：OG_315
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_024, new Modifier(300));//绿皮船长 Captain Greenskin  ID：NEW1_024
            }

            //if (board.ManaAvailable < 7
              //  && board.Hand.Exists(x => x.Template.Id == Card.Cards.DRG_025))
            //{
              //  p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-50));//7费前优先挂海盗之锚
            //}

            if ( board.Hand.Exists(x => x.Template.Id == Card.Cards.DAL_063)//Card.Cards.DAL_063,//圣剑扳手
               && board.EnemyClass == Card.CClass.PRIEST //对方是牧师
               || board.EnemyClass == Card.CClass.MAGE //对方是法师
               )
            {
                p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DAL_063, new Modifier(-50));//牧师优先挂扳手
            }


            if (board.Hand.Exists(x => x.Template.Id == Card.Cards.BT_124)//海盗藏品 Corsair Cache  ID：BT_124
              && board.EnemyClass == Card.CClass.PRIEST //对方是牧师
              || board.EnemyClass == Card.CClass.MAGE //对方是法师
              )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_124, new Modifier(-50));   //海盗藏品 Corsair Cache  ID：BT_124
            }


            //八爪鱼
            {
                int shoupaiCount = board.Hand.Count;
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_177, new Modifier(80 * (shoupaiCount - 5)));//八爪巨怪 Octosari  ID：ULD_177
            }

            //场上有八爪鱼，手牌小于等于3，优先把八爪鱼送了

            if (board.HasCardOnBoard(Card.Cards.ULD_177)//八爪巨怪 Octosari  ID：ULD_177
                && board.Hand.Count <= 3
                )
            {
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.ULD_177, new Modifier(-20));//八爪巨怪 Octosari  ID：ULD_177
            }
            //书虫
            if (board.Hand.Count <= 3
                && board.HasCardInHand(Card.Cards.SCH_142)//贪婪的书虫 Voracious Reader  ID：SCH_142
                 )
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(-40));//贪婪的书虫 Voracious Reader  ID：SCH_142
                }
                else
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(300));//贪婪的书虫 Voracious Reader  ID：SCH_142
                }
            //如果场上有鲨鳍后援 Sharkfin Fan ID：TRL_507 ，手上有空中炮艇 Skybarge  ID：DRG_023，手上有刀，提高空中炮艇优先级
          if (board.HasCardOnBoard(Card.Cards.TRL_507)
                && board.HasCardInHand(Card.Cards.DRG_023)
                && board.WeaponFriend != null
                 )
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(-999));
                }

            //空降歹徒
            if (board.Hand.Count >=5
                && board.Hand.Count ==1
                 )
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(-20));
                }



            //武器优先值
            p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-80));//海盗之锚 Ancharrr  ID：DRG_025
            p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DAL_063, new Modifier(-80));///圣剑扳手 Wrenchcalibur  ID：DAL_063
           
            
            //武器攻击保守性
            p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.OG_058, new Modifier(-50));//锈蚀铁钩 Rusty Hook  ID：OG_058
            p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-200));//海盗之锚 Ancharrr  ID：DRG_025
            p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.EX1_409t, new Modifier(-50));//重斧 Heavy Axe  ID：EX1_409t
            p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.DAL_063, new Modifier(-50));//Card.Cards.DAL_063,//圣剑扳手
            p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.DMF_521t, new Modifier(-50));//断颚之刃 Jawbreaker ID：DMF_521t 

            //法术
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_409, new Modifier(50));   //升级 Upgrade!  ID：EX1_409
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_124, new Modifier(-40));   //海盗藏品 Corsair Cache  ID：BT_124



          
            
         
            //随从
           // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.AT_075, new Modifier(150)); //战马训练师 Warhorse Trainer ID：AT_075
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_311, new Modifier(150)); //活化扫帚 Animated Broomstick  ID：SCH_311
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_521, new Modifier(-5)); //吞剑艺人 Sword Eater ID：DMF_521 
            // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_526, new Modifier(200)); //巴罗夫领主 Lord Barov  ID：SCH_526
            // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_240, new Modifier(-350));//救赎者洛萨克森 Lothraxion the Redeemed ID：DMF_240
            //不送的怪
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(300)); //修饰贪婪的书虫 Voracious Reader  ID：SCH_142
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(300)); //修饰空中炮艇 Skybarge  ID：DRG_023
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.GVG_075, new Modifier(300)); //修饰船载火炮
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.TRL_507, new Modifier(300)); //修饰鲨鳍后援 Sharkfin Fan ID：TRL_507 
           // p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.DMF_237, new Modifier(500)); //修饰狂欢报幕员 Carnival Barker ID：DMF_237，数值越高越保守，就是不会拿去交换随从

            //武器优先值
            // p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-80));//海盗之锚 Ancharrr  ID：DRG_025


            //武器攻击保守性
            //  p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.OG_058, new Modifier(-20));//锈蚀铁钩 Rusty Hook  ID：OG_058

            //法术
            // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_087, new Modifier(150));//力量祝福 Blessing of Might  ID：CS2_087
            // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_138, new Modifier(50));//威能祝福 Blessing of Authority  ID：SCH_138
            // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.UNG_961, new Modifier(40));//适者生存 Adaptation  ID：UNG_961
            // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_909, new Modifier(-40));//水晶学 Crystology  ID：BOT_909
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_244, new Modifier(100));//游园日 Day at the Faire ID：DMF_244
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DMF_244t, new Modifier(-20));//游园日 Day at the Faire ID：DMF_244t

			//场上有5个及以上的随从就不要用詹迪斯·巴罗夫 Jandice Barov ID：SCH_351
			if (board.MinionFriend.Count >= 5
			&& board.HasCardInHand(Card.Cards.SCH_351)
			){
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_351, new Modifier(600));//詹迪斯·巴罗夫 Jandice Barov ID：SCH_351
				Bot.Log("随从太多不用詹迪斯·巴罗夫");
			}


            //如果对面是奇数骑，优先解掉白银之手新兵、狂欢报幕员、夜色镇执法官
			if (board.EnemyAbility.Template.Id == Card.Cards.HERO_04bp2){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS2_101t, new Modifier(200));//白银之手新兵
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_237, new Modifier(210));//狂欢报幕员 Carnival Barker ID：DMF_237
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.OG_310, new Modifier(220));//夜色镇执法官 Steward of Darkshire ID：OG_310
			}
			
            if (board.Hand.Count <= 3
                && board.HasCardInHand(Card.Cards.SCH_142)//贪婪的书虫 Voracious Reader  ID：SCH_142
                 )
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(-40));//贪婪的书虫 Voracious Reader  ID：SCH_142
                }
                else
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(300));//贪婪的书虫 Voracious Reader  ID：SCH_142
                }

                //场上有书虫，提高手里硬币优先值
                if ( board.HasCardOnBoard(Card.Cards.SCH_142)//贪婪的书虫 Voracious Reader  ID：SCH_142
                && board.HasCardInHand(Card.Cards.GAME_005)
                 )
                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(-10));
                }  
               

                //奇数骑技能提高
               //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.HERO_04bp2, new Modifier(-20));

                           
			//提高战斗邪犬威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_351, new Modifier(200));
			//优先用任务
			if(board.HasCardInHand(Card.Cards.SW_028)
			&& board.HasCardInHand(Card.Cards.GAME_005)
			){
				p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SW_028, new Modifier(-9999));//开进码头 Raid the Docks ID：SW_028
				p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(200));   //硬币
				Bot.Log("不叫硬币直接交任务");
			}
			//随从数小于5直接拍船长
			if(board.MinionFriend.Count <= 5
			&& board.HasCardInHand(Card.Cards.SW_028t5)
			){
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SW_028t5, new Modifier(-9999));//船长洛卡拉 Cap'n Rokara ID：SW_028t5
				Bot.Log("随从数量少于5直接拍船长");
			}
		



			
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
