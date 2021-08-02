using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        //TODO make either const or readonly
        private char PlusSign = '+';
        private char MinusSign = '-';
        private char WhiteSpace = ' ';
        private char NullNumber = '0';

        public int Parse(string stringValue)
        {
            var parsedDigits = new List<int>();

            if (stringValue == null)
            {
                throw new ArgumentNullException(message: "Input argument is null", paramName: nameof(stringValue));
            }

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException("Input argument is incorrect format");
            }

            var sign = PlusSign;
            var preparedString = stringValue;

            //TODO rework to optimized memory algorithm
            if (IsSign(stringValue[0]))
            {
                sign = stringValue[0];
                preparedString = preparedString[1..];
            }

            var indexNumberNotNull = preparedString.ToCharArray().ToList().FindIndex(c => c != NullNumber);
            if (indexNumberNotNull != -1)
            {
                preparedString = preparedString[indexNumberNotNull..];
            }


            foreach (var digit in preparedString)
            {
                if (digit == WhiteSpace)
                    continue;

                parsedDigits.Add(GetDigit(digit));
            }

            return ToInt(parsedDigits, sign);
        }

        private int GetDigit(char digit)
        {
            //TODO remove it to field
            var digitsInterpreter = new Dictionary<char, int>
            {
                {'0', 0},
                {'1', 1},
                {'2', 2},
                {'3', 3},
                {'4', 4},
                {'5', 5},
                {'6', 6},
                {'7', 7},
                {'8', 8},
                {'9', 9},
            };

            if (digitsInterpreter.ContainsKey(digit))
            {
                return digitsInterpreter[digit];
            }

            throw new FormatException("Input argument is incorrect format");
        }

        private bool IsSign(char value)
        {
            return value == MinusSign || value == PlusSign;
        }

        private int ToInt(List<int> parsedDigits, char sign)
        {
            var number = parsedDigits[0];
            if (sign == MinusSign)
            {
                number *= -1;
            }

            for (var i = 1; i < parsedDigits.Count; i++)
            {
                checked
                {
                    number = number > 0
                        ? number * 10 + parsedDigits[i]
                        : number * 10 - parsedDigits[i];
                }
            }

            return number;
        }
    }
}