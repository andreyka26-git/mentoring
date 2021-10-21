using System;

namespace Snippet2
{
    public class Parrot
    {
        private const double LoadFactorConst = 9.0;
        private const double BaseSpeedConst = 12.0;

        private readonly ParrotTypeEnum _type;
        private readonly int _numberOfCoconuts;
        private readonly double _voltage;
        private readonly bool _isNailed;

        public Parrot(ParrotTypeEnum type, int numberOfCoconuts, double voltage, bool isNailed)
        {
            _type = type;
            _numberOfCoconuts = numberOfCoconuts;
            _voltage = voltage;
            _isNailed = isNailed;
        }

        public double GetSpeed()
        {
            return _type switch
            {
                ParrotTypeEnum.European => BaseSpeedConst,
                ParrotTypeEnum.African => Math.Max(0, BaseSpeedConst - LoadFactorConst * _numberOfCoconuts),
                ParrotTypeEnum.NorwegianBlue => _isNailed ? 0 : GetBaseSpeed(_voltage),
                _ => throw new Exception("Should be unreachable")
            };
        }

        private static double GetBaseSpeed(double voltage) => Math.Min(24.0, voltage * BaseSpeedConst);
    }
}
