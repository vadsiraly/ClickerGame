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
            Gain = Math.Round(gain, Settings.FractionPrecision);
            Power = power;

            Normalize();
        }

        public static Value operator *(Value v1, Value v2)
        {
            var pow = v1.Power + v2.Power;
            var gain = v1.Gain * v2.Gain;

            return new Value(gain, pow).Normalize();
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
                return v1.Normalize();
            }
            else if (v2.Power > (v1.Power + Settings.PowerDifferenceCeiling))
            {
                return v2.Normalize();
            }
            else
            {
                if (v1.Power > v2.Power)
                {
                    var pow = v1.Power - v2.Power;
                    var gain = v1.Gain + v2.Gain / Math.Pow(10,pow);

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

            while (Gain <= -1000 || Gain >= 1000)
            {
                Gain = Gain / 1000;
                Power += 3;
            }

            while(Gain < 1 && Gain > -1)
            {
                Gain = Gain * 10;
                Power--;
            }

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

        public override string ToString()
        {
            if (Settings.ScientificNotation)
            {
                return $"{Gain}*10^{Power}";
            }
            else
            {
                return $"{Gain} {PowerNamer.GetName(Power)}";
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
