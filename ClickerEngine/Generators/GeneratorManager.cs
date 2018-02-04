using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClickerEngine.Generators
{
    public class GeneratorManager : IGeneratorManager
    {
        private ObservableCollectionEx<Generator> _generators;
        private ObservableCollectionEx<Generator> _purchasedGenerators;

        public ObservableCollectionEx<Generator> Generators { get { return _generators; } private set { _generators = value; } }
        public ObservableCollectionEx<Generator> PurchasedGenerators { get { return _purchasedGenerators; } private set { _purchasedGenerators = value; } }

        public GeneratorManager()
        {
            Generators = new ObservableCollectionEx<Generator>();
            PurchasedGenerators = new ObservableCollectionEx<Generator>();

            LoadGenerators();
        }

        private void LoadGenerators()
        {
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
            foreach (var generator in generators)
            {
                string name = generator.Name;
                string desc = generator.Description;
                string thumbnail = generator.Thumbnail;

                Generators.Add(new Generator(name, desc, thumbnail, new Value(1,3)));
            }
        }

        public void PurchaseGenerator(Generator generator)
        {
            if (PurchasedGenerators.Any(x => x == generator))
            {
                generator.PurchasedAmount++;
            }
            else
            {
                generator.PurchasedAmount = 1;
                PurchasedGenerators.Add(generator);
            }
        }

        public void PurchaseGenerators(ObservableCollectionEx<Generator> generators)
        {
            foreach(var gen in generators)
            {
                PurchaseGenerator(gen);
            }
        }

        public void Update(Value currentValue)
        {
            foreach(var gen in Generators)
            {
                gen.Update(currentValue);
            }
        }
    }
}
