using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public class Bonus
    {
        public Bonus(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public double Value { get; set; }
        public string Name { get; set; }

        public int Percent
        {
            get
            {
                return (int)Value * 100;
            }
        }

        public static List<Bonus> AdditiveBonuses()
        {
            var abonuses = new List<Bonus>();

            abonuses.Add(new Bonus("Flickeree", 1.5));
            abonuses.Add(new Bonus("Cackaroo", 2));
            abonuses.Add(new Bonus("Lollipop", 3));
            abonuses.Add(new Bonus("ZuckerMacher", 1.5));
            abonuses.Add(new Bonus("Kuckerlay", 2.5));
            abonuses.Add(new Bonus("Nompadomp", 6.5));

            return abonuses;
        }

        public static List<Bonus> MultiplicativeBonuses()
        {
            var mbonuses = new List<Bonus>();

            mbonuses.Add(new Bonus("Tosztojka", 1.5));
            mbonuses.Add(new Bonus("Nyompelé", 2));
            mbonuses.Add(new Bonus("Kurdagung", 3));
            mbonuses.Add(new Bonus("Hammpendoszt", 1.5));
            mbonuses.Add(new Bonus("Trompedúr", 2.5));
            mbonuses.Add(new Bonus("Asztakomp", 6.5));

            return mbonuses;
        }
    }
}
