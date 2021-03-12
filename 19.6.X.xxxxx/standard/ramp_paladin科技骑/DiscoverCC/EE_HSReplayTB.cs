using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SmartBot.Database;
using SmartBot.Discover;
using SmartBot.Plugins.API;

namespace HSReplayDiscover
{
    public class HSReplayDiscover : DiscoverPickHandler
    {
        // Declaration variables
        readonly String[] heroes = { "none", "WARRIOR", "SHAMAN", "ROGUE", "PALADIN", "HUNTER", "DRUID", "WARLOCK", "MAGE", "PRIEST", "DEMONHUNTER" };
        private IniManager _iniTierList0;
        private IniManager _iniTierList1;
        private Random rnd = new Random();
        private Card.Cards bestChoice;

        // Retrieve card details from SB
        public Card.Cards HandlePickDecision(Card.Cards originCard, List<Card.Cards> choices, Board board) //originCard; ID of card played by SB: choices; names of cards for selection: board; 3 states , Even, Losing, Winning
        {
            int _standard = 0, _wild = 0, _indexDiscoverList = 0;
            bool _wildCards = false;
            string discoverFile = CardTemplate.LoadFromId(originCard).Name; // converts cardID to CardName from CardTemplate

            List<CardValue> choicesCardValue = new List<CardValue>();
            bool _reDiscoverCard;
            do
            {
                if (_indexDiscoverList == 0)
                {
                    _iniTierList0 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + "Standard" + '\\' + discoverFile + ".ini"); // _iniTierList; name and location of data file with extension .ini
                    _iniTierList1 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + "Wild" + '\\' + discoverFile + ".ini"); // location and name of second preference file. IniManager; load into buffer
                }
                else
                {
                    _iniTierList0 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + "Standard" + '\\' + "discover.ini"); // _iniTierList; name and location of data file with extension .ini
                    _iniTierList1 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + "Wild" + '\\' + "discover.ini"); // location and name of second preference 
                }
                choicesCardValue.Clear();

                // Decide to use standard or wild database
                for (int i = 0; i < 3; i++)
                {
                    foreach (var card in choices) // loops for each card 
                    {
                        var cardTemplate = CardTemplate.LoadFromId(card); //converts cardID to CardName
                        double p;
                        string HeroClass = board.FriendClass.ToString();// Get friendly hero class
                        if (!_wildCards) //Standard list
                        {
                            double.TryParse(_iniTierList0.GetString(cardTemplate.Name, HeroClass, "0"), out p); //search for card from TierList hero class
                            if (p < 0.001) // if points is zero, search for default points
                            {
                                double.TryParse(_iniTierList0.GetString(cardTemplate.Name, "points", "0"), out p); //search for card from TierList default points
                                HeroClass = "none";
                            }
                        }
                        else //Wild list
                        {
                            double.TryParse(_iniTierList1.GetString(cardTemplate.Name, HeroClass, "0"), out p); //search for card from TierList hero class
                            if (p < 0.001) // if points is zero, search for default points
                            {
                                double.TryParse(_iniTierList1.GetString(cardTemplate.Name, "points", "0"), out p);  //search for card from TierList default points
                                HeroClass = "none";
                            }
                        }
                        switch (i)
                        {
                            case 0:
                                if (p > 0)
                                    _standard++;
                                break;
                            case 1:
                                if (p > 0)
                                    _wild++;
                                break;
                            default:
                                int c = Array.IndexOf(heroes, HeroClass);
                                choicesCardValue.Add(new CardValue(card, p, c)); //add points, class to each card
                                break;
                        }
                    }
                    _wildCards = (i > 0 && _wild <= _standard) ? false : true; //toggle from false -> true 1st turn, 2nd turn select true/false on bestChoice of cards
                }
                int a = _standard + _wild + _indexDiscoverList; //Break from loop when condition cards found
                if (a > 0)
                {
                    _reDiscoverCard = false; //Re-discover card?, Exit while loop if standard or wild card found or after loop2
                }
                else
                {
                    _reDiscoverCard = true; // if no cards found, repeat loop using cards from "discover.ini", wild and standard tierList
                    _indexDiscoverList++; //index list
                }
            } while (_reDiscoverCard);

            // Select best card
            double bestPoints = 0;
            for (var i = 0; i < choicesCardValue.Count; i++) //loops through each card
            {
                var cardValue = choicesCardValue[i]; // index through cardValue
                double pts = cardValue.GetPoints(); // calls cardValue subroutine, get points
                int heroIndex = cardValue.GetHeroClass(); // calls cardValue subroutine, get hero class
                string hero = heroIndex != 0 ? ", f - " + heroes[heroIndex] : ""; //preparation to log
                Bot.Log(i + 1 + ") " + CardTemplate.LoadFromId(cardValue.GetCard()).Name + ": " + pts + hero); // output to SB log
                if (!(bestPoints < pts)) continue; // selects highest points
                bestChoice = cardValue.GetCard(); // calls cardValue subroutine, get card assign to bestChoice
                bestPoints = pts; // calls cardValue subroutine, get points assign to bestPoints
            }

            // Out to Bot log
            string fileName = _indexDiscoverList == 0 ? ": from " + discoverFile + ", " : ": from discover.ini, "; // retrieve name of file assign to fileName
            string tierList = !_wildCards ? "Standard tier-list" : "Wild tier-list"; // get file directory
            Bot.Log("============================");
            Bot.Log("Best: " + CardTemplate.LoadFromId(bestChoice).Name + ": " + bestPoints + fileName + tierList);
            Bot.Log("============================");
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