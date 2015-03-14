using Android.Media;

namespace KabeDon.XamarinForms.Droid
{
    class AndroidSoundPlayer : ISoundPlayer
    {
        // 数チャネル同時再生できるようにする？
        MediaPlayer _player = new MediaPlayer();

        public void Play(string path)
        {
            _player.SetDataSource(path);
            _player.Start();
        }
    }
}