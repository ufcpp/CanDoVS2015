namespace KabeDon.Sound
{
    /// <summary>
    /// サウンド再生用。
    /// Forms はサウンドまでクロス プラットフォーム対応していないので。
    /// </summary>
    public interface ISoundPlayer
    {
        /// <summary>
        /// サウンドを再生する。
        /// </summary>
        /// <param name="uri">サウンドのフルパス。</param>
        void Play(string path);
    }
}
