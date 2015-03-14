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
        /// <param name="x">左端が0、右端が1のX座標。</param>
        /// <param name="y">上端が0、下端が1のY座標。</param>
        public void OnTouch(float x, float y)
        {
            _touch.OnNext(new Point(x, y));
        }

        /// <summary>
        /// タッチ イベント。
        /// 引数は座標。
        /// X は左端が0、右端が1、Y は上端が0、下端が1。
        /// </summary>
        public IObservable<Point> Touch => _touch;
        private readonly Subject<Point> _touch = new Subject<Point>();
    }
}
