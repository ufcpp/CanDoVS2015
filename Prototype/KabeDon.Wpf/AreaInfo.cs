using System.Windows;
using System.Windows.Controls;

namespace KabeDon.Wpf
{
    internal class AreaInfo
    {
        public MediaElement Sound { get; }
        public Rect Rect { get; }
        public Image Image { get; }
        public int Score { get; }

        public AreaInfo(Rect rect, MediaElement sound, Image image, int score)
        {
            Rect = rect;
            Sound = sound;
            Image = image;
            Score = score;
        }
    }
}