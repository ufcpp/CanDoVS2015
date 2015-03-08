namespace KabeDon.DataModels
{
    /// <summary>
    /// 特定の矩形領域をタップしたときの挙動に関する情報。
    /// </summary>
    public class AreaInfo
    {
        /// <summary>
        /// 矩形領域の座標。
        /// </summary>
        public Rect Rect { get; set; }

        /// <summary>
        /// 得点。
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 【編集用】
        /// 説明。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// タップしたときに鳴る音のファイル名。
        /// </summary>
        public string Sound { get; set; }

        /// <summary>
        /// タップ後、この時間だけタップを受け付けない。
        /// </summary>
        public TimeFrame CoolTime { get; set; }

        /// <summary>
        /// タップ時の状態遷移テーブル。
        /// 遷移先の ID を指定。
        /// null なら遷移なし(自身の ID 指定だと経過時間はリセット、null だと経過続行)。
        /// </summary>
        public Probability.Table<string> Transition { get; set; }
    }
}
