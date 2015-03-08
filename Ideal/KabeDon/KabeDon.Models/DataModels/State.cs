using System.Collections.Generic;
using System.Linq;

namespace KabeDon.DataModels
{
    public class State
    {
        /// <summary>
        /// ID。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 【編集用】
        /// 説明。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 画像ファイル名。
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 領域設定のない場所をタップしたときに鳴る音のファイル名。
        /// </summary>
        public string Sound { get; set; }

        /// <summary>
        /// タップしなくても一定時間で遷移を起こしたいときに使う。
        /// null ならタップ以外で遷移しない。
        /// </summary>
        public TimeFrame TimeFrame { get; set; }

        /// <summary>
        /// <see cref="TimeFrame"/> 経過時の状態遷移テーブル。
        /// 遷移先の ID を指定。
        /// null なら遷移なし(自身の ID 指定遷移だと経過時間はリセット、null だと経過時間累積)。
        /// </summary>
        public Probability.Table<string> Transition { get; set; }

        /// <summary>
        /// 領域設定。
        /// 座標がかぶっている領域があった場合、先頭側ほど優先。
        /// </summary>
        public List<AreaInfo> Area { get; } = new List<AreaInfo>();

        /// <summary>
        /// 点 <paramref name="p"/> を含む最初の領域を探す。
        /// </summary>
        /// <param name="p">探したい点。</param>
        /// <returns>1つもなければ null。</returns>
        public AreaInfo GetArea(Point p) => Area.FirstOrDefault(a => a.Contains(p));
    }
}
