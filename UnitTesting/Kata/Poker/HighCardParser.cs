using System.Collections.Generic;
using System.Linq;

namespace Kata.Poker
{
    public static class HighCardParser
    {
        public static int GetHighCardNumber(Combination combination, Dictionary<string, List<string>> hand)
        {
            var cardNumbers = GetCardNumbers(hand);
            
            if (combination == Combination.StraightFlush || combination == Combination.FullHouse ||
                combination == Combination.Flush || combination == Combination.RoyalFlush ||
                combination == Combination.Straight || combination == Combination.HighCard)
                return GetLastCard(cardNumbers);
            
            if (combination == Combination.FourOfAKind)
                return GetFourOfAKind(cardNumbers);
            if (combination == Combination.ThreeOfAKind)
                return GetThreeOfAKind(cardNumbers);
            if (combination == Combination.TwoPair)
                return GetTwoPair(cardNumbers);
            if (combination == Combination.OnePair)
                return GetPair(cardNumbers);

            return 0;
        }

        private static List<int> GetCardNumbers(Dictionary<string, List<string>> hand)
        {
            var keyList = hand.Keys.ToList();
            var parsedList = new List<int>();
            foreach (var key in keyList)
            {
                parsedList.Add(int.TryParse(key, out var value) ? value : PokerCardsConst.CardConverted[key]);
            }

            return parsedList.OrderBy(s => s).ToList();
        }

        private static int GetLastCard(List<int> cardNumbers) => cardNumbers.Last();

        private static int GetFourOfAKind(List<int> cardNumbers)
        {
            return cardNumbers.Last(n => cardNumbers.Count(i => i == n) != 4);
        }

        private static int GetThreeOfAKind(List<int> cardNumbers)
        {
            return cardNumbers.Last(n => cardNumbers.Count(i => i == n) != 3);
        }

        private static int GetTwoPair(List<int> cardNumbers)
        {
            return cardNumbers.First(n => cardNumbers.Count(i => i == n) != 2);
        }

        private static int GetPair(List<int> cardNumbers)
        {
            return cardNumbers.Last(n => cardNumbers.Count(i => i == n) != 2);
        }
    }
}