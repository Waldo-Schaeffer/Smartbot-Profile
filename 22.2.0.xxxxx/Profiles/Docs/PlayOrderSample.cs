using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins.API;
using SmartBotAPI.Plugins.API;



namespace SmartBotProfiles
{
    [Serializable]
    public class PlayOrderPriority : Profile
    {
		public ProfileParameters GetParameters(Board board)
        {
            var p = new ProfileParameters(BaseProfile.Rush) {DiscoverSimulationValueThresholdPercent = -10};			  
			
			/* In the seed below, Life tap is played first, then Shattered Sun Cleric (VAN_EX1_019)
			   The following rule increases the play order priority of Shattered Sun Cleric to be played first
			*/
			p.PlayOrderModifiers.AddOrUpdate(Card.Cards.VAN_EX1_019, new Modifier(500));
			
			/* Most cards have the default value "1" in the AI
			   The bot will try to play cards with higher value first, so here we put VAN_EX1_019 value to 500 to make sure it's higher than anything else hardcoded in the base profiles
		    */
			
			//You can also make a card to be played last using negative values : 
			//p.PlayOrderModifiers.AddOrUpdate(Card.Cards.VAN_EX1_019, new Modifier(-500));
			
            return p;
        }
        
        public Card.Cards SirFinleyChoice(List<Card.Cards> choices)
        {
            return choices[0];
        }

        public Card.Cards KazakusChoice(List<Card.Cards> choices)
        {
            return choices[0];
        }
    }
}

