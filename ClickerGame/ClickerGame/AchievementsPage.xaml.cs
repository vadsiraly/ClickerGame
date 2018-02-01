using ClickerEngine;
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
	public partial class AchievementsPage : ContentPage
	{
		public AchievementsPage (Engine engine)
		{
			InitializeComponent ();
		}
	}
}