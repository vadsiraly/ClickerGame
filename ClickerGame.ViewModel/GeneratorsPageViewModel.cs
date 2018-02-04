using ClickerEngine;
using ClickerEngine.Generators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ClickerGame.ViewModel
{
    public class GeneratorsPageViewModel : BaseViewModel
    {
        private Engine engine;

        public ObservableCollectionEx<Generator> Generators { get { return engine.GeneratorManager.Generators; } }
        public ObservableCollectionEx<Generator> PurchasedGenerators { get { return engine.GeneratorManager.PurchasedGenerators; } }

        public ICommand PurchaseGeneratorCommand { get; set; }

        public GeneratorsPageViewModel(Engine engine)
        {
            this.engine = engine;
            PurchaseGeneratorCommand = new Command(PurchaseGenerator);
        }

        private void PurchaseGenerator(object parameters)
        {
            var generator = parameters as Generator;
            
            if(generator != null)
            {
                engine.GeneratorManager.PurchaseGenerator(generator);
                engine.RefreshGenerators();
            }
        }
    }
}
