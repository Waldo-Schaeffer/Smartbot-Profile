using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_heiyanshu : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_heiyanshu";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
               Card.Cards.SCH_514,//亡者复生 Raise Dead  ID：SCH_514
               Card.Cards.SCH_312,//巡游向导 Tour Guide  ID：SCH_312
               Card.Cards.BOT_447,//晶化师 Crystallizer  ID：BOT_447
               Card.Cards.EX1_302,//死亡缠绕 Mortal Coil  ID：EX1_302
               Card.Cards.BOT_222,//灵魂炸弹 Spirit Bomb  ID：BOT_222
               Card.Cards.EX1_319,//烈焰小鬼 Flame Imp  ID：EX1_319
               Card.Cards.LOOT_014,//狗头人图书管理员 Kobold Librarian  ID：LOOT_014
               Card.Cards.BOT_568,//莫瑞甘的灵界 The Soularium  ID：BOT_568
               Card.Cards.ICC_041,//亵渎 Defile  ID：ICC_041
               Card.Cards.LOOT_013,//粗俗的矮劣魔 Vulgar Homunculus  ID：LOOT_013
               Card.Cards.ULD_163,//过期货物专卖商 Expired Merchant  ID：ULD_163
               Card.Cards.EX1_317,//感知恶魔 Sense Demons  ID：EX1_317
               Card.Cards.BT_307,//黑眼 Darkglare  ID：BT_307
               Card.Cards.LOOT_043,//小型法术紫水晶 Lesser Amethyst Spellstone  ID：LOOT_043
               Card.Cards.FP1_030,//洛欧塞布 Loatheb  ID：FP1_030
               Card.Cards.BT_300,//古尔丹之手 Hand of Gul'dan  ID：BT_300
               Card.Cards.SCH_140,//血肉巨人 Flesh Giant  ID：SCH_140
               Card.Cards.EX1_620,//熔核巨人 Molten Giant  ID：EX1_620
               Card.Cards.EX1_058,//日怒保卫者 Sunfury Protector  ID：EX1_058
               Card.Cards.EX1_303,//暗影烈焰 Shadowflame  ID：EX1_303
               Card.Cards.GVG_107,//强化机器人 Enhance-o Mechano  ID：GVG_107
               Card.Cards.EX1_316,//力量的代价 Power Overwhelming  ID：EX1_316
               Card.Cards.EX1_316,//坎雷萨德·埃伯洛克 Kanrethad Ebonlocke  ID：BT_309
               Card.Cards.EX1_308,//灵魂之火 Soulfire  ID：EX1_308
               Card.Cards.KAR_089,//玛克扎尔的小鬼 Malchezaar's Imp  ID：KAR_089
               Card.Cards.BT_301,//夜影主母 Nightshade Matron  ID：BT_301
               Card.Cards.EX1_310,//末日守卫 Doomguard  ID：EX1_310
               Card.Cards.SCH_158,//恶魔研习 Demonic Studies  ID：SCH_158
               Card.Cards.TRL_249,//残酷集结 Grim Rally  ID：TRL_249
               Card.Cards.ULD_161,//尼斐塞特鞭笞者 Neferset Thrasher  ID：ULD_161
               Card.Cards.TRL_512,//调皮的噬踝者 Cheaty Anklebiter  ID：TRL_512
               Card.Cards.UNG_803,//翡翠掠夺者 Emerald Reaver  ID：UNG_803
               Card.Cards.SCH_311,//活化扫帚 Animated Broomstick  ID：SCH_311
               Card.Cards.DRG_206,//火焰之雨 Rain of Fire  ID：DRG_206
               Card.Cards.EX1_116,//火车王里诺艾 Leeroy Jenkins  ID：EX1_116
               Card.Cards.BOT_548,//奇利亚斯 Zilliax  ID：BOT_548
               Card.Cards.SCH_700,//精魂狱卒 Spirit Jailer  ID：SCH_700
               Card.Cards.EX1_001,//圣光护卫者 Lightwarden  ID：EX1_001
               Card.Cards.DAL_544,//药水商人 Potion Vendor  ID：DAL_544
               Card.Cards.TRL_249,//残酷集结 Grim Rally  ID：TRL_249
               Card.Cards.LOOT_388,//菌菇附魔师 Fungal Enchanter  ID：LOOT_388
               Card.Cards.ICC_700,//开心的食尸鬼 Happy Ghoul  ID：ICC_700
               Card.Cards.GIL_622,//吸血蚊 Lifedrinker  ID：GIL_622
               Card.Cards.ICC_851,//凯雷塞斯王子 Prince Keleseth  ID：ICC_851
               Card.Cards.SCH_248,//甩笔侏儒 Pen Flinger  ID：SCH_248
            };
        }
    }
}