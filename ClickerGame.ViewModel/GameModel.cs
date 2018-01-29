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
    public class GameModel : INotifyPropertyChanged
    {
        private Engine engine;
        private Value currentValue;

        public List<Bonus> Bonuses { get; set; }
        public List<Generator> Generators { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public GameModel()
        {
            currentValue = new Value(0, 0);
            engine = new Engine();
            engine.CurrentValueChanged += CurrentValueChanged;

            Bonuses = Bonus.Bonuses();
            Generators = Generator.Generators();
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
