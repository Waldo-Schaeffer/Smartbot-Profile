using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SmartBot.Database;
using SmartBot.Plugins.API;
using SmartBotAPI.Plugins.API;

namespace SmartBot.Mulligan
{
    /*
    //Console Mulligan Tester
    class Program
    {
        static void Main(string[] args)
        {
            Universal mulligan = new Universal();
            List<Card.Cards> cardList = new List<Card.Cards>
            {
                //Start Hand
                Cards.CorridorCreeper,
                Cards.FireFly,
                Cards.FireFly,
                //Cards.CorridorCreeper
            };
    
            List<Card.Cards> keeped = mulligan.HandleMulligan(cardList, Card.CClass.DRUID, Card.CClass.DRUID);
            foreach (var card in keeped)
            {
                Console.WriteLine(CardTemplate.LoadFromId(card).Name);
            }
            Console.ReadLine();
        }
    }
    */

    [Serializable]
    public class Universal : MulliganProfile
    {
        //DEBUG
        private const bool BlnUserDebugMode = true;                         // user debug flag
        private const bool BlnDebugMode = false;                            // debug flag


        private const Bot.Mode CurrentDebugMode = Bot.Mode.ArenaAuto;       // bot mode override

        //Mulligan description
        private const string Version = "1.0";                               // current mulligan version
        private const string SBuild = "0029";                               // current build version
        private const string SName = "Mulligan Kit";                        // mulligan name
        private string _sTierListName;                                      // tierlist name
        private string _sTierListVersion;                                   // tierlist version
        private bool _checkAllIds = true;

        //Log variables
        private string _log = "";                                           // log variable
        private static readonly string SDivider = new string('=', 55);      // block divider

        //Mulligan related variables
        private List<Card.Cards> _myDeck;                                   // deck
        private List<Card.Cards> _choices;                                  // cards that were given to choose from
        private readonly List<Card.Cards> _keep = new List<Card.Cards>();   // cards that we are keeping
        private Card.CClass _opponentClass;                                 // opponent game class

        //Mulligan process variables
        private Dictionary<string, int> _comboDic;                          // card combo dictionary

        //Base loader
        private IniManager _iniTierList =                                   // tierlist file
            new IniManager(Directory.GetCurrentDirectory() + @"\MulliganProfiles\Files\SMK_Arena.ini");

        //================================================//
        // Function Keep                                  //
        //                                                //
        // 2 parameters:                                  //
        // 1st parameter: string with keep reason         //
        // 2nd parameter: cards array                     //
        //                                                //
        // Adds all cards from array to _keep variable    //
        // and remove them from _choices if cards exist   //
        // in _choices                                    //
        //================================================//
        private void Keep(string reason, params Card.Cards[] cards)
        {
            var count = true;
            var str = "> Keep: ";
            foreach (var card in cards)
            {
                if (!_choices.Contains(card)) continue;

                var cardTemp = CardTemplate.LoadFromId(card);
                str += cardTemp.Name + ",";
                _choices.Remove(card);
                _keep.Add(card);
                count = false;
            }
            if (count) return;
            str = str.Remove(str.Length - 1);
            if (reason != null)
                str += " because " + reason;
            AddLog(str);
        }

        //================================================//
        // Function Remove                                //
        //                                                //
        // 1 parameter:                                   //
        // 1st parameter: cards array                     //
        //                                                //
        // Removes all cards from array in _keep variable //
        // if they exist in _choices                      //
        //================================================//
        private bool Remove(params Card.Cards[] cards)
        {
            var count = true;
            var str = "> Remove: ";

            foreach (var card in cards)
            {
                if (!_keep.Contains(card)) continue;
                str += CardTemplate.LoadFromId(card).Name + ",";
                _keep.Remove(card);
                _choices.Add(card);
                count = false;
            }

            if (count) return false;
            str = str.Remove(str.Length - 1);
            AddLog(str);

            return true;
        }

        private static Bot.Mode CurrentGameMode()
        {
            Bot.Mode z;
            try
            {
                z = Bot.CurrentMode();
            }
            catch
            {
                z = CurrentDebugMode;
            }

            return z;
        }

        //================================================//
        // Function AddLog                                //
        //                                                //
        // 1 parameter:                                   //
        // 1st parameter: text                            //
        //                                                //
        // Adds text to log variable                      //
        //================================================//
        private void AddLog(string s)
        {
            _log += "\r\n" + s;
        }

