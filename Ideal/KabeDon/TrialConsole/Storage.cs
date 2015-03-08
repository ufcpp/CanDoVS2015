using KabeDon.Packages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TrialConsole
{
    /// <summary>
    /// 決め打ちで固定パスにデータを置く。
    /// デスクトップ上の temp\KabeDonSample フォルダー。
    /// </summary>
    class Storage : IStorage
    {
        private string _path;

        public Storage()
        {
            _path = @"../../../KabeDon.Wpf/仮データ";
        }

        private Storage(string path)
        {
            _path = path;
        }

        public Task<string[]> GetFilesAsync()
        {
            var files = Directory.GetFiles(_path);
            return Task.FromResult(files);
        }

        public Task<IStorage> GetSubfolderAsync(string folder)
        {
            var sub = Path.Combine(_path, folder);
            return Task.FromResult<IStorage>(new Storage(sub));
        }

        public Task<Stream> OpenReadAsync(string file)
        {
            var path = Path.Combine(_path, file);
            return Task.FromResult<Stream>(File.OpenRead(path));
        }

        public Task<Stream> OpenWriteAsync(string file)
        {
            var path = Path.Combine(_path, file);
            return Task.FromResult<Stream>(File.OpenWrite(path));
        }
    }
}
