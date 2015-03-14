using Android.Media;
using System;
using System.Collections.Generic;

namespace KabeDon.XamarinForms.Droid
{
    class AndroidSoundPlayer : ISoundPlayer
    {
        // 数チャネル同時再生できるようにする？
        private readonly Dictionary<string, MediaPlayer>  _players = new Dictionary<string, MediaPlayer>();

        public void Play(string path)
        {
            try
            {
                MediaPlayer player;
            if (!_players.TryGetValue(path, out player))
            {
                player = new MediaPlayer();
                player.SetAudioStreamType(Stream.Music);
                player.SetDataSource(path);
                player.Prepare();
                    _players.Add(path, player);
            }

                if (player.IsPlaying)
                {
                    player.Stop();
                }
                player.Reset();
                player.Start();
            }
            catch (Exception ex)
            {

            }
        }
    }
}