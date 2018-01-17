using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public class Value
    {
        public double Gain { get; set; }
        public int Power { get; set; }

        public Value(double gain, int power)
        {
            Gain = Math.Round(gain, Settings.FractionPrecision);
            Power = power;
        }

        public static Value operator *(Value v1, Value v2)
        {
            var pow = v1.Power + v2.Power;
            var gain = v1.Gain * v2.Gain;

            return new Value(gain, pow);
        }

        public static Value operator +(Value v1, Value v2)
        {
            if(v1.Gain + v2.Gain >= 10000)
            {
                throw new ArgumentException("The gain cannot be larger than 999.9999...");
            }

            if (v1.Power == v2.Power)
            {
                var pow = v1.Power;
                var gain = v1.Gain + v2.Gain;

                if (gain >= 1000)
                {
                    gain = gain / 1000;
                    pow += 3;
                }

                return new Value(gain, pow);
            }
            else if (v1.Power > (v2.Power + Settings.PowerDifferenceCeiling))
            {
                return v1;
            }
            else if (v2.Power > (v1.Power + Settings.PowerDifferenceCeiling))
            {
                return v2;
            }
            else
            {
                if (v1.Power > v2.Power)
                {
                    var pow = v1.Power - v2.Power;
                    var gain = v1.Gain + v2.Gain / Math.Pow(10,pow);

                    if (gain >= 1000)
                    {
                        gain = gain / 1000;
                        pow += 3;
                    }

                    return new Value(gain, v1.Power);
                }
                else if (v2.Power > v1.Power)
                {
                    var pow = v2.Power - v1.Power;
                    var gain = v2.Gain + v1.Gain / Math.Pow(10, pow);

                    if (gain >= 1000)
                    {
                        gain = gain / 1000;
                        pow+=3;
                    }

                    return new Value(gain, v2.Power);
                }
            }

            return null;
        }

        public override string ToString()
        {
            if (Settings.ScientificNotation)
            {
                return $"{Gain}*10^{Power}";
            }
            else
            {
                return $"{Gain} {Power.PowerName()}";
            }
        }
    }
}
