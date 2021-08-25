using System.Collections.Generic;

namespace Kata.Poker
{
    public static class PokerCardsConst
    {
        public static readonly string Ten = "T";
        public static readonly string Jack = "J";
        public static readonly string Queen = "Q";
        public static readonly string King = "K";
        public static readonly string Ace = "A";

        public static readonly Dictionary<string, int> CardConverted = new Dictionary<string, int>
        {
            {Ten, 10},
            {Jack, 11},
            {Queen, 12},
            {King, 13},
            {Ace, 14}
        };
    }
}