using System.Collections.Generic;
using System.Linq;

namespace Kata
{
    public class PokerHand
    {
        private const string Ten = "10";
        private const string Jack = "T";
        private const string Queen = "Q";
        private const string King = "K";
        private const string Ace = "A";

        public Dictionary<string, List<string>> Hand { get; }

        public PokerHand(string combination)
        {
            Hand = ParseCombination(combination);
        }

        public Result CompareWith(PokerHand other)
        {
            var firstCombination = DetermineCombination(Hand);
            var secondCombination = DetermineCombination(other.Hand);
            if (firstCombination > secondCombination)
                return Result.Win;

            if (secondCombination > firstCombination)
                return Result.Loss;

            return Result.Tie;
        }

        private Dictionary<string, List<string>> ParseCombination(string combination)
        {
            var dictionary = new Dictionary<string, List<string>>();
            var splitedString = combination.Split(" ");
            foreach (var pair in splitedString)
            {
                var value = pair[0].ToString();
                var suit = pair[1].ToString();
                if (!dictionary.TryAdd(value, new List<string> {suit}))
                {
                    dictionary[value].Add(suit);
                }
            }

            return dictionary;
        }

        private Combination DetermineCombination(Dictionary<string, List<string>> combination)
        {
            if (IsRoyalFlush(combination))
                return Combination.RoyalFlush;

            if (IsStraightFlush(combination))
                return Combination.StraightFlush;

            if (IsFourOfKind(combination))
                return Combination.FourOfAKind;

            if (IsFullHouse(combination))
                return Combination.FullHouse;

            if (IsFlush(combination))
                return Combination.Flush;

            if (IsStraight(combination))
                return Combination.Straight;

            if (IsThreeOfAKind(combination))
                return Combination.ThreeOfAKind;

            if (IsTwoPair(combination))
                return Combination.TwoPair;

            if (IsOnePair(combination))
                return Combination.OnePair;

            return Combination.HighCard;
        }

        private bool IsSameSuit(Dictionary<string, List<string>> hand)
        {
            var suit = hand.First().Value.First();
            return hand.Values.SelectMany(i => i).All(s => s.Equals(suit));
        }

        private bool IsSuccessively(Dictionary<string, List<string>> hand)
        {
            var keyList = hand.Keys.ToList();
            var parsedList = new List<int>();
            foreach (var key in keyList)
            {
                if (int.TryParse(key, out int value))
                    parsedList.Add(value);
                else
                {
                    if (key.Equals(Ten))
                        parsedList.Add(10);
                    else if (key.Equals(Jack))
                        parsedList.Add(11);
                    else if (key.Equals(Queen))
                        parsedList.Add(12);
                    else if (key.Equals(King))
                        parsedList.Add(13);
                    else if (key.Equals(Ace))
                        parsedList.Add(14);
                }
            }

            parsedList = parsedList.OrderBy(s => s).ToList();
            for (var i = 0; i < parsedList.Count - 1; i++)
            {
                if (parsedList[i] != parsedList[i + 1] - 1)
                    return false;
            }

            return true;
        }


        private bool IsRoyalFlush(Dictionary<string, List<string>> hand)
        {
            if (!hand.ContainsKey(Ten) && !hand.ContainsKey(Jack) && !hand.ContainsKey(Queen) &&
                !hand.ContainsKey(King) && !hand.ContainsKey(Ace))
                return false;

            return IsSameSuit(hand);
        }

        private bool IsStraightFlush(Dictionary<string, List<string>> hand)
        {
            if (!IsSameSuit(hand) || hand.Count != 5)
                return false;

            return IsSuccessively(hand);
        }

        private bool IsFourOfKind(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 2)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Last().Value.Count;
            return firstCaseCount == 4 || secondCaseCount == 4;
        }

        private bool IsFullHouse(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 2)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Last().Value.Count;
            return firstCaseCount == 3 && secondCaseCount == 2 || firstCaseCount == 2 && secondCaseCount == 3;
        }

        private bool IsFlush(Dictionary<string, List<string>> hand)
        {
            return hand.Count == 5 && IsSameSuit(hand);
        }

        private bool IsStraight(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 5)
                return false;

            return IsSuccessively(hand);
        }

        private bool IsThreeOfAKind(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 3)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Skip(1).First().Value.Count;
            var thirdCaseCount = hand.Last().Value.Count;

            return firstCaseCount == 3 || secondCaseCount == 3 || thirdCaseCount == 3;
        }

        private bool IsTwoPair(Dictionary<string, List<string>> hand)
        {
            if (hand.Count != 3)
                return false;

            var firstCaseCount = hand.First().Value.Count;
            var secondCaseCount = hand.Skip(1).First().Value.Count;
            var thirdCaseCount = hand.Last().Value.Count;

            return firstCaseCount == 2 && secondCaseCount == 2 || firstCaseCount == 2 && thirdCaseCount == 2 ||
                   thirdCaseCount == 2 && secondCaseCount == 2;
        }

        private bool IsOnePair(Dictionary<string, List<string>> hand)
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

    public enum Combination
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public enum Result
    {
        Win,
        Loss,
        Tie
    }
}