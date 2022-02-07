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
		    {Card.Cards.BT_100, 3},//毒蛇神殿传送门 Serpentshrine Portal  ID：BT_100
            {Card.Cards.EX1_238, 3},//闪电箭 Lightning Bolt  ID：EX1_238
            {Card.Cards.EX1_241, 5},//熔岩爆裂 Lava Burst  ID：EX1_241
            {Card.Cards.CS2_037, 1},//冰霜震击 Frost Shock  ID：CS2_037
            {Card.Cards.BT_495, 4},//刃缚精锐 Glaivebound Adept ID：BT_495
        };
		//version 1.2 (2021-09-17)


        //攻击模式



        public ProfileParameters GetParameters(Board board)
        {


			//用BaseProfile函数调用加载云端的Rush策略
            var p = new ProfileParameters(BaseProfile.Rush);
            p.DiscoverSimulationValueThresholdPercent = 10;
			
			//贫瘠之地默认优先级-BAR
			
			//贫瘠之地铁匠 Barrens Blacksmith ID：BAR_073
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_073, new Modifier(200));
			//十字路口哨所 Crossroads Watch Post ID：BAR_075
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_075, new Modifier(200));
			//布鲁坎 Bru'kan ID：BAR_048
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_048, new Modifier(200));
			//凯瑞尔·罗姆 Cariel Roame ID：BAR_902
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_902, new Modifier(200));
			//战地医师老兵 Veteran Warmedic ID：BAR_878
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_878, new Modifier(200));
			//腐烂的普雷莫尔 Plaguemaw the Rotting ID：BAR_540
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_540, new Modifier(200));
			//钢鬃卫兵 Razormane Battleguard ID：BAR_537
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_537, new Modifier(200));
			//勘探者车队 Prospector's Caravan ID：BAR_033
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_033, new Modifier(200));
			//科卡尔驯犬者 Kolkar Pack Runner ID：BAR_035
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_035, new Modifier(200));
			//士兵车队 Soldier's Caravan ID：BAR_871
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_871, new Modifier(200));
			//占卜者车队 Soothsayer's Caravan ID：BAR_312
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_312, new Modifier(200));
			//高效八爪机器人 Efficient Octo-bot ID：BAR_320
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_320, new Modifier(200));
			//鱼人宝宝车队 Tinyfin's Caravan ID：BAR_043
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_043, new Modifier(200));
			//火焰术士弗洛格尔 Firemancer Flurgl ID：BAR_860
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_860, new Modifier(200));
			//剂师车队 Apothecary's Caravan ID：BAR_912
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_912, new Modifier(200));			
			//甜水鱼人斥候 Lushwater Scout ID：BAR_063
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_063, new Modifier(200));	
			//前沿哨所 Far Watch Post ID：BAR_074
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_074, new Modifier(200));	
			//古夫·符文图腾 Guff Runetotem ID：BAR_720
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_720, new Modifier(200));	
			//塔维什·雷矛 Tavish Stormpike ID：BAR_038
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_038, new Modifier(200));	
			//奥术发光体 Arcane Luminary ID：BAR_545
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_545, new Modifier(200));	
			//霜舌半人马 Rimetongue ID：BAR_888 
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_888, new Modifier(200));	
			//原野联络人 Field Contact ID：BAR_317
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_317, new Modifier(200));	
			//莫尔杉哨所 Mor'shan Watch Post ID：BAR_076
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_076, new Modifier(200));	
			//十字路口大嘴巴 Crossroads Gossiper ID：BAR_890
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_890, new Modifier(200));	
			//贫瘠之地诱捕者 Barrens Trapper ID：BAR_082
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_082, new Modifier(200));
			
            //自定义命名
			p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(50));//幸运币 The Coin  ID：GAME_005
			int a = (board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - BoardHelper.GetEnemyHealthAndArmor(board);//敌我血量差
			
			

			//随从优先解
			
			//核心
			//提高雏龙（暂定）
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116t, new Modifier(200));
			//提高铸甲师 Armorsmith ID：CORE_EX1_402
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_402, new Modifier(200));
			//加基森拍卖师 Gadgetzan Auctioneer ID：CORE_EX1_095
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_095, new Modifier(200));
			
			//外域的灰烬-BT
			//灵魂之匣 Reliquary of Souls ID：BT_197
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_197, new Modifier(200));
			//星术师索兰莉安 Astromancer Solarian ID：BT_028
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_028, new Modifier(200));
			//莫戈尔·莫戈尔格 Murgur Murgurgle ID：BT_019
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_019, new Modifier(200));
			//坎雷萨德·埃伯洛克 Kanrethad Ebonlocke ID：BT_309
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_309, new Modifier(200));
			//孢子首领姆希菲 Archspore Msshi'fn ID：BT_136
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_136, new Modifier(200));
			//莫顶级捕食者兹克索尔 Zixor, Apex Predator ID：BT_210
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_210, new Modifier(200));
			//阿卡玛 Akama ID：BT_713
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_713, new Modifier(200));
			//瓦丝琪女士 Lady Vashj ID：BT_109
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_109, new Modifier(200));
			//卡加斯·刃拳 Kargath Bladefist ID：BT_123
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_123, new Modifier(200));
			//终极卡加斯 Kargath Prime ID：BT_123t
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_123t, new Modifier(200));
			//提高战斗邪犬威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_351, new Modifier(200));
			//提高流放者奥图里斯威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_937, new Modifier(200));
			//提高暗影珠宝师汉纳尔威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_188, new Modifier(200));
			//提高萨特监工威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_352, new Modifier(200));
			//提高怒鳞纳迦威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_355, new Modifier(200));
			//强能箭猪 Augmented Porcupine  ID：BT_201
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_201, new Modifier(200));
			//提高黑眼
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(200));
			//提高废料场巨像 Scrapyard Colossus  ID：BT_155
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_155, new Modifier(200));
			//改进型恐惧魔王 Enhanced Dreadlord  ID：BT_304
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_304, new Modifier(200));
			//塞泰克织巢者 Sethekk Veilweaver ID：BT_254
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_254, new Modifier(200));
			
			//通灵学院-SCH
			//决斗大师莫扎奇 Mozaki, Master Duelist ID：SCH_400
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_400, new Modifier(200));
			//狐人淬毒师 Vulpera Toxinblade  ID：SCH_519
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_519, new Modifier(200));
			//贪婪的书虫 Voracious Reader ID：SCH_142
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(200));
			
			//暗月马戏团-DMF
			//伊格诺斯 Il'gynoth ID：DMF_230
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_230, new Modifier(200));
			//纳兹曼尼织血者 Nazmani Bloodweaver ID：DMF_120
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_120, new Modifier(200));
			
			//暗月竞速赛-YOP
			//死亡守望者 Deathwarden ID：YOP_012
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_012, new Modifier(200));
			//铁锈特使拉斯维克斯 Envoy Rustwix ID：YOP_004
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_004, new Modifier(200));


			
			
			//贫瘠之地的锤炼-BAR
			//塔姆辛·罗姆 Tamsin Roame ID：BAR_918
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_918, new Modifier(200));
			//火焰术士弗洛格尔 Firemancer Flurgl ID：BAR_860
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_860, new Modifier(200));
			//古夫·符文图腾 Guff Runetotem ID：BAR_720
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_720, new Modifier(200));
			//塔维什·雷矛 Tavish Stormpike ID：BAR_038
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_038, new Modifier(200));
			//洛卡拉 Rokara ID：BAR_847
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_847, new Modifier(200));
			//腐烂的普雷莫尔 Plaguemaw the Rotting ID：BAR_540
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_540, new Modifier(200));
			//凯瑞尔·罗姆 Cariel Roame ID：BAR_902
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_902, new Modifier(200));
			
			//针对职业
			
			//如果对面是贼，需要提高魔杖窃贼的威胁值
			if (board.EnemyClass == Card.CClass.ROGUE
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SCH_350)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_350, new Modifier(199));//魔杖窃贼 Wand Thief ID：SCH_350
				//Bot.Log("对面是潜行者要优先解掉魔杖窃贼");
			}
			
			//如果对面是贼，需要提高狐人老千的威胁值
			if (board.EnemyClass == Card.CClass.ROGUE
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_511)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_511, new Modifier(199));//狐人老千 Foxy Fraud ID：DMF_511
				//Bot.Log("对面是潜行者要优先解掉狐人老千");
			}
			
			//如果对面是萨满，需要提高沙漠野兔的威胁值
			if (board.EnemyClass == Card.CClass.SHAMAN
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_703)
			|| board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_703t)
			|| board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_706t)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_703, new Modifier(200));//死斗场管理者 Pit Master ID：DMF_703
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_703t, new Modifier(200));//死斗场管理者（已腐蚀） Pit Master ID：DMF_703t
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_706t, new Modifier(200));//大帐决斗者 Pavilion Duelist ID：DMF_706t
				//Bot.Log("对面是萨满要优先解掉野兔、死斗场决斗者");
			}
			
			//如果对面是萨满，需要优先解掉大眼睛
			if (board.EnemyClass == Card.CClass.SHAMAN
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_709)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_709, new Modifier(200));//巨型图腾埃索尔 Grand Totem Eys'or ID：DMF_709
				//Bot.Log("对面是萨满优先解掉大眼睛");
			}
			
			//如果对面是瞎子提高血法师威胁值
			if (board.EnemyClass == Card.CClass.DEMONHUNTER
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CORE_EX1_012)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_012, new Modifier(199));//血法师萨尔诺斯 Bloodmage Thalnos ID：CORE_EX1_012
				//Bot.Log("对面是瞎子提高血法师威胁值");
			}
			
			//如果对面是法师提高法强随从威胁值
			if (board.EnemyClass == Card.CClass.MAGE){
				//p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_245, new Modifier(200));//卷轴管理者 Steward of Scrolls ID：SCH_245
				//p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_028, new Modifier(200));//星术师索兰莉安 Astromancer Solarian ID：BT_028
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_273, new Modifier(200));//莱斯·霜语 Ras Frostwhisper ID：SCH_273
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_021, new Modifier(200));//被禁锢的凤凰 Imprisoned Phoenix ID：YOP_021
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_310, new Modifier(200));//研究伙伴 Lab Partner ID：SCH_310
				//Bot.Log("对面是法师提高法强随从威胁值");
			}
			
			
			
			//降低攻击优先值
			//降低龙裔小鬼威胁值
			//p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_238t12t2, new Modifier(60));
			
			//手工策略
			//牌库数量大于10张的时候，提高过牌的优先级，降低333优先级
			Bot.Log("我方牌库剩余牌数" + board.FriendDeckCount);
			if(board.FriendDeckCount > 10){
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_916, new Modifier(150));//血岩碎片刺背野猪人 Blood Shard Bristleback ID：BAR_916
				p.CastSpellsModifiers.AddOrUpdate(Card.Cards.YOP_033, new Modifier(-200));//赛车回火 Backfire ID：YOP_033
				p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_056_H1, new Modifier(-100));//生命分流 Life Tap ID：HERO_07bp
			}
			//对方没有随从降低333优先级
			if (board.MinionEnemy.Count == 0){
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_916, new Modifier(150));//血岩碎片刺背野猪人 Blood Shard Bristleback ID：BAR_916
			}
			//提高闪金镇狼人的优先级
			p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SW_062, new Modifier(-200));//闪金镇豺狼人 Goldshire Gnoll ID：SW_062
			//提高术士技能优先级
			
			//提高力量图腾、团队领袖的威胁值
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS2_058, new Modifier(200));//力量图腾 Strength Totem ID：CS2_058
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_CS2_122, new Modifier(200));//团队领袖 Raid Leader ID：CORE_CS2_122

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
			
			//得到友方的血量和护甲之和
			public static int GetFriendHealthAndArmor(Board board)
			{
				return board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor;
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
