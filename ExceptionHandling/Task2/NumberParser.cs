using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            var parsedDigits = new List<char>();

            if (stringValue == null)
            {
                throw new ArgumentNullException(message: "Input argument is null", paramName: nameof(stringValue));
            }

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException("Input argument is incorrect format");
            }

            var digitsArray = stringValue.ToCharArray();
            var sign = TrimSign(ref digitsArray);
            TrimNullNumbers(ref digitsArray);

            foreach (var digit in digitsArray)
            {
                if (digit == ' ')
                    continue;

                if (GetDigit(digit) != null)
                {
                    parsedDigits.Add(digit);
                }
                else
                {
                    throw new FormatException("Input argument is incorrect format");
                }
            }

            return ToInt(parsedDigits, sign);
        }

        private void TrimNullNumbers(ref char[] digitsArray)
        {
            var firstNumberIndex = digitsArray.ToList().FindIndex(digit => digit != '0');
            if (firstNumberIndex != -1)
            {
                digitsArray = digitsArray.Skip(firstNumberIndex).ToArray();
            }
        }

        private int? GetDigit(char digit)
        {
            var number = 0;
            while (number != 10)
            {
                if (number.ToString() == digit.ToString())
                {
                    return number;
                }

                number++;
            }

            return null;
        }

        private bool IsSign(char value)
        {
            return value == '-' || value == '+';
        }

        private char TrimSign(ref char[] digits)
        {
            char sign = default;
            if (IsSign(digits[0]))
            {
                sign = digits[0];
                digits = digits.Skip(1).ToArray();
            }

            return sign;
        }

        private int ToInt(List<char> parsedDigits, char sign)
        {
            var number = GetDigit(parsedDigits[0]).Value;
            if (sign == '-')
            {
                number *= -1;
            }

            for (var i = 1; i < parsedDigits.Count; i++)
            {
                checked
                {
                    number = number > 0
                        ? number * 10 + GetDigit(parsedDigits[i]).Value
                        : number * 10 - GetDigit(parsedDigits[i]).Value;
                }
            }

            return number;
        }
    }
}