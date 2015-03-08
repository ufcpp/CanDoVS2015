namespace KabeDon.DataModels
{
    /// <summary>
    /// 乱数分布の種類。
    /// </summary>
    public enum Distribution
    {
        /// <summary>
        /// ランダム性なし。固定値。
        /// </summary>
        Constant,

        /// <summary>
        /// 指数分布。
        /// </summary>
        Exponential,
    }
}
