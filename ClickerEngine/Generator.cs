using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            var json = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("ClickerEngine.Resources.generators.json"))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
            }

            dynamic generators = JsonConvert.DeserializeObject(json);
            foreach(var generator in generators)
            {
                string name = generator.Name;
                string desc = generator.Description;
                string thumbnail = generator.Thumbnail;

                ret.Add(new Generator(name, desc, thumbnail, new Value(100)));
            }

            return ret;
        }
    }
}