        //================================================//
        // Function PrintLog                              //
        //                                                //
        // Prints log to file and to bot                  //
        //================================================//
        private void PrintLog()
        {
            Bot.Log(_log);

            var sDir = Directory.GetCurrentDirectory() + @"\Logs\Soviet\Universal\";

            if (!Directory.Exists(sDir))
                Directory.CreateDirectory(sDir);

            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Logs\Soviet\Universal\" + _sTierListName + "_" + _sTierListVersion + ".log", _log);

            _log = "\r\n---Soviet " + SName + " v" + Version + "---";
        }

        //================================================//
        // Function MulliganInfo                          //
        //                                                //
        // return:                                        //
        // string with mulligan info or null if there is  //
        // no tierlist file                               //
        //                                                //
        // Adds basic info about mulligan to log variable //
        //================================================//
        private string MulliganInfo()
        {
            _sTierListVersion = _iniTierList.GetString("info", "version", "unknown"); // load version
            _sTierListName = _iniTierList.GetString("info", "name", "Basic");         // load name
            
            if (_sTierListName == "Basic")   //return null if there is no tierlist file
            {
                return null;
            }

            //return mulligan info
            var info = SDivider + "\r\nSoviet " + SName + ": " + _sTierListName + "\r\n" + SDivider;

            info += "\r\nCore version: Build" + SBuild;
            info += "\r\nTierList version: Build" + _sTierListVersion;
            info += "\r\n" + SDivider;

            return info;
        }

        private void LoadSettings()
        {
            int tmp;
            int.TryParse(
                _iniTierList.GetString("settings", "check_all_ids", "1"),
                out tmp);
            _checkAllIds = tmp == 1;
        }

        //================================================//
        // Function LogDebug                              //
        //                                                //
        // Adds debug info to log variable                //
        //================================================//
        private void LogDebug()
        {
            AddLog("[SU-DЕBUG] Dir  = " + Directory.GetCurrentDirectory());
            AddLog("[SU-DЕBUG] Mode = " + CurrentGameMode());
            AddLog("[SU-DЕBUG] Time = " + DateTime.Now);
            AddLog(SDivider);
        }

        //================================================//
        // Function GetTokenCount                         //
        //                                                //
        // return:                                        //
        // int with card's minion number                  //
        //                                                //
        // Counts card's tokens (kinda outdated)          //
        //================================================//
        private int GetTokenCount(Card.Cards card)
        {
            var cardTmp = CardTemplate.LoadFromId(card);
            if (_tokens.ContainsKey(card))
                return _tokens[card];

            return cardTmp.Type == Card.CType.MINION ? 1 : 0;
        }
        #region DATA

        //DATA
        private readonly Dictionary<Card.Cards, int> _tokens = new Dictionary<Card.Cards, int>()    //Token count dictionary
        {
            //SUMMON mechanic
            //Druid
            {Cards.MireKeeper, 2},
            {Cards.WitchingHour, 1},
            {Cards.WisperingWoods, 4},
            //Hunter
            {Cards.Alleycat, 2},
            {Cards.KindlyGrandmother, 2},
            {Cards.SnakeTrap, 3},
            {Cards.VenomstrikeTrap, 1},
            {Cards.AnimalCompanion, 1},
            {Cards.RatPack, 2},
            {Cards.UnleashtheHounds, 2}, //?
            {Cards.FlankingStrike, 1},
            {Cards.InfestedWolf, 2},
            {Cards.RatTrap, 1},
            //Mage
            {Cards.MirrorImage, 2},
            {Cards.MirrorEntity, 1},
            //{Cards.VexCrow, 2},
            //Paladin
            {Cards.LostintheJungle, 2},
            {Cards.NobleSacrifice, 1},
            {Cards.LesserPearlSpellstone, 1},
            //Priest
            {Cards.MirageCaller, 2},
            {Cards.EternalServitude, 1},
            {Cards.VividNightmare, 1},
            {Cards.CoffinCrasher, 2},
            //Rogue
            {Cards.DefiasRingleader, 2}, //coin
            {Cards.JadeShuriken, 2}, //coin
            {Cards.JadeSwarmer, 2},
            //Shaman
            {Cards.JadeClaws, 1},
            {Cards.KoboldHermit, 2},
            {Cards.MaelstromPortal, 1},
            {Cards.FeralSpirit, 2}, //hmm 
            {Cards.PrimalfinTotem, 2},
            {Cards.CallintheFinishers, 4},
            //Warlock
            {Cards.PossessedVillager, 2},
            {Cards.CorneredSentry, 4}, //3 for opponent, hmm
            {Cards.Duskbat, 2}, //3 but 2 on average
            {Cards.FiendishCircle, 4},
            //Neutral
            {Cards.BilefinTidehunter, 2},
            {Cards.MurlocTidehunter, 2},
            {Cards.Eggnapper, 3},
            {Cards.HarvestGolem, 2},
            {Cards.ImpMaster, 2},
            {Cards.Moroes, 2},
            {Cards.PantrySpider, 2},
            {Cards.RazorfenHunter, 2},
            {Cards.SewerCrawler, 2},
            {Cards.ShriekingShroom, 2},
            {Cards.Arcanosmith, 2},
            {Cards.Barnes, 2},
            {Cards.CursedDisciple, 2},
            {Cards.DragonlingMechanic, 2},
            {Cards.GrimNecromancer, 3},
            {Cards.InfestedTauren, 2},
            {Cards.JadeSpirit, 2},
            {Cards.MeatWagon, 2},
            {Cards.RattlingRascal, 2},
            {Cards.SaroniteChainGang, 2},
            {Cards.MarshDrake, 2}, //1 for opponent
            {Cards.MuckHunter, 3}, //2 for opponent
            {Cards.SplittingFesteroot, 7},  
            //ADD mechanic
            //Neutral
            {Cards.FireFly, 2},
            {Cards.IgneousElemental, 3},
            {Cards.Ravencaller, 3},
            {Cards.TanglefurMystic, 2}, //3 one for opponent
            //Hunter
            {Cards.JeweledMacaw, 2},
            //Mage
            {Cards.FlameGeyser, 1},
            //Paladin
            {Cards.DrygulchJailor, 4},
        };

