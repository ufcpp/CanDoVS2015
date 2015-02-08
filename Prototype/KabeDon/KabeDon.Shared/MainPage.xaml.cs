using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KabeDon
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _areaSettings = new[]
            {
                new AreaInfo(new Rect(758, 602, 116, 132), ExcellentSound),
                new AreaInfo(new Rect(786, 420, 256, 362), GoodSound),
                new AreaInfo(new Rect(936, 810,  82, 106), EasterEggSound),
                new AreaInfo(new Rect(400, 292, 352, 188), HatSound),
                new AreaInfo(new Rect(406, 466, 306, 274), FaceSound),
            };

            CloudiaImage.Tapped += CloudiaImage_Tapped; ;
        }

        private AreaInfo[] _areaSettings;
        private Rect ForbiddenArea = new Rect(142, 413, 182, 547);

        private void CloudiaImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var position = e.GetPosition(CloudiaImage);
            position.X *= 1080 / CloudiaImage.ActualWidth;
            position.Y *= 1920 / CloudiaImage.ActualHeight;

            if (ForbiddenArea.Contains(position))
            {
                System.Diagnostics.Debug.WriteLine("禁止領域");
            }

            foreach (var area in _areaSettings)
            {
                if (area.Rect.Contains(position))
                {
                    System.Diagnostics.Debug.WriteLine($"プレイ { position.X}, { position.Y}");
                    area.Sound.Position = TimeSpan.Zero;
                    area.Sound.Play();
                    return;
                }
            }

            System.Diagnostics.Debug.WriteLine($"他の場所クリック { position.X}, { position.Y}");
        }
    }
}
