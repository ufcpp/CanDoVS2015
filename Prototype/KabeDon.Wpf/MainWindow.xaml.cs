using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
                new AreaInfo(new Rect(758, 602, 116, 132), ExcellentSound, Surprised, 2),
                new AreaInfo(new Rect(786, 420, 256, 362), GoodSound, Surprised, 1),
                new AreaInfo(new Rect(936, 810,  82, 106), EasterEggSound, null, 0),
                new AreaInfo(new Rect(400, 292, 352, 188), HatSound, Angry, -1),
                new AreaInfo(new Rect(406, 466, 306, 274), FaceSound, Embarrassed, -1),
                new AreaInfo(new Rect(0, 0, 1080, 1920), GoodSound, null, 0),
            };

            Cloudia.MouseDown += CloudiaImage_MouseDown;
        }

        private AreaInfo[] _areaSettings;
        private Rect ForbiddenArea = new Rect(142, 413, 182, 547);

        private async void CloudiaImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.MouseDevice.GetPosition(Cloudia);
            position.X *= 1080 / Cloudia.ActualWidth;
            position.Y *= 1920 / Cloudia.ActualHeight;

            if (ForbiddenArea.Contains(position))
            {
                System.Diagnostics.Debug.WriteLine("禁止領域");
                return;
            }

            foreach (var area in _areaSettings)
            {
                if (area.Rect.Contains(position))
                {
                    System.Diagnostics.Debug.WriteLine($"プレイ { position.X}, { position.Y}");
                    area.Sound.Position = TimeSpan.Zero;
                    area.Sound.Play();

                    if (area.Image != null)
                    {
                        Normal.Visibility = Visibility.Hidden;
                        area.Image.Visibility = Visibility.Visible;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        area.Image.Visibility = Visibility.Hidden;
                        Normal.Visibility = Visibility.Visible;
                    }

                    return;
                }
            }

            System.Diagnostics.Debug.WriteLine($"他の場所クリック { position.X}, { position.Y}");
        }
    }
}
