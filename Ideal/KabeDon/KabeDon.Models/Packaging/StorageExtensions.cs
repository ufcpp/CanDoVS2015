using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KabeDon.Packaging
{
    /// <summary>
    /// <see cref="IStorage"/> 向け拡張。
    /// </summary>
    public static class StorageExtensions
    {
        /// <summary>
        /// ファイルの中身を UTF8 テキストとして読み出し。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<string> ReadStringAsync(this IStorage s, string file)
        {
            using (var stream = await s.OpenReadAsync(file))
            using (var sr = new StreamReader(stream, Encoding.UTF8))
                return await sr.ReadToEndAsync();
        }
    }
}
