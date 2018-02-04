using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ClickerEngine.Generators
{
    public interface IGeneratorManager
    {
        ObservableCollectionEx<Generator> Generators { get; }
        ObservableCollectionEx<Generator> PurchasedGenerators { get; }

        void PurchaseGenerator(Generator generator);
        void PurchaseGenerators(ObservableCollectionEx<Generator> generators);
        void Update(Value currentValue);
    }
}
