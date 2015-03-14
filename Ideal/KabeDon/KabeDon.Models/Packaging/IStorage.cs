using System;
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
        string FullPath { get; }

        /// <summary>
        /// サブフォルダーを取得する。
        /// </summary>
        /// <param name="folder">フォルダーの相対パス</param>
        /// <returns>サブフォルダー。</returns>
        Task<IStorage> GetSubfolderAsync(string folder);

        /// <summary>
        /// サブフォルダーを取得する。
        /// </summary>
        /// <param name="uri">フォルダーのパス。</param>
        /// <returns>サブフォルダー。</returns>
        Task<IStorage> GetSubfolderAsync(Uri uri);

        /// <summary>
        /// フォルダー内のサブフォルダー一覧を絶対パスで取得する。
        /// </summary>
        /// <returns></returns>
        Task<string[]> GetSubfolderPathsAsync();

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
        /// ファイル読みこみ用ストリームを取得。
        /// </summary>
        /// <param name="uri">ファイルのパス。</param>
        /// <returns>読みこみ用ストリーム。</returns>
        Task<Stream> OpenReadAsync(Uri uri);

        /// <summary>
        /// ファイル書きこみ用ストリームを取得。
        /// </summary>
        /// <param name="file">ファイルの相対パス。</param>
        /// <returns>書きこみ用ストリーム。</returns>
        Task<Stream> OpenWriteAsync(string file);

        /// <summary>
        /// ファイル書きこみ用ストリームを取得。
        /// </summary>
        /// <param name="uri">ファイルのパス。</param>
        /// <returns>書きこみ用ストリーム。</returns>
        Task<Stream> OpenWriteAsync(Uri uri);
    }
}
