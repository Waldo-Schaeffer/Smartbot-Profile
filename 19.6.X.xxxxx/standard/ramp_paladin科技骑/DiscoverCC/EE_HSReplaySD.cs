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
        private string _mode = "Wild";
        private Random rnd = new Random();
        private Card.Cards bestChoice;

        public Card.Cards HandlePickDecision(Card.Cards originCard, List<Card.Cards> choices, Board board) //originCard; ID of card played by SB: choices; names of cards for selection: board; 3 states , Even, Losing, Winning
        {
            // Get play mode
            if (IsStandardMode(Bot.CurrentMode())) // retrieve game mode from SB ,Standard, Arena
                _mode = "Standard";
            if (IsArenaMode(Bot.CurrentMode()))
                _mode = "Arena";

            //Get card list
            List<CardValue> choicesCardValue = new List<CardValue>(); // creates an empty list, adding stuff to it afterwards
            _iniTierList0 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + _mode + '\\' + "discover.ini"); // location and name of second preference file. IniManager; load into buffer

            //Search for best points add to choicesCardValue List
            int h;
            int.TryParse(_iniTierList0.GetString("Hero", "FriendClass", "0"), out h);
            foreach (var card in choices) // loops for each card
            {
                var cardTemplate = CardTemplate.LoadFromId(card); //converts cardID to CardName
                double p;
                string HeroClass = h == 1 ? board.FriendClass.ToString() : board.EnemyClass.ToString();// Get friendly/opponent hero class
                double.TryParse(_iniTierList0.GetString(cardTemplate.Name, HeroClass, "0"), out p); // search for card from TierList, retrieve points from opponent hero
                if (p < 0.001) // if points is zero, search for default points
                {
                    double.TryParse(_iniTierList0.GetString(cardTemplate.Name, "points", "0"), out p);
                    HeroClass = "none";
                }
                int c = Array.IndexOf(heroes, HeroClass);
                choicesCardValue.Add(new CardValue(card, p, c, h)); //add card, points, class, hero to each choicesCardValue List
            }

            //Select best card
            double bestPoints = 0;
            for (var i = 0; i < choicesCardValue.Count; i++) //loops through each card
            {
                var cardValue = choicesCardValue[i]; // index through cardValue
                double pts = cardValue.GetPoints(); // calls cardValue subroutine, get points
                int heroIndex = cardValue.GetHeroClass(); // calls cardValue subroutine, get hero class
                bool friend = Convert.ToBoolean(cardValue.GetFriend()); // calls cardValue subroutine, get friend/opponent hero
                string hero = heroIndex != 0 ? friend ? ", f - " + heroes[heroIndex] : ", e - " + heroes[heroIndex] : ""; //preparation to log
                Bot.Log(i + 1 + ") " + CardTemplate.LoadFromId(cardValue.GetCard()).Name + ": " + pts + hero); //output to SB log
                if (!(bestPoints < pts)) continue; // selects highest points
                bestChoice = cardValue.GetCard(); // calls cardValue subroutine, get card assign to bestChoice
                bestPoints = pts; // calls cardValue subroutine, get points assign to bestPoints
            }

            // Out to Bot log
            Bot.Log("============================");
            Bot.Log("Best: " + CardTemplate.LoadFromId(bestChoice).Name + ": " + bestPoints + ": discover.ini, " + _mode + " tier-list");
            Bot.Log("============================");
            return bestChoice; // returns bestChoice of card to SB
        }

        //Get from list
        public class CardValue
        {
            private readonly double _points;
            private readonly int _hero;
            private readonly int _friend;
            private readonly Card.Cards _card;

            public CardValue(Card.Cards card, double points, int hero, int friend)
            {
                _card = card;
                _points = points;
                _hero = hero;
                _friend = friend;
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

            public int GetFriend()
            {
                return _friend;
            }
        }

        // Memory management, input/output operations
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

        private bool IsStandardMode(Bot.Mode mode)
        {
            return mode == Bot.Mode.UnrankedStandard || mode == Bot.Mode.RankedStandard;
        }

        private bool IsArenaMode(Bot.Mode mode)
        {
            return mode == Bot.Mode.Arena || mode == Bot.Mode.ArenaAuto;
        }
    }
}