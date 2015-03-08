using System;
using System.Collections.Generic;
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
        public List<Item<T>> Items { get; } = new List<Item<T>>();

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

        /// <summary>
        /// 項目の追加。
        /// </summary>
        /// <param name="probability"><see cref="Item{T}.Probability"/></param>
        /// <param name="occurrence"><see cref="Item{T}.Occurrence"/></param>
        public void Add(int probability, T occurrence) => Items.Add(probability, occurrence);
    }
}

namespace KabeDon.DataModels
{
    using Probability;

    /// <summary>
    /// <see cref="Table{T}"/> 向け拡張。
    /// </summary>
    public static class TableExtensions
    {
        public static void Add<T>(this List<Item<T>> items, int probability, T occurrence) => items.Add(new Item<T> { Probability = probability, Occurrence = occurrence });
    }
}
