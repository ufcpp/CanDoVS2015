using System;
using System.Reactive.Subjects;
using Xamarin.Forms;

namespace KabeDon.XamarinForms
{
    /// <summary>
    /// タッチ位置を取れる <see cref="Image"/>。
    /// </summary>
    public class TouchEventImage : Image
    {
        /// <summary>
        /// renderer から呼んでもらう。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OnTouch(float x, float y)
        {
            _touch.OnNext(new Point(x, y));
        }

        /// <summary>
        /// タッチ イベント。
        /// </summary>
        public IObservable<Point> Touch => _touch;
        private readonly Subject<Point> _touch = new Subject<Point>();
    }
}
