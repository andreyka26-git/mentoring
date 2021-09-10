using System;

namespace Kata
{
    public class BerlinClock
    {
        private const byte FirstRowCellsMax = 4;
        private const byte SecondRowCellsMax = 4;
        private const byte ThirdRowCellsMax = 11;
        private const byte FourthRowCellsMax = 4;

        private const string YellowLight = "Y";
        private const string RedLight = "R";
        private const string LightOff = "O";

        /// <summary>
        /// Convert world time to Berlin representation.
        /// </summary>
        /// <param name="time">Time to convert.</param>
        /// <returns>Parsed Berlin time.</returns>
        public static string Execute(string time)
        {
            var berlinTime = string.Empty;

            var splitedTime = time.Split(":");

            var hours = Convert.ToByte(splitedTime[0]);
            var minutes = Convert.ToByte(splitedTime[1]);
            var seconds = Convert.ToByte(splitedTime[2]);

            // Parse round yellow light
            berlinTime += seconds % 2 == 0 ? YellowLight : LightOff;
            berlinTime += '\n';

            // Parse hours
            var firstRowRedLights = hours / 5;
            AddLights(ref berlinTime, RedLight, firstRowRedLights);
            AddLights(ref berlinTime, LightOff, FirstRowCellsMax - firstRowRedLights);
            berlinTime += '\n';

            var secondRowLights = hours - firstRowRedLights * 5;
            AddLights(ref berlinTime, RedLight, secondRowLights);
            AddLights(ref berlinTime, LightOff, SecondRowCellsMax - secondRowLights);
            berlinTime += '\n';

            // Parse minutes
            var thirdRowLights = minutes / 5;
            for (int i = 1; i <= thirdRowLights; i++)
            {
                if (i % 3 != 0)
                {
                    AddLights(ref berlinTime, YellowLight, 1);
                }
                else
                {
                    AddLights(ref berlinTime, RedLight, 1);
                }
            }
            AddLights(ref berlinTime, LightOff, ThirdRowCellsMax - thirdRowLights);
            berlinTime += '\n';

            var fourthRowRedLights = minutes - thirdRowLights * 5;
            AddLights(ref berlinTime, YellowLight, fourthRowRedLights);
            AddLights(ref berlinTime, LightOff, FourthRowCellsMax - fourthRowRedLights);

            return berlinTime;
        }

        private static void AddLights(ref string str, string light, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                str += light;
            }
        }
    }
}
