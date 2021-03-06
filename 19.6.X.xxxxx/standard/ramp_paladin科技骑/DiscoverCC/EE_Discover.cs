﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SmartBot.Database;
using SmartBot.Discover;
using SmartBot.Plugins.API;

namespace EE_Discover
{
    //=================================================//
    // Operation                                       //
    //                                                 //
    // Selects the best card from a discovered card    //
    // 1) Searches for custom file made the public,    //
    //    using the word "Custom" and "Card name"      //
    //    if none found;                               //
    // 2) Searches for card using statistics from      //
    //    "HSReplay Most popular Discover choices"     //
    //    if none found,                               //
    // 3) Searches for best card from a backup file    //
    //    using statistics from "HSReplay" includes    //
    //    all cards                                    //
    //                                                 //
    //=================================================//

    public class EE_Discover : DiscoverPickHandler
    {
        // Declaration variables
        readonly String[] heroes = { "none", "WARRIOR", "SHAMAN", "ROGUE", "PALADIN", "HUNTER", "DRUID", "WARLOCK", "MAGE", "PRIEST", "DEMONHUNTER" };
        private IniManager _iniTierList0;
        private string _mode = "Wild", _discoverFile, FileName;
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
            List<CardValue> choicesCardValue = new List<CardValue>();

            int _indexDiscoverList;
            // Get custom file if exists
            try
            {
                // Only get files that contains with the word "Custom" and bot current deck or discover card name.
                string[] fileNames = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + _mode + '\\', "Custom*.ini");
                foreach (string discoverFile in fileNames)
                {
                    // if (discoverFile.Contains(Bot.CurrentDeck().Name)) // using deck name
                    if (discoverFile.Contains(CardTemplate.LoadFromId(originCard).Name)) //using card name
                        _discoverFile = discoverFile;
                }
                _indexDiscoverList = File.Exists(_discoverFile) ? 0 : 1;
            }
            catch (Exception)
            {
                Bot.Log("[Discover] -> File not found");
                Bot.Log("More information in SmartBot forums");
                Bot.Log("https://smartbot.ws/forums/index.php?/topic/9672-evil_eyes-hsreplay-discover-choices/");
                _indexDiscoverList = 1;
            }

            //Main loop starts here
            bool _reDiscoverCard;
            do
            {
                switch (_indexDiscoverList)
                {
                    case 0:
                        _iniTierList0 = new IniManager(_discoverFile); // load custom file into List
                        FileName = Path.GetFileName(_discoverFile);
                        break;
                    case 1:
                        _iniTierList0 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + _mode + '\\' + CardTemplate.LoadFromId(originCard).Name + ".ini"); // load HSReplay file into List
                        FileName = CardTemplate.LoadFromId(originCard).Name + ".ini";
                        break;
                    case 2:
                        _iniTierList0 = new IniManager(Directory.GetCurrentDirectory() + @"\DiscoverCC\" + _mode + '\\' + "discover.ini"); // load "discover.ini" file into List
                        FileName = "discover.ini";
                        break;
                }
                choicesCardValue.Clear();

                //Search for best points
                int h;
                int.TryParse(_iniTierList0.GetString("Hero", "FriendClass", "0"), out h);
                double TotalPoints = 0;
                foreach (var card in choices) // loops for each card
                {
                    var cardTemplate = CardTemplate.LoadFromId(card); //converts cardID to CardName
                    string HeroClass = h == 1 ? board.FriendClass.ToString() : board.EnemyClass.ToString();// Get friendly/opponent hero class
                    double p;
                    double.TryParse(_iniTierList0.GetString(cardTemplate.Name, HeroClass, "0"), out p); // search for card from TierList
                    if (p < 0.001) // if points is zero, search for default points
                    {
                        double.TryParse(_iniTierList0.GetString(cardTemplate.Name, "points", "0"), out p);
                        HeroClass = "none";
                    }
                    int c = Array.IndexOf(heroes, HeroClass);
                    choicesCardValue.Add(new CardValue(card, p, c, h)); //add points, class, hero to each card
                    TotalPoints += p; // Adding points, check if points is not = zero
                }
                if (TotalPoints > 0 || _indexDiscoverList > 1) //Break from loop if points > 0 or on 3rd loop
                    _reDiscoverCard = false;
                else
                {
                    _reDiscoverCard = true; // no points found, re-loop using other tier lists
                    _indexDiscoverList++;
                }
            } while (_reDiscoverCard);

            // Select best card
            double bestPoints = 0;
            for (var i = 0; i < choicesCardValue.Count; i++) //loops through each card
            {
                var cardValue = choicesCardValue[i]; // index through cardValue
                double pts = cardValue.GetPoints(); // calls cardValue subroutine, get points
                int heroIndex = cardValue.GetHeroClass(); // calls cardValue subroutine, get hero class
                bool friend = Convert.ToBoolean(cardValue.GetFriend()); // calls cardValue subroutine, get friend/opponent hero
                string hero = heroIndex != 0 ? friend ? ", f - " + heroes[heroIndex] : ", e - " + heroes[heroIndex] : ""; //preparation to log
                Bot.Log(i + 1 + ") " + CardTemplate.LoadFromId(cardValue.GetCard()).Name + ": " + pts + hero); // output to SB log
                if (!(bestPoints < pts)) continue; // selects highest points
                bestChoice = cardValue.GetCard(); // calls cardValue subroutine, get card assign to bestChoice
                bestPoints = pts; // calls cardValue subroutine, get points assign to bestPoints
            }

            // Out to Bot log
            Bot.Log("============================");
            Bot.Log("Best: " + CardTemplate.LoadFromId(bestChoice).Name + ": " + bestPoints + ": " + FileName + ", " + _mode + " tier-list");
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
