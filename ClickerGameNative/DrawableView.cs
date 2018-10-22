using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ClickerGame.AndroidNative
{
    public class DrawableView : View
    {
        private Bitmap mBitmap;
        private Paint paint;

        private List<Tuple<float, float>> _clickCoordinates;

        public DrawableView(Context context, IAttributeSet attrs)
        : base(context, attrs)
        {
            Initialize();
        }

        public DrawableView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            Initialize();
        }

        public DrawableView(Context context) : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            _clickCoordinates = new List<Tuple<float, float>>();
            mBitmap = Bitmap.CreateBitmap(400, 800, Bitmap.Config.Argb8888);
            paint = new Paint();
            paint.Color = Color.Red;
            paint.SetStyle(Paint.Style.Fill);
        }

        public void ReplaceClickCoordinates(List<Tuple<float, float>> coordinates)
        {
            _clickCoordinates = coordinates;
        }

        public void AddClickCoordinates(List<Tuple<float, float>> coordinates)
        {
            _clickCoordinates.AddRange(coordinates);
        }

        public void ResetClickCoordinates(List<Tuple<float, float>> coordinates)
        {
            _clickCoordinates = new List<Tuple<float, float>>();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            foreach (var coordinate in _clickCoordinates)
            {
                canvas.DrawCircle(coordinate.Item1, coordinate.Item2, 10, paint);
                canvas.DrawText("10", coordinate.Item1+15, coordinate.Item2+15, paint);
            }
        }
    }
}