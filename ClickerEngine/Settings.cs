using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public static class Settings
    {
        // If the power difference between two Values are higher than this during addition, the smaller value is treated as it were 0
        public static int PowerDifferenceCeiling { get; set; }
        // Precision of Value Fraction
        public static int FractionPrecision { get; set; }
        // Scientific notation: 3.5*10^3, Non scientific notation: 3.5 Quadchillion
        public static bool ScientificNotation { get; set; }

        static Settings()
        {
            PowerDifferenceCeiling = 4;
            FractionPrecision = 4;
            ScientificNotation = false;
        }
    }
}
