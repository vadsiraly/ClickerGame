using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClickerEngine
{
    public class Engine
    {
        private Value _currentValue;
        public event EventHandler<Value> CurrentValueChanged;

        public Engine()
        {
            _currentValue = new Value(0, 0);
            LastExecution = DateTime.Now;
            ValuePerSecond = new Value(100, 0);
            ValuePerClick = new Value(100.123456789, 0);
            Timer = new Timer(Update, null, 0, 1000);

            AdditiveBonuses = new List<Bonus>();
            MultiplicativeBonuses = new List<Bonus>();
        }

        public Value CurrentValue { get { return _currentValue; } set { _currentValue = value; OnCurrentValueChanged(_currentValue); } }

        private DateTime LastExecution { get; set; }
        private Value ValuePerSecond { get; set; }
        private Value ValuePerClick { get; set; }
        private List<Bonus> AdditiveBonuses { get; set; }
        private List<Bonus> MultiplicativeBonuses { get; set; }
        private Timer Timer { get; set; }

        private void Update(object state)
        {
            CurrentValue += new Value(ValuePerSecond.Gain
                * (AdditiveBonuses.Any() ? AdditiveBonuses.Select(x => x.Value).Aggregate((cur, next) => cur + next) : 1)
                * (MultiplicativeBonuses.Any() ? MultiplicativeBonuses.Select(x => x.Value).Aggregate((cur, next) => cur * next) : 1)
                , ValuePerSecond.Power);
        }

        public void Click()
        {
            CurrentValue += ValuePerClick;
        }

        protected void OnCurrentValueChanged(Value currentValue)
        {
            CurrentValueChanged?.Invoke(this, currentValue);
        }
    }
}
