using System.Collections.Generic;
using Kata.Poker;

namespace Kata
{
    public class PokerHand
    {
        public Dictionary<string, List<string>> Hand { get; }

        public PokerHand(string combination)
        {
            Hand = ParseCombination(combination);
        }

        public Result CompareWith(PokerHand other)
        {
            var firstCombination = DetermineHandResult(Hand);
            var secondCombination = DetermineHandResult(other.Hand);
            
            if (firstCombination.Combination > secondCombination.Combination)
                return Result.Win;

            if (secondCombination.Combination > firstCombination.Combination)
                return Result.Loss;

            if(firstCombination.HighCardNumber > secondCombination.HighCardNumber)
                return Result.Win;

            if (firstCombination.HighCardNumber < secondCombination.HighCardNumber)
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
                if (!dictionary.TryAdd(value, new List<string> { suit }))
                {
                    dictionary[value].Add(suit);
                }
            }

            return dictionary;
        }

        private HandResult DetermineHandResult(Dictionary<string, List<string>> combination)
        {
            var handResult = new HandResult();
            var combinationResult = Combination.HighCard;
           
            if (CombinationParser.IsRoyalFlush(combination))
                combinationResult = Combination.RoyalFlush;
            else if (CombinationParser.IsStraightFlush(combination))
                combinationResult = Combination.StraightFlush;
            else if (CombinationParser.IsFourOfKind(combination))
                combinationResult = Combination.FourOfAKind;
            else if (CombinationParser.IsFullHouse(combination))
                combinationResult = Combination.FullHouse;
            else if (CombinationParser.IsFlush(combination))
                combinationResult = Combination.Flush;
            else if (CombinationParser.IsStraight(combination))
                combinationResult = Combination.Straight;
            else if (CombinationParser.IsThreeOfAKind(combination))
                combinationResult = Combination.ThreeOfAKind;
            else if (CombinationParser.IsTwoPair(combination))
                combinationResult = Combination.TwoPair;
            else if (CombinationParser.IsOnePair(combination))
                combinationResult = Combination.OnePair;

            handResult.Combination = combinationResult;
            handResult.HighCardNumber = HighCardParser.GetHighCardNumber(combinationResult, combination);
            return handResult;
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