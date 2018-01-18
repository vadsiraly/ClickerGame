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
        GameModel gameModel = new GameModel();

		public MainPage()
		{
			InitializeComponent();
            BindingContext = gameModel;

            //Button btn = this.FindByName<Button>("MyButton");
            //btn.Clicked += MyButton_Clicked;
        }

        private void MyButton_Clicked(object sender, EventArgs e)
        {
            gameModel.Click();
        }
    }
}
