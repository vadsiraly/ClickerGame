using ClickerEngine;
using ClickerEngine.Generators;
using ClickerGame.ViewModel;
using MWX.XamForms.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClickerGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneratorsPage : ContentPage
    {
        private GeneratorsPageViewModel _viewModel;
        private Popup InfoPopup;

        public GeneratorsPage(Engine engine)
        {
            InitializeComponent();
            _viewModel = new GeneratorsPageViewModel(engine);
            BindingContext = _viewModel;

            InfoPopup = new Popup
            {
                XPositionRequest = 0.5,
                YPositionRequest = 0.2,
                ContentHeightRequest = 0.1,
                ContentWidthRequest = 0.4,
                Padding = 10,

                Body = new ContentView
                {
                    BackgroundColor = Color.Black,
                    Content = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        TextColor = Color.White,
                        Text = "Popup!"
                    }
                }
            };

            new PopupPageInitializer(this) { InfoPopup };
        }

        private void ShowInfoPopup(object sender, MR.Gestures.LongPressEventArgs e)
        {
            InfoPopup.Show();
        }

        private void HideInfoPopup(object sender, MR.Gestures.LongPressEventArgs e)
        {
            InfoPopup.Hide();
        }

        private void DisableSelection(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
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

            if (toggledSwitch.IsToggled)
                foreach (var sw in switches.Where(s => s != toggledSwitch))
                {
                    sw.IsToggled = false;
                }
        }
    }
}