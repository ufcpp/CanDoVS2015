using KabeDon.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace KabeDon.Packaging
{
    /// <summary>
    /// 設定データ(JSON 保存)と各種アセット(画像、音声)を ZIP パッケージ化して管理する。
    /// </summary>
    public class PackageManager
    {
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

            var imageFolder = await root.GetSubfolderAsync("Images");
            ImageFiles = await imageFolder.GetFilesAsync();

            var soundFolder = await root.GetSubfolderAsync("Sounds");
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
    }
}
