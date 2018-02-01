using ClickerEngine;
using ClickerGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClickerGame
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GeneratorsPage : ContentPage
	{
        private GeneratorsPageViewModel _viewModel;

		public GeneratorsPage (Engine engine)
		{
			InitializeComponent ();
            _viewModel = new GeneratorsPageViewModel(engine);
            BindingContext = _viewModel;
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