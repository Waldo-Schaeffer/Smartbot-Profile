using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;

namespace SmartBotAPI.Plugins.API
{

    public class SMK_77yuansusa : Archetype
    {
        public string ArchetypeName()
        {
            return "SMK_77yuansusa";
        }

        public List<Card.Cards> ArchetypeCardSet()
        {
            return new List<Card.Cards>()
            {
				
				        Card.Cards.WC_042,//哀嚎蒸汽 Wailing Vapor ID：WC_042 
                Card.Cards.BAR_854 ,//火光元素 Kindling Elemental ID：BAR_854 
                Card.Cards.WC_005,//原初地下城历险家 Primal Dungeoneer ID：WC_005 
                Card.Cards.BOT_533,//凶恶的雨云 Menacing Nimbus ID：BOT_533 
                Card.Cards.BAR_848,//荷塘潜伏者 Lilypad Lurker ID：BAR_848 
                Card.Cards.BAR_045,//旱地风暴 Arid Stormer ID：BAR_045 
                Card.Cards.DMF_704,//笼斗管理员 Cagematch Custodian ID：DMF_704 
                Card.Cards.DMF_701,//深水炸弹 Dunk Tank ID：DMF_701
                Card.Cards.BAR_750,//大地亡魂 Earth Revenant ID：BAR_750 
                Card.Cards.DMF_062,//旋岩虫 Gyreworm ID：DMF_062 
	            Card.Cards.BT_100,//毒蛇神殿传送门 Serpentshrine Portal ID：BT_100 
                Card.Cards.CS3_031,//生命的缚誓者阿莱克丝塔萨 Alexstrasza the Life-Binder ID：CS3_031 
                Card.Cards.DMF_705,//敲狼锤 Whack-A-Gnoll Hammer ID：DMF_705 
                Card.Cards.EX1_238,//闪电箭 Lightning Bolt ID：EX1_238 
                Card.Cards.VAN_CS2_042,//火元素 Fire Elemental ID：VAN_CS2_042 
                Card.Cards.SCH_507,//导师火心 Instructor Fireheart ID：SCH_507 
                Card.Cards.SW_033,//运河慢步者 Canal Slogger ID：SW_033
                Card.Cards.SW_032,//花岗岩熔铸体 Granite Forgeborn ID：SW_032 
                Card.Cards.VAN_NEW1_010,//风领主奥拉基尔 Al'Akir the Windlord ID：VAN_NEW1_010 
                Card.Cards.SW_025,//拍卖行木槌 Auctionhouse Gavel ID：SW_025 
                Card.Cards.BAR_074,//前沿哨所 Far Watch Post ID：BAR_074 
                Card.Cards.BAR_076,//莫尔杉哨所 Mor'shan Watch Post ID：BAR_076
                Card.Cards.BAR_075,//十字路口哨所 Crossroads Watch Post ID：BAR_075 
                Card.Cards.BAR_077,//卡加尔·战痕 Kargal Battlescar ID：BAR_077 
            };
        }
    }
}