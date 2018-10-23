using ClickerEngine.Enumerations;
using ClickerEngine.PowerNames;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public class Value : IEquatable<Value>
    {
        public double Gain { get; set; }
        public int Power { get; set; }

        public Value(double gain, int power = 0)
        {
            Gain = gain;
            Power = power;

            Normalize();
        }

        public static Value operator *(Value v1, Value v2)
        {
            var pow = v1.Power + v2.Power;
            var gain = v1.Gain * v2.Gain;

            return new Value(gain, pow).Normalize();
        }

        public static Value operator /(Value v1, Value v2)
        {
            var pow = v1.Power - v2.Power;
            var gain = v1.Gain / v2.Gain;

            return new Value(gain, pow).Normalize();
        }

        public int IntValue
        {
            get
            {
                if(Power < 6)
                {
                    return (int)(Gain * Math.Pow(10, Power));
                }

                throw new Exception("Calculating with too big numbers!!!");
            }
        }


        public static Value operator +(Value v1, Value v2)
        {
            v1.Normalize();
            v2.Normalize();

            if (v1.Power == v2.Power)
            {
                var pow = v1.Power;
                var gain = v1.Gain + v2.Gain;

                return new Value(gain, pow).Normalize();
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
                    var gain = v1.Gain + v2.Gain / Math.Pow(10, pow);

                    return new Value(gain, v1.Power).Normalize();
                }
                else if (v2.Power > v1.Power)
                {
                    var pow = v2.Power - v1.Power;
                    var gain = v2.Gain + v1.Gain / Math.Pow(10, pow);

                    return new Value(gain, v2.Power).Normalize();
                }
            }

            return null;
        }

        public Value Normalize()
        {
            if (Gain == 0)
            {
                Power = 0;
                return this;
            }

            var offset = Power % 3;
            if (offset != 0)
            {
                Power += (3 - offset);
                Gain = Gain / Math.Pow(10, (3 - offset));
            }

            // Gain cannot be larger than 1000 or lower than -1000
            while (Gain <= -1000 || Gain >= 1000)
            {
                Gain = Gain / 1000;
                Power += 3;
            }

            /*
            while (((Gain > 1 && Gain < 100) || (Gain < -1 && Gain > -100)) && Power > 0)
            {
                Gain = Gain / 10;
                Power++;
            }
            */

            // Gain cannot be lower than 1 and greater than -1
            while (Gain < 1 && Gain > -1)
            {
                Gain = Gain * 1000;
                Power-=3;
            }

            Gain = Math.Round(Gain, Settings.FractionPrecision);

            return this;
        }

        public static Value operator -(Value v1, Value v2)
        {
            if (v1.Power == v2.Power)
            {
                var pow = v1.Power;
                var gain = v1.Gain - v2.Gain;

                return new Value(gain, pow).Normalize();
            }
            else if (v1.Power > (v2.Power + Settings.PowerDifferenceCeiling))
            {
                return v1.Normalize();
            }
            else if (v2.Power > (v1.Power + Settings.PowerDifferenceCeiling))
            {
                return new Value((-1) * v2.Gain, v2.Power).Normalize();
            }
            else
            {
                if (v1.Power > v2.Power)
                {
                    var difference = v1.Power - v2.Power;

                    var gain = (v1.Gain * Math.Pow(10, difference) - v2.Gain) / Math.Pow(10, difference);

                    return new Value(gain, v1.Power).Normalize();
                }
                else if (v2.Power > v1.Power)
                {
                    var difference = v2.Power - v1.Power;
                    var gain = (v1.Gain - v2.Gain * Math.Pow(10, difference)) / Math.Pow(10, difference);

                    return new Value(gain, v2.Power).Normalize();
                }
            }

            return null;
        }

        public bool IsZero()
        {
            return Gain == 0;
        }

        public override string ToString()
        {
            if (Settings.ScientificNotation)
            {
                return $"{Gain}*10^{Power}";
            }
            else
            {
                var powerName = PowerNamer.GetName(Power);
                if (string.IsNullOrEmpty(powerName))
                {
                    return $"{Gain}";
                }
                else
                {
                    return $"{Gain} {powerName}";
                }
            }
        }

        public string ToString(ValueFormat format)
        {
            if (format == ValueFormat.Scientific)
            {
                if(Power >= 3)
                    return $"{Gain}*10^{Power}";
                else
                    return $"{Gain}";
            }
            if (format == ValueFormat.Literal)
            {
                var powerName = PowerNamer.GetName(Power);
                if (string.IsNullOrEmpty(powerName))
                {
                    return $"{Gain}";
                }
                else
                {
                    return $"{Gain} {powerName}";
                }
            }
            else
            {
                //Impossible to reach, convert to switch ??
                return "";
            }
        }

        public static bool operator ==(Value obj1, Value obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (ReferenceEquals(obj1, null))
            {
                return false;
            }
            if (ReferenceEquals(obj2, null))
            {
                return false;
            }

            return (obj1.Gain == obj2.Gain
                    && obj1.Power == obj2.Power);
        }

        // this is second one '!='
        public static bool operator !=(Value obj1, Value obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Value)obj);
        }

        public bool Equals(Value other)
        {
            if(ReferenceEquals(other, null))
            {
                return false;
            }
            if(ReferenceEquals(this, other))
            {
                return true;
            }

            return Gain == other.Gain
                && Power == other.Power;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Power.GetHashCode();
                hashCode = (hashCode * 397) ^ Gain.GetHashCode();
                return hashCode;
            }
        }
    }
}
