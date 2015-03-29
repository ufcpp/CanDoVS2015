using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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

            var areas = new[]
            {
                new Area { X = 396, Y = 238, Width = 320, Height =  108, Image = "Cloudia4", Sound = "chui" },
                new Area { X = 365, Y = 215, Width = 380, Height =  529, Image = "Cloudia2", Sound = "ah" },
                new Area { X = 276, Y = 340, Width =  65, Height =  415, Image = "Cloudia3", Sound = "hai" },
                new Area { X = 739, Y = 340, Width =  65, Height =  415, Image = "Cloudia3", Sound = "hai" },
                new Area { X = 271, Y = 772, Width = 590, Height = 1148, Image = "Cloudia4", Sound = "chui" },
            };

            var i = 1;

            image.PointerPressed += async (sender, arg) =>
            {
                var pos = arg.GetCurrentPoint(image);

                // 今の座標 ÷ 表示されている画像サイズ × 元の画像サイズ
                var x = pos.Position.X / image.ActualWidth * 1080;
                var y = pos.Position.Y / image.ActualHeight * 1920;

                var dialog = new MessageDialog($"({x}, {y}) をタップしました");
                await dialog.ShowAsync();

                ++i;
                if (i > 4) i = 1;

                mediaElement.Play();

                var bitmap = new BitmapImage(new Uri($"ms-appx:///Assets/Image/Cloudia{i}.png"));
                image.Source = bitmap;
            };
        }
    }
}
