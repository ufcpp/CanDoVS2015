using Foundation;
using KabeDon.XamarinForms;
using KabeDon.XamarinForms.Droid;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TouchEventImage), typeof(TouchEventImageRenderer))]

namespace KabeDon.XamarinForms.Droid
{

    public class TouchEventImageRenderer : ImageRenderer
    {
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            //todo: iOS Œü‚¯ŽÀ‘•

            base.TouchesEnded(touches, evt);
        }
        //public override bool OnTouchEvent(MotionEvent e)
        //{
        //    var p = new MotionEvent.PointerCoords();
        //    e.GetPointerCoords(0, p);

        //    var image = (TouchEventImage)Element;
        //    image.OnTouch(p.X / Width, p.Y / Height);

        //    return base.OnTouchEvent(e);
        //}
    }
}