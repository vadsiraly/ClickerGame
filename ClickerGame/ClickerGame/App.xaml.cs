using ClickerEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClickerGame
{
	public partial class App : Application
	{
        private Engine engine;
		public App ()
		{
			InitializeComponent();
            engine = new Engine();

            MainPage = new NavigationPage(new MainPage(engine));
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
