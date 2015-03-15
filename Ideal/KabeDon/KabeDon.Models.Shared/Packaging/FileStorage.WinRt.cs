#if WINDOWS_APP || WINDOWS_PHONE_APP

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace KabeDon.Packaging
{
    /// <summary>
    /// <see cref="ApplicationData.LocalFolder"/> 内に、指定した名前のフォルダーを作ってその中身を管理。
    /// </summary>
    class FileStorage : IStorage
    {
        StorageFolder _storage;

        internal static FileStorage Root()
        {
            return new FileStorage(ApplicationData.Current.LocalFolder);
        }

        internal static async Task<FileStorage> GetLevelAsync(string levelName)
        {
            var storage = await ApplicationData.Current.LocalFolder.GetFolderAsync(levelName);
            return new FileStorage(storage);
        }

        private FileStorage(StorageFolder storage)
        {
            _storage = storage;
        }

        public string FullPath => _storage.Path + "/";

        public async Task<string[]> GetSubfolderPathsAsync()
        {
            var files = await _storage.GetFoldersAsync();
            return files.Select(x => x.Path).ToArray();
        }

        public async Task<string[]> GetFilesAsync()
        {
            var files = await _storage.GetFilesAsync();
            return files.Select(x => x.Path).ToArray();
        }

        public async Task<IStorage> GetSubfolderAsync(string folder)
        {
            var sub = await _storage.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);
            return new FileStorage(sub);
        }

        public Task<IStorage> GetSubfolderAsync(Uri uri) => GetSubfolderAsync(MakeRelative(uri));

        public async Task<Stream> OpenReadAsync(string file)
        {
            return await _storage.OpenStreamForReadAsync(file);
        }

        public Task<Stream> OpenReadAsync(Uri uri) => OpenReadAsync(MakeRelative(uri));

        public async Task<Stream> OpenWriteAsync(string file)
        {
            return await _storage.OpenStreamForWriteAsync(file, CreationCollisionOption.ReplaceExisting);
        }

        public Task<Stream> OpenWriteAsync(Uri uri) => OpenWriteAsync(MakeRelative(uri));

        private string MakeRelative(Uri uri)
        {
            if (uri.IsAbsoluteUri) uri = new Uri(FullPath, UriKind.Absolute).MakeRelativeUri(uri);
            return uri.OriginalString;
        }
    }
}

#endif
