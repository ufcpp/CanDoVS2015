using System.Windows;
using System.Windows.Controls;

namespace KabeDon.Wpf
{
    internal class AreaInfo
    {
        public MediaElement Sound { get; }
        public Rect Rect { get; }

        public AreaInfo(Rect rect, MediaElement sound)
        {
            this.Rect = rect;
            this.Sound = sound;
        }
    }
}