using Android.App;
using Android.Widget;
using Android.OS;
using ClickerEngine;
using System;
using Android.Views;
using ClickerEngine.PowerNames;
using System.Linq;
using System.Collections.Generic;

namespace ClickerGame.AndroidNative
{
    [Activity(Label = "ClickerGame.AndroidNative", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Engine _engine;
        private bool _gestureLock = false;
        private bool _singlePointerEvent = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _engine = new Engine();
            _engine.CurrentValueChanged += UpdateUI;

            DrawableView view1 = FindViewById<DrawableView>(Resource.Id.drawable_view1);
            view1.Touch += View1_Touch;
        }

        private void View1_Touch(object sender, View.TouchEventArgs e)
        {
            if(e.Event.Action == MotionEventActions.Down)
            {
                _singlePointerEvent = true;
            }

            if (!_gestureLock && e.Event.Action == MotionEventActions.Move)
            {
                var coordinates = new List<Tuple<float, float>>();
                foreach (var i in Enumerable.Range(0, e.Event.PointerCount))
                {
                    coordinates.Add(Tuple.Create(e.Event.GetX(i),e.Event.GetY(i)));
                    _engine.Click();
                }

                DrawableView view1 = FindViewById<DrawableView>(Resource.Id.drawable_view1);
                view1.AddClickCoordinates(coordinates);
                view1.Invalidate();

                _singlePointerEvent = false;
                _gestureLock = true;
            }

            if(e.Event.Action == MotionEventActions.Up)
            {
                if (_singlePointerEvent)
                {
                    var coordinates = new List<Tuple<float, float>>();
                    coordinates.Add(Tuple.Create(e.Event.GetX(), e.Event.GetY()));
                    _engine.Click();

                    DrawableView view1 = FindViewById<DrawableView>(Resource.Id.drawable_view1);
                    view1.AddClickCoordinates(coordinates);
                    view1.Invalidate();
                }

                //The last finger has left the screen so we are ready to process a new Move action.
                _gestureLock = false;
            }
        }

        private void UpdateUI(object sender, Value e)
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

