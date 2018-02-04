using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ClickerEngine.Generators
{
    public class Generator : INotifyPropertyChanged
    {
        private Value _vps;
        private Value _vpc;

        private int _purchasedCount;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }

        public int AvailableAmount { get; set; }
        public int PurchasedAmount {
            get{ return _purchasedCount; }
            set { _purchasedCount = value; OnPropertyChanged("PurchasedAmount"); }
        }

        public Value Price { get; set; }

        public Value ValuePerSecond { get; set; }
        public Value ValuePerClick { get; set; }
        public List<Bonus> AvailableBonuses { get; set; }

        public Generator(string name, string description, string thumbnail, Value initialPrice)
        {
            Name = name;
            Description = description;
            Thumbnail = thumbnail;
            Price = initialPrice;

            AvailableAmount = 0;
            PurchasedAmount = 0;

            ValuePerSecond = new Value(100);
            ValuePerClick = new Value(2);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update(Value currentValue)
        {

        }
    }
}
