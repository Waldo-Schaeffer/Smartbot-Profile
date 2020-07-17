using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_WildEvenShaman : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Wild_Even_Shaman";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
               Card.Cards.EX1_244,//图腾之力 Totemic Might  ID：EX1_244
               Card.Cards.ULD_171,//图腾潮涌 Totemic Surge  ID：ULD_171
               Card.Cards.ICC_056,//低温静滞 Cryostasis  ID：ICC_056
               Card.Cards.AT_053,//先祖知识 Ancestral Knowledge  ID：AT_053
               Card.Cards.GIL_586,//大地之力 Earthen Might  ID：GIL_586
               Card.Cards.AT_052,//图腾魔像 Totem Golem  ID：AT_052
               Card.Cards.KAR_073,//大漩涡传送门 Maelstrom Portal  ID：KAR_073
               Card.Cards.ULD_276,//怪盗图腾 EVIL Totem  ID：ULD_276
               Card.Cards.CFM_696,//衰变 Devolve  ID：CFM_696
               Card.Cards.GVG_038,//连环爆裂 Crackle  ID：GVG_038
               Card.Cards.GIL_530,//阴燃电鳗 Murkspark Eel  ID：GIL_530
               Card.Cards.ULD_413,//分裂战斧 Splitting Axe  ID：ULD_413
               Card.Cards.AT_047,//德莱尼图腾师 Draenei Totemcarver  ID：AT_047
               Card.Cards.BT_737,//玛维·影歌 Maiev Shadowsong  ID：BT_737
               Card.Cards.GIL_692,//吉恩·格雷迈恩 Genn Greymane  ID：GIL_692
               Card.Cards.OG_028,//深渊魔物 Thing from Below  ID：OG_028
               Card.Cards.BOT_245,//风暴聚合器 The Storm Bringer  ID：BOT_245
               Card.Cards.BT_230,//鱼斯拉 The Lurker Below  ID：BT_230
               Card.Cards.ULD_177,//八爪巨怪 Octosari  ID：ULD_177
               Card.Cards.EX1_586,//海巨人 Sea Giant  ID：EX1_586
               Card.Cards.DRG_071,//厄运信天翁 Bad Luck Albatross ID：DRG_071
                Card.Cards.ULD_173,//维西纳 Vessina  ID：ULD_173
                 Card.Cards.OG_254,//奥秘吞噬者 Eater of Secrets  ID：OG_254
                   Card.Cards.ICC_701,//游荡恶鬼 Skulking Geist  ID：ICC_701
            };
        }
    }
}