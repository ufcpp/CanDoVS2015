#if WINDOWS_APP || WINDOWS_PHONE_APP

using KabeDon.Packaging;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace KabeDon.Sound
{
    class SoundPlayerFactory : ISoundPlayerFactory
    {
        public ISoundPlayer Create(IStorage soundStorage)
        {
            return new SoundPlayer(soundStorage);
        }
    }

    class SoundPlayer : ISoundPlayer
    {
        private IStorage _soundStorage;
        private Dictionary<string, MediaElement> _players = new Dictionary<string, MediaElement>();

        public SoundPlayer(IStorage soundStorage)
        {
            _soundStorage = soundStorage;
        }

        public async void Play(string path)
        {
            MediaElement player;
            if (!_players.TryGetValue(path, out player))
            {
                player = new MediaElement();
                var stream = await _soundStorage.OpenReadAsync(path);
                player.SetSource(stream.AsRandomAccessStream(), "audio/mp3");
            }

            player.Play();
        }
    }
}

#endif
