using Android.App;
using Android.Widget;
using Android.OS;
using ClickerEngine;

namespace ClickerGame.AndroidNative
{
    [Activity(Label = "ClickerGame.AndroidNative", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Engine _engine;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _engine = new Engine();

            RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

