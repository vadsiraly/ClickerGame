using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public static class ExtensionMethods
    {
        public static string PowerName(this int power)
        {
            if (power < 3)
            {
                return string.Empty;
            }
            else if (power < 6)
            {
                return "Thousand";
            }
            else if (power < 9)
            {
                return "Million";
            }
            else if (power < 12)
            {
                return "Billion";
            }
            else if (power < 15)
            {
                return "Trillion";
            }
            else if (power < 18)
            {
                return "Quadrillion";
            }
            else if (power < 21)
            {
                return "Quintillion";
            }
            else if (power < 24)
            {
                return "Sextillion";
            }
            else if (power < 27)
            {
                return "Septillion";
            }
            else if (power < 30)
            {
                return "Octillion";
            }
            else if (power < 33)
            {
                return "Nonillion";
            }
            else if (power < 36)
            {
                return "Decillion";
            }
            else
            {
                return "Fucking huuuge man!";
            }
            /*
            return "Undecillion";
            return "Duodecillion";
            return "Tredecillion";
            return "Quattuordecillion";
            return "Quindecillion";
            return "Sexdecillion";
            return "Septendecillion";
            return "Octodecillion";
            return "Novemdecillion";
            return "Vigintillion";
            return "Unvingintillion";*/
        }
    }
}
