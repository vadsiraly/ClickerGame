using ClickerEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClickerGame
{
	public partial class MainPage : ContentPage
	{
        Engine engine = new Engine();
        Value currentValue = new Value(0, 0);
        Label lbl;

        public string CurrentValue
        {
            get { return currentValue.ToString(); }
            private set { }
        }

		public MainPage()
		{
			InitializeComponent();
            BindingContext = this;

            Button btn = this.FindByName<Button>("MyButton");
            lbl = this.FindByName<Label>("MyLabel");

            btn.Clicked += Btn_Clicked;

            engine.CurrentValueChanged += CurrentValueChanged;
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            engine.Click();
        }

        private void CurrentValueChanged(object sender, Value currentValue)
        {
            this.currentValue = currentValue;
            OnPropertyChanged("CurrentValue");
        }
    }
}
