using System;
using System.Collections.Generic;

namespace Task1
{
    internal class Program
    {
        private static string GetFirstLetter(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Input line is empty", nameof(text));
            }

            return text[0].ToString();
        }

        private static void Main(string[] args)
        {
            const int lineCount = 5;
            var firstLettersResult = new List<string>();
            try
            {
                for (var i = 0; i < lineCount; i++)
                {
                    var text = Console.ReadLine();
                    firstLettersResult.Add(GetFirstLetter(text));
                    Console.ReadLine();
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            firstLettersResult.ForEach(letter => Console.Write($"{letter} "));
        }
    }
}