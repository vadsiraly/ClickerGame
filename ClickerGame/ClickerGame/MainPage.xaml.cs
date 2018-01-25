using ClickerEngine;
using ClickerGame.ViewModel;
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
        GameModel gameModel;

		public MainPage()
		{
			InitializeComponent();

            gameModel = new GameModel();          
            BindingContext = gameModel;
        }

        private void ClickButton_Clicked(object sender, EventArgs e)
        {
            gameModel.Click();
        }

        private void PurchaseButton_Clicked(object sender, EventArgs e)
        {
            var pickedBonus = this.FindByName<Picker>("BonusPicker").SelectedItem as Bonus;
            if (pickedBonus != null)
            {
                gameModel.PurchaseBonus(pickedBonus);
            }
        }
    }
}
