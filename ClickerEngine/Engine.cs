using ClickerEngine.Enumerations;
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

        private List<Generator> _purchasedGenerators;

        public event EventHandler<Value> CurrentValueChanged;

        public Engine()
        {
            _currentValue = new Value(0, 0);
            LastExecution = DateTime.Now;
            ValuePerSecond = new Value(0, 0);
            ValuePerClick = new Value(1, 0);
            Timer = new Timer(Update, null, 0, 1000);

            _purchasedAdditiveBonuses = new List<Bonus>();
            _purchasedMultiplicativeBonuses = new List<Bonus>();
            _purchasedGenerators = new List<Generator>();
        }

        public Value CurrentValue { get { return _currentValue; } set { _currentValue = value; OnCurrentValueChanged(_currentValue); } }

        public List<Generator> PurchasedGenerators
        {
            get
            {
                return _purchasedGenerators;
            }
        }

        public List<Bonus> PurchasedBonuses
        {
            get
            {
                return _purchasedAdditiveBonuses.Concat(_purchasedMultiplicativeBonuses).ToList();
            }
        }
        public List<Bonus> PurchasedAdditiveBonuses
        {
            get
            {
                return _purchasedAdditiveBonuses;
            }
        }
        public List<Bonus> PurchasedMultiplicativeBonuses
        {
            get
            {
                return _purchasedMultiplicativeBonuses;
            }
        }

        public Value ValuePerSecond { get; private set; }
        public Value ValuePerClick { get; private set; }

        private DateTime LastExecution { get; set; }
        private Timer Timer { get; set; }

        private void Update(object state)
        {
            CurrentValue += ValuePerSecond;
        }

        public void Click()
        {
            CurrentValue += ValuePerClick;
        }

        public void PurchaseBonus(Bonus pickedBonus)
        {
            if ((CurrentValue - pickedBonus.Price).Gain >= 0)
            {
                if (ValuePerSecond == new Value(0, 0))
                {
                    ValuePerSecond = new Value(1, 0);
                }

                CurrentValue -= pickedBonus.Price;
                switch(pickedBonus.Type)
                {
                    case BonusType.Additive:
                        _purchasedAdditiveBonuses.Add(pickedBonus);
                        break;
                    case BonusType.Multiplicative:
                        _purchasedMultiplicativeBonuses.Add(pickedBonus);
                        break;
                }

                var additiveBonusValues = (_purchasedAdditiveBonuses.Any() ? _purchasedAdditiveBonuses.Select(x => x.Value).Aggregate((cur, next) => cur + next) : 1);
                var multilpicativeBonusValues = (_purchasedMultiplicativeBonuses.Any() ? _purchasedMultiplicativeBonuses.Select(x => x.Value).Aggregate((cur, next) => cur * next) : 1);

                var gain = ValuePerSecond.Gain * additiveBonusValues * multilpicativeBonusValues;
                var power = ValuePerSecond.Power;

                ValuePerSecond = new Value(gain, power).Normalize();
            }
        }

        protected void OnCurrentValueChanged(Value currentValue)
        {
            CurrentValueChanged?.Invoke(this, currentValue);
        }
    }
}
