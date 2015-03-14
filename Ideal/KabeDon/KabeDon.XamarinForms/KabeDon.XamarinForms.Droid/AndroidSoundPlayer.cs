using Android.Media;
using System;
using System.Collections.Generic;

namespace KabeDon.XamarinForms.Droid
{
    class AndroidSoundPlayer : ISoundPlayer
    {
        private SoundPool _pool = new SoundPool(20, Stream.Music, 0);
        private readonly Dictionary<string, int>  _soundIds = new Dictionary<string, int>();

        public void Play(string path)
        {
            try
            {
                int id;
                if (!_soundIds.TryGetValue(path, out id))
                {
                    id = _pool.Load(path, 1);
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