        #endregion

        //MAIN

        //TODO: 
        //Fields Has_0Drop, Has_1Drop

        //DECK INFO

        //Race fields: Beast, murloc etc
        public List<Card.Cards> HandleMulligan(List<Card.Cards> choices, Card.CClass opponentClass,
            Card.CClass ownClass)
        {
            _comboDic = new Dictionary<string, int>();

            //Load deck
            try
            {
                _myDeck = Bot.CurrentDeck().Cards.Select(card => (Card.Cards)Enum.Parse(typeof(Card.Cards), card)).ToList();
            }
            catch
            {
                _myDeck = new List<Card.Cards>();
            }

            if (BlnDebugMode)
                _myDeck = new List<Card.Cards>()
                {
                    Cards.ShieldSlam,
                    Cards.ShieldSlam,
                    Card.Cards.BOT_299, //Omega Assembly
                    Card.Cards.BOT_299, //Omega Assembly
                    Card.Cards.BOT_042, //Weapons Project
                    Card.Cards.BOT_042, //Weapons Project
                    Cards.Warpath,
                    Cards.Warpath,
                    Cards.CorneredSentry,
                    Cards.CorneredSentry,
                    Cards.DeadMansHand,
                    Cards.DrywhiskerArmorer,
                    Cards.DrywhiskerArmorer,
                    Cards.Execute,
                    Cards.Execute,
                    Cards.AcolyteofPain,
                    Cards.AcolyteofPain,
                    Cards.BloodRazor,
                    Cards.BloodRazor,
                    Cards.HarrisonJones,
                    Cards.Brawl,
                    Cards.Brawl,
                    Card.Cards.BOT_270, //Giggling Inventor
                    Card.Cards.BOT_270, //Giggling Inventor
                    Card.Cards.BOT_238, //Dr. Boom, Mad Genius
                    Cards.GrommashHellscream,
                    Cards.GeosculptorYip,
                    Cards.ScourgelordGarrosh,
                    Cards.TheLichKing,
                    Card.Cards.BOT_069 //The Boomship
                };

            var currentDeckInfo = new DeckInfo(_myDeck);

            //ARCHETYPE DETECTOR

            var archetypes = new List<Archetype>();
            archetypes.AddRange(Bot.GetArchetypes().Where(archetype => archetype.ArchetypeName().StartsWith("SMK_")));
            var friend = ArchetypeDetector.DetectArchetype(_myDeck, archetypes, 70);
            if (BlnDebugMode)
            {
                var s = "";
                foreach (var card in _myDeck)
                {
                    s += CardTemplate.LoadFromId(card).Name + ", ";
                }
                Bot.Log(s);
            }

            //Choosing ini file
            if (!(Bot.CurrentMode() == Bot.Mode.Arena || Bot.CurrentMode() == Bot.Mode.ArenaAuto))
            {
                AddLog(SDivider);
                if (friend != null)
                {
                    AddLog("Archetype = " + friend.ArchetypeName());
                    var fName = Directory.GetCurrentDirectory() + @"\MulliganProfiles\Files\" + friend.ArchetypeName() +
                                ".ini";
                    if (File.Exists(fName))
                    {
                        _iniTierList = new IniManager(fName);
                    }
                }
                else
                {
                    AddLog("Archetype = NONE");
                }
            }
            var currentFile = _iniTierList;

            //Loading info about mulligan
            var tempString = MulliganInfo();
            LoadSettings();

            if (tempString != null)
            {
                AddLog(tempString);
            }
            else
            {
                AddLog(SDivider);
                AddLog("ERROR!!!");
                AddLog("MULLIGAN IS INSTALLED WRONG");
                AddLog("PLEASE, FOLLOW THE INSTRUCTIONS AT THE TOPIC:");
                AddLog("https://smartbot.ws/index.php?/topic/9020-SMK/&");
                AddLog("Doesn't matter if this is arena mulligan or not.");
                AddLog(SDivider);
                LogDebug();
                PrintLog();
                return _keep;
            }

            //Setting mode combo preferences
            #region Mode preferences

            var tempCount = -1917;
            int.TryParse(
                currentFile.GetString("combo", "count", "-1917"),
                out tempCount);

            if (tempCount != -1917)
            {
                //Bot.Log("Custom curve: ");
                for (var i = 0; i < tempCount; i++)
                {
                    var tempArgs = currentFile.GetString("combo", string.Format("combo_{0}", i), null);
                    if (tempArgs == null) continue;

                    var args = tempArgs.Split('*');
                    if (!_comboDic.ContainsKey(args[0]))
                    {
                        //Bot.Log(String.Format("{0} - {1}", args[0], args[1]));
                        _comboDic.Add(args[0], int.Parse(args[1]));
                    }
                }
            }


            if (_comboDic.Count == 0)
            {
                if (Bot.CurrentMode() == Bot.Mode.ArenaAuto || Bot.CurrentMode() == Bot.Mode.Arena) //ARENA
                {
                    _comboDic = new Dictionary<string, int>()
                    {
                        //coin
                        {"12200", 35},
                        {"12110", 35},
                        {"12100", 20},
                        {"11111", 40},
                        {"11110", 30},
                        {"11102", 35},
                        {"11101", 25},
                        {"11100", 20},
                        {"11021", 35},
                        {"11020", 25},
                        {"11010", 15},
                        {"10211", 35},
                        {"10210", 25},
                        {"10200", 0},
                        {"10110", 10},
                        {"10010", 0},
                        {"10020", 0},
                        {"10030", -45},
                        {"10040", -60},
                        //no coin
                        {"01110", 30},
                        {"01100", 20},
                        {"01010", 15},
                        {"02010", 0},
                        {"00110", 0},
                        {"00030", -45},
                        {"00020", -30},
                        {"00010", 0},
                    };
                }
                else //Ranked/Unranked/Practice
                {
                    _comboDic = new Dictionary<string, int>()
                    {
                        //coin
                        {"12200", 30},
                        {"12100", 0},
                        {"11111", 40},
                        {"11110", 30},
                        {"11102", 35},
                        {"11101", 25},
                        {"11100", 20},
                        {"11021", 35},
                        {"11020", 25},
                        {"11010", 15},
                        {"10211", 35},
                        {"10210", 25},
                        {"10030", -45},
                        {"10040", -60},
                        //no coin
                        {"01110", 30},
                        {"01100", 20},
                        {"01010", 15},
                        {"02100", -10},
                    };
                }
            }

            #endregion

            //Define our globals
            _choices = choices;
            _opponentClass = opponentClass;

            //New class choices
            //var mChoices = new List<Mcard>
            //{
            //    new Mcard(choices[0]),
            //    new Mcard(choices[1]),
            //    new Mcard(choices[2]),
            //};
            //if (choices.Count > 3) mChoices.Add(new Mcard(choices[3]));


            AddLog("Match info: ");
            AddLog("Class: " + ownClass);
            AddLog("Opponent: " + _opponentClass);


            var coin = _choices.Count >= 4;    //Defines the coin
            if (coin)
                Keep(null, Card.Cards.GAME_005);

            AddLog("Coin: " + coin);
            AddLog(SDivider);

            AddLog("Offered:");
            foreach (var card in _choices)
            {
                AddLog("> " + CardTemplate.LoadFromId(card).Name + " (" + card + ")");
            }
            AddLog(SDivider);

            //LOAD ID 

            var iCardPts = new int[_choices.Count];
            var iCardCombo = new int[_choices.Count];
            var iCardInteractionPts = new int[_choices.Count, _choices.Count];

            for (var i = 0; i < choices.Count; i++)
            {
                var card = choices[i];
                var id = card.ToString();
                var totalPoints = 0;

                int.TryParse(_iniTierList.GetString(string.Format("card_{0}", id), "class_ANY", "-50"),
                    out iCardPts[i]);

                int temp;
                int.TryParse(
                    _iniTierList.GetString(string.Format("card_{0}", id), string.Format("class_{0}", ownClass), "-1917"),
                    out temp);

                iCardPts[i] = (temp == -1917) ? iCardPts[i] : temp;

                int.TryParse(
                    _iniTierList.GetString(string.Format("card_{0}", id), "combo", "0"),
                    out iCardCombo[i]);
                totalPoints += iCardCombo[i];

                if (i == 0)
                {
                    int.TryParse(
                    _iniTierList.GetString(string.Format("card_{0}", id), "outcast", "0"),
                    out temp);
                    iCardPts[i] += temp;
                }

                int.TryParse(
                    _iniTierList.GetString(string.Format("card_{0}", id), string.Format("opp_{0}", _opponentClass), "0"),
                    out temp);

                iCardPts[i] += temp;

                if (coin)
                {
                    int.TryParse(_iniTierList.GetString(string.Format("card_{0}", id), "coin", "0"), out temp);
                    iCardPts[i] += temp;
                }

                for (var j = 0; j < choices.Count; j++)
                {
                    if (j == i) continue;
                    var card2 = choices[j];
                    var card2Id = card2.ToString();

                    int.TryParse(
                        _iniTierList.GetString(string.Format("card_{0}", id), string.Format("pCard_{0}", card2),
                            "-99999"), out iCardInteractionPts[i, j]); //set default to -9999 if card2 id not found, reset to 0 later

                    if (_checkAllIds && iCardInteractionPts[i, j] == -99999 && (card2Id.StartsWith("CORE_") || card2Id.StartsWith("VAN_")))
                    {
                        card2Id = card2Id.Substring(id.IndexOf('_') + 1);
                        int.TryParse(
                            _iniTierList.GetString(string.Format("card_{0}", id), string.Format("pCard_{0}", card2Id),
                                "0"), out iCardInteractionPts[i, j]);
                    }

                    if (iCardInteractionPts[i, j] == -99999)
                        iCardInteractionPts[i, j] = 0; //Reset to initial default value

                    totalPoints += iCardInteractionPts[i, j];
                }
                totalPoints += iCardPts[i];


                if (_checkAllIds && totalPoints == -50 && (id.StartsWith("CORE_") || id.StartsWith("VAN_")))
                {
                    id = id.Substring(id.IndexOf('_') + 1);
                    AddLog("No " + card + " in tierlist, checking for " + id);

                    int.TryParse(_iniTierList.GetString(string.Format("card_{0}", id), "class_ANY", "-50"),
                        out iCardPts[i]);

                    int.TryParse(
                        _iniTierList.GetString(string.Format("card_{0}", id), string.Format("class_{0}", ownClass), "-1917"),
                        out temp);

                    iCardPts[i] = (temp == -1917) ? iCardPts[i] : temp;

                    int.TryParse(
                        _iniTierList.GetString(string.Format("card_{0}", id), "combo", "0"),
                        out iCardCombo[i]);

                    if (i == 0)
                    {
                        int.TryParse(
                            _iniTierList.GetString(string.Format("card_{0}", id), "outcast", "0"),
                            out temp);
                        iCardPts[i] += temp;
                    }

                    int.TryParse(
                        _iniTierList.GetString(string.Format("card_{0}", id), string.Format("opp_{0}", _opponentClass), "0"),
                        out temp);

                    iCardPts[i] += temp;

                    if (coin)
                    {
                        int.TryParse(_iniTierList.GetString(string.Format("card_{0}", id), "coin", "0"), out temp);
                        iCardPts[i] += temp;
                    }

                    for (var j = 0; j < choices.Count; j++)
                    {
                        if (j == i) continue;
                        var card2 = choices[j];
                        var card2Id = card2.ToString();

                        int.TryParse(
                            _iniTierList.GetString(string.Format("card_{0}", id), string.Format("pCard_{0}", card2),
                                "-99999"), out iCardInteractionPts[i, j]); //set default to -9999 if card2 id not found, reset to 0 later

                        if (_checkAllIds && iCardInteractionPts[i, j] == -99999 && (card2Id.StartsWith("CORE_") || card2Id.StartsWith("VAN_")))
                        {
                            card2Id = card2Id.Substring(id.IndexOf('_') + 1);
                            int.TryParse(
                                _iniTierList.GetString(string.Format("card_{0}", id), string.Format("pCard_{0}", card2Id),
                                    "0"), out iCardInteractionPts[i, j]);
                        }

                        if (iCardInteractionPts[i, j] == -99999)
                            iCardInteractionPts[i, j] = 0; //Reset to initial default value

                        totalPoints += iCardInteractionPts[i, j];
                    }
                }
            }

            var combinationPts = new int[2, 2, 2, 2];
            var combinationPtsPerCrd = new double[2, 2, 2, 2];
            var combinationCmb = new bool[2, 2, 2, 2];
            var combinationInt = new int[2, 2, 2, 2, 4];
            var combinationCst = new int[2, 2, 2, 2, 6];

            var bestComboVal = "0000";
            var bestComboPts = 0;
            double bestComboPtsPerCard = 0;
            var bestCardCount = 0;

            do
            {
                var i = new int[4];

                for (i[0] = 0; i[0] < 2; i[0]++)
                    for (i[1] = 0; i[1] < 2; i[1]++)
                        for (i[2] = 0; i[2] < 2; i[2]++)
                            for (i[3] = 0; (i[3] < 2 && coin || i[3] < 1 && !coin); i[3]++)
                            {
                                //List<Card.Cards> currentChoices = new List<Card.Cards>();
                                //for (var it = 0; it < choices.Count; it++)
                                //{
                                //    if (i[it] == 1)
                                //    {
                                //        currentChoices.Add(choices[it]);
                                //    }
                                //}

                                for (var it = 0; it < choices.Count; it++)
                                {
                                    //var card = choices[it];

                                    if (i[it] == 0) continue;

                                    combinationPts[i[0], i[1], i[2], i[3]] += iCardPts[it];
                                    combinationInt[i[0], i[1], i[2], i[3], it] += iCardPts[it];

                                    for (var jt = 0; jt < choices.Count; jt++)
                                    {
                                        if ((jt == it) || (i[jt] == 0)) continue;

                                        combinationInt[i[0], i[1], i[2], i[3], jt] += iCardInteractionPts[it, jt];
                                        combinationPts[i[0], i[1], i[2], i[3]] += iCardInteractionPts[it, jt];
                                    }
                                }

                                for (var it = 0; it < choices.Count; it++)
                                {
                                    var card = choices[it];
                                    var cardTmp = CardTemplate.LoadFromId(card);

                                    if (i[it] == 0) continue;

                                    var tmp = cardTmp.Cost;

                                    /* TODO: FIXME: dirty hack */
                                    if (card == Cards.NerubianProphet) tmp = 3;
                                    if (card == Cards.CorridorCreeper) tmp = 3;
                                    if (card == Cards.MoguFleshshaper) tmp = 4;

                                    combinationCst[i[0], i[1], i[2], i[3], Math.Min(5, tmp)]++;
                                }

                                //TODO: MB BETTER TO REWORK COMBO FORMAT
                                var curCombo = string.Format
                                    (
                                        "{0}{1}{2}{3}{4}",
                                        coin ? 1 : 0,
                                        combinationCst[i[0], i[1], i[2], i[3], 1],
                                        combinationCst[i[0], i[1], i[2], i[3], 2],
                                        combinationCst[i[0], i[1], i[2], i[3], 3],
                                        combinationCst[i[0], i[1], i[2], i[3], 4]
                                    );

                                if (_comboDic.ContainsKey(curCombo) && combinationCst[i[0], i[1], i[2], i[3], 5] == 0)
                                {
                                    combinationPts[i[0], i[1], i[2], i[3]] += _comboDic[curCombo];
                                    combinationCmb[i[0], i[1], i[2], i[3]] = true;

                                    for (var j = 0; j < 4; j++)
                                    {
                                        if (i[j] == 1)
                                        {
                                            combinationPts[i[0], i[1], i[2], i[3]] += iCardCombo[j];
                                        }
                                    }
                                }

                                //Extra cards value reduce
                                if (!combinationCmb[i[0], i[1], i[2], i[3]])
                                {
                                    combinationPts[i[0], i[1], i[2], i[3]] -= combinationCst[i[0], i[1], i[2], i[3], 5] * 150;
                                    combinationPts[i[0], i[1], i[2], i[3]] -= combinationCst[i[0], i[1], i[2], i[3], 4] * 100;
                                    combinationPts[i[0], i[1], i[2], i[3]] -= combinationCst[i[0], i[1], i[2], i[3], 3] * 75;

                                    if (!coin && combinationCst[i[0], i[1], i[2], i[3], 2] > 1)
                                        combinationPts[i[0], i[1], i[2], i[3]] -= (combinationCst[i[0], i[1], i[2], i[3], 2] - 1) * 55;
                                    else if (coin && combinationCst[i[0], i[1], i[2], i[3], 2] > 2)
                                    {
                                        combinationPts[i[0], i[1], i[2], i[3]] -= (combinationCst[i[0], i[1], i[2], i[3], 2] - 2) * 55;
                                    }
                                    if (!coin && combinationCst[i[0], i[1], i[2], i[3], 1] > 2)
                                        combinationPts[i[0], i[1], i[2], i[3]] -= (combinationCst[i[0], i[1], i[2], i[3], 1] - 2) * 30;
                                    else if (coin && combinationCst[i[0], i[1], i[2], i[3], 1] > 2)
                                    {
                                        combinationPts[i[0], i[1], i[2], i[3]] -= (combinationCst[i[0], i[1], i[2], i[3], 1] - 2) * 20;
                                    }
                                }
                                if (combinationCst[i[0], i[1], i[2], i[3], 0] > 1)
                                {
                                    combinationPts[i[0], i[1], i[2], i[3]] -= (combinationCst[i[0], i[1], i[2], i[3], 0] - 1) * 20;
                                }

                                /* TODO: FIXME: dirty hack */
                                if ((i[0] == 1 && choices[0] == Cards.CorridorCreeper) ||
                                    (i[1] == 1 && choices[1] == Cards.CorridorCreeper) ||
                                    (i[2] == 1 && choices[2] == Cards.CorridorCreeper) ||
                                    (i[3] == 1 && choices[3] == Cards.CorridorCreeper))
                                {
                                    for (var it = 0; it < choices.Count; it++)
                                    {
                                        if (i[it] == 0) continue;
                                        combinationPts[i[0], i[1], i[2], i[3]] += GetTokenCount(choices[it]) * 10;
                                    }
                                }

                                //weapon penalty
                                var weaponCount = choices.Where((t, it) => i[it] == 1 && CardTemplate.LoadFromId(t).Type == Card.CType.WEAPON).Count();
                                if (weaponCount > 1)
                                {
                                    combinationPts[i[0], i[1], i[2], i[3]] -= (weaponCount - 1) * 30;
                                }

                                var cardCount = i[0] + i[1] + i[2] + i[3];
                                if (i[0] + i[1] + i[2] + i[3] != 0)
                                    combinationPtsPerCrd[i[0], i[1], i[2], i[3]] = (double)combinationPts[i[0], i[1], i[2], i[3]] / cardCount;

                                if (BlnUserDebugMode)
                                {
                                    AddLog(string.Format("Combination: {0}{1}{2}{3} -- Points: {4,5} -- Pts/Card: {5:###.##}", i[0],
                                        i[1], i[2], i[3], combinationPts[i[0], i[1], i[2], i[3]], combinationPtsPerCrd[i[0], i[1], i[2], i[3]]));
                                }

                                if (combinationPts[i[0], i[1], i[2], i[3]] >= cardCount * 100 &&
                                    (bestComboPts < combinationPts[i[0], i[1], i[2], i[3]] ||
                                     bestComboPts == combinationPts[i[0], i[1], i[2], i[3]] &&
                                     cardCount > bestCardCount))
                                {
                                    bestCardCount = cardCount;
                                    bestComboPts = combinationPts[i[0], i[1], i[2], i[3]];
                                    bestComboPtsPerCard = combinationPtsPerCrd[i[0], i[1], i[2], i[3]];
                                    bestComboVal = string.Format("{0}{1}{2}{3}", i[0], i[1], i[2], i[3]);
                                }
                            }
            } while (false);

            AddLog(string.Format("Best Combination: {0} -- Points: {1} -- Pts/Card: {2:###.##}", bestComboVal,
                bestComboPts, bestComboPtsPerCard));
            AddLog(SDivider);

            AddLog("Finally keeping:");

            var lccKeeping = new List<Card.Cards>();
            for (var i = _choices.Count - 1; i >= 0; i--)
            {
                if (bestComboVal[i] == '1')
                    lccKeeping.Add(_choices[i]);
            }

            foreach (var card in lccKeeping)
            {
                Keep(null, card);
            }

            if (lccKeeping.Count == 0) AddLog("Nothing");

            AddLog(SDivider);

            if (BlnUserDebugMode)
                LogDebug();

            //Ending
            PrintLog();

            return _keep;
        }
    }

