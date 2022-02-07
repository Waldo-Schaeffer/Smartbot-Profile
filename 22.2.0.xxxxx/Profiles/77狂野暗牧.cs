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
            {Card.Cards.CORE_EX1_238, 3},//闪电箭 Lightning Bolt     CORE_EX1_238
            {Card.Cards.DMF_701, 4},//深水炸弹 Dunk Tank     DMF_701
            {Card.Cards.DMF_701t, 4},//深水炸弹 Dunk Tank     DMF_701t
            {Card.Cards.BT_100, 3},//毒蛇神殿传送门 Serpentshrine Portal     BT_100 
            //德鲁伊

            //猎人
            {Card.Cards.BAR_801, 1},//击伤猎物 Wound Prey     BAR_801
            {Card.Cards.CORE_DS1_185, 2},//奥术射击 Arcane Shot     CORE_DS1_185
            {Card.Cards.CORE_BRM_013, 3},//快速射击 Quick Shot     CORE_BRM_013
            {Card.Cards.BT_205, 3},//废铁射击 Scrap Shot     BT_205 
            //法师
            {Card.Cards.BAR_541, 2},//符文宝珠 Runed Orb     BAR_541 
            {Card.Cards.CORE_CS2_029, 6},//火球术 Fireball     CORE_CS2_029
            {Card.Cards.BT_291, 5},//埃匹希斯冲击 Apexis Blast     BT_291 
            //骑士
            {Card.Cards.CORE_CS2_093, 2},//奉献 Consecration     CORE_CS2_093 
            //牧师
            //盗贼
            {Card.Cards.BAR_319, 2},//邪恶挥刺（等级1） Wicked Stab (Rank 1)     BAR_319
            {Card.Cards.BAR_319t, 4},//邪恶挥刺（等级2） Wicked Stab (Rank 2)     BAR_319t
            {Card.Cards.BAR_319t2, 6},//邪恶挥刺（等级3） Wicked Stab (Rank 3)     BAR_319t2 
            {Card.Cards.CORE_CS2_075, 3},//影袭 Sinister Strike     CORE_CS2_075
            //术士
            {Card.Cards.CORE_CS2_062, 3},//地狱烈焰 Hellfire     CORE_CS2_062
            //战士
            {Card.Cards.DED_006, 6},//重拳先生 DED_006
            //中立
            {Card.Cards.DREAM_02, 5},//伊瑟拉苏醒 Ysera Awakens     DREAM_02
        };
        //直伤随从卡牌（必须可以打脸）
        private static readonly Dictionary<Card.Cards, int> _MinionDamagesTable = new Dictionary<Card.Cards, int>
        {
            //盗贼
            {Card.Cards.BAR_316, 2},//油田伏击者 Oil Rig Ambusher     BAR_316 
            //萨满
            {Card.Cards.CORE_CS2_042, 4},//火元素 Fire Elemental     CORE_CS2_042 
            //德鲁伊
            //术士
            {Card.Cards.CORE_CS2_064, 1},//恐惧地狱火 Dread Infernal     CORE_CS2_064 
            //中立
            {Card.Cards.CORE_CS2_189, 1},//精灵弓箭手 Elven Archer     CORE_CS2_189
            {Card.Cards.CS3_031, 8},//生命的缚誓者阿莱克丝塔萨 Alexstrasza the Life-Binder     CS3_031 
            {Card.Cards.DMF_174t, 4},//马戏团医师 Circus Medic     DMF_174t
            {Card.Cards.DMF_066, 2},//小刀商贩 Knife Vendor     DMF_066 
            {Card.Cards.SCH_199t2, 2},//转校生 Transfer Student     SCH_199t2 
            {Card.Cards.SCH_273, 1},//莱斯·霜语 Ras Frostwhisper     SCH_273
            {Card.Cards.BT_187, 3},//凯恩·日怒 Kayn Sunfury     BT_187
            {Card.Cards.BT_717, 2},//潜地蝎 Burrowing Scorpid     BT_717 
            {Card.Cards.CORE_EX1_249, 2},//迦顿男爵 Baron Geddon     CORE_EX1_249 
            {Card.Cards.DMF_254, 30},//迦顿男爵 Baron Geddon     CORE_EX1_249 
            {Card.Cards.GVG_009, 3},//暗影投弹手 GVG_009
            {Card.Cards.DS1_233, 5},//心灵震爆 DS1_233 
            {Card.Cards.EX1_625t, 2},//心灵尖刺 EX1_625t  
        };
        #endregion

