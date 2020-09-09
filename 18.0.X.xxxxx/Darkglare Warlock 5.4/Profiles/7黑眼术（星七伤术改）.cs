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
		//萨满
		private const Card.Cards TotemicCall = Card.Cards.HERO_02bp



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
            {Card.Cards.EX1_308, 4},//灵魂之火 Soulfire  ID：EX1_308
            {Card.Cards.SCH_248, 1},//甩笔侏儒 Pen Flinger  ID：SCH_248
            {Card.Cards.TRL_512, 1},//调皮的噬踝者 Cheaty Anklebiter  ID：TRL_512
            {Card.Cards.GVG_015, 3},//暗色炸弹 Darkbomb  ID：GVG_015
            {Card.Cards.EX1_316, 4},//力量的代价 Power Overwhelming  ID：EX1_316
            {Card.Cards.EX1_116, 6},//火车王里诺艾 Leeroy Jenkins  ID：EX1_116


        };


        //攻击模式



       
        public ProfileParameters GetParameters(Board board)
        {


            //var p = new ProfileParameters(BaseProfile.Default) { DiscoverSimulationValueThresholdPercent = -10 };
            Card z = board.Hand.Find(x => x.Template.Id > 0);
            Card y = board.Hand.FindLast(x => x.Template.Id > 0);
            int OutcastCards = board.Hand.Count(x => x.CurrentCost > 0 && BoardHelper.IsOutCastCard(x, board) == true);
            int GuldanOutcastCards = board.Hand.Count(x => x.CurrentCost > 0 && BoardHelper.IsGuldanOutCastCard(x, board) == true);


            Bot.Log("OutcastCards: " + (int)(OutcastCards + GuldanOutcastCards));
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

            if (board.HeroFriend.CurrentHealth <= 15)      //血小于等于10             
            {
                p.GlobalAggroModifier = (int)(a * 0.625 + 70.5);            //则切换为控场模式
            }

            Bot.Log("攻击性：" + p.GlobalAggroModifier.Value);


           

            



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



               



                if (board.HasCardOnBoard(Card.Cards.GVG_107)//强化机器人 Enhance-o Mechano ID：GVG_107
                    && board.MinionFriend.Count >= 2
                    || board.HeroFriend.CurrentHealth <9
                    && myAttack>=12)
                {
                    
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GVG_107, new Modifier(-50)); //强化机器人 Enhance-o Mechano  ID：GVG_107
                }
                else
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.GVG_107, new Modifier(350)); //强化机器人 Enhance-o Mechano  ID：GVG_107
                }


                if (board.HasCardOnBoard(Card.Cards.EX1_058)//日怒保卫者 Sunfury Protector  ID：EX1_058
                  && board.HeroFriend.CurrentHealth < 15)
                {
                  
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_058, new Modifier(-60)); //日怒保卫者 Sunfury Protector  ID：EX1_058
                }
                else
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_058, new Modifier(500)); //日怒保卫者 Sunfury Protector  ID：EX1_058
                }

                //如果下一轮可以斩杀对面提高洛欧塞布 Loatheb  ID：FP1_030
                if (board.HasCardInHand(Card.Cards.FP1_030)//洛欧塞布 Loatheb  ID：FP1_030
                && !board.MinionEnemy.Any(x => x.IsTaunt)
                && (BoardHelper.GetEnemyHealthAndArmor(board) - BoardHelper.GetPotentialMinionDamages(board) - BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <= BoardHelper.GetTotalBlastDamagesInHand(board))
                || myAttack>= (board.HeroEnemy.CurrentHealth)-5

                )

                {
                    
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.FP1_030, new Modifier(-100));//洛欧塞布 Loatheb  ID：FP1_030
                }

               







                //血量小于10开始提高紫水晶优先值大型法术紫水晶 Greater Amethyst Spellstone  ID：LOOT_043t3
                if ((board.HeroFriend.CurrentHealth < 15)
                && board.HasCardInHand(Card.Cards.LOOT_043t3)
                || board.HasCardInHand(Card.Cards.LOOT_043)//小型法术紫水晶 Lesser Amethyst Spellstone  ID：LOOT_043
                || board.HasCardInHand(Card.Cards.LOOT_043t2)//法术紫水晶 Amethyst Spellstone  ID：LOOT_043t2
                && board.MinionEnemy.Count == 0
                || board.MinionFriend.Count == 7

                )

                {
                   

                    // 20血 优先级 1000 0
                    // 15血 优先级 350 -350
                    // 11血 优先级 100 -900
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t3, new Modifier(-80));//大型法术紫水晶 Greater Amethyst Spellstone  ID：LOOT_043t3
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t2, new Modifier(-60));//法术紫水晶 Amethyst Spellstone  ID：LOOT_043t2
                                                                                                //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t3, new Modifier(waldo));//p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t3, new Modifier(waldo));//紫水晶 Greater Amethyst Spellstone  ID：LOOT_043t3



                }


                if (board.ManaAvailable <= 3
                 && board.HasCardInHand(Card.Cards.ICC_041)//亵渎 Defile  ID：ICC_041
                 && board.MinionEnemy.Sum(x => x.CurrentAtk) <4
                 
                 )

                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.ICC_041, new Modifier(350)); //亵渎 Defile  ID：ICC_041
                }//对面没随从，或者随从伤害低，不打亵渎
               


                //if ((board.MinionFriend.Count(x => x.Race == Card.CRace.DEMON) + board.Hand.Count(x => x.Race == Card.CRace.DEMON) + board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Race == Card.CRace.DEMON) - board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Id == Card.Cards.BT_307) - board.MinionFriend.Count(x => x.Template.Id == Card.Cards.BT_307) ) >= 4)
                //{
                //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_317, new Modifier(500));
                //}//牌库没有恶魔，不打感知恶魔









                //if (!board.MinionEnemy.Any(x => x.IsTaunt) &&
                //   (BoardHelper.GetEnemyHealthAndArmor(board) -
                //  BoardHelper.GetPotentialMinionDamages(board) -
                //BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <=
                //BoardHelper.GetTotalBlastDamagesInHand(board)))
                //{
                //  p.GlobalAggroModifier = 450;
                //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CFM_120, new Modifier(300));
                //}//如果下一轮可以斩杀对面，攻击性提高

            }


            //自定义命名




            //判定亡者复生，是否可用
            //int Raise_num;

            //bool Raise_active;

            //if( board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Type == Card.CType.MINION) == 0 )
            //{
            //  Raise_num = 0;

            //Raise_active = false;

            //}
            //else
            //{
            //  if( board.HasCardInHand(Card.Cards.SCH_514))
            //    {
            //  Raise_active = true;

            //                }
            //              else
            //            {
            //              Raise_active = false;

            //        }
            //      if( board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Type == Card.CType.MINION) == 1 )
            //    {
            //      Raise_num = 1;

            //}
            //else
            //{
            //  Raise_num = 2;

            //                }
            //          }





            //具体策略


            //血量小于15点，提高紫水晶优先级
            // if (board.HasCardOnBoard(Card.Cards.LOOT_043)
            //    || board.HasCardOnBoard(Card.Cards.LOOT_043t2)
            //  || board.HasCardOnBoard(Card.Cards.LOOT_043t3)
            //    && (board.HeroFriend.CurrentHealth < 15)
            //  )
            //{
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043, new Modifier(-100));//小型法术紫水晶
            //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t2, new Modifier(-300));//法术紫水晶
            //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t3, new Modifier(-900));//大型法术紫水晶
            //}
            //else
            //{
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043, new Modifier(300));//小型法术紫水晶
            // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t2, new Modifier(300));//法术紫水晶
            //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t3, new Modifier(300));//大型法术紫水晶
            //}




            //初始化完毕


            //通过策略调整优先级


            //总血量不少于15不下晶化师
            if (((board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) < 16)
                && board.HasCardInHand(Card.Cards.BOT_447)
                )
            {
               p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_447, new Modifier(-200));//晶化师
            }
            else
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_447, new Modifier(350));//晶化师
           }

            

            


            //如果有黑眼，没硬币，三费不下狗头人，不下小鬼
            if (board.HasCardInHand(Card.Cards.BT_307)//黑眼 Darkglare  ID：BT_307
                || !board.HasCardOnBoard(Card.Cards.BT_307) //自己场上有黑眼 Darkglare  ID：BT_307
                && board.ManaAvailable == 3
                && board.HasCardInHand(Card.Cards.EX1_319)//烈焰小鬼 Flame Imp  ID：EX1_319
                || board.HasCardInHand(Card.Cards.LOOT_014)//狗头人图书管理员 Kobold Librarian  ID：LOOT_014
                || board.HasCardInHand(Card.Cards.SCH_248)//甩笔侏儒 Pen Flinger  ID：SCH_248
                || board.HasCardInHand(Card.Cards.BOT_447)//晶化师 Crystallizer  ID：BOT_447
               // || board.HasCardInHand(Card.Cards.SCH_312)//巡游向导 Tour Guide  ID：SCH_312

                )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(350));//甩笔侏儒 Pen Flinger  ID：SCH_248
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_447, new Modifier(350));//晶化师 Crystallizer  ID：BOT_447
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_319, new Modifier(350));//烈焰小鬼 Flame Imp  ID：EX1_319
               // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_312, new Modifier(350));//巡游向导 Tour Guide  ID：SCH_312

            }
            else
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_319, new Modifier(-25));//烈焰小鬼 Flame Imp  ID：EX1_319
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_014, new Modifier(-30));//狗头人图书管理员 Kobold Librarian  ID：LOOT_014
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(-30));//甩笔侏儒 Pen Flinger  ID：SCH_248
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_447, new Modifier(-20));//晶化师 Crystallizer  ID：BOT_447
               // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_312, new Modifier(-20));//巡游向导 Tour Guide  ID：SCH_312

            }



           





            //如果有黑眼，则3费之前不用硬币
            if (board.HasCardInHand(Card.Cards.BT_307)//黑眼 Darkglare  ID：BT_307
                && board.HasCardInHand(Card.Cards.GAME_005)
                && board.ManaAvailable <=2

                )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(350));
            }

            //如果有感知恶魔 Sense Demons  ID：EX1_317，则2费之前不用硬币
            if (board.HasCardInHand(Card.Cards.EX1_317)//感知恶魔 Sense Demons  ID：EX1_317
                && board.HasCardInHand(Card.Cards.GAME_005)
                && board.ManaAvailable == 1

                )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.GAME_005, new Modifier(350));
            }
            

           
            



            if (board.EnemyClass == Card.CClass.MAGE
                   && ((board.HeroFriend.CurrentHealth + board.HeroFriend.CurrentArmor) <= 18 || board.Hand.Count >= 7)
                   && !board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.GVG_021))
            {
                p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_056_H1, new Modifier(1000));
            }//对面是法师，减少抽牌
            else
            {
                p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_056_H1, new Modifier(10));
            }//对面是法师，减少抽牌





            //if (board.ManaAvailable == 3
            //     && board.Hand.Exists(x => x.Template.Id == Card.Cards.EX1_317)
            //   && board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.NEW1_021))
            //{
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_317, new Modifier(-200));
            //p.CastHeroPowerModifier.AddOrUpdate(Card.Cards.CS2_056_H1, new Modifier(300));
            //}//3费对面有末日，打感知恶魔









            //血量不少于15不下铁钩掠夺者 Hooked Reaver  ID：LOOT_018（不算护甲）
            if ((board.HeroFriend.CurrentHealth > 15)
                && board.HasCardInHand(Card.Cards.LOOT_018)
                )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_018, new Modifier(500));//Hooked Reaver  ID：LOOT_018（不算护甲）
            }
            else
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_018, new Modifier(50));//铁钩掠夺者 Hooked Reaver  ID：LOOT_018
            }


            //血量不少于15不下熔核巨人（不算护甲）
            //if ((board.HeroFriend.CurrentHealth > 15)
            //  && board.HasCardInHand(Card.Cards.EX1_620)
            //)
            //{
            //  p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_620, new Modifier(350));//熔核巨人
            //}
            //else
            //{
            //根据血量提高熔核巨人的优先级

            //血量小于等于10直接拍巨人
            //  if ((board.HeroFriend.CurrentHealth < 11)
            //&& board.HasCardInHand(Card.Cards.EX1_620))
            //{
            //  p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_620, new Modifier(-350));//熔核巨人
            //}
            //else
            //{
            //  int waldo = -40 * (10-board.HeroFriend.CurrentHealth ) ;
            // 20血 优先级 1000 0
            // 15血 优先级 350 -350
            // 11血 优先级 100 -900
            //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_620, new Modifier(waldo));//熔核巨人
            //}
            //}


















            //血量不少于20不下血肉巨人（不算护甲）
            //if ((board.HeroFriend.CurrentHealth > 20)
            //   && board.HasCardInHand(Card.Cards.SCH_140)
            // )
            //{
            //  p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_140, new Modifier(350));//血肉巨人
            //}
            //else
            //{
            //  //根据血量提高熔核巨人的优先级

            ////血量小于等于15直接拍巨人
            //if ((board.HeroFriend.CurrentHealth < 15)
            //&& board.HasCardInHand(Card.Cards.SCH_140))
            //{
            //  p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_140, new Modifier(-350));//血肉巨人
            //}
            //else
            //{
            //  int waldo = 100 * (board.HeroFriend.CurrentHealth - 15) - 1000;
            // 25血 优先级 1000 0
            // 20血 优先级 350 -350
            // 16血 优先级 100 -900
            //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_140, new Modifier(waldo));//血肉巨人
            //}
            //}



            //没有4费不拍黑眼
            if (board.ManaAvailable >=4
                && board.HasCardInHand(Card.Cards.BT_307)
                
                )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(-200));//黑眼
            }
            else
            {
                //手上有幸运币，3费可以拍黑眼
                if (board.ManaAvailable == 3
                    && board.HasCardInHand(Card.Cards.GAME_005)
                    && board.HasCardInHand(Card.Cards.BT_307)
                 
                )
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(-80));//黑眼

                }
                else
                {
                    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(999));//黑眼
                }
            }

            //场上没有黑眼且费用小于10，不拍灵界(测试）
            if (board.ManaAvailable>=3
               && board.HasCardOnBoard(Card.Cards.BT_307)//黑眼 Darkglare  ID：BT_307
             && board.HasCardInHand(Card.Cards.BOT_568)//莫瑞甘的灵界 The Soularium ID：BOT_568
            )
            {
              p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_568, new Modifier(-9999));//莫瑞甘的灵界莫瑞甘的灵界 The Soularium  ID：BOT_568
            }

            else
            {
                if (board.ManaAvailable>=5)
                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_568, new Modifier(-9999));//莫瑞甘的灵界
                }
                else
                {
                    p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_568, new Modifier(999));//莫瑞甘的灵界
                }
            }






            //如果坟场怪小于2，不使用亡者复生 Raise Dead  ID：SCH_514
            // if (board.HasCardInHand(Card.Cards.SCH_514)
            //&& board.FriendGraveyard.Count(card => CardTemplate.LoadFromId(card).Race == Card.CRace.DEMON)<2  //坟场恶魔>=1
            //&& board.ManaAvailable<=3
            //)

            //{
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(500));//亡者复生 Raise Dead  ID：SCH_514
            //}
            //else
            //{
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(-15));
            //}
            //如果坟场有黑眼或者巨人提高亡者复生 Raise Dead  ID：SCH_514
            if (board.HasCardInHand(Card.Cards.SCH_514)//亡者复生 Raise Dead  ID：SCH_514
            && board.FriendGraveyard.Contains(Card.Cards.BT_307)  //自己坟场有黑眼 Darkglare  ID：BT_307
            || board.FriendGraveyard.Contains(Card.Cards.SCH_140) //自己坟场有血肉巨人 Flesh Giant  ID：SCH_140
            || board.FriendGraveyard.Contains(Card.Cards.SCH_514) //自己坟场有熔核巨人 Molten Giant  ID：EX1_620
            || board.FriendGraveyard.Contains(Card.Cards.ICC_851) //自己坟场有凯雷塞斯王子 Prince Keleseth  ID：ICC_851
             )

                       {
                         p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(-900));//亡者复生 Raise Dead  ID：SCH_514
                                                                                                  
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_056_H1, new Modifier(200, board.HeroEnemy.Id)); //降低术士的技能生命分流优先级
            }



            if (board.EnemyGraveyard.Any(card => CardTemplate.LoadFromId(card).Id == Card.Cards.ULD_291)
                   || board.EnemyClass == Card.CClass.DRUID
                   || board.EnemyClass == Card.CClass.PRIEST
                   || board.EnemyClass == Card.CClass.WARRIOR
                   || board.EnemyGraveyard.Any(card => CardTemplate.LoadFromId(card).Id == Card.Cards.CFM_621)
                   || board.EnemyGraveyard.Any(card => CardTemplate.LoadFromId(card).Id == Card.Cards.ULD_003)
                   && board.ManaAvailable >= 6
                   && board.EnemyDeckCount >= 10)
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_415, new Modifier(-400));
            }//对面是宇宙/otk卡组，提高林恩优先度

            if (board.MinionFriend.Any(minion => minion.Template.Id == Card.Cards.LOOT_415)
                && (board.EnemyGraveyard.Any(card => CardTemplate.LoadFromId(card).Id == Card.Cards.ULD_291)
                || board.EnemyClass == Card.CClass.DRUID
                || board.EnemyClass == Card.CClass.PRIEST
                || board.EnemyClass == Card.CClass.WARRIOR
                || board.EnemyGraveyard.Any(card => CardTemplate.LoadFromId(card).Id == Card.Cards.CFM_621)
                || board.EnemyGraveyard.Any(card => CardTemplate.LoadFromId(card).Id == Card.Cards.ULD_003))
                && board.EnemyDeckCount >= 8)
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_017, new Modifier(-1000, Card.Cards.LOOT_415));
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.LOOT_415, new Modifier(-200));
            }//送掉林恩



            // 血量大于4提高亡者复生 Raise Dead  ID：SCH_514
            //if (board.HasCardInHand(Card.Cards.SCH_514)//亡者复生 Raise Dead  ID：SCH_514
            //&& board.HeroFriend.CurrentHealth <=10
            //)

            //        {
            //        p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(-20));//亡者复生 Raise Dead  ID：SCH_514
            //}






            //如果手上有紫水晶，且血量大于15点，提高自残的优先级
            // if (board.HasCardInHand(Card.Cards.LOOT_043)
            //    || board.HasCardInHand(Card.Cards.LOOT_043t2)
            //   && board.HeroFriend.CurrentHealth > 16
            //   )
            // {
            //      p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_319, new Modifier(90));//烈焰小鬼
            //    p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_319, new Modifier(80));//狗头人图书管理员
            //  p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_013, new Modifier(100));//粗俗的矮劣魔
            //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_206, new Modifier(120));//火焰之雨
            //  p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_222, new Modifier(110));//灵魂炸弹
            // p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_803, new Modifier(50));//翡翠掠夺者
            // }


            //有黑眼秃鹫增加亡者复生
            // if (board.HasCardInHand(Card.Cards.BT_307)
            //   || board.HasCardInHand(Card.Cards.ULD_167)
            // )
            //{
            //  p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(-60));//亡者复生 Raise Dead  ID：SCH_514
            //}

            //有古尔丹之手没有熔核巨人 提高21优先值
            if (board.HasCardInHand(Card.Cards.BT_300)//古尔丹之手 Hand of Gul'dan  ID：BT_300
                && board.HasCardInHand(Card.Cards.ULD_163)//过期货物专卖商 Expired Merchant  ID：ULD_163
                &&!board.HasCardInHand(Card.Cards.EX1_620)//熔核巨人 Molten Giant  ID：EX1_620
                || board.HeroFriend.CurrentHealth <=15
             )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_163, new Modifier(-600));//过期货物专卖商 Expired Merchant  ID：ULD_163
            }






            //没有5费不拍兀鹫
            if ( board.HasCardInHand(Card.Cards.ULD_167)
                && board.ManaAvailable > 4
                )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_167, new Modifier(-100));//兀鹫
            }
			else 
			{
				//手上有幸运币，4费可以拍兀鹫
				if (board.ManaAvailable == 4
					&& board.HasCardInHand(Card.Cards.GAME_005)
					&& board.HasCardInHand(Card.Cards.ULD_167)
				)
				{
					p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_167, new Modifier(-100));
					//兀鹫
				}
				else 
				{
					p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_167, new Modifier(350));//兀鹫
				}
			}


            //场上有黑眼或者兀鹫，血量大于10点，直接自残
            if (board.HeroFriend.CurrentHealth > 3
                && board.HasCardOnBoard(Card.Cards.BT_307)
                || board.HasCardOnBoard(Card.Cards.ULD_167)
                )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.TB_011, new Modifier(-10));   //硬币
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_319, new Modifier(-30));//烈焰小鬼
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_319, new Modifier(-200));//狗头人图书管理员
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_013, new Modifier(-50));//粗俗的矮劣魔
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.TRL_512, new Modifier(-50));//调皮的噬踝者 Cheaty Anklebiter  ID：TRL_512
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_206, new Modifier(-50));//火焰之雨
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_222, new Modifier(-100));//灵魂炸弹灵魂炸弹 Spirit Bomb  ID：BOT_222
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.UNG_803, new Modifier(-100));//翡翠掠夺者
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_447, new Modifier(-60));//晶化师 Crystallizer  ID：BOT_447
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(-100));//甩笔侏儒 Pen Flinger  ID：SCH_248
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_312, new Modifier(-300));//巡游向导 Tour Guide  ID：SCH_312
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_302, new Modifier(-100));//死亡缠绕 Mortal Coil ID：EX1_302

            }

            //费用等于1 不使用药水商人 Potion Vendor  ID：DAL_544
            if (board.ManaAvailable == 1
               && board.HasCardInHand(Card.Cards.DAL_544)//药水商人 Potion Vendor  ID：DAL_544
                )
            {
                
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DAL_544, new Modifier(500));//药水商人 Potion Vendor  ID：DAL_544
            }
            else
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DAL_544, new Modifier(40));//药水商人 Potion Vendor  ID：DAL_544
            }
            //费用等于2 不使用狗头人图书管理员 Kobold Librarian  ID：LOOT_014
            if (board.ManaAvailable == 2
               && board.HasCardInHand(Card.Cards.LOOT_014)//狗头人图书管理员 Kobold Librarian  ID：LOOT_014
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_014, new Modifier(500));//狗头人图书管理员 Kobold Librarian  ID：LOOT_014
            }
           



            //费用等于1 不使用甩笔侏儒 Pen Flinger  ID：SCH_248
            if (board.ManaAvailable == 2
               && board.HasCardInHand(Card.Cards.SCH_248)//甩笔侏儒 Pen Flinger  ID：SCH_248
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(500));//甩笔侏儒 Pen Flinger  ID：SCH_248
            }
            //费用等于1 不使用甩笔侏儒 Pen Flinger  ID：SCH_248
            if (board.ManaAvailable == 1
               && board.HasCardInHand(Card.Cards.SCH_248)//甩笔侏儒 Pen Flinger  ID：SCH_248
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(500));//甩笔侏儒 Pen Flinger  ID：SCH_248
            }

            //费用等于2 不使用巡游向导 Tour Guide  ID：SCH_312
            if (board.ManaAvailable ==2
               && board.HasCardInHand(Card.Cards.SCH_312)//巡游向导 Tour Guide  ID：SCH_312
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_312, new Modifier(500));//巡游向导 Tour Guide  ID：SCH_312
            }
            


           
           

            //费用等于2 不使用精魂狱卒 Spirit Jailer  ID：SCH_700
            if (board.ManaAvailable == 2
               && board.HasCardInHand(Card.Cards.SCH_700)//精魂狱卒 Spirit Jailer  ID：SCH_700
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_700, new Modifier(500));//精魂狱卒 Spirit Jailer  ID：SCH_700
            }
           

            //手里有血肉巨人，增加笨蛋打自己优先值
            //if ( board.HasCardInHand(Card.Cards.TRL_512)//噬踝者 Cheaty Anklebiter  ID：TRL_512
              //  )
            //{

              //  p.CastMinionsModifiers.AddOrUpdate(Card.Cards.TRL_512, new Modifier(-40, Card.Cards.HERO_07bp));//提高调皮的噬踝者打自己脸优先度调皮的噬踝者 Cheaty Anklebiter  ID：TRL_512

            //}


            //if (!board.MinionEnemy.Any(x => x.IsTaunt) &&
              //   (BoardHelper.GetEnemyHealthAndArmor(board) -
                //  BoardHelper.GetPotentialMinionDamages(board) -
                  //BoardHelper.GetPlayableMinionSequenceDamages(BoardHelper.GetPlayableMinionSequence(board), board) <=
                  //BoardHelper.GetTotalBlastDamagesInHand(board)))
            //{
              //  p.GlobalAggroModifier = 450;
                //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.CFM_120, new Modifier(300));
            //}//如果下一轮可以斩杀对面，攻击性提高

            



            //手上没黑眼。场上黑眼+坟场黑眼小于2，则优先使用感知恶魔
            if ( board.HasCardInHand(Card.Cards.EX1_317)//感知恶魔 Sense Demons  ID：EX1_317
               && !board.HasCardInHand(Card.Cards.BT_307)//黑眼 Darkglare  ID：BT_307
               )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_317, new Modifier(-100));//感知恶魔 Sense Demons  ID：EX1_317

            }
            
            //4费之前不打姐夫
            if (board.ManaAvailable <3
               && board.HasCardInHand(Card.Cards.ULD_003)//了不起的杰弗里斯 Zephrys the Great  ID：ULD_003
               )
            {
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_003, new Modifier(500)); //了不起的杰弗里斯 Z二批很容易使theGreat IDULD_003
            }

            //场上有狗头人图书管理员，手上有亡者复生，送掉狗头人管理员
            if (board.HasCardOnBoard(Card.Cards.LOOT_014)//狗头人图书管理员 Kobold Librarian  ID：LOOT_014
               && board.HasCardInHand(Card.Cards.SCH_514)//亡者复生 Raise Dead  ID：SCH_514
                &&  board.HasCardOnBoard(Card.Cards.BT_307)

               )
            {
                p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.LOOT_014, new Modifier(-999)); //狗头人图书管理员 Kobold Librarian  ID：LOOT_014

            }


            //场上有黑眼，增加死缠打自己怪的优先度
            if ( board.HasCardOnBoard(Card.Cards.BT_307)//黑眼
               && board.HasCardInHand(Card.Cards.EX1_302)//死亡缠绕 Mortal Coil  ID：EX1_302
               && board.MinionEnemy.Count == 0
               || board.MinionFriend.Count ==7

               )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_302, new Modifier(-60, Card.Cards.SCH_312)); //巡游向导 Tour Guide  ID：SCH_312死亡缠绕 Mortal Coil  ID：EX1_302
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_302, new Modifier(-60, Card.Cards.BOT_447)); //晶化师 Crystallizer  ID：BOT_447死亡缠绕 Mortal Coil  ID：EX1_302
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_302, new Modifier(-60, Card.Cards.LOOT_014)); //狗头人图书管理员 Kobold Librarian  ID：LOOT_014死亡缠绕 Mortal Coil  ID：EX1_302
               
            }
            
            //场上有黑眼，增加灵魂炸弹打自己怪的优先度
            if (board.HasCardOnBoard(Card.Cards.BT_307)//黑眼
               && board.HasCardInHand(Card.Cards.BOT_222)//灵魂炸弹 Spirit Bomb  ID：BOT_222
               && board.MinionEnemy.Count == 0
               || board.MinionFriend.Count == 7

               )
            {
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_222, new Modifier(-60, Card.Cards.SCH_312)); //巡游向导 Tour Guide  ID：SCH_312灵魂炸弹 Spirit Bomb  ID：BOT_222
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_222, new Modifier(-60, Card.Cards.BOT_447)); //晶化师 Crystallizer  ID：BOT_447灵魂炸弹 Spirit Bomb  ID：BOT_222
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_222, new Modifier(-60, Card.Cards.LOOT_014)); //狗头人图书管理员 Kobold Librarian  ID：LOOT_014灵魂炸弹 Spirit Bomb  ID：BOT_222

            }

            //费用等于2 不使用晶化师 Crystallizer  ID：BOT_447
            if (board.ManaAvailable == 2
               && board.HasCardInHand(Card.Cards.BOT_447)//晶化师 Crystallizer  ID：BOT_447
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BOT_447, new Modifier(500));//晶化师 Crystallizer  ID：BOT_447
            }

            //对面有随从，手上有法术紫水晶 Amethyst Spellstone  ID：LOOT_043t2和铁钩掠夺者 Hooked Reaver  ID：LOOT_018，且血量小于10，降低铁钩 提高法力紫水晶
            if (board.MinionEnemy.Count >=3  //对面有怪在场上
               && board.HasCardInHand(Card.Cards.LOOT_043t2)//法术紫水晶 Amethyst Spellstone  ID：LOOT_043t2
               && board.HasCardInHand(Card.Cards.LOOT_018)//铁钩掠夺者 Hooked Reaver  ID：LOOT_018
               && board.HeroFriend.CurrentHealth <= 10//自己英雄的健康
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_018, new Modifier(350));//铁钩掠夺者 Hooked Reaver  ID：LOOT_018
                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_043t2, new Modifier(-40));//法术紫水晶 Amethyst Spellstone  ID：LOOT_043t2
            }

            //血量小于等于8 不用粗俗的矮劣魔粗俗的矮劣魔 Vulgar Homunculus  ID：LOOT_013
            if ( board.HasCardInHand(Card.Cards.LOOT_013)//粗俗的矮劣魔 Vulgar Homunculus  ID：LOOT_013
               && board.HeroFriend.CurrentHealth <= 8//自己英雄的健康
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_013, new Modifier(350));//粗俗的矮劣魔 Vulgar Homunculus  ID：LOOT_013
            }


            //如果对面场上有深潜炸弹 Depth Charge ID：DRG_078，不下黑眼 Darkglare ID：BT_307
            if (board.MinionEnemy.Any(minion => minion.Template.Id == Card.Cards.DRG_078) //深潜炸弹 Depth Charge ID：DRG_078
               && board.HasCardInHand(Card.Cards.BT_307)//黑眼 Darkglare ID：BT_307
                )
            {

                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(350));//黑眼 Darkglare ID：BT_307
            }

            //如果手上有熔核巨人，降低巨人，提高抽一口
            if (board.HasCardInHand(Card.Cards.EX1_620)//熔核巨人 Molten Giant  ID：EX1_620
                )
            {

                p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_056_H1, new Modifier(5, board.HeroEnemy.Id));
                p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_620, new Modifier(6)); //熔核巨人 Molten Giant  ID：EX1_620
            }



            //武器优先值
            // p.CastWeaponsModifiers.AddOrUpdate(Card.Cards.DRG_025, new Modifier(-80));//海盗之锚 Ancharrr  ID：DRG_025


            //武器攻击保守性
            //  p.WeaponsAttackModifiers.AddOrUpdate(Card.Cards.OG_058, new Modifier(-20));//锈蚀铁钩 Rusty Hook  ID：OG_058

            //法术
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.LOOT_541t, new Modifier(-80));//国王的赎金 King's Ransom  ID：LOOT_541t
            //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_317, new Modifier(-200));//感知恶魔 Sense Demons  ID：EX1_317
            // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_303, new Modifier(200));//暗影烈焰 Shadowflame  ID：EX1_303
            // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_316, new Modifier(200));//力量的代价 Power Overwhelming  ID：EX1_316
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_308, new Modifier(200));//灵魂之火 Soulfire  ID：EX1_308
           // p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_514, new Modifier(-15));//亡者复生 Raise Dead  ID：SCH_514
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BT_300, new Modifier(200));//古尔丹之手 Hand of Gul'dan  ID：BT_300
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.SCH_307, new Modifier(-20));//校园精魂 School Spirits  ID：SCH_307
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.TRL_097, new Modifier(-80));//灵媒术 Seance  ID：TRL_097
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DAL_602, new Modifier(-40));//情势反转 Plot Twist  ID：DAL_602




            //硬币优先级
            //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.TB_011, new Modifier(50));   //硬币

            //直伤法术优先值		
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.BOT_222, new Modifier(200));//灵魂炸弹 Spirit Bomb  ID：BOT_222
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_308, new Modifier(100));//灵魂之火
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_062, new Modifier(300));//地狱烈焰
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.DRG_206, new Modifier(120));//火焰之雨
            //p.CastSpellsModifiers.AddOrUpdate(Card.Cards.EX1_317, new Modifier(-200));//感知恶魔 Sense Demons  ID：EX1_317

            //提高术士的技能生命分流优先级
            p.CastSpellsModifiers.AddOrUpdate(Card.Cards.CS2_056_H1, new Modifier(5, board.HeroEnemy.Id));

            //随从优先值
            //提高坎雷萨德·埃伯洛克优先级
            //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_309, new Modifier(-80));


            //随从优先值

            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_620, new Modifier(20)); //粗俗的矮劣魔 Vulgar Homunculus  ID：LOOT_013
            //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.EX1_620, new Modifier(-25)); //熔核巨人 Molten Giant  ID：EX1_620
           //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(200)); //甩笔侏儒 Pen Flinger  ID：SCH_248
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ICC_700, new Modifier(100)); //开心的食尸鬼 Happy Ghoul  ID：ICC_700
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.LOOT_415, new Modifier(-200)); //首席门徒林恩 Rin, the First Disciple  ID：LOOT_415
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.DRG_209, new Modifier(-20)); //扭曲巨龙泽拉库 Zzeraku the Warped  ID：DRG_209
            //p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ULD_163, new Modifier(-40)); //过期货物专卖商 Expired Merchant ID：ULD_163
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_301, new Modifier(20)); //夜影主母 Nightshade Matron  ID：BT_301
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_140, new Modifier(-20)); //血肉巨人 Flesh Giant  ID：SCH_140
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.ICC_701, new Modifier(-200)); //游荡恶鬼 Skulking Geist  ID：ICC_701
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.BT_309, new Modifier(-40)); //坎雷萨德·埃伯洛克 Kanrethad Ebonlocke  ID：BT_309
            p.CastMinionsModifiers.AddOrUpdate(Card.Cards.SCH_713, new Modifier(-40));//异教低阶牧师 Cult Neophyte  ID：SCH_713
            //不送的怪
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.BT_307, new Modifier(150)); //修饰黑眼 Darkglare  ID：BT_307，数值越高越保守，就是不会拿去交换随从
            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.BT_309, new Modifier(150)); //修饰坎雷萨德·埃伯洛克 Kanrethad Ebonlocke  ID：BT_309
            //p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_248, new Modifier(150)); //修饰甩笔侏儒 Pen Flinger  ID：SCH_248


            //随从打自己
           p.CastMinionsModifiers.AddOrUpdate(Card.Cards.TRL_512, new Modifier(-50, board.HeroFriend.Id));//提高调皮的噬踝者打自己脸优先度调皮的噬踝者 Cheaty Anklebiter  ID：TRL_512


            //降低价值
            //p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.SCH_312, new Modifier(-20)); //巡游向导 Tour Guide  ID：SCH_312

            p.OnBoardFriendlyMinionsValuesModifiers.AddOrUpdate(Card.Cards.ULD_163, new Modifier(-20)); //过期货物专卖商 Expired Merchant ID：ULD_163
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
            //恐鳞 Dreadscale  ID：AT_063t
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.AT_063t, new Modifier(200));
           
            //尼鲁巴蛛网领主 Nerub'ar Weblord  ID：FP1_017
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_017, new Modifier(200));
            //狐人淬毒师 Vulpera Toxinblade  ID：SCH_519
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.SCH_519, new Modifier(200));
            //深潜炸弹 Depth Charge ID：DRG_078
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_078, new Modifier(200));









            //降低攻击优先值
            //降低麦迪文的男仆 Medivh's Valet  ID：KAR_092威胁值
            p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.KAR_092, new Modifier(100));
                //降低资深探险者 Licensed Adventurer ID：YOD_030威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.GVG_051, new Modifier(100));
                //降低恩佐斯的副官 威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.OG_312, new Modifier(100));
                //降低疯狂的科学家 Mad Scientist ID：FP1_004 威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_004, new Modifier(100));
                //降低资深探险者 Licensed Adventurer ID：YOD_030威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.YOD_030, new Modifier(100));
                //降低空灵召唤者威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.FP1_022, new Modifier(100));
                //降低龙裔小鬼威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.DRG_238t12t2, new Modifier(100));
                //降低卡扎库斯 Kazakus  ID：CFM_621威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.CFM_621, new Modifier(100));
                //降低玛克扎尔的小鬼 Malchezaar's Imp  ID：KAR_089威胁值
                p.OnBoardBoardEnemyMinionsModifiers.AddOrUpdate(Card.Cards.KAR_089, new Modifier(100));




           

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
