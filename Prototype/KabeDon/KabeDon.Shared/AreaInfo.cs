using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace KabeDon
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