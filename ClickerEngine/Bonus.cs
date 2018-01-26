using ClickerEngine.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public class Bonus
    {
        public Bonus(string name, double value, Value price, BonusType type = BonusType.Additive)
        {
            Name = name;
            Value = value;
            Price = price;
            Type = type;
        }

        public double Value { get; set; }
        public string Name { get; set; }
        public Value Price { get; set; }
        public BonusType Type { get; set; }

        public string TempDisplayName
        {
            get { return $"{Name} ({Value}x multiplier {(Type == BonusType.Additive ? "(A)" : "(M)")}) for {Price}cc"; }
            set { }
        }

        public int Percent
        {
            get
            {
                return (int)Value * 100;
            }
        }

        public static List<Bonus> Bonuses()
        {
            var allBonuses = new List<Bonus>();
            allBonuses.AddRange(AdditiveBonuses());
            allBonuses.AddRange(MultiplicativeBonuses());

            return allBonuses;
        }

        public static List<Bonus> AdditiveBonuses()
        {
            var abonuses = new List<Bonus>();

            abonuses.Add(new Bonus("Flickeree", 1.5, new Value(100, 0)));
            abonuses.Add(new Bonus("Cackaroo", 2, new Value(100, 0)));
            abonuses.Add(new Bonus("Lollipop", 3, new Value(100, 0)));
            abonuses.Add(new Bonus("ZuckerMacher", 1.5, new Value(100, 0)));
            abonuses.Add(new Bonus("Kuckerlay", 2.5, new Value(100, 0)));
            abonuses.Add(new Bonus("Nompadomp", 6.5, new Value(100, 0)));

            return abonuses;
        }

        public static List<Bonus> MultiplicativeBonuses()
        {
            var mbonuses = new List<Bonus>();

            mbonuses.Add(new Bonus("Tosztojka", 1.5, new Value(100, 0), BonusType.Multiplicative));
            mbonuses.Add(new Bonus("Nyompelé", 2, new Value(100, 0), BonusType.Multiplicative));
            mbonuses.Add(new Bonus("Kurdagung", 3, new Value(100, 0), BonusType.Multiplicative));
            mbonuses.Add(new Bonus("Hammpendoszt", 1.5, new Value(100, 0), BonusType.Multiplicative));
            mbonuses.Add(new Bonus("Trompedúr", 2.5, new Value(100, 0), BonusType.Multiplicative));
            mbonuses.Add(new Bonus("Asztakomp", 6.5, new Value(100, 0), BonusType.Multiplicative));

            return mbonuses;
        }
    }
}
