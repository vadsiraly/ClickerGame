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
        private Engine engine;
        private MainPageViewModel _viewModel;
        private DateTime lastEvent = DateTime.Now;

		public MainPage(Engine engine)
		{
			InitializeComponent();
            this.engine = engine;

            _viewModel = new MainPageViewModel(engine);
            BindingContext = _viewModel;

            var settingsButton = this.FindByName<Image>("SettingsButton");
            var settingsClickRecognizer = new TapGestureRecognizer();
            settingsClickRecognizer.Tapped += async delegate {
                await Navigation.PushAsync(new SettingsPage(engine));
            };
            settingsButton.GestureRecognizers.Add(settingsClickRecognizer);

            var achievementsButton = this.FindByName<Image>("AchievementsButton");
            var achievementsClickRecognizer = new TapGestureRecognizer();
            achievementsClickRecognizer.Tapped += async delegate {
                await Navigation.PushAsync(new AchievementsPage(engine));
            };
            achievementsButton.GestureRecognizers.Add(achievementsClickRecognizer);
        }

        private void ClickField_Clicked(object sender, MR.Gestures.TapEventArgs e)
        {
            var fingerCount = e.NumberOfTouches;
            var label = this.FindByName<Label>("ClickMeLabel");
            label.Text = $"{fingerCount} finger tap";
            
            var adjustedClickCount = AdjustedSumValue(fingerCount) - AdjustedSumValue(fingerCount -1);

            foreach (var i in Enumerable.Range(0, adjustedClickCount))
                _viewModel.Click();
        }

        private void PurchaseButton_Clicked(object sender, EventArgs e)
        {
            var pickedBonus = this.FindByName<Picker>("BonusPicker").SelectedItem as Bonus;
            if (pickedBonus != null)
            {
                _viewModel.PurchaseBonus(pickedBonus);
            }
        }

        private async void GeneratorsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GeneratorsPage(engine));
        }

        private int AdjustedSumValue(int a)
        {
            return AdjustedSumValueIter(a, a-1);
        }

        private int AdjustedSumValueIter(int a, int i)
        {
            if (i <= 0)
            {
                return a;
            }

            return AdjustedSumValueIter(a + i, --i);
        }
    }
}
