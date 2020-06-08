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

namespace XamiNotes.Clases
{
    [Register("XamiNotes.Clases.EstiloBlockDeNotas")]
    class EstiloBlockDeNotas:EditText
    {
        public Rect rect;
        public Paint paint;
        public EstiloBlockDeNotas(Context context, IAttributeSet attr):base(context,attr)
        {

            rect = new Rect();
            paint = new Paint();
            paint.SetStyle(Android.Graphics.Paint.Style.FillAndStroke);
            paint.Color = Color.AliceBlue;
        }

        protected override void OnDraw(Canvas canvas)
        {
            //int count = LineCount;

            //for (int i = 0; i < count; i++)
            //{
            //    int baseline = GetLineBounds(i, rect);

            //    canvas.DrawLine(rect.Left, baseline + 1, rect.Right, baseline + 1, paint);
            //}
            int alto = Height;
            int altoLineas = LineHeight;

            int count = alto / altoLineas;

            if (LineCount > count)
            {
                count = LineCount;
            }

            Rect r = rect;
            Paint p = paint;

            int baseLine = GetLineBounds(0, r);
            for (int i = 0; i < count; i++)
            {
                

                canvas.DrawLine(rect.Left, baseLine + 1, rect.Right, baseLine + 1, p);
                baseLine += LineHeight;
            }
            base.OnDraw(canvas);
        }
    }
}