#region 攻击模式和自定义 
      public ProfileParameters GetParameters(Board board)
      {

            var p = new ProfileParameters(BaseProfile.Rush) { DiscoverSimulationValueThresholdPercent = -10 };           
            //Bot.Log("玩家信息: " + rank+"/n"+Legend);
            int a = (board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) - BoardHelper.GetEnemyHealthAndArmor(board);
            //攻击模式切换
        
            // if ( board.EnemyClass == Card.CClass.SHAMAN
            //     || board.EnemyClass == Card.CClass.PALADIN
            //     || board.EnemyClass == Card.CClass.WARRIOR
            //     || board.EnemyClass == Card.CClass.MAGE
            //     || board.EnemyClass == Card.CClass.ROGUE
            //     )
            // {
            //     p.GlobalAggroModifier = (int)(a * 0.625 + 96.5);
            //     Bot.Log("攻击值"+(a * 0.625 + 96.5));
            // }
            // else
            // {
            //     p.GlobalAggroModifier = (int)(a * 0.625 + 113.5);
            //     Bot.Log("攻击值"+(a * 0.625 + 103.5));
            // }	   
                     
           p.GlobalAggroModifier = (int)(a * 0.625 + 150.5);
                Bot.Log("攻击值"+(a * 0.625 + 150.5));

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
         //定义场攻  用法 myAttack <= 5 自己场攻大于小于5  enemyAttack  <= 5 对面场攻大于小于5  已计算武器伤害

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
            // 友方随从数量
            int friendCount = board.MinionFriend.Count;
            // 海盗数量
            int haidaonum=board.Hand.Count(card => card.Race == Card.CRace.PIRATE);
            Bot.Log("手上海盗数量"+haidaonum);
            // 除去空降匪徒
            int filterHaidao=haidaonum-board.Hand.Count(x => x.Template.Id == Card.Cards.DRG_056);
            Bot.Log("除去空降匪徒海盗数量"+haidaonum);
            int changshanghaidaonum=board.MinionFriend.Count(card => card.Race == Card.CRace.PIRATE);
            Bot.Log("场上海盗数量"+changshanghaidaonum);
            int luokala=board.MinionFriend.Count(x => x.Template.Id == Card.Cards.SW_028t5)+board.Hand.Count(x => x.Template.Id == Card.Cards.SW_028t5)+board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.SW_028t5);
            Bot.Log("船长洛卡拉"+luokala);
            // 坟场海盗数量
            int fenchanghaidao=board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Race  == Card.CRace.PIRATE);
            Bot.Log("坟场海盗数量"+fenchanghaidao);
          
            // 使用过的空降歹徒 DRG_056数量
            int nanhai=board.MinionFriend.Count(x => x.Template.Id == Card.Cards.DRG_056)+board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.DRG_056);
            // 使用过的海盗帕奇斯 CFM_637  加空降歹徒数量
            int paqisi=board.MinionFriend.Count(x => x.Template.Id == Card.Cards.CFM_637)+board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.CFM_637);
            Bot.Log("使用过的帕奇斯和空降歹徒数量"+(nanhai+paqisi));
              // 使用过的海盗数量(不算帕奇斯和空降歹徒)
            int usedhaidao=fenchanghaidao+changshanghaidaonum-nanhai-paqisi;
            Bot.Log("使用过的海盗数量(不算帕奇斯和空降歹徒)"+usedhaidao);
            // 手上有暗影法术
            int handAnYing=board.Hand.Count(x => x.Template.Id == Card.Cards.DS1_233)+board.Hand.Count(x => x.Template.Id == Card.Cards.CS3_028)+board.Hand.Count(x => x.Template.Id == Card.Cards.UNG_029);
            Bot.Log("手上有暗影法术"+handAnYing);

 #endregion

