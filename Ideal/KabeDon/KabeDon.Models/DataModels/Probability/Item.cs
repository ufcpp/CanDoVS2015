namespace KabeDon.DataModels.Probability
{
    /// <summary>
    /// 確率テーブルの項目。
    /// </summary>
    /// <typeparam name="T">生起物の型。</typeparam>
    public class Item<T>
    {
        /// <summary>
        /// 確率。
        /// </summary>
        public int Probability { get; set; }

        /// <summary>
        /// その確率で起きうるもの。
        /// </summary>
        public T Occurrence { get; set; }
    }
}
