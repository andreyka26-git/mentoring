using System;

namespace Snippet2
{
    public class Parrot
    {
        private const double LoadFactorConst = 9.0;
        private const double BaseSpeedConst = 12.0;

        private readonly ParrotTypeEnum _mType;
        private readonly int _mNumberOfCoconuts;
        private readonly double _mVoltage;
        private readonly bool _mIsNailed;

        public Parrot(ParrotTypeEnum type, int numberOfCoconuts, double voltage, bool isNailed)
        {
            _mType = type;
            _mNumberOfCoconuts = numberOfCoconuts;
            _mVoltage = voltage;
            _mIsNailed = isNailed;
        }

        public double GetSpeed()
        {
            return _mType switch
            {
                ParrotTypeEnum.European => BaseSpeedConst,
                ParrotTypeEnum.African => Math.Max(0, BaseSpeedConst - LoadFactorConst * _mNumberOfCoconuts),
                ParrotTypeEnum.NorwegianBlue => _mIsNailed ? 0 : GetBaseSpeed(_mVoltage),
                _ => throw new Exception("Should be unreachable")
            };
        }

        private static double GetBaseSpeed(double voltage) => Math.Min(24.0, voltage * BaseSpeedConst);
    }
}