    //TODO: Card class
    //public class Mcard
    //{
    //    public int CardPts;
    //    public readonly CardTemplate Template;
    //    public readonly Dictionary<Card.Cards, int> SynergyPts;
    //    public readonly Dictionary<Card.CClass, int> Opponnent;
    //    public readonly Dictionary<string, int> Fields;
    //
    //    public Mcard(Card.Cards card, Dictionary<string, int> fields)
    //    {
    //        Fields = fields;
    //        Template = CardTemplate.LoadFromId(card);
    //        SynergyPts = new Dictionary<Card.Cards, int>();
    //        Opponnent = new Dictionary<Card.CClass, int>();
    //    }
    //
    //    protected Mcard(Mcard mcard)
    //    {
    //        Fields = mcard.Fields;
    //        CardPts = mcard.CardPts;
    //        Template = mcard.Template;
    //        SynergyPts = mcard.SynergyPts;
    //        Opponnent = mcard.Opponnent;
    //    }
    //
    //    protected Mcard()
    //    {
    //    }
    //
    //    public virtual Mcard Copy()
    //    {
    //        return new Mcard(this);
    //    }
    //}

    public class DeckInfo
    {
        private readonly List<Card.Cards> _deck;        //deck field

        public DeckInfo(List<Card.Cards> deck)
        {
            _deck = deck;
        }

