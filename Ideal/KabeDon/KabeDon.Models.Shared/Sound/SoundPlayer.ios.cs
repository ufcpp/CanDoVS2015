#if __IOS__

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

        public SoundPlayer(IStorage soundStorage)
        {
            _soundStorage = soundStorage;
        }

        public void Play(string path)
        {
            //todo: iOS 向け実装
        }
    }
}

#endif
