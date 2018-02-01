using ClickerEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerGame.ViewModel
{
    public class GeneratorsPageViewModel
    {
        private Engine engine;
        public List<Generator> Generators { get; private set; }

        public GeneratorsPageViewModel(Engine engine)
        {
            this.engine = engine;
            Generators = Generator.Generators();
        }
    }
}
