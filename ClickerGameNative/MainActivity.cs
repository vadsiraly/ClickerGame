using Android.App;
using Android.Widget;
using Android.OS;
using ClickerEngine;
using System;
using Android.Views;
using ClickerEngine.PowerNames;
using System.Linq;

namespace ClickerGame.AndroidNative
{
    [Activity(Label = "ClickerGame.AndroidNative", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Engine _engine;
        private bool _gestureLock = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _engine = new Engine();
            _engine.CurrentValueChanged += Update;

            View view1 = FindViewById<View>(Resource.Id.view1);
            view1.Touch += View1_Click;
        }

        private void View1_Click(object sender, View.TouchEventArgs e)
        {
            if (!_gestureLock && e.Event.Action == MotionEventActions.Move)
            {
                //var adjustedClickCount = AdjustedSumValue(e.Event.PointerCount) - AdjustedSumValue(e.Event.PointerCount - 1);

                foreach (var i in Enumerable.Range(0, e.Event.PointerCount))
                    _engine.Click();

                _gestureLock = true;
            }

            if(e.Event.Action == MotionEventActions.Up)
            {
                //The last finger has left the screen so we are ready to process a new Move action.
                _gestureLock = false;
            }
        }

        private void Update(object sender, Value e)
        {
            TextView tw1 = FindViewById<TextView>(Resource.Id.textview1);
            TextView tw2 = FindViewById<TextView>(Resource.Id.textview2);
            
            RunOnUiThread(() => { 
                if(tw1 != null)
                    tw1.Text = _engine.CurrentValue.Gain.ToString();
                if (tw2 != null)
                    tw2.Text = PowerNamer.GetName(_engine.CurrentValue.Power);
            });
        }
    }
}

