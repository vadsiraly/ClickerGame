using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClickerEngine
{
    public class Engine
    {
        private Value _currentValue;
        private List<Bonus> _purchasedAdditiveBonuses;
        private List<Bonus> _purchasedMultiplicativeBonuses;

        public event EventHandler<Value> CurrentValueChanged;

        public Engine()
        {
            _currentValue = new Value(0, 0);
            LastExecution = DateTime.Now;
            ValuePerSecond = new Value(100, 0);
            ValuePerClick = new Value(100.123456789, 0);
            Timer = new Timer(Update, null, 0, 1000);

            _purchasedAdditiveBonuses = new List<Bonus>();
            _purchasedMultiplicativeBonuses = new List<Bonus>();
        }

        public Value CurrentValue { get { return _currentValue; } set { _currentValue = value; OnCurrentValueChanged(_currentValue); } }
        public List<Bonus> Bonuses
        {
            get
            {
                return Bonus.AdditiveBonuses().Concat(Bonus.MultiplicativeBonuses()).ToList();
            }
        }
        public List<Bonus> AdditiveBonuses {
            get
            {
                return Bonus.AdditiveBonuses();
            }
        }
        public List<Bonus> MultiplicativeBonuses
        {
            get
            {
                return Bonus.MultiplicativeBonuses();
            }
        }

        private DateTime LastExecution { get; set; }
        private Value ValuePerSecond { get; set; }
        private Value ValuePerClick { get; set; }
        private Timer Timer { get; set; }

        private void Update(object state)
        {
            CurrentValue += new Value(ValuePerSecond.Gain
                * (_purchasedAdditiveBonuses.Any() ? _purchasedAdditiveBonuses.Select(x => x.Value).Aggregate((cur, next) => cur + next) : 1)
                * (_purchasedMultiplicativeBonuses.Any() ? _purchasedMultiplicativeBonuses.Select(x => x.Value).Aggregate((cur, next) => cur * next) : 1)
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
