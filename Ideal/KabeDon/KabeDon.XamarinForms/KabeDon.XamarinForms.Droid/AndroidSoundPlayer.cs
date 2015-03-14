using Android.Media;

namespace KabeDon.XamarinForms.Droid
{
    class AndroidSoundPlayer : ISoundPlayer
    {
        // ”ƒ`ƒƒƒlƒ‹“¯Ä¶‚Å‚«‚é‚æ‚¤‚É‚·‚éH
        MediaPlayer _player = new MediaPlayer();

        public void Play(string path)
        {
            _player.SetDataSource(path);
            _player.Start();
        }
    }
}