#region 武器优先级
    // p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.SW_025, new Modifier(-50));//拍卖行木槌 Auctionhouse Gavel     SW_025 
#endregion

#region  随从优先级
    // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DMF_062, new Modifier(-20)); //提高旋岩虫 Gyreworm     DMF_062 优先级
  
#endregion
  
#region 法术优先级
    // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(55));
#endregion

#region 不送怪的逻辑
    // p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SW_080, new Modifier(250));// 考内留斯·罗姆 Cornelius Roame     SW_080

#endregion

#region 增加随从威胁值
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_155, new Modifier(200));//废料场巨像 Scrapyard Colossus     BT_155 
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_709, new Modifier(200));// 增加巨型图腾埃索尔 Grand Totem Eys'or     DMF_709 的攻击优先值
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SW_419, new Modifier(200));// 增加艾露恩神谕者      SW_419 的攻击优先值
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SW_319, new Modifier(200));// 增加农夫 的攻击优先值
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.WC_006, new Modifier(200));
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SW_033, new Modifier(200));//运河慢步者 Canal Slogger     SW_033
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_007, new Modifier(200));//电击学徒 Novice Zapper     CS3_007 
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_074, new Modifier(200));//前沿哨所      BAR_074 
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_918, new Modifier(200));//塔姆辛·罗姆 Tamsin Roame     BAR_918 
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_230, new Modifier(200));//伊格诺斯 Il'gynoth     DMF_230 
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_733, new Modifier(200));//莫尔葛工匠 Mo'arg Artificer     BT_733 
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DMF_734, new Modifier(200));//格雷布     DMF_734  
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_035, new Modifier(200));//月牙 Moon方    YOP_035
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GVG_040, new Modifier(200));//沙鳞灵魂行者 Siltfin Spiritwalker ID：GVG_040
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BT_304, new Modifier(200));//改进型恐惧魔王 Enhanced Dreadlord ID：BT_304
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SW_068, new Modifier(200));//莫尔葛熔魔 Mo'arg Forgefiend ID：SW_068 
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_860, new Modifier(200));//火焰术士弗洛格尔 Firemancer Flurgl ID：BAR_860
    p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DED_519, new Modifier(200));//迪菲亚炮手  DED_519
  
#endregion


#region 亡者复生 Raise Dead   SCH_514
// 如果坟场没有随从,不用亡者复生
if(board.HasCardInHand(Card.Cards.SCH_514)
&&(board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Type == Card.CType.MINION)==0)
){
  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(999));
  Bot.Log("亡者复生 999 条件1");
}
if(board.HasCardInHand(Card.Cards.SCH_514)
&&(board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Type == Card.CType.MINION)<=3)
&&board.FriendGraveyard.Contains(Card.Cards.BAR_911)//魂撕裂 Soul Rend     BAR_911
){
  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(999));
  Bot.Log("亡者复生 999 条件3");
}
if(board.HasCardInHand(Card.Cards.SCH_514)
&&board.Hand.Count>=10
){
  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(999));
  Bot.Log("亡者复生 999 条件2");
}
// 如果坟场没有随从,不用亡者复生
if(board.HasCardInHand(Card.Cards.SCH_514)
&&(board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Type == Card.CType.MINION)>=2)
){
  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(-40));
  Bot.Log("亡者复生 -40");
}

#endregion


#region 空降歹徒 DRG_056
            if(board.HasCardInHand(Card.Cards.DRG_056)
            &&filterHaidao>=1
            ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_056, new Modifier(999)); 
            Bot.Log("空降歹徒 999");
            }
#endregion
#region 葛拉卡爬行蟹 UNG_807
            if(board.HasCardInHand(Card.Cards.UNG_807)
             ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_807, new Modifier(130)); 
            Bot.Log("葛拉卡爬行蟹 130");
            }
#endregion
#region 海盗帕奇斯 CFM_637 
            if(board.HasCardInHand(Card.Cards.CFM_637)
            ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CFM_637, new Modifier(-20)); 
            Bot.Log("海盗帕奇斯 -20");
            }
#endregion
#region 迪菲亚麻风侏儒 DED_513 
            if(board.HasCardInHand(Card.Cards.DED_513)
            &&handAnYing==0
            ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DED_513, new Modifier(130)); 
            Bot.Log("迪菲亚麻风侏儒 130");
            }
#endregion
#region 暗影布缝针 SW_012
            if(board.HasCardInHand(Card.Cards.SW_012)
            &&handAnYing>0
            &&board.WeaponFriend == null
            ){
            p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.SW_012, new Modifier(-99));  
            Bot.Log("暗影布缝针 -99");
            }
#endregion


#region 血帆海盗 Bloodsail Corsair NEW1_025 
            if(board.HasCardInHand(Card.Cards.NEW1_025)
            ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_025, new Modifier(-40)); 
            Bot.Log("血帆海盗 -40");
            }
            if(board.HasCardInHand(Card.Cards.NEW1_025)
            &&board.WeaponEnemy != null
             ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_025, new Modifier(-99)); 
            Bot.Log("血帆海盗 -99");
            }
#endregion
#region 船载火炮 GVG_075 
          if(board.HasCardInHand(Card.Cards.GVG_075)
            &&haidaonum>=1
            ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GVG_075, new Modifier(-999)); 
            p.PlayOrderModifiers.AddOrUpdate(Card.Cards.GVG_075, new Modifier(550)); 
            Bot.Log("船载火炮 -999 优先级 550");
            }
#endregion
#region  空中炮艇 Skybarge ID：DRG_023  
          if(board.HasCardInHand(Card.Cards.DRG_023)
            &&haidaonum>=1
            &&board.MaxMana ==3
            &&enemyAttack<5
            ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(-99)); 
            p.PlayOrderModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(550)); 
            Bot.Log("空中炮艇 -99 优先级 550");
            }
          if(board.HasCardInHand(Card.Cards.DRG_023)
            &&haidaonum>=1
            &&(board.MaxMana >=3||(board.MaxMana==2&&board.HasCardInHand(Card.Cards.GAME_005)))
                ){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(-99)); 
            p.PlayOrderModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(550)); 
            Bot.Log("空中炮艇 -99 优先级 550");
            }
          if(board.HasCardOnBoard(Card.Cards.DRG_023)
            ){
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.DRG_023, new Modifier(250)); 
           Bot.Log("空中炮艇 不送");
            }
#endregion

#region Card.Cards.HERO_01bp 英雄技能
        p.PlayOrderModifiers.AddOrUpdate(Card.Cards.HERO_01bp, new Modifier(-550)); 
#endregion


#region 恐怖海盗 Dread Corsair  NEW1_022
        if(board.HasCardInHand(Card.Cards.NEW1_022)){
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.NEW1_022, new Modifier(130));  
            Bot.Log("恐怖海盗 130 ");
            }
#endregion

#region 十字路口哨所      BAR_075

      // 如果没有随从,降低4费马桶优先级,如果有则提高优先级
      if(board.HasCardInHand(Card.Cards.BAR_075)){
      p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_075, new Modifier(-200*(friendCount)));//十字路口哨所      BAR_075 
      Bot.Log("十字路口哨所"+-200*friendCount);
      }
      // if(board.HasCardOnBoard(Card.Cards.BAR_075)
      // &&board.HasCardInHand(Card.Cards.BAR_075)
      // ){
      // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_075, new Modifier(150));//十字路口哨所      BAR_075 
      // Bot.Log("十字路口哨所 150");
      // }
          
#endregion


#region 对阵牧师
      //对面是牧师，火车王随便下
			if (board.EnemyClass == Card.CClass.PRIEST)
			{
				p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116, new Modifier(-50));//修改火车王的优先级
                    //提高雏龙威胁值雏龙 Whelp ID：EX1_116t 
			p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_116t, new Modifier(100));
			}
#endregion

#region 贪婪的书虫      SCH_142
    // 书虫相关
        if (board.Hand.Count <=3
       &&board.Hand.Count(x=>x.CurrentCost==3 && x.Template.Id==Card.Cards.SCH_142)==1
       )//贪婪的书虫      SCH_142
        {
          p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(-99));//贪婪的书虫      SCH_142
          p.PlayOrderModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(480)); 
          Bot.Log("贪婪的书虫 -99");
        }else{
          p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(150));//贪婪的书虫      SCH_142
        }
        if (board.Hand.Count <=4
       &&board.Hand.Count(x=>x.CurrentCost==3 && x.Template.Id==Card.Cards.SCH_142)==2
       )//贪婪的书虫      SCH_142
        {
          p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(-99));//贪婪的书虫      SCH_142
          p.PlayOrderModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(480)); 
          Bot.Log("贪婪的书虫 -99");
        }else{
          p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(150));//贪婪的书虫      SCH_142
        }
    //场上有书虫，提高手里硬币优先值
        if (board.HasCardOnBoard(Card.Cards.SCH_142)//贪婪的书虫      SCH_142
        && board.HasCardInHand(Card.Cards.GAME_005)
        )
        {
          p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(-10));
          p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_427, new Modifier(-10));//雷霆绽放      SCH_427
          Bot.Log("雷霆绽放 -10 硬币 -10");
        }
    
    //书虫分散投资
        if (board.HasCardOnBoard(Card.Cards.SCH_142)//贪婪的书虫      SCH_142S
        )
        {
          p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_142, new Modifier(150));//贪婪的书虫      SCH_142
          Bot.Log("贪婪的书虫 150");
        }
    
#endregion

#region 毁灭战舰 The Juggernaut ID：SW_028t6
//  女巫跟班 Witchy Lackey ID：DAL_615 毁灭战舰 The Juggernaut ID：SW_028t6 
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DAL_615, new Modifier(999,Card.Cards.SW_028t6)); 
#endregion
#region 大型魔像

        if(board.ManaAvailable >= 5
            && board.HasCardInHand(Card.Cards.BAR_079_m2)//大型魔像 Greater Golem     BAR_079_m2
        )
        {
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BAR_079_m2, new Modifier(-200));
        } //剩余5费提高用大型魔像优先

#endregion

