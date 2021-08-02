using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        private const char PlusSign = '+';
        private const char MinusSign = '-';
        private const char WhiteSpace = ' ';
        private const char NullNumber = '0';
        private readonly Dictionary<char, int> _digitsInterpreter = new Dictionary<char, int>
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
            {'9', 9}
        };

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

            stringValue = stringValue.Trim();
            var sign = PlusSign;
            var index = 0;

            if (IsSign(stringValue[index]))
            {
                sign = stringValue[index];
                index++;
            }

            while (index < stringValue.Length && stringValue[index] == NullNumber)
                index++;

            if (index >= stringValue.Length)
                return 0;
           
            var preparedString = stringValue.Substring(index);
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
            if (_digitsInterpreter.ContainsKey(digit))
            {
                return _digitsInterpreter[digit];
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