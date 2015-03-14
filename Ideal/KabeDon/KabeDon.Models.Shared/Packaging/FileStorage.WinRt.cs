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
        //todo: async なファクトリメソッドを作って、こいつは StorageFolder _storage; に変えたい。
        IAsyncOperation<StorageFolder> _storage;

        public FileStorage(string levelName)
        {
            _storage = ApplicationData.Current.LocalFolder.GetFolderAsync(levelName);
        }

        private FileStorage(IAsyncOperation<StorageFolder> storage)
        {
            _storage = storage;
        }

        public string FullPath => _storage.GetResults().Path;

        public async Task<string[]> GetSubfolderPathsAsync()
        {
            var s = await _storage;
            var files = await s.GetFoldersAsync();
            return files.Select(x => x.Path).ToArray();
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

        internal static FileStorage Root()
        {
            throw new NotImplementedException();
        }


        //todo: この辺りたぶんバグってる。パスの相対・絶対おかしい。ストアアプリ対応始めたら直す。

        public async Task<IStorage> GetSubfolderAsync(Uri uri)
        {
            var s = await _storage;
            var sub = s.GetFolderAsync(uri.AbsolutePath);
            return new FileStorage(sub);
        }

        public async Task<Stream> OpenReadAsync(string file)
        {
            var s = await _storage;
            return await s.OpenStreamForReadAsync(file);
        }

        public async Task<Stream> OpenReadAsync(Uri uri)
        {
            var s = await _storage;
            return await s.OpenStreamForReadAsync(uri.AbsolutePath);
        }

        public async Task<Stream> OpenWriteAsync(string file)
        {
            var s = await _storage;
            return await s.OpenStreamForWriteAsync(file, CreationCollisionOption.ReplaceExisting);
        }

        public async Task<Stream> OpenWriteAsync(Uri uri)
        {
            var s = await _storage;
            return await s.OpenStreamForWriteAsync(uri.AbsolutePath, CreationCollisionOption.ReplaceExisting);
        }
    }
}

#endif
