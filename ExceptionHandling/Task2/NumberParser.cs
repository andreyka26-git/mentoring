using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        //TODO put signs here
        private char PlusSign = '+';

        public int Parse(string stringValue)
        {
            //TODO change to List<int>
            var parsedDigits = new List<char>();

            if (stringValue == null)
            {
                throw new ArgumentNullException(message: "Input argument is null", paramName: nameof(stringValue));
            }

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException("Input argument is incorrect format");
            }

            var index = 0;
            
            var sign = PlusSign;

            if (IsSign(stringValue[index]))
                sign = stringValue[index];

            while (stringValue[index] == 0)
                index++;

            //TODO check bounds
            var preparedString = stringValue.Substring(index);

            foreach (var digit in preparedString)
            {
                //TODO decouple to constant
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

        //TODO return int instead of nullable
        //TODO in case of not parsed digit throw FormatException
        private int? GetDigit(char digit)
        {
            //TODO get it throug the dictionary
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
            //TODO change to constands
            return value == '-' || value == PlusSign;
        }

        private int ToInt(List<int> parsedDigits, char sign)
        {
            var number = parsedDigits[0];
            if (sign == '-')
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