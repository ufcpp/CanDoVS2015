using System;
using System.Windows;
using System.Windows.Media;

namespace KabeDon.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            _areaSettings = new[]
            {
                new AreaInfo(new Rect(379, 301, 58, 66), ExcellentSound),
                new AreaInfo(new Rect(393, 210, 128, 181), GoodSound),
                new AreaInfo(new Rect(468, 405, 41, 53), EasterEggSound),
                new AreaInfo(new Rect(200, 146, 176, 94), HatSound),
                new AreaInfo(new Rect(203, 233, 153, 137), FaceSound),
            };

            CloudiaImage.MouseDown += CloudiaImage_MouseDown;
        }

        private AreaInfo[] _areaSettings;

        private Rect ExcellentKabeDon = new Rect(379, 301, 58, 66);
        private Rect GoodKabeDon = new Rect(393, 210, 128, 181);
        private Rect EasterEgg = new Rect(468, 405, 41, 53);
        private Rect Hat = new Rect(200, 146, 176, 94);
        private Rect Face = new Rect(203, 233, 153, 137);
        private Rect ForbiddenArea = new Rect(142, 413, 182, 547);

        private void CloudiaImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.MouseDevice.GetPosition(CloudiaImage);

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
