#if WINDOWS_DESKTOP && !CONSOLE

using KabeDon.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

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
        private Dictionary<string, MediaPlayer> _players = new Dictionary<string, MediaPlayer>();

        public SoundPlayer(IStorage soundStorage)
        {
            _soundStorage = soundStorage;
        }

        public async void Play(string path)
        {
            MediaPlayer player;
            if (!_players.TryGetValue(path, out player))
            {
                player = new MediaPlayer();
                var fullPath = (await _soundStorage.GetFilesAsync()).First(x => x.EndsWith(path));
                player.Open(new Uri(fullPath, UriKind.Absolute));
            }

            player.Play();
        }
    }
}

#endif