#region 攻击优先 卡牌威胁（通用）  更新到贫瘠之地


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.ULD_231))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.ULD_231, new Modifier(200));
            }//连环腿大师 Whirlkick Master ULD_231 
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SW_115 ))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SW_115 , new Modifier(200));
            }//伯尔纳·锤喙 Bolner Hammerbeak ID：SW_115 

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
            }//暗月雕像 Darkmoon Statue     DMF_082t 

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
            }//如果对面场上有对空奥术法师 Arcane Flakmage     ULD_240，提高攻击优先度


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
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BRM_002, new Modifier(500));
            }//如果对面场上有火妖，提高攻击优先度


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CFM_020))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CFM_020, new Modifier(0));
            }//如果对面场上有缚链者拉兹 Raza the Chained CFM_020，降低攻击优先度                     


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.EX1_608))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.EX1_608, new Modifier(250));
            }//如果对面场上有巫师学徒 Sorcerer's Apprentice     X1_608，提高攻击优先度

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.VAN_EX1_608))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.VAN_EX1_608, new Modifier(250));
            }//如果对面场上有巫师学徒 Sorcerer's Apprentice     VAN_EX1_608，提高攻击优先度

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
            }//如果对面场上有螃蟹骑士 Crabrider     YOP_031，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_537))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_537, new Modifier(200));
            }//如果对面场上有钢鬃卫兵  BAR_537，提高攻击优先度  

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
            }//如果对面场上有前沿哨所  BAR_074 ，提高攻击优先度  

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
            }//如果对面场上有十字路口哨所  BAR_075，提高攻击优先度  

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
            }//如果对面场上有考瓦斯·血棘 Kor'vas Bloodthorn     CS3_019 ，提高攻击优先度 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CORE_FP1_031))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_FP1_031, new Modifier(200));
            }//如果对面场上有瑞文戴尔男爵 Baron Rivendare     CORE_FP1_031 ，提高攻击优先度 

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CS3_032))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_032, new Modifier(200));
            }//如果对面场上有龙巢之母奥妮克希亚 Onyxia the Broodmother     CS3_032 ，提高攻击优先度   

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SCH_317))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_317, new Modifier(200));
            }//如果对面场上有团伙核心 Playmaker     SCH_317 ，提高攻击优先度  

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_847))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_847, new Modifier(200));
            }//如果对面场上有洛卡拉 Rokara     BAR_847 ，提高攻击优先度  


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CS3_025))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_025, new Modifier(200));
            }//如果对面场上有伦萨克大王 Overlord Runthak     CS3_025 ，提高攻击优先度  


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.YOP_021))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOP_021, new Modifier(200));
            }//如果对面场上有被禁锢的凤凰 Imprisoned Phoenix     YOP_021  ，提高攻击优先度  


        //    if ((board.HeroEnemy.CurrentHealth + board.HeroEnemy.CurrentArmor)>= 20
        //      && board.MinionEnemy.Count(x=>x.IsLifeSteal==true && x.Template.Id==Card.Cards.CS3_031)>=1
        //    )
        //    {
        //        p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_031, new Modifier(200));
        //    }//如果对面场上有生命的缚誓者阿莱克丝塔萨 Alexstrasza the Life-Binder     CS3_031 有吸血属性，提高攻击优先度
        //    else
        //    {
        //        p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_031, new Modifier(0));
        //    }//如果对面场上有生命的缚誓者阿莱克丝塔萨 Alexstrasza the Life-Binder     CS3_031 没吸血属性，降低攻击优先度



            if ((board.HeroEnemy.CurrentHealth + board.HeroEnemy.CurrentArmor)>= 20
                && board.MinionEnemy.Count(x=>x.IsLifeSteal==true && x.Template.Id==Card.Cards.CS3_033)>=1
            )
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_033, new Modifier(200));
            }//如果对面场上有沉睡者伊瑟拉 Ysera the Dreamer     CS3_033 有吸血属性，提高攻击优先度
            else
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_033, new Modifier(0));
            }//如果对面场上有沉睡者伊瑟拉 Ysera the Dreamer     CS3_033 没吸血属性，降低攻击优先度

                                   
            if ((board.HeroEnemy.CurrentHealth + board.HeroEnemy.CurrentArmor)>= 20
              && board.MinionEnemy.Count(x=>x.IsLifeSteal==true && x.Template.Id==Card.Cards.CS3_034)>=1
            )
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_034, new Modifier(200));
            }//如果对面场上有织法者玛里苟斯 Malygos the Spellweaver     CS3_034 有吸血属性，提高攻击优先度
            else
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CS3_034, new Modifier(0));
            }//如果对面场上有织法者玛里苟斯 Malygos the Spellweaver     CS3_034 没吸血属性，降低攻击优先度


            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.CORE_EX1_110))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CORE_EX1_110, new Modifier(0));
            }//如果对面场上有凯恩·血蹄 Cairne Bloodhoof     CORE_EX1_110 ，降低攻击优先度   


            //对面如果是盗贼 巴罗夫拉出来的怪威胁值优先（主要防止战吼怪被回手重新使用）
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.BAR_072))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.BAR_072, new Modifier(0));
            }//如果对面场上有火刃侍僧 Burning Blade Acolyte     BAR_072 ，降低攻击优先度   

            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.SCH_351))
            {
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_351, new Modifier(200));
            }//如果对面场上有詹迪斯·巴罗夫 Jandice Barov     SCH_351 ，提高攻击优先度  


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