using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickerEngine
{
    public class Generator
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }

        public int AvailableAmount { get; set; }
        public int PurchasedAmount { get; set; }

        public Value Price { get; set; }

        public Value ValuePerSecond { get; set; }
        public Value ValuePerClick { get; set; }
        public List<Bonus> AvailableBonuses { get; set; }

        public Generator(string name, string description, string thumbnail, Value initialPrice)
        {
            Name = name;
            Description = description;
            Thumbnail = thumbnail;
            Price = initialPrice;

            AvailableAmount = 0;
            PurchasedAmount = 0;
        }

        public void Update(Value currentValue)
        {

        }

        public static List<Generator> Generators()
        {
            var ret = new List<Generator>();

            foreach(var i in Enumerable.Range(0,50))
                ret.Add(new Generator("Hyper turbulator", "This appliance helps in improving the turbulation.", "icon_settings.png", new Value(100)));

            return ret;
        }
    }
}
