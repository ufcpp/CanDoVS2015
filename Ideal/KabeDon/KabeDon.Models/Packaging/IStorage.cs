using System.IO;
using System.Threading.Tasks;

namespace KabeDon.Packaging
{
    /// <summary>
    /// ファイル ストレージを抽象化。
    /// 1つのフォルダー内を管理。
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// このフォルダーの絶対パス。
        /// </summary>
        Task<string> GetPathAsync();

        /// <summary>
        /// サブフォルダーを取得する。
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        Task<IStorage> GetSubfolderAsync(string folder);

        /// <summary>
        /// フォルダー内のファイル一覧を絶対パスで取得する。
        /// </summary>
        /// <returns></returns>
        Task<string[]> GetFilesAsync();

        /// <summary>
        /// ファイル読みこみ用ストリームを取得。
        /// </summary>
        /// <param name="file">ファイルの相対パス。</param>
        /// <returns>読みこみ用ストリーム。</returns>
        Task<Stream> OpenReadAsync(string file);

        /// <summary>
        /// ファイル書きこみ用ストリームを取得。
        /// </summary>
        /// <param name="file">ファイルの相対パス。</param>
        /// <returns>書きこみ用ストリーム。</returns>
        Task<Stream> OpenWriteAsync(string file);
    }
}
