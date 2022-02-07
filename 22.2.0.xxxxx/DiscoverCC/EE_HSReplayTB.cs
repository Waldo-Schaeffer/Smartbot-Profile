using SmartBot.Database;
using SmartBot.Discover;
using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace HSReplayDiscover
{
    public class HSReplayDiscover : DiscoverPickHandler
    {
        // Declaration variables
        readonly String[] heroes = { "none", "WARRIOR", "SHAMAN", "ROGUE", "PALADIN", "HUNTER", "DRUID", "WARLOCK", "MAGE", "PRIEST", "DEMONHUNTER" };
        private string _mode, fileName;
        private IniManager _iniTierList0;
        private Card.Cards bestChoice;
        private readonly string Version = "TBV3.1";
        private readonly string Divider = new string('=', 45);
        private readonly string Divider2 = new string('=', 50);

        // Retrieve card details from SB
        public Card.Cards HandlePickDecision(Card.Cards originCard, List<Card.Cards> choices, Board board) //originCard; ID of card played by SB: choices; names of cards for selection: board; 3 states , Even, Losing, Winning
        {
            // Local variables declaration
            int _discoverIndex = 0, _standard = 0, _wild = 0;
            bool ExitLoop = false;
 
            //Create card list
            List<CardValue> choicesCardValue = new List<CardValue>();

            // Search for cards from external files
            do
            {
                // Decide to use standard, wild or discover database
                switch (_discoverIndex)
                {
                    case 0:
                        fileName = CardTemplate.LoadFromId(originCard).Name;
                        _mode = "Standard";
                        break;
                    case 1:
                        _mode = "Wild";
                        break;
                    case 2:
                        if (_standard + _wild > 0)
                        {
                            if (_standard >= _wild)
                            {
                                _mode = "Standard";
                            }
                            else
                            {
                                _mode = "Wild";
                            }
                            ExitLoop = true; //Re-discover card?, Exit while loop add standard or wild cards to list
                        }
                        else
                        {
                            fileName = "discover";
                            _standard = 0;
                            _wild = 0;
                            _mode = "Standard";
                            Bot.Log("Cards not found, using: " + fileName + ".ini");
                        }
                        break;
                    case 3:
                        _mode = "Wild";
                         break;
                    case 4:
                        if (_standard + _wild > 0)
                        {
                            if (_standard >= _wild)
                            {
                                _mode = "Standard";
                            }
                            else
                            {
                                _mode = "Wild";
                            }
                        }
                        ExitLoop = true; //Re-discover card?, Exit while loop add standard or wild cards to list
                        break;
                }
                _iniTierList0 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + _mode + '\\' + fileName + ".ini"); // load HSReplay file into List
                choicesCardValue.Clear();
                double points;
                int heroIndex = 0;
                string HeroClass = board.FriendClass.ToString();// Get friendly hero class

                //Search for best points from external files
                foreach (var card in choices) // loops for each card 
                {
                    var cardTemplate = CardTemplate.LoadFromId(card); //converts cardID to CardName
                    double.TryParse(_iniTierList0.GetString(cardTemplate.Name, HeroClass, "0"), NumberStyles.Any, CultureInfo.InvariantCulture, out points); //search for card from TierList hero class
                    if (points < 0.001) // if points is zero, search for default points
                        double.TryParse(_iniTierList0.GetString(cardTemplate.Name, "points", "0"), NumberStyles.Any, CultureInfo.InvariantCulture, out points); //search for card from TierList default points
                    else
                        heroIndex = Array.IndexOf(heroes, HeroClass);
                    // Add cards found for comparison
                    switch (_mode)
                    {
                        case "Standard":
                            if (points > 0)
                                _standard++;
                            break;
                        case "Wild":
                            if (points > 0)
                                _wild++;
                            break;
                    }
                    if (ExitLoop) // _rediscover set to false, add points, class and hero class to card list
                        choicesCardValue.Add(new CardValue(card, points, heroIndex)); //add points, class to each card
                }
                _discoverIndex++;
            } while (!(ExitLoop || _discoverIndex > 4));

            // Select best card
            double bestPoints = 0;
            for (var i = 0; i < choicesCardValue.Count; i++) //loops through each card
            {
                var cardValue = choicesCardValue[i]; // index through cardValue
                double pts = cardValue.GetPoints(); // calls cardValue subroutine, get points
                int heroIndex = cardValue.GetHeroClass(); // calls cardValue subroutine, get hero class
                string hero = heroIndex != 0 ? ", friend - " + heroes[heroIndex] : ""; //preparation to log
                Bot.Log(i + 1 + ") " + CardTemplate.LoadFromId(cardValue.GetCard()).Name + ": " + pts + hero); // output to SB log
                if (!(bestPoints < pts)) continue; // selects highest points
                bestChoice = cardValue.GetCard(); // calls cardValue subroutine, get card assign to bestChoice
                bestPoints = pts; // calls cardValue subroutine, get points assign to bestPoints
            }

            // Out to Bot log
            Bot.Log(Version + Divider);
            Bot.Log("Best: " + CardTemplate.LoadFromId(bestChoice).Name + ": " + bestPoints + ", " + fileName + ".ini, " + _mode + " tier list");
            Bot.Log(Divider2);
            return bestChoice; // returns bestChoice of card to SB
        }

        //Get from list
        public class CardValue
        {
            private readonly double _points;
            private readonly int _hero;
            private readonly Card.Cards _card;

            public CardValue(Card.Cards card, double points, int hero)
            {
                _card = card;
                _points = points;
                _hero = hero;
            }

            public Card.Cards GetCard()
            {
                return _card;
            }

            public double GetPoints()
            {
                return _points;
            }

            public int GetHeroClass()
            {
                return _hero;
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
                StringBuilder buffer = new StringBuilder(CSize);
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
}