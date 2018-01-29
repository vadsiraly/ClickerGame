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

            var settingsButton = this.FindByName<Image>("SettingsButton");
            var settingsClickRecognizer = new TapGestureRecognizer();
            settingsClickRecognizer.Tapped += async delegate {
                await Navigation.PushAsync(new SettingsPage());
            };
            settingsButton.GestureRecognizers.Add(settingsClickRecognizer);

            var achievementsButton = this.FindByName<Image>("AchievementsButton");
            var achievementsClickRecognizer = new TapGestureRecognizer();
            achievementsClickRecognizer.Tapped += async delegate {
                await Navigation.PushAsync(new AchievementsPage());
            };
            achievementsButton.GestureRecognizers.Add(achievementsClickRecognizer);
        }

        private void Scale_Toggled(object sender, EventArgs e)
        {
            var switches = new List<Switch>();
            switches.Add(this.FindByName<Switch>("Switch_1"));
            switches.Add(this.FindByName<Switch>("Switch_10"));
            switches.Add(this.FindByName<Switch>("Switch_100"));
            switches.Add(this.FindByName<Switch>("Switch_Max"));

            var toggledSwitch = sender as Switch;
            if (toggledSwitch == null) return;

            if(toggledSwitch.IsToggled)
            foreach (var sw in switches.Where(s => s != toggledSwitch))
            {
                sw.IsToggled = false;
            }
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
