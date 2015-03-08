using KabeDon.Packages;
using System.IO;
using System.Threading.Tasks;

namespace KabeDon
{
    /// <summary>
    /// <see cref="IStorage"/> の <see cref="File"/> 実装。
    /// </summary>
    class FileStorage : IStorage
    {
        private string _path;

        public FileStorage(string path)
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
            return Task.FromResult<IStorage>(new FileStorage(sub));
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
