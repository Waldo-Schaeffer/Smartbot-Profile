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
    public class StandPurePaladin : Profile
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
			//血色nmsl BY 77
        };
		//Profile Version 1.15


        //攻击模式



        public ProfileParameters GetParameters(Board board)
        {



            var p = new ProfileParameters(BaseProfile.Rush);
            p.DiscoverSimulationValueThresholdPercent = 10;
            //自定义命名
            int LibraryResidueSecret = 2 - (board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).IsSecret) + board.Hand.Count(card => card.Template.IsSecret) + board.Secret.Count);//牌库剩余奥秘

            //针对一些随从优先解掉

            //攻击模式切换

            //具体策略

            //武器优先值
            // p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-80));//海盗之锚 Ancharrr  ID：DRG_025


            //武器攻击保守性
            p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.CS2_091, new Modifier(0));//圣光的正义 Light's Justice  ID：CS2_091
            p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.BT_011t, new Modifier(0));//迟到的正义 Overdue Justice  ID：BT_011t

            //法术

            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(50));//幸运币 The Coin  ID：GAME_005
            //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_011, new Modifier(140));//正义圣契 Libram of Justice  ID：BT_011


            //随从优先值
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_020, new Modifier(-200));//奥尔多侍从 Aldor Attendant  ID：BT_020
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_026, new Modifier(-200));//奥尔多真理追寻者 Aldor Truthseeker  ID：BT_026
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_232, new Modifier(5));//光铸狂热者 Lightforged Zealot  ID：DRG_232
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_019t, new Modifier(15));//终极莫戈尔格 Murgurgle Prime ID：BT_019t
            //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_334, new Modifier(20));//女伯爵莉亚德琳 Lady Liadrin  ID：BT_334
			
			//如果牌库里有炸弹或者鸽子不下光铸卡(实测无效，不影响)
			if(board.Deck.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.BOT_511t) >= 1//炸弹 Bomb ID：BOT_511t
			|| board.Deck.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.DRG_071t) >= 1//信天翁 Albatross ID：DRG_071t
            ){
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_231, new Modifier(150));//光铸远征军 Lightforged Crusader ID：DRG_231
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_232, new Modifier(150));//光铸狂热者 Lightforged Zealot ID：DRG_232
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_241, new Modifier(150));//大主教伊瑞尔 High Exarch Yrel ID：DMF_241
            }
			
			//刀耐久为1，手上没武器，手牌太少本回合英雄不攻击
            if (board.Hand.Count <= 3
            && (board.WeaponFriend != null)
            && !(board.HasCardInHand(Card.Cards.DMF_238))
            && !(board.HasCardInHand(Card.Cards.CS2_097))
            ){
                //对面是战士有可能会带黏指狗头人
				if (board.WeaponFriend.CurrentDurability == 1
				&& !(board.EnemyClass == Card.CClass.WARRIOR)){
                    p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.DMF_238, new Modifier(6000));//纳鲁之锤 Hammer of the Naaru ID：DMF_238
                    p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.CS2_097, new Modifier(6000));//真银圣剑 Truesilver Champion ID：CS2_097
                    Bot.Log("刀耐久为1，手上没武器，手牌太少本回合英雄不攻击");
                }
            }
			
			//手上有刀，手上有纳鲁之锤，先攻击后使用纳鲁之锤
			if((board.WeaponFriend != null)
			&& (board.HasCardInHand(Card.Cards.DMF_238))
			){
				p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.DMF_238, new Modifier(-600));//纳鲁之锤 Hammer of the Naaru ID：DMF_238
                p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.CS2_097, new Modifier(-600));//真银圣剑 Truesilver Champion ID：CS2_097
			}
			
			//如果新生入学给了安全检察员，给了中立卡，不下
			//漏掉了贼猫会给复生卡
			//如果手上有 安全检查员 Safety Inspector ID：DMF_125
			if(board.HasCardInHand(Card.Cards.DMF_125)){
				//判定手上有无其他中立卡
				if(
					//通灵学院
					board.HasCardInHand(Card.Cards.SCH_231)//新生刺头 Intrepid Initiate ID：SCH_231
					|| board.HasCardInHand(Card.Cards.SCH_311)//活化扫帚 Animated Broomstick ID：SCH_311
					|| board.HasCardInHand(Card.Cards.SCH_312)//巡游向导 Tour Guide ID：SCH_312
					|| board.HasCardInHand(Card.Cards.SCH_248)//甩笔侏儒 Pen Flinger ID：SCH_248
					//外域的灰烬
					|| board.HasCardInHand(Card.Cards.BT_722)//防护改装师 Guardian Augmerchant ID：BT_722
					|| board.HasCardInHand(Card.Cards.BT_723)//火箭改装师 Rocket Augmerchant ID：BT_723
					|| board.HasCardInHand(Card.Cards.BT_724)//虚灵改装师 Ethereal Augmerchant ID：BT_724
					|| board.HasCardInHand(Card.Cards.BT_727)//魂缚灰舌 Soulbound Ashtongue ID：BT_727
					|| board.HasCardInHand(Card.Cards.BT_731)//传染孢子 Infectious Sporeling ID：BT_731
					//巨龙降临
					|| board.HasCardInHand(Card.Cards.DRG_239)//灼光战斗法师 Blazing Battlemage ID：DRG_239
					|| board.HasCardInHand(Card.Cards.DRG_057)//热气球 Hot Air Balloon ID：DRG_057
					|| board.HasCardInHand(Card.Cards.DRG_078)//深潜炸弹 Depth Charge ID：DRG_078
					//奥丹姆奇兵
					|| board.HasCardInHand(Card.Cards.ULD_282)//陶罐商人 Jar Dealer ID：ULD_282
					|| board.HasCardInHand(Card.Cards.ULD_191)//欢快的同伴 Beaming Sidekick ID：ULD_191
					|| board.HasCardInHand(Card.Cards.ULD_705)//魔古信徒 Mogu Cultist ID：ULD_705
					|| board.HasCardInHand(Card.Cards.ULD_723)//鱼人木乃伊 Murmy ID：ULD_723
					//暗影崛起
					|| board.HasCardInHand(Card.Cards.DAL_544)//药水商人 Potion Vendor ID：DAL_544
					|| board.HasCardInHand(Card.Cards.DAL_077)//毒鳍鱼人 Toxfin ID：DAL_077
					//经典
					|| board.HasCardInHand(Card.Cards.EX1_080)//奥秘守护者 Secretkeeper ID：EX1_080
					|| board.HasCardInHand(Card.Cards.CS2_188)//叫嚣的中士 Abusive Sergeant ID：CS2_188
					|| board.HasCardInHand(Card.Cards.NEW1_017)//鱼人杀手蟹 Hungry Crab ID：NEW1_017
					|| board.HasCardInHand(Card.Cards.EX1_509)//鱼人招潮者 Murloc Tidecaller ID：EX1_509
					|| board.HasCardInHand(Card.Cards.CS2_169)//幼龙鹰 Young Dragonhawk ID：CS2_169
					|| board.HasCardInHand(Card.Cards.EX1_029)//麻风侏儒 Leper Gnome ID：EX1_029
					|| board.HasCardInHand(Card.Cards.CS2_146)//南海船工 Southsea Deckhand ID：CS2_146
					|| board.HasCardInHand(Card.Cards.EX1_008)//银色侍从 Argent Squire ID：EX1_008
					|| board.HasCardInHand(Card.Cards.EX1_405)//持盾卫士 Shieldbearer ID：EX1_405
					|| board.HasCardInHand(Card.Cards.EX1_010)//狼人渗透者 Worgen Infiltrator ID：EX1_010
					|| board.HasCardInHand(Card.Cards.NEW1_025)//血帆海盗 Bloodsail Corsair ID：NEW1_025
					|| board.HasCardInHand(Card.Cards.EX1_004)//年轻的女祭司 Young Priestess ID：EX1_004
					|| board.HasCardInHand(Card.Cards.EX1_001)//圣光护卫者 Lightwarden ID：EX1_001
					|| board.HasCardInHand(Card.Cards.EX1_009)//愤怒的小鸡 Angry Chicken ID：EX1_009
					//基本
					|| board.HasCardInHand(Card.Cards.EX1_011)//巫医 Voodoo Doctor ID：EX1_011
					|| board.HasCardInHand(Card.Cards.CS2_168)//鱼人袭击者 Murloc Raider ID：CS2_168
					|| board.HasCardInHand(Card.Cards.CS2_189)//精灵弓箭手 Elven Archer ID：CS2_189
					|| board.HasCardInHand(Card.Cards.EX1_508)//暗鳞先知 Grimscale Oracle ID：EX1_508
					|| board.HasCardInHand(Card.Cards.CS2_171)//石牙野猪 Stonetusk Boar ID：CS2_171
					|| board.HasCardInHand(Card.Cards.CS1_042)//闪金镇步兵 Goldshire Footman ID：CS1_042
					//贼猫给的复生卡
					|| board.HasCardInHand(Card.Cards.ULD_723)//鱼人木乃伊 Murmy ID：ULD_723
					|| board.HasCardInHand(Card.Cards.ULD_185)//神殿狂战士 Temple Berserker ID：ULD_185
					|| board.HasCardInHand(Card.Cards.ULD_205)//夺烛木乃伊 Candletaker ID：ULD_205
					|| board.HasCardInHand(Card.Cards.ULD_214)//慷慨的木乃伊 Generous Mummy ID：ULD_214
					|| board.HasCardInHand(Card.Cards.ULD_275)//白骨怨灵 Bone Wraith ID：ULD_275
					|| board.HasCardInHand(Card.Cards.ULD_274)//废土刺客 Wasteland Assassin ID：ULD_274
					|| board.HasCardInHand(Card.Cards.ULD_702)//机械法医 Mortuary Machine ID：ULD_702
					|| board.HasCardInHand(Card.Cards.ULD_208)//卡塔图防御者 Khartut Defender ID：ULD_208
					|| board.HasCardInHand(Card.Cards.ULD_215)//被缚的魔像 Wrapped Golem ID：ULD_215
					|| board.Hand.Count(minion => minion.Template.Id == Card.Cards.DMF_125) >= 2//如果给了2张安全检察员（发牌员你去死吧）！！！
					//应该不会有月亮巨人洗到牌库里这个情况吧？？他可是10费卡
				){//如果手上有中立卡
					//如果手上和牌库里有光铸卡，那么这张卡不能用
					if(board.Deck.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.DRG_231) >= 1//光铸远征军 Lightforged Crusader ID：DRG_231
					|| board.Deck.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.DRG_232) >= 1//光铸狂热者 Lightforged Zealot ID：DRG_232
					|| board.Deck.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.DMF_241) >= 1//大主教伊瑞尔 High Exarch Yrel ID：DMF_241
					|| board.HasCardInHand(Card.Cards.DRG_231)//光铸远征军 Lightforged Crusader ID：DRG_231
					|| board.HasCardInHand(Card.Cards.DRG_232)//光铸狂热者 Lightforged Zealot ID：DRG_232
					|| board.HasCardInHand(Card.Cards.DMF_241)//大主教伊瑞尔 High Exarch Yrel ID：DMF_241
					){
						p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_125, new Modifier(999));//安全检查员 Safety Inspector ID：DMF_125
					}else{
						//光铸卡打完了，直接过牌
						p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_125, new Modifier(-100));//安全检查员 Safety Inspector ID：DMF_125
					}
				}else{
					//手上没中立卡，过一张牌岂不美哉？
					p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_125, new Modifier(-100));//安全检查员 Safety Inspector ID：DMF_125
				}
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


            //送掉怪


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
			//加基森拍卖师 Gadgetzan Auctioneer ID：EX1_095
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_095, new Modifier(200));
			//菲里克·飞刺 Flik Skyshiv ID：DRG_037
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_037, new Modifier(200));
			//死亡守望者 Deathwarden ID：YOP_012
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_012, new Modifier(200));
			
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
			
			//如果对面是贼，需要提高狐人老千的威胁值
			if (board.EnemyClass == Card.CClass.ROGUE
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DMF_511)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_511, new Modifier(199));//狐人老千 Foxy Fraud ID：DMF_511
				//Bot.Log("对面是潜行者要优先解掉狐人老千");
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
				Bot.Log("对面是萨满要优先解掉野兔、死斗场决斗者");
			}
			
			//如果对面是瞎子提高血法师威胁值
			if (board.EnemyClass == Card.CClass.DEMONHUNTER
			&& board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.EX1_012)
			){
				p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_012, new Modifier(199));//血法师萨尔诺斯 Bloodmage Thalnos ID：EX1_012
				Bot.Log("对面是瞎子提高血法师威胁值");
			}
			
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

            //降低攻击优先值
            //降低资深探险者 Licensed Adventurer ID：YOD_030威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_030, new Modifier(70));
            //降低龙裔小鬼威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_238t12t2, new Modifier(60));

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
