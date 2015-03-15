#if ANDROID

using Android.Media;
using KabeDon.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KabeDon.Sound
{
    public class SoundPlayerFactory : ISoundPlayerFactory
    {
        public ISoundPlayer Create(IStorage soundStorage)
        {
            return new SoundPlayer(soundStorage);
        }
    }

    class SoundPlayer : ISoundPlayer
    {
        private IStorage _soundStorage;
        private SoundPool _pool = new SoundPool(20, Stream.Music, 0);
        private readonly Dictionary<string, int>  _soundIds = new Dictionary<string, int>();

        public SoundPlayer(IStorage soundStorage)
        {
            _soundStorage = soundStorage;
        }

        public async void Play(string path)
        {
            try
            {
                int id;
                if (!_soundIds.TryGetValue(path, out id))
                {
                    var fullPath = (await _soundStorage.GetFilesAsync()).First(x => x.EndsWith(path));
                    id = _pool.Load(fullPath, 1);
                    _soundIds.Add(path, id);
                }
                _pool.Play(id, 1, 1, 1, 0, 1);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

#endif
