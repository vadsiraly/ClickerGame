using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public class Bonus
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public int Percent
        {
            get
            {
                return Value * 100;
            }
        }
    }
}
