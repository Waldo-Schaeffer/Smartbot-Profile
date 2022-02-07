using HsReplayDiscoverParser;
using SmartBot.Database;
using SmartBot.Discover;
using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HsReplayDiscover
{
    public class HSReplayDiscover : DiscoverPickHandler
    {

        public Card.Cards HandlePickDecision(Card.Cards originCard, List<Card.Cards> choices, Board board)
        {
            try
            {
                Parser HsReplayParser = new Parser();

                Log(string.Format("Trying to parse HSReplay for {0} in {1} mode ...", CardTemplate.LoadFromId(originCard).Name, Bot.CurrentMode()));

                var parsedResults = HsReplayParser.GetHSReplayResults(Database.Get.GetDbfIdFromCardId(originCard.ToString()), ConvertMode(Bot.CurrentMode()));

                Log("Datas parsed successfully !");

                /*foreach (var res in parsedResults)
                {
                    Log(res.CardId + " - Popularity : " + res.Popularity + " - Winrate :" + res.Winrate);
                }*/

                return FindBestPick(parsedResults, choices);
            }
            catch (Exception e)
            {
                Log(e.ToString());
                return Card.Cards.CRED_01;
            }
        }

        private Card.Cards FindBestPick(List<HSReplayResultEntry> entries, List<Card.Cards> choices)
        {
            Dictionary<Card.Cards, float> values = new Dictionary<Card.Cards, float>();

            foreach (var choice in choices)
            {
                if (!values.ContainsKey(choice))
                {
                    float value = FindCardValue(entries, choice);
                    if (value != 0)
                    {
                        Log(string.Format("Choice : {0} - {1}", CardTemplate.TemplateList[choice].Name,
   value.ToString("F")));
                        values.Add(choice, value);
                    }
                }
            }

            if (values.Count == 0)
            {
                throw new Exception("None of the choices had an entry in HSreplay database, falling back to default logic ...");
            }

            var bestChoice = values.FirstOrDefault(x => x.Value == values.Values.Max()).Key;
            Log(string.Format("Best choice : {0} - {1}", CardTemplate.TemplateList[bestChoice].Name,
                    values[bestChoice].ToString("F")));
            return bestChoice;
        }

        private float FindCardValue(List<HSReplayResultEntry> entries, Card.Cards card)
        {
            HSReplayResultEntry entry = entries.FirstOrDefault(x => x.CardId == card.ToString());

            if (entry == null)
            {
                Log(string.Format("Choice [{0}] doesn't have value in HSReplay datas", CardTemplate.LoadFromId(card).Name));
                return 0;
            }

            return ComputeValue(entry.Popularity, entry.Winrate);

        }

        private float ComputeValue(float popularity, float winrate)
        {
            return popularity * winrate;
        }

        private void Log(string log)
        {
            Bot.Log("[HsReplayDiscover]" + log);
        }

        private Mode ConvertMode(Bot.Mode mode)
        {
            switch (mode)
            {
                case Bot.Mode.Arena:
                case Bot.Mode.ArenaAuto:
                    return Mode.ARENA;

                case Bot.Mode.Standard:
                    return Mode.RANKED_STANDARD;

                case Bot.Mode.Wild:
                    return Mode.RANKED_WILD;

                case Bot.Mode.Classic:
                    return Mode.RANKED_CLASSIC;

                default:
                    return Mode.RANKED_WILD;
            }
        }
    }

}
