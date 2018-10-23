using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ClickerGame.AndroidNative
{
    public class TouchObject
    {
        private long _bornTime;

        public TouchObject(float x, float y, string clickValue)
        {
            X = x;
            Y = y;
            ClickValue = clickValue;
            _bornTime = Java.Lang.JavaSystem.NanoTime();
        }

        public float X { get; private set; }
        public float Y { get; private set; }
        public string ClickValue { get; private set; }

        public long AliveMillis
        {
            get
            {
                return (Java.Lang.JavaSystem.NanoTime() - _bornTime)/1000000;
            }
        }
    }
}