        public int CountDrops(int mana)
        {
            return _deck.Select(CardTemplate.LoadFromId).Count(templateCard => templateCard.Cost == mana);
        }

        public bool IsKelesethDeck()
        {
            return _deck.Contains(Cards.PrinceKeleseth) && CountDrops(2) == 1;
        }

        public bool IsTaldaramDeck()
        {
            return _deck.Contains(Cards.PrinceTaldaram) && CountDrops(3) == 1;
        }

        public bool IsValanarDeck()
        {
            return _deck.Contains(Cards.PrinceValanar) && CountDrops(4) == 1;
        }

        public bool HasNoDuplicates()
        {
            return _deck.GroupBy(n => n).Any(c => c.Count() > 1);
        }

        public bool HasOnlyEvenCards()
        {
            return _deck.Select(CardTemplate.LoadFromId).All(tmp => tmp.Cost % 2 != 1);
        }

        public bool HasOnlyOddCards()
        {
            return _deck.Select(CardTemplate.LoadFromId).All(tmp => tmp.Cost % 2 != 0);
        }

        public bool HasWeapons()
        {
            return _deck.Select(CardTemplate.LoadFromId).Any(tmpCard => tmpCard.Type == Card.CType.WEAPON);
        }

        public bool HasRushMinions()
        {
            return _deck.Select(CardTemplate.LoadFromId).Any(tmpCard => tmpCard.HasRush);
        }

        public bool HasMinions()
        {
            return _deck.Select(CardTemplate.LoadFromId).Any(tmpCard => tmpCard.Type == Card.CType.MINION);
        }

        public bool HasSpells()
        {
            return _deck.Select(CardTemplate.LoadFromId).Any(tmpCard => tmpCard.Type == Card.CType.SPELL);
        }
    }

    public class IniManager
    {
        private const int CSize = 1024;

        public IniManager(string path)
        {
            Path = path;
        }

        public IniManager()
            : this("")
        {
        }

        public string Path { get; set; }

        public string GetString(string section, string key, string Default = null)
        {
            var buffer = new StringBuilder(CSize);
            GetString(section, key, Default, buffer, CSize, Path);
            return buffer.ToString();
        }

        public void WriteString(string section, string key, string sValue)
        {
            WriteString(section, key, sValue, Path);
        }

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetString(string section, string key, string def, StringBuilder bufer, int size, string path);

        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WriteString(string section, string key, string str, string path);
    }
}