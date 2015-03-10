#if WINDOWS_APP || WINDOWS_PHONE_APP

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;

namespace KabeDon.Packaging
{
    /// <summary>
    /// <see cref="ApplicationData.LocalFolder"/> 内に、指定した名前のフォルダーを作ってその中身を管理。
    /// </summary>
    class FileStorage : IStorage
    {
        IAsyncOperation<StorageFolder> _storage;

        public FileStorage(string levelName)
        {
            _storage = ApplicationData.Current.LocalFolder.GetFolderAsync(levelName);
        }

        private FileStorage(IAsyncOperation<StorageFolder> storage)
        {
            _storage = storage;
        }

        public async Task<string[]> GetFilesAsync()
        {
            var s = await _storage;
            var files = await s.GetFilesAsync();
            return files.Select(x => x.Path).ToArray();
        }

        public async Task<IStorage> GetSubfolderAsync(string folder)
        {
            var s = await _storage;
            var sub = s.GetFolderAsync(folder);
            return new FileStorage(sub);
        }

        public async Task<Stream> OpenReadAsync(string file)
        {
            var s = await _storage;
            return await s.OpenStreamForReadAsync(file);
        }

        public async Task<Stream> OpenWriteAsync(string file)
        {
            var s = await _storage;
            return await s.OpenStreamForWriteAsync(file, CreationCollisionOption.ReplaceExisting);
        }
    }
}

#endif
