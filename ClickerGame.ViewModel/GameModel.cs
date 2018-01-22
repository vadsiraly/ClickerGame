using ClickerEngine;
using ClickerEngine.Enumerations;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public GameModel()
        {
            currentValue = new Value(0, 0);
            engine = new Engine();
            engine.CurrentValueChanged += CurrentValueChanged;

            Bonuses = Bonus.Bonuses();
        }

        private void CurrentValueChanged(object sender, Value currentValue)
        {
            this.currentValue = currentValue;
            OnPropertyChanged("CurrentValue");
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
            get { return currentValue.ToString(); }
            private set { }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
