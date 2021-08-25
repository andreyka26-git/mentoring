using System.Collections.Generic;
using System.Linq;

namespace Kata.Poker
{
    public static class CombinationParser
    {
        public static bool IsSameSuit(Dictionary<string, List<string>> hand)
        {
            var suit = hand.First().Value.First();
            return hand.Values.SelectMany(i => i).All(s => s.Equals(suit));
        }

        public static bool IsSuccessively(Dictionary<string, List<string>> hand)
        {
            var keyList = hand.Keys.ToList();
            var parsedList = new List<int>();
            foreach (var key in keyList)
            {
                parsedList.Add(int.TryParse(key, out var value) ? value : PokerCardsConst.CardConverted[key]);
            }

            parsedList = parsedList.OrderBy(s => s).ToList();
            for (var i = 0; i < parsedList.Count - 1; i++)
            {
                if (parsedList[i] != parsedList[i + 1] - 1)
                    return false;
            }

            return true;
        }
        public static bool IsRoyalFlush(Dictionary<string, List<string>> hand)
        {
            if (!hand.ContainsKey(PokerCardsConst.Ten) && !hand.ContainsKey(PokerCardsConst.Jack) &&
                !hand.ContainsKey(PokerCardsConst.Queen) &&
                !hand.ContainsKey(PokerCardsConst.King) && !hand.ContainsKey(PokerCardsConst.Ace))
                return false;

            return IsSameSuit(hand);
        }

        public static bool IsStraightFlush(Dictionary<string, List<string>> hand)
        {
            if (!IsSameSuit(hand) || hand.Count != 5)
                return false;

            return IsSuccessively(hand);
        }

        public static bool IsFourOfKind(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 2)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Last().Value.Count;
            return firstCaseCount == 4 || secondCaseCount == 4;
        }

        public static bool IsFullHouse(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 2)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Last().Value.Count;
            return firstCaseCount == 3 && secondCaseCount == 2 || firstCaseCount == 2 && secondCaseCount == 3;
        }

        public static bool IsFlush(Dictionary<string, List<string>> hand)
        {
            return hand.Count == 5 && IsSameSuit(hand);
        }

        public static bool IsStraight(Dictionary<string, List<string>> hand)
        {
            return hand.Count == 5 && IsSuccessively(hand);
        }

        public static bool IsThreeOfAKind(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 3)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Skip(1).First().Value.Count;
            var thirdCaseCount = hand.Last().Value.Count;

            return firstCaseCount == 3 || secondCaseCount == 3 || thirdCaseCount == 3;
        }

        public static bool IsTwoPair(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 3)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Skip(1).First().Value.Count;
            var thirdCaseCount = hand.Last().Value.Count;

            return firstCaseCount == 2 && secondCaseCount == 2 || firstCaseCount == 2 && thirdCaseCount == 2 ||
                   thirdCaseCount == 2 && secondCaseCount == 2;
        }

        public static bool IsOnePair(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 4)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Skip(1).First().Value.Count;
            var thirdCaseCount = hand.Skip(2).First().Value.Count;
            var fourthCaseCount = hand.Last().Value.Count;

            return firstCaseCount == 2 || secondCaseCount == 2 || thirdCaseCount == 2 || fourthCaseCount == 2;
        }
    }
}