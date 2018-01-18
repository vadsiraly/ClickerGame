using ClickerEngine;
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

        public List<string> BonusNames;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameModel()
        {
            currentValue = new Value(0, 0);
            engine = new Engine();
            engine.CurrentValueChanged += CurrentValueChanged;

            Bonuses = engine.Bonuses;
            BonusNames = Bonuses.Select(x => x.Name).ToList();
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

        public string CurrentValue
        {
            get { return currentValue.ToString(); }
            private set { }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
