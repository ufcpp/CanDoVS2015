using KabeDon.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace KabeDon.Packaging
{
    /// <summary>
    /// 設定データ(JSON 保存)と各種アセット(画像、音声)を ZIP パッケージ化して管理する。
    /// </summary>
    public class PackageManager
    {
        private const string Images = "Images";
        private const string Sounds = "Sounds";

        public Level Level { get; set; }

        /// <summary>
        /// 画像データ一覧(フルパス)。
        /// </summary>
        public string[] ImageFiles { get; set; }

        /// <summary>
        /// 音声データ一覧(フルパス)。
        /// </summary>
        public string[] SoundFiles { get; set; }

        /// <summary>
        /// データの読み込み。
        /// </summary>
        /// <param name="root">レベル データの入っているフォルダー。</param>
        public async Task LoadFrom(IStorage root)
        {
            var json = await root.ReadStringAsync("level.json");
            Level = Newtonsoft.Json.JsonConvert.DeserializeObject<Level>(json);

            var imageFolder = await root.GetSubfolderAsync(Images);
            ImageFiles = await imageFolder.GetFilesAsync();

            var soundFolder = await root.GetSubfolderAsync(Sounds);
            SoundFiles = await soundFolder.GetFilesAsync();
        }

        /// <summary>
        /// 画像のフルパス取得。
        /// </summary>
        /// <param name="name">画像ファイル名。</param>
        /// <returns><paramref name="name"/>のフルパス。</returns>
        public string GetImage(string name) => ImageFiles.First(x => x.EndsWith(name));

        /// <summary>
        /// 音声のフルパス取得。
        /// </summary>
        /// <param name="name">音声ファイル名。</param>
        /// <returns><paramref name="name"/>のフルパス。</returns>
        public string GetSound(string name) => SoundFiles.First(x => x.EndsWith(name));

        /// <summary>
        /// ZIP アーカイブ化する。
        /// </summary>
        /// <param name="root">アーカイブ化したいフォルダー。</param>
        /// <param name="writeStream">圧縮結果の書きこみ先。</param>
        /// <param name="leaveOpen">メソッドを抜けた後、<paramref name="writeStream"/> を開いたままにするかどうか。</param>
        /// <returns></returns>
        /// <remarks>
        /// 必要なこと以外さぼってるので注意。
        /// タイムスタンプとか適当(全ファイル、このメソッドを呼んだ時刻になる)。
        /// </remarks>
        public async Task Pack(IStorage root, Stream writeStream, bool leaveOpen = false)
        {
            var path = root.FullPath;
            var now = DateTime.Now;

            using (var zipArchive = new ZipArchive(writeStream, ZipArchiveMode.Create, leaveOpen, System.Text.Encoding.UTF8))
            {
                foreach (var file in await root.GetFilesAsync())
                    await Pack(zipArchive, root, now, path, file);

                var imageFolder = await root.GetSubfolderAsync(Images);
                foreach (var file in await imageFolder.GetFilesAsync())
                    await Pack(zipArchive, imageFolder, now, path, file);

                var soundFolder = await root.GetSubfolderAsync(Sounds);
                foreach (var file in await soundFolder.GetFilesAsync())
                    await Pack(zipArchive, soundFolder, now, path, file);
            }
        }

        private static async Task Pack(ZipArchive zipArchive, IStorage folder, DateTime now, string rootFolderPath, string filename)
        {
            using (var readStream = await folder.OpenReadAsync(new Uri(filename, UriKind.Absolute)))
            {
                var relative = filename.Substring(rootFolderPath.Length, filename.Length - rootFolderPath.Length);
                relative = relative.TrimStart(new[] { '/', '\\' });

                var zipArchiveEntry = zipArchive.CreateEntry(relative, CompressionLevel.Optimal);
                zipArchiveEntry.LastWriteTime = now;
                using (var zipStream = zipArchiveEntry.Open())
                {
                    readStream.CopyTo(zipStream);
                }
            }
        }

        /// <summary>
        /// ZIP アーカイブからファイルを取り出す。
        /// </summary>
        /// <param name="root">取り出し先のフォルダー。</param>
        /// <param name="readStream">ZIP アーカイブの入ったストリーム。</param>
        /// <returns></returns>
        /// <remarks>
        /// 信用できない ZIP を開く想定持ってない。
        /// チェックさぼってる。
        /// </remarks>
        public async Task Unpack(IStorage root, Stream readStream)
        {
            var path = root.FullPath;

            using (var zipArchive = new ZipArchive(readStream, ZipArchiveMode.Read, false, System.Text.Encoding.UTF8))
            {
                var imageFolder = await root.GetSubfolderAsync(Images);
                var soundFolder = await root.GetSubfolderAsync(Sounds);

                foreach (var entry in zipArchive.Entries)
                {
                    var file = entry.Name;
                    var storage =
                        entry.FullName.StartsWith(Images) ? imageFolder :
                        entry.FullName.StartsWith(Sounds) ? soundFolder :
                        root;

                    using (var stream = await storage.OpenWriteAsync(file))
                    {
                        using (Stream stream2 = entry.Open())
                        {
                            stream2.CopyTo(stream);
                        }
                    }
                }
            }
        }
    }
}
