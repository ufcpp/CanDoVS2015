using Android.Views;
using KabeDon.XamarinForms;
using KabeDon.XamarinForms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TouchEventImage), typeof(TouchEventImageRenderer))]

namespace KabeDon.XamarinForms.Droid
{

    public class TouchEventImageRenderer : ImageRenderer
    {
        public override bool OnTouchEvent(MotionEvent e)
        {
            var p = new MotionEvent.PointerCoords();
            e.GetPointerCoords(0, p);

            var image = (TouchEventImage)Element;
            image.OnTouch(p.X, p.Y);

            return base.OnTouchEvent(e);
        }
    }
}