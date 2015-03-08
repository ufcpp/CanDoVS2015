using System;

namespace KabeDon.DataModels
{
    public class Rect
    {
        /// <summary>
        /// 左端 X 座標。
        /// </summary>
        public int X1 { get; set; }

        /// <summary>
        /// 上端 Y 座標。
        /// </summary>
        public int Y1 { get; set; }

        /// <summary>
        /// 右端 X 座標。
        /// </summary>
        public int X2 { get; set; }

        /// <summary>
        /// 下端 Y 座標。
        /// </summary>
        public int Y2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"><see cref="X1"/></param>
        /// <param name="y1"><see cref="Y1"/></param>
        /// <param name="x2"><see cref="X2"/></param>
        /// <param name="y2"><see cref="Y2"/></param>
        public Rect(int x1 = 0, int y1 = 0, int x2 = 0, int y2 = 0)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        /// <summary>
        /// この矩形内に点 <paramref name="p"/> が含まれるかどうか。
        /// </summary>
        /// <param name="p">点。</param>
        /// <returns>含むとき true。</returns>
        public bool Contains(Point p)
        {
            return X1 <= p.X && p.X <= X2 && Y1 <= p.Y && p.Y <= Y2;
        }

        /// <summary>
        /// 幅、高さ指定。
        /// </summary>
        /// <param name="left">左端座標。</param>
        /// <param name="top">上端座標。</param>
        /// <param name="width">幅。</param>
        /// <param name="height">高さ。</param>
        /// <returns></returns>
        public static Rect LeftTop(int left, int top, int width, int height) => new Rect(left, top, left + width, top + height);

        public override string ToString() => $"[({X1}, {Y1}) - ({X2}, {Y2})";
    }
}
