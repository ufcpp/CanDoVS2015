#if WINDOWS_DESKTOP || ANDROID || __IOS__

using System;
using System.IO;
using System.Threading.Tasks;

namespace KabeDon.Packaging
{
    /// <summary>
    /// <see cref="IStorage"/> の <see cref="File"/> 実装。
    /// <see cref="Environment.SpecialFolder.Personal"/> フォルダー内にファイルを置く。
    /// </summary>
    class FileStorage : IStorage
    {
        const string AppName = "KabeDon";

        private string _path;

        public FileStorage(string levelName)
        {
            var personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _path = Path.Combine(personal, AppName, levelName);
        }

        private FileStorage() { }

        public Task<string[]> GetFilesAsync()
        {
            var files = Directory.GetFiles(_path);
            return Task.FromResult(files);
        }

        public Task<IStorage> GetSubfolderAsync(string folder)
        {
            var sub = Path.Combine(_path, folder);
            return Task.FromResult<IStorage>(new FileStorage { _path = sub });
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

#endif
