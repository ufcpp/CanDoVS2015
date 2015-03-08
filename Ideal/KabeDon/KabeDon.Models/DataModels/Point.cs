namespace KabeDon.DataModels
{
    /// <summary>
    /// 点。
    /// タップ判定用。
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// X 座標。
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y 座標。
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"><see cref="X"/></param>
        /// <param name="y"><see cref="Y"/></param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";
    }
}
