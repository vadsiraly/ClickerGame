using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
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
        private static readonly int ANIMATION_MILLISECONDS = 750; // 1 second fade effect
        private static readonly int ANIMATION_STEP = 10;          // 10ms refresh

        private static readonly int TEXT_SCALE_MIN = 45;
        private static readonly int TEXT_SCALE_MAX = 75;
        
        private static readonly int LOCATION_Y_OFFSET = 75;

        // Initializes the alpha to 255
        private Paint textPaint = new Paint();

        private Bitmap mBitmap;
        private Paint paint = new Paint();

        private List<TouchObject> _touchObjects;

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
            _touchObjects = new List<TouchObject>();
            mBitmap = Bitmap.CreateBitmap(400, 800, Bitmap.Config.Argb8888);

            paint.Color = Color.Red;
            paint.SetStyle(Paint.Style.Fill);

            textPaint.Color = Color.Green;
            textPaint.TextSize = 35;
        }

        public void ReplaceClickCoordinates(List<TouchObject> coordinates)
        {
            _touchObjects = coordinates;
        }

        public void AddClickCoordinates(List<TouchObject> tObjects)
        {
            _touchObjects.AddRange(tObjects);
        }

        public void ResetClickCoordinates(List<TouchObject> coordinates)
        {
            _touchObjects = new List<TouchObject>();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            foreach (var tobject in _touchObjects)
            {
                canvas.DrawCircle(tobject.X, tobject.Y, 15, paint);
                AdjustTextPaint(tobject, textPaint);
                if (textPaint.Alpha > 0)
                {
                    canvas.DrawText(tobject.ClickValue, tobject.X + 25, tobject.Y - 25 - LOCATION_Y_OFFSET * (1 - CalculateRatio(tobject, ANIMATION_MILLISECONDS)), textPaint);

                    PostInvalidateDelayed(ANIMATION_STEP);
                }
            }
        }

        private void AdjustTextPaint(TouchObject tobject, Paint textPaint)
        {
            textPaint.Alpha = CalculateAlpha(tobject);
            textPaint.TextSize = CalculateTextSize(tobject);
        }

        private float CalculateRatio(TouchObject tobject, int animationMillis)
        {
            var aliveMillis = tobject.AliveMillis;
            if (aliveMillis > ANIMATION_MILLISECONDS)
                return 0;

            else
            {
                var ratio = 1 - (aliveMillis / (float)ANIMATION_MILLISECONDS);
                return ratio;
            }
        }

        private float CalculateRatio(long aliveMillis, int animationMillis)
        {
            if (aliveMillis > ANIMATION_MILLISECONDS)
                return 0;

            else
            {
                var ratio = 1 - (aliveMillis / (float)ANIMATION_MILLISECONDS);
                return ratio;
            }
        }

        private int CalculateTextSize(TouchObject tobject)
        {
            var aliveMillis = tobject.AliveMillis;
            var baseTextScale = TEXT_SCALE_MIN;
            var variableTextScale = TEXT_SCALE_MAX - TEXT_SCALE_MIN;

            var textSize = baseTextScale + (variableTextScale * CalculateRatio(aliveMillis, ANIMATION_MILLISECONDS));
            return (int)textSize;
        }

        private int CalculateAlpha(TouchObject tobject)
        {
            var aliveMillis = tobject.AliveMillis;

            var alpha = 255 * CalculateRatio(aliveMillis, ANIMATION_MILLISECONDS);
            return (int)alpha;
        }
    }
}