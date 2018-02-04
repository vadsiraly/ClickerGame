using ClickerEngine;
using ClickerEngine.Enumerations;
using ClickerEngine.PowerNames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ClickerGame.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private Engine engine;
        private Value currentValue;

        public List<Bonus> Bonuses { get; set; }

        public MainPageViewModel(Engine engine)
        {
            currentValue = new Value(0, 0);
            this.engine = engine;
            engine.CurrentValueChanged += CurrentValueChanged;

            Bonuses = Bonus.Bonuses();
        }

        private void CurrentValueChanged(object sender, Value currentValue)
        {
            this.currentValue = currentValue;
            OnPropertyChanged("CurrentValue");
            OnPropertyChanged("CurrentPower");
        }

        public void Click()
        {
            engine.Click();
        }

        public void PurchaseBonus(Bonus pickedBonus)
        {
            engine.PurchaseBonus(pickedBonus);
        }

        public string CurrentValue
        {
            get { return currentValue.Gain.ToString(); }
            private set { }
        }

        public string CurrentPower
        {
            get { return PowerNamer.GetName(currentValue.Power); }
            private set { }
        }
    }
}
