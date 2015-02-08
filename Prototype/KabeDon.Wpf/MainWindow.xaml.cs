using System;
using System.Windows;

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
                new AreaInfo(new Rect(758, 602, 116, 132), ExcellentSound),
                new AreaInfo(new Rect(786, 420, 256, 362), GoodSound),
                new AreaInfo(new Rect(936, 810,  82, 106), EasterEggSound),
                new AreaInfo(new Rect(400, 292, 352, 188), HatSound),
                new AreaInfo(new Rect(406, 466, 306, 274), FaceSound),
            };

            CloudiaImage.MouseDown += CloudiaImage_MouseDown;
        }

        private AreaInfo[] _areaSettings;
        private Rect ForbiddenArea = new Rect(142, 413, 182, 547);

        private void CloudiaImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.MouseDevice.GetPosition(CloudiaImage);
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
