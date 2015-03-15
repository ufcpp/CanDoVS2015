using KabeDon.Packaging;
using System.Linq;

namespace KabeDon.Sound
{
    class DummySoundPlayerFactory : ISoundPlayerFactory
    {
        public ISoundPlayer Create(IStorage soundStorage)
        {
            return new DummySoundPlayer(soundStorage);
        }
    }

    class DummySoundPlayer : ISoundPlayer
    {
        private IStorage _soundStorage;

        public DummySoundPlayer(IStorage soundStorage)
        {
            _soundStorage = soundStorage;
        }

        public async void Play(string path)
        {
            var fullPath = (await _soundStorage.GetFilesAsync()).First(x => x.EndsWith(path));
#if WINDOWS_DESKTOP
            System.Diagnostics.Debug.WriteLine("play sound: " + fullPath);
#endif
        }
    }
}
