using ClickerEngine.Enumerations;
using ClickerEngine.Generators;
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

        private IGeneratorManager _generatorManager;

        public event EventHandler<Value> CurrentValueChanged;

        public Engine()
        {
            _currentValue = new Value(0, 0);
            LastExecution = DateTime.Now;
            ValuePerTick = new Value(0, 0);
            BaseValuePerTick = new Value(0, 0);
            BaseValuePerClick = new Value(10, 0);
            ValuePerClick = new Value(10, 0);

            _purchasedAdditiveBonuses = new List<Bonus>();
            _purchasedMultiplicativeBonuses = new List<Bonus>();

            _generatorManager = new GeneratorManager();

            CurrentValueChanged += (s, e) =>
            {
                _generatorManager.Update(CurrentValue);
            };
        }

        public Value CurrentValue { get { return _currentValue; } set { _currentValue = value; OnCurrentValueChanged(_currentValue); } }

        public void PurchaseGenerator(Generator generator)
        {
            if((CurrentValue - generator.Price).Gain >= 0)
            {
                CurrentValue = (CurrentValue - generator.Price).Normalize();
                GeneratorManager.PurchaseGenerator(generator);
                RefreshGenerators();
            }
        }

        public IGeneratorManager GeneratorManager
        {
            get
            {
                return _generatorManager;
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

        public Value BaseValuePerTick { get; private set; }
        public Value ValuePerTick { get; private set; }
        public Value BaseValuePerClick { get; private set; }
        public Value ValuePerClick { get; private set; }

        private DateTime LastExecution { get; set; }
        private Timer Timer { get; set; }

        public void Tick(object state)
        {
            var additiveBonusValues = (_purchasedAdditiveBonuses.Any() ? _purchasedAdditiveBonuses.Select(x => x.Value).Aggregate((cur, next) => cur + next) : 1);
            var multilpicativeBonusValues = (_purchasedMultiplicativeBonuses.Any() ? _purchasedMultiplicativeBonuses.Select(x => x.Value).Aggregate((cur, next) => cur * next) : 1);

            var gain = ValuePerTick.Gain * additiveBonusValues * multilpicativeBonusValues;
            var power = ValuePerTick.Power;

            var VpsWithBonuses = new Value(gain, power).Normalize();

            CurrentValue += VpsWithBonuses;

            _generatorManager.Update(CurrentValue);
        }

        public void Click()
        {
            CurrentValue = (CurrentValue + ValuePerClick).Normalize();
        }

        private void RefreshGenerators()
        {
            var sumVpt = new Value(0);
            var sumVpc = new Value(0);

            foreach (var generator in GeneratorManager.PurchasedGenerators)
            {
                sumVpt += generator.ValuePerTick * new Value(generator.PurchasedAmount);
                sumVpc += generator.ValuePerClick * new Value(generator.PurchasedAmount);
            }

            ValuePerTick = BaseValuePerTick + sumVpt;
            ValuePerClick = BaseValuePerClick + sumVpc;
        }

        public void PurchaseBonus(Bonus pickedBonus)
        {
            if ((CurrentValue - pickedBonus.Price).Gain >= 0)
            {
                if (ValuePerTick == new Value(0, 0))
                {
                    ValuePerTick = new Value(1, 0);
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
            }
        }

        protected void OnCurrentValueChanged(Value currentValue)
        {
            CurrentValueChanged?.Invoke(this, currentValue);
        }
    }
}
