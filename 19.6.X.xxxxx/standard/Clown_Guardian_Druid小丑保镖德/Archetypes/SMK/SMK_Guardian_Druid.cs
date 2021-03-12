using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

//
//NGA精英玩家俱乐部一键生成工具
//https://hs.fbigame.com/
//

//version 0004 for profile 2.0+

/* 
 * 
 * https://github.com/Waldo-Schaeffer
 * https://gitee.com/m586
 * Copyleft 2016 - 2021 SunGuanqi. All Rights Reserved
 * Attribution 4.0 International (Attribution 4.0 International (CC-BY-SA 4.0)
 * 使用时请遵守知识共享署名 4.0 国际许可协议，且不可删除本版权信息
 * 
 */
 
namespace SmartBotAPI.Plugins.API
{

    public class SMK_Guardian_Druid : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_Guardian_Druid";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {  //0
               Card.Cards.EX1_169,//激活 Innervate ID：EX1_169
			   Card.Cards.SCH_427,//雷霆绽放 Lightning Bloom ID：SCH_427
			   //1
			   Card.Cards.DAL_350,//水晶之力 Crystal Power ID：DAL_350
			   Card.Cards.SCH_333,//自然研习 Nature Studies ID：SCH_333
			   //2
			   Card.Cards.DRG_318,//梦境吐息 Breath of Dreams ID：DRG_318
			   Card.Cards.DMF_730,//月触项链 Moontouched Amulet ID：DMF_730
			   //3
			   Card.Cards.YOP_025,//迷梦幼龙 Dreaming Drake ID：YOP_025
			   Card.Cards.CS2_013, //野性成长 Wild Growth ID：CS2_013
			   //4
			   Card.Cards.BT_130,//过度生长 Overgrowth ID：BT_130
			   //5
			   Card.Cards.SCH_616,//夜行虎 Twilight Runner ID：SCH_616
			   Card.Cards.YOP_035,//月牙 Moonfang ID：YOP_035
			   Card.Cards.SCH_605,//止水湖蛇颈龙 Lake Thresher ID：SCH_605
			   Card.Cards.ULD_138,//阿努比萨斯防御者 Anubisath Defender ID：ULD_138
			   //7
			   Card.Cards.DMF_078,//大力士 Strongman ID：DMF_078
			   Card.Cards.ULD_273,//溢流 Overflow ID：ULD_273
			   //8
			   Card.Cards.SCH_610,//动物保镖 Guardian Animals ID：SCH_610
			   //9
			   Card.Cards.DRG_320,//觉醒巨龙伊瑟拉 Ysera, Unleashed ID：DRG_320
			   Card.Cards.DMF_163,//狂欢小丑 Carnival Clown ID：DMF_163
			   //10
			   Card.Cards.SCH_609,//优胜劣汰 Survival of the Fittest ID：SCH_609

            };
        }
    }
}