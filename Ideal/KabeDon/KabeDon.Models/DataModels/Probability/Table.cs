using System;
using System.Linq;

namespace KabeDon.DataModels.Probability
{
    /// <summary>
    /// 確率テーブル。
    /// </summary>
    public class Table<T>
    {
        /// <summary>
        /// 確率と生起物の一覧。
        /// </summary>
        public Item<T>[] Items { get; set; }

        /// <summary>
        /// 乱数を引いて、1つ生起物を得る。
        /// </summary>
        /// <param name="random">一様乱数の生成器。</param>
        /// <returns>生起物。</returns>
        public T GetOccurrence(Random random)
        {
            var sum = Items.Sum(x => x.Probability);
            var t = random.NextDouble() * sum;

            foreach (var x in Items)
            {
                if (x.Probability >= t) return x.Occurrence;
                t -= x.Probability;
            }

            // double の誤差で確率値の累積が狂ったとき用。
            return Items.Last().Occurrence;
        }
    }
}
