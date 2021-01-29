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
 * Copyleft 2016 - 2021 SunGuanqi. All Rights Reserved
 * Attribution 4.0 International (Attribution 4.0 International (CC-BY-SA 4.0)
 * 使用时请遵守知识共享署名 4.0 国际许可协议，且不可删除本版权信息
 * 
 */

namespace SmartBotProfiles
{
	[Serializable]
	public class StandRush : Profile
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
			{DaggerMastery,6},//匕首精通 Dagger Mastery
			{Reinforce, 2},//援军 Reinforce
			{Shapeshift, 6},//变形 Shapeshift
			{DemonsBite, 4},
			{Fireblast, 8},//火焰冲击 Fireblast
			{ArmorUp, 3},//全副武装” "Armor Up"
			{LesserHeal, 1},//次级治疗术 Lesser Heal
		};

		//直伤卡牌
		private static readonly Dictionary<Card.Cards, int> _spellDamagesTable = new Dictionary<Card.Cards, int>
		{
			{Card.Cards.CS2_093, 2},//奉献 Consecration  ID：CS2_093
			{Card.Cards.SCH_248, 1},//甩笔侏儒 Pen Flinger ID：SCH_248
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
            {Card.Cards.BT_495, 4},//刃缚精锐
			//sm血色 偷你妈的头 by 77
		};
		
		//Rush-Pro for 19.4HS in Wild


		//攻击模式



		public ProfileParameters GetParameters(Board board)
		{



			var p = new ProfileParameters(BaseProfile.Rush);
			p.DiscoverSimulationValueThresholdPercent = 10;
			//自定义命名
			p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(50));//幸运币 The Coin  ID：GAME_005
			int a = (board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - BoardHelper.GetEnemyHealthAndArmor(board);//敌我血量差
			
			//判定是否过载
			bool guozai;
			if (board.LockedMana == 0
				&& board.OverloadedMana == 0){
				guozai = false;
			}else{
				guozai = true;
			}
			
			if (!board.MinionEnemy.Any(x => x.IsTaunt) &&
				   (BoardHelper.GetEnemyHealthAndArmor(board) -
				  BoardHelper.GetPotentialMinionDamages(board) -
				BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <=
				BoardHelper.GetTotalBlastDamagesInHand(board)))
			{
				p.GlobalAggroModifier = 450;
			}//如果下一轮可以斩杀对面，攻击性提高
			
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
			//Bot.Log("我方本回合可用血色马的数量为:" + board.ManaAvailable);
			//我方最大法力水晶
			//Bot.Log("我方最大法力水晶:" + board.MaxMana);
			//法术提供的可用总水晶上限
			//Bot.Log("血色的马上限是:" + manaAvailable);
			
			//具体策略

			//武器优先值
			p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-80));//海盗之锚 Ancharrr  ID：DRG_025
			p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DAL_063, new Modifier(-80));//圣剑扳手 Wrenchcalibur  ID：DAL_063
			
			//优先沉默对象
			
			//铁喙猫头鹰 Ironbeak Owl ID：CS2_203
			//智慧圣契 Libram of Wisdom ID：BT_025
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-80, Card.Cards.BT_025));//优先沉默圣骑士的圣契
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.SCH_621));//优先沉默血骨傀儡 Rattlegore ID：SCH_621
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_044));//任务达人 Questing Adventurer ID：EX1_044
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_613));//艾德温·范克里夫 Edwin VanCleef ID：EX1_613
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_613));//萨赫特的傲狮 Salhet's Pride ID：ULD_438
			
			//如果对面是圣骑士还要沉默亡语过牌
			if (board.EnemyClass == Card.CClass.PALADIN){
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_096));//战利品贮藏者 Loot Hoarder ID：EX1_096
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_012));//血法师萨尔诺斯 Bloodmage Thalnos ID：EX1_012
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.DMF_194));//赤鳞驯龙者 Redscale Dragontamer ID：DMF_194
				//Bot.Log("对面是圣骑士，优先沉默亡语过牌");
			}


			//武器攻击保守性
			p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.CS2_091, new Modifier(0));//圣光的正义 Light's Justice  ID：CS2_091
			p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.BT_011t, new Modifier(0));//迟到的正义 Overdue Justice  ID：BT_011t
			p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.OG_058, new Modifier(-50));//锈蚀铁钩 Rusty Hook  ID：OG_058
			p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-200));//海盗之锚 Ancharrr  ID：DRG_025
			p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.EX1_409t, new Modifier(-50));//重斧 Heavy Axe  ID：EX1_409t
			p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.DAL_063, new Modifier(-50));//Card.Cards.DAL_063,//圣剑扳手

			//法术

			p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(50));//幸运币 The Coin  ID：GAME_005
			p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_409, new Modifier(50));   //升级 Upgrade!  ID：EX1_409
			p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_124, new Modifier(-40));   //海盗藏品 Corsair Cache  ID：BT_124
			p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_541t, new Modifier(-200));//国王的赎金 King's Ransom ID：LOOT_541t
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.AT_053, new Modifier(-5));//先祖知识 Ancestral Knowledge  ID：AT_053
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_427, new Modifier(20));//雷霆绽放 Lightning Bloom ID：SCH_427 


			//随从优先值
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_020, new Modifier(-200));//奥尔多侍从 Aldor Attendant  ID：BT_020
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_026, new Modifier(-200));//奥尔多真理追寻者 Aldor Truthseeker  ID：BT_026
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_232, new Modifier(5));//光铸狂热者 Lightforged Zealot  ID：DRG_232
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019t, new Modifier(15));//终极莫戈尔格 Murgurgle Prime ID：BT_019t
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ICC_701, new Modifier(-200)); //游荡恶鬼 Skulking Geist  ID：ICC_701
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_537, new Modifier(-80)); //戏法图腾 Trick Totem ID：SCH_537
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_230, new Modifier(100));//鱼斯拉 The Lurker Below ID：BT_230
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.AT_052, new Modifier(-20));//图腾魔像 Totem Golem  ID：AT_052
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_071, new Modifier(-15));//厄运信天翁 Bad Luck Albatross  ID：DRG_071
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_807, new Modifier(45));//葛拉卡爬行蟹 Golakka Crawler ID：UNG_807 
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_158, new Modifier(250));//沙暴元素 Sandstorm Elemental ID：ULD_158 
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_428, new Modifier(-40));//博学者普克尔特 Lorekeeper Polkelt ID：SCH_428 
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_173, new Modifier(250));//维西纳 Vessina ID：ULD_173
			
			 //如果下一轮可以斩杀对面提高洛欧塞布 Loatheb  ID：FP1_030
			if (board.HasCardInHand(Card.Cards.FP1_030)//洛欧塞布 Loatheb  ID：FP1_030
				&& !board.MinionEnemy.Any(x => x.IsTaunt)
				&& (BoardHelper.GetEnemyHealthAndArmor(board) - BoardHelper.GetPotentialMinionDamages(board) - BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <= BoardHelper.GetTotalBlastDamagesInHand(board))
				|| myAttack >= (board.HeroEnemy.CurrentHealth) - 5
				){
					p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030, new Modifier(-500));//洛欧塞布 Loatheb  ID：FP1_030
			}
				
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
			
			//对面没随从,降低扫帚和巴罗夫优先级
			if (board.MinionEnemy.Count <=1
			&& board.HasCardInHand(Card.Cards.SCH_311)//活化扫帚 Animated Broomstick  ID：SCH_311
			|| board.HasCardInHand(Card.Cards.SCH_526)//巴罗夫领主 Lord Barov  ID：SCH_526
			)
			{
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_311, new Modifier(150)); //活化扫帚 Animated Broomstick  ID：SCH_311
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_526, new Modifier(200)); //巴罗夫领主 Lord Barov  ID：SCH_526
			}
			
			//随从交换
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.ULD_276, new Modifier(150)); //修饰怪盗图腾，数值越高越保守，就是不会拿去交换随从
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_537, new Modifier(150)); //修饰戏法图腾 Trick Totem ID：SCH_537 ，数值越高越保守，就是不会拿去交换随从
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(250)); //修饰书虫，数值越高越保守，就是不会拿去交换随从 贪婪的书虫 Voracious Reader  ID：SCH_142
			p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.DMF_237, new Modifier(500)); //修饰狂欢报幕员 Carnival Barker ID：DMF_237，数值越高越保守，就是不会拿去交换随从
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_538, new Modifier(500)); //修饰金牌猎手克里 Ace Hunter Kreen ID：SCH_538

            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_737, new Modifier(130));//玛维·影歌 Maiev Shadowsong  ID：BT_737
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_173, new Modifier(130));//维西纳 Vessina  ID：ULD_173
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_563, new Modifier(20));//玛里苟斯 Malygos  ID：EX1_563
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.AT_054, new Modifier(-200));//唤雾者伊戈瓦尔 The Mistcaller  ID：AT_054

			//如果对方费用大于5，而且对面是牧师，提高洛欧塞布和古神的优先级
			if ((board.EnemyMaxMana >= 5)
			   && (board.EnemyClass == Card.CClass.PRIEST)
			   )
			{
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030 , new Modifier(-50));
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_236 , new Modifier(-50));
			}
			
			//一费对面没怪 不下甩笔侏儒
            if (board.HasCardInHand(Card.Cards.SCH_248)
            && board.ManaAvailable == 1
            && board.MinionEnemy.Count == 0 
            )//甩笔侏儒 Pen Flinger ID：SCH_248
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(350));//甩笔侏儒 Pen Flinger ID：SCH_248
            }

			// 自己场上没随从不下金牌猎手
			if (board.HasCardInHand(Card.Cards.SCH_538)//金牌猎手克里 Ace Hunter Kreen ID：SCH_538
            && board.MinionFriend.Count ==0
            )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_538, new Modifier(350));//金牌猎手克里 Ace Hunter Kreen ID：SCH_538
            }
			
			//对面是牧师，火车王随便下
			if (board.EnemyClass == Card.CClass.PRIEST)
			{
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116, new Modifier(-50));//修改火车王的优先级
				
				//提高雏龙威胁值雏龙 Whelp ID：EX1_116t 
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116t, new Modifier(200));
			}
			
			//沉默相关
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
			
			//优先沉默对象
			
			//铁喙猫头鹰 Ironbeak Owl ID：CS2_203
			//智慧圣契 Libram of Wisdom ID：BT_025
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-80, Card.Cards.BT_025));//优先沉默圣骑士的圣契
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.SCH_621));//优先沉默血骨傀儡 Rattlegore ID：SCH_621
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_044));//任务达人 Questing Adventurer ID：EX1_044
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_613));//艾德温·范克里夫 Edwin VanCleef ID：EX1_613
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_613));//萨赫特的傲狮 Salhet's Pride ID：ULD_438
			
			//如果对面是圣骑士还要沉默亡语过牌
			if (board.EnemyClass == Card.CClass.PALADIN){
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_096));//战利品贮藏者 Loot Hoarder ID：EX1_096
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.EX1_012));//血法师萨尔诺斯 Bloodmage Thalnos ID：EX1_012
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CS2_203, new Modifier(-40, Card.Cards.DMF_194));//赤鳞驯龙者 Redscale Dragontamer ID：DMF_194
				//Bot.Log("对面是圣骑士，优先沉默亡语过牌");
			}
			
			//随从优先解
			//提高战斗邪犬威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_351, new Modifier(200));
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
			//提高鲨鳍后援威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.TRL_507, new Modifier(200));
			//提高小鬼骑士 Tiny Knight of Evil  ID：AT_021威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.AT_021, new Modifier(200));
			//提高对空奥术法师 Arcane Flakmage  ID：ULD_240威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_240, new Modifier(200));
			//提高巫师学徒 Sorcerer's Apprentice  ID：EX1_608威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_608, new Modifier(200));
			//提高银色神官帕尔崔丝 Confessor Paletress ID：AT_018威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.AT_018, new Modifier(200));
			//提高连环腿大师 Whirlkick Master ID：ULD_231威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_231, new Modifier(200));
			//提高了不起的杰弗里斯 Zephrys the Great ID：ULD_003威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_003, new Modifier(200));
			//塞泰克织巢者 Sethekk Veilweaver ID：BT_254
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_254, new Modifier(199));
			//艾德温·范克里夫 Edwin VanCleef ID：EX1_613
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_613, new Modifier(200));
			//伊格诺斯 Il'gynoth ID：DMF_230
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_230, new Modifier(200));
			//纳兹曼尼织血者 Nazmani Bloodweaver ID：DMF_120
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_120, new Modifier(200));
			
			//如果对面是猎人，需要提高矮人神射手的威胁值
			if (board.EnemyClass == Card.CClass.HUNTER
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DRG_253)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_253, new Modifier(200));//矮人神射手 Dwarven Sharpshooter ID：DRG_253
				//Bot.Log("对面是猎人要优先解掉神射手");
			}
			
			//如果对面是贼，需要提高魔杖窃贼的威胁值
			if (board.EnemyClass == Card.CClass.ROGUE
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SCH_350)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_350, new Modifier(199));//魔杖窃贼 Wand Thief ID：SCH_350
				//Bot.Log("对面是潜行者要优先解掉魔杖窃贼");
			}
			
			//场上有5个及以上的随从就不要用詹迪斯·巴罗夫 Jandice Barov ID：SCH_351
			if (board.MinionFriend.Count >= 5
			&& board.HasCardInHand(Card.Cards.SCH_351)
			){
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_351, new Modifier(600));//詹迪斯·巴罗夫 Jandice Barov ID：SCH_351
				Bot.Log("随从太多不用詹迪斯·巴罗夫");
			}
			
			//场上有鸽子，优先把鸽子送了

            if (board.HasCardOnBoard(Card.Cards.DRG_071)//厄运信天翁 Bad Luck Albatross  ID：DRG_071
                  )
            {
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.DRG_071, new Modifier(-50));//厄运信天翁 Bad Luck Albatross  ID：DRG_071
            }
            //场上有八爪鱼，手牌小于等于3，优先把八爪鱼送了

            if (board.HasCardOnBoard(Card.Cards.ULD_177)//八爪巨怪 Octosari  ID：ULD_177
                && board.Hand.Count <=3
                )
            {
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.ULD_177, new Modifier(-20));//八爪巨怪 Octosari  ID：ULD_177
            }


			//如果对面是奇数骑，优先解掉白银之手新兵、狂欢报幕员、夜色镇执法官
			if (board.EnemyAbility.Template.Id == Card.Cards.HERO_04bp2){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS2_101t, new Modifier(200));//白银之手新兵
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_237, new Modifier(210));//狂欢报幕员 Carnival Barker ID：DMF_237
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.OG_310, new Modifier(220));//夜色镇执法官 Steward of Darkshire ID：OG_310
			}
			
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
				
				//奥秘体系
                ////对面没有奥秘，不下张杰督察
                if (!board.SecretEnemy
                && board.HasCardInHand(Card.Cards.GVG_038)//连环爆裂 Crackle  ID：GVG_038
                    || board.EnemyClass == Card.CClass.PALADIN//对方是骑士
                     || board.EnemyClass == Card.CClass.MAGE //对方是法师
                      || board.EnemyClass == Card.CClass.ROGUE//对方是贼
                       || board.EnemyClass == Card.CClass.HUNTER//对方是猎人
                    )
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_254, new Modifier(200));//奥秘吞噬者 Eater of Secrets  ID：OG_254
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GIL_648, new Modifier(200));//总督察 Chief Inspector ID：GIL_648
                }
                else
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.OG_254, new Modifier(-999));//奥秘吞噬者 Eater of Secrets  ID：OG_254
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GIL_648, new Modifier(-999));//总督察 Chief Inspector ID：GIL_648
                }
				
			//只有手牌少于3才拍书虫
			if (board.Hand.Count <= 3
				&& board.HasCardInHand(Card.Cards.SCH_142)//贪婪的书虫 Voracious Reader  ID：SCH_142
				){
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
			
			//如果对面是萨满，需要提高沙漠野兔的威胁值
			if (board.EnemyClass == Card.CClass.SHAMAN
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ULD_719)
			|| board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_703)
			|| board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_703t)
			|| board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_706t)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_719, new Modifier(200));//沙漠野兔 Desert Hare ID：ULD_719
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_703, new Modifier(200));//死斗场管理者 Pit Master ID：DMF_703
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_703t, new Modifier(200));//死斗场管理者（已腐蚀） Pit Master ID：DMF_703t
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_706t, new Modifier(200));//大帐决斗者 Pavilion Duelist ID：DMF_706t
				//Bot.Log("对面是萨满要优先解掉野兔、死斗场决斗者");
			}
			
			//如果对面是瞎子提高血法师威胁值
			if (board.EnemyClass == Card.CClass.DEMONHUNTER
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.EX1_012)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_012, new Modifier(199));//血法师萨尔诺斯 Bloodmage Thalnos ID：EX1_012
				Bot.Log("对面是瞎子提高血法师威胁值");
			}
			
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
			//提高要塞指挥官 Garrison Commander  ID：AT_080威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.AT_080, new Modifier(200));
			//贪婪的书虫 Voracious Reader ID：SCH_142
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(200));
			//塞泰克织巢者 Sethekk Veilweaver ID：BT_254
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_254, new Modifier(200));
			//提高了不起的杰弗里斯 Zephrys the Great ID：ULD_003威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_003, new Modifier(200));
			//塞泰克织巢者 Sethekk Veilweaver ID：BT_254
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_254, new Modifier(200));
			//艾德温·范克里夫 Edwin VanCleef ID：EX1_613
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_613, new Modifier(200));
            //提高女猎手 Huntress ID：BT_159t威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_159t, new Modifier(200));
            //提高尼鲁巴蛛网领主 Nerub'ar Weblord ID：FP1_017 威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_017, new Modifier(200));
			
			//提高空中炮艇 Skybarge ID：DRG_023威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(200));
			//提高邪能领主贝图格 Fel Lord Betrug  ID：DAL_607威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_607, new Modifier(200));
			//提高强能箭猪 Augmented Porcupine  ID：BT_201威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_201, new Modifier(200));
			//提高黑眼
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(200));
			//提高废料场巨像 Scrapyard Colossus  ID：BT_155
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_155, new Modifier(200));
			//改进型恐惧魔王 Enhanced Dreadlord  ID：BT_304
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_304, new Modifier(200));
			
			//狐人淬毒师 Vulpera Toxinblade  ID：SCH_519
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_519, new Modifier(200));
			//贪婪的书虫 Voracious Reader ID：SCH_142
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(200));
			//任务达人 Questing Adventurer ID：EX1_044
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_044, new Modifier(200));
		   
			//提高空中炮艇 Skybarge ID：DRG_023威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(200));
			//提高邪能领主贝图格 Fel Lord Betrug  ID：DAL_607威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_607, new Modifier(200));
			//提高鲨鳍后援威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.TRL_507, new Modifier(200));
			//提高小鬼骑士 Tiny Knight of Evil  ID：AT_021威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.AT_021, new Modifier(200));
			
			//提高对空奥术法师 Arcane Flakmage  ID：ULD_240威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_240, new Modifier(200));
			//提高火妖 Flamewaker  ID：BRM_002威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BRM_002, new Modifier(200));
			//提高巫师学徒 Sorcerer's Apprentice  ID：EX1_608威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_608, new Modifier(200));
			//提高银色神官帕尔崔丝 Confessor Paletress ID：AT_018威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.AT_018, new Modifier(200));
		   
			//提高索瑞森大帝 Emperor Thaurissan  ID：WE1_039威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.WE1_039, new Modifier(200));
			//提高观星者露娜 Stargazer Luna ID：BOT_103威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BOT_103, new Modifier(200));
			//提高资深档案管理员 Grand Archivist ID：LOOT_414威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_414, new Modifier(200));
			//狐人淬毒师 Vulpera Toxinblade  ID：SCH_519
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_519, new Modifier(200));
			//贪婪的书虫 Voracious Reader ID：SCH_142
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(200));
			//提高空中炮艇 Skybarge ID：DRG_023威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(200));
			//提高邪能领主贝图格 Fel Lord Betrug  ID：DAL_607威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DAL_607, new Modifier(200));
			//提高强能箭猪 Augmented Porcupine  ID：BT_201威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_201, new Modifier(200));
			//提高黑眼
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(200));
			//提高废料场巨像 Scrapyard Colossus  ID：BT_155
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_155, new Modifier(200));
			//改进型恐惧魔王 Enhanced Dreadlord  ID：BT_304
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_304, new Modifier(200));

			//降低攻击优先值
			//降低资深探险者 Licensed Adventurer ID：YOD_030威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_030, new Modifier(70));
			//降低龙裔小鬼威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_238t12t2, new Modifier(60));
			//降低麦迪文的男仆 Medivh's Valet  ID：KAR_092威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.KAR_092, new Modifier(50));
			//降低资深探险者 Licensed Adventurer ID：YOD_030威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GVG_051, new Modifier(50));
			//降低恩佐斯的副官 威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.OG_312, new Modifier(50));
			//降低疯狂的科学家 Mad Scientist ID：FP1_004 威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(50));
			//降低资深探险者 Licensed Adventurer ID：YOD_030威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_030, new Modifier(50));
			//降低空灵召唤者威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_022, new Modifier(50));
			//降低龙裔小鬼威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_238t12t2, new Modifier(50));
			//降低卡扎库斯 Kazakus  ID：CFM_621威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CFM_621, new Modifier(50));
			
			//如果对面是进化萨，要考虑希拉斯转过去后要卖掉它
			if (board.EnemyClass == Card.CClass.SHAMAN){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_074, new Modifier(200));
			}
			
			//葛拉卡爬行者遇到 贼 德 战降低优先级场上没有海盗
            if ( board.HasCardInHand(Card.Cards.UNG_807) //葛拉卡爬行蟹 Golakka Crawler ID：UNG_807 
               && board.EnemyClass == Card.CClass.ROGUE
               || board.EnemyClass == Card.CClass.DRUID
               || board.EnemyClass == Card.CClass.WARRIOR
               && board.MinionFriend.Count(card => card.Race == Card.CRace.PIRATE) ==0
                  )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_807, new Modifier(300));//葛拉卡爬行蟹 Golakka Crawler ID：UNG_807 
            }
            else{
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_807, new Modifier(40));//葛拉卡爬行蟹 Golakka Crawler ID：UNG_807 
            }
			
			//如果过载，随从大于3，提高维西纳优先级
            if ( guozai
               && board.HasCardInHand(Card.Cards.ULD_173) //维西纳 Vessina  ID：ULD_173
               && board.MinionFriend.Count >= 3
                )
            {
               p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_173, new Modifier(-60));//维西纳 Vessina  ID：ULD_173
            }    
            else
              {
               p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_173, new Modifier(300));//维西纳 Vessina  ID：ULD_173
            }
			
			//低温静滞优先贴图腾
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_056, new Modifier(-15, Card.Cards.NEW1_009));//低温静滞 Cryostasis  ID：ICC_056，治疗图腾 Healing Totem  ID：NEW1_009
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_056, new Modifier(0, Card.Cards.CS2_050));//低温静滞 Cryostasis  ID：ICC_056，灼热图腾 Searing Totem  ID：CS2_050
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_056, new Modifier(-5, Card.Cards.CS2_051));//低温静滞 Cryostasis  ID：ICC_056，石爪图腾 Stoneclaw Totem  ID：CS2_051
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_056, new Modifier(-10, Card.Cards.CS2_052));//低温静滞 Cryostasis  ID：ICC_056，空气之怒图腾 Wrath of Air Totem  ID：CS2_052
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_056, new Modifier(5, Card.Cards.AT_052));//低温静滞 Cryostasis  ID：ICC_056，图腾魔像 Totem Golem  ID：AT_052
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_056, new Modifier(-20, Card.Cards.ULD_276));//低温静滞 Cryostasis  ID：ICC_056，怪盗图腾 EVIL Totem  ID：ULD_276
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_056, new Modifier(-20, Card.Cards.SCH_537));//低温静滞 Cryostasis  ID：ICC_056，戏法图腾 Trick Totem  ID：SCH_537

            //大地之力优先贴图腾
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_586, new Modifier(-15, Card.Cards.NEW1_009));//大地之力 Earthen Might  ID：GIL_586，治疗图腾 Healing Totem  ID：NEW1_009
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_586, new Modifier(0, Card.Cards.CS2_050));//大地之力 Earthen Might  ID：GIL_586，灼热图腾 Searing Totem  ID：CS2_050
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_586, new Modifier(-5, Card.Cards.CS2_051));//大地之力 Earthen Might  ID：GIL_586，石爪图腾 Stoneclaw Totem  ID：CS2_051
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_586, new Modifier(-10, Card.Cards.CS2_052));//大地之力 Earthen Might  ID：GIL_586，空气之怒图腾 Wrath of Air Totem  ID：CS2_052
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_586, new Modifier(5, Card.Cards.AT_052));//大地之力 Earthen Might  ID：GIL_586，图腾魔像 Totem Golem  ID：AT_052
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_586, new Modifier(-20, Card.Cards.SCH_537));//低温静滞 Cryostasis  ID：ICC_056，戏法图腾 Trick Totem  ID：SCH_537
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GIL_586, new Modifier(-20, Card.Cards.ULD_276));//低温静滞 Cryostasis  ID：ICC_056，怪盗图腾 EVIL Totem  ID：ULD_276

			//泰坦跟班相关
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_616, new Modifier(200, Card.Cards.ULD_276));//泰坦造物跟班 Titanic Lackey ID：ULD_616 ，怪盗图腾 EVIL Totem  ID：ULD_276
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_616, new Modifier(200, Card.Cards.NEW1_009));//泰坦造物跟班 Titanic Lackey ID：ULD_616 ，治疗图腾 Healing Totem  ID：NEW1_009
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_616, new Modifier(200, Card.Cards.CS2_050));//泰坦造物跟班 Titanic Lackey ID：ULD_616 ，灼热图腾 Searing Totem  ID：CS2_050
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_616, new Modifier(200, Card.Cards.CS2_051));//泰坦造物跟班 Titanic Lackey ID：ULD_616 ，石爪图腾 Stoneclaw Totem  ID：CS2_051
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_616, new Modifier(200, Card.Cards.CS2_052));//泰坦造物跟班 Titanic Lackey ID：ULD_616 ，空气之怒图腾 Wrath of Air Totem  ID：CS2_052
			
			

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