/* SEED : 

10~10~2~0~0~12~16~0~False~10~False~5~0~0~HERO_07a*-1*0*0*0*13*0*66*30*0*20*17*0*False*False*False*False*False*False*False*False*False*False*False*False*False*20*False*True*False*1*False*False*False*False*False*False*False*50=2&202=3&45=30&12=1&49=1&53=66&203=4&380=47788&1556=1&271=20&44=13&1715=28&36=0&318=0&1173=0&18=67~HERO_01*-1*5*0*0*7*0*64*30*0*20*23*0*False*False*False*False*True*False*False*False*False*False*False*False*False*20*False*True*False*0*False*False*False*False*False*False*False*50=1&202=3&45=30&12=1&49=1&53=64&201=3&203=2&380=725&1556=1&271=20&479=0&47=0&267=44&43=0&44=7&1715=62&38=0&297=0&318=0&1173=0&18=62&36=0&292=5~CS2_056_H1*-1*0*0*2*0*0*67*0*0*20*0*0*False*False*False*False*False*False*False*False*False*False*False*False*False*20*False*True*False*1*False*False*False*False*False*False*False*50=2&202=10&466=2&48=2&12=1&49=1&53=67&201=3&313=66&1037=2&1086=51468&1284=47817&1556=1&271=20&43=0~HERO_01bp*-1*0*0*2*0*0*65*0*0*20*0*0*False*False*False*False*True*False*False*False*False*False*False*False*False*20*False*True*False*0*False*False*False*False*False*False*False*50=1&202=10&466=2&48=2&12=1&49=1&53=65&201=3&203=2&313=64&1037=1&1086=2745&1284=7&1556=1&271=20&43=0~VAN_EX1_310*0*0*5*5*0*0*62*7*0*2*7*0*True*False*False*False*False*False*False*False*False*False*False*False*False*2*False*True*False*1*False*False*False*False*False*False*False*49=1&50=2&53=62&202=4&466=5&479=5&48=5&47=5&45=7&197=1&200=15&201=3&203=3&218=1&478=2&858=631&890=2&1043=1&1068=0&1413=631&1556=1&263=1&1570=18&1037=0&44=0&1380=1&261=0&1196=0&267=64&43=0&1715=62&38=0&297=0&271=2|VAN_EX1_405*1*0*0*1*0*0*60*4*0*4*4*0*False*False*True*False*False*False*False*False*False*False*False*False*False*4*False*True*False*2*False*False*False*False*False*False*False*49=1&50=2&53=60&202=4&466=1&48=1&45=4&190=1&201=3&203=1&478=2&858=866&1037=0&1043=1&1068=0&1413=866&1556=1&263=2&1570=16&261=0&1196=0&43=0&271=4~VAN_EX1_572*0*0*4*9*0*0*13*12*0*1*12*0*False*False*False*False*True*False*False*False*False*False*False*False*False*1*False*True*False*0*False*False*False*False*False*False*False*49=1&50=1&53=13&263=1&202=4&466=9&479=4&48=9&47=4&45=12&12=1&32=1&114=1&200=24&201=3&203=5&261=0&273=5&478=1&858=1186&1037=0&1043=1&1068=0&1413=1186&1556=1&1570=9&1196=0&43=1&271=1~VAN_EX1_046*0*0*4*4*0*0*47*4*0*0*4*0*False*False*False*False*False*False*True*False*False*False*False*False*False*0*False*True*False*0*False*False*False*False*False*False*False*49=3&50=2&53=47&202=4&466=4&479=4&48=4&47=4&45=4&201=2&203=1&218=1&478=2&858=348&1037=2&1043=1&1068=0&1413=348&1556=1&263=1&1570=18&273=1|VAN_EX1_019*1*0*3*3*0*0*39*2*0*0*2*0*False*False*False*False*False*False*True*False*False*False*False*False*False*0*False*True*False*0*False*False*False*False*False*False*False*49=3&50=2&53=39&202=4&466=3&479=3&48=3&47=3&45=2&12=1&201=3&203=2&218=1&478=2&858=608&1037=2&1043=1&1068=0&1413=608&1556=1&263=2&1570=20~0~VAN_EX1_004|VAN_CS2_065|VAN_CS2_065|VAN_EX1_008|VAN_CS2_203|VAN_EX1_393|VAN_EX1_162|VAN_EX1_004|VAN_CS2_189|VAN_EX1_093|DMF_COIN2|VAN_EX1_308|VAN_EX1_008|VAN_EX1_405|VAN_NEW1_019~0~VAN_EX1_603|VAN_CS2_106|VAN_EX1_391|VAN_EX1_400|VAN_EX1_402|VAN_EX1_410|VAN_CS2_179|VAN_CS2_179|VAN_EX1_407~0~True=True=True=True=True=False~0~0~0~False~VAN_EX1_308,VAN_EX1_308,VAN_CS2_188,VAN_CS2_188,VAN_EX1_008,VAN_EX1_008,VAN_CS2_189,VAN_CS2_189,VAN_EX1_319,VAN_EX1_319,VAN_EX1_405,VAN_EX1_405,VAN_CS2_065,VAN_CS2_065,VAN_EX1_004,VAN_EX1_004,VAN_EX1_393,VAN_EX1_162,VAN_EX1_162,VAN_CS2_203,VAN_NEW1_019,VAN_NEW1_019,VAN_EX1_019,VAN_EX1_019,VAN_EX1_046,VAN_EX1_046,VAN_EX1_093,VAN_EX1_093,VAN_EX1_310,VAN_EX1_310~0~0~0~0~0~False~False~10~False~0~0~0~0~0~0~0~0~0~0~False~0~False~0~0~8~0~4~0~30=2&50=2&202=2&27=66&28=10&29=4&31=2&49=1&53=3&176=10&1556=1&17=1&467=0&272=1&7=75&305=4&271=20&1292=1&23=1&26=10&399=1&995=14&25=0&418=34&269=0&317=0&397=62&266=0&358=0&368=0&398=0&412=13&464=0&1575=0&417=0&1025=8&1573=4&406=0&1739=0&394=4&430=0&1780=1~30=1&50=1&202=2&27=64&28=10&29=4&31=1&49=1&53=2&176=10&1556=1&17=1&23=0&24=1&467=0&272=1&7=75&305=4&271=20&26=10&399=1&995=11&1292=0&25=9&418=40&269=1&317=1&397=13&266=0&358=1&334=0&464=0&1575=0&1573=2&417=0&368=0&398=0&412=4&430=0&1780=4&406=0&1739=0&394=4~0~0~=1=PLAY=False|=2=PLAY=False|=3=PLAY=True|=4=DECK=False|VAN_CS2_106=5=GRAVEYARD=False|VAN_CS2_179=6=GRAVEYARD=False|=7=DECK=False|=8=DECK=False|=9=HAND=False|=10=DECK=False|VAN_EX1_603=11=GRAVEYARD=False|VAN_EX1_410=12=GRAVEYARD=False|VAN_EX1_572=13=PLAY=False|VAN_EX1_391=14=GRAVEYARD=False|=15=DECK=False|=16=HAND=False|=17=HAND=False|=18=DECK=False|=19=DECK=False|=20=DECK=False|VAN_EX1_402=21=GRAVEYARD=False|=22=DECK=False|=23=DECK=False|VAN_EX1_407=24=GRAVEYARD=False|=25=DECK=False|=26=DECK=False|=27=DECK=False|VAN_CS2_179=28=GRAVEYARD=False|VAN_EX1_400=29=GRAVEYARD=False|=30=HAND=False|=31=DECK=False|=32=DECK=False|=33=DECK=False|VAN_EX1_008=34=GRAVEYARD=True|=35=DECK=True|=36=DECK=True|VAN_EX1_162=37=GRAVEYARD=True|=38=DECK=True|VAN_EX1_019=39=HAND=True|VAN_EX1_393=40=GRAVEYARD=True|VAN_EX1_008=41=GRAVEYARD=True|VAN_CS2_203=42=GRAVEYARD=True|VAN_EX1_093=43=GRAVEYARD=True|VAN_CS2_065=44=GRAVEYARD=True|VAN_CS2_065=45=GRAVEYARD=True|=46=DECK=True|VAN_EX1_046=47=HAND=True|=48=DECK=True|=49=DECK=True|VAN_CS2_189=50=GRAVEYARD=True|VAN_EX1_004=51=GRAVEYARD=True|=52=DECK=True|=53=DECK=True|VAN_EX1_308=54=GRAVEYARD=True|VAN_EX1_004=55=GRAVEYARD=True|=56=DECK=True|=57=DECK=True|VAN_NEW1_019=58=GRAVEYARD=True|=59=DECK=True|VAN_EX1_405=60=PLAY=True|VAN_EX1_405=61=GRAVEYARD=True|VAN_EX1_310=62=PLAY=True|=63=DECK=True|HERO_01=64=PLAY=False|HERO_01bp=65=PLAY=False|HERO_07a=66=PLAY=True|CS2_056_H1=67=PLAY=True|DMF_COIN2=68=GRAVEYARD=True|=79=REMOVEDFROMGAME=False|=84=REMOVEDFROMGAME=True|=86=REMOVEDFROMGAME=True|=89=REMOVEDFROMGAME=True|=92=REMOVEDFROMGAME=True|=100=REMOVEDFROMGAME=True|=101=REMOVEDFROMGAME=True|=110=HAND=False|~

*/


