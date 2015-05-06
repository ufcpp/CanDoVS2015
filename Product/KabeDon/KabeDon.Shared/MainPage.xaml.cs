using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

            image.Tapped += async (sender, arg) =>
            {
                image.IsTapEnabled = false;

                var pos = arg.GetPosition(image);

                HitTest(pos);

                await Task.Delay(1000);
                image.IsTapEnabled = true;

                ShowImage(new Uri("ms-appx:///Assets/Image/Cloudia1.png"));
            };
        }

        private void HitTest(Point pos)
        {
            // 今の座標 ÷ 表示されている画像サイズ × 元の画像サイズ
            var x = pos.X / image.ActualWidth * 1080;
            var y = pos.Y / image.ActualHeight * 1920;

            foreach (var area in areas)
            {
                if (area.X < x && x < area.X + area.Width
                    && area.Y < y && y < area.Y + area.Height)
                {
                    OnHit(area);
                    break;
                }
            }
        }

        private void OnHit(Area area)
        {
            var mediaUri = new Uri($"ms-appx:///Assets/Sound/{area.Sound}.mp3");
            PlaySound(mediaUri);

            var imageUri = new Uri($"ms-appx:///Assets/Image/{area.Image}.png");
            ShowImage(imageUri);
        }

        static readonly Area[] areas = new[]
        {
            new Area { X = 396, Y = 238, Width = 320, Height =  108, Image = "Cloudia4", Sound = "ah" },
            new Area { X = 365, Y = 215, Width = 380, Height =  529, Image = "Cloudia2", Sound = "ah" },
            new Area { X = 276, Y = 340, Width =  65, Height =  415, Image = "Cloudia3", Sound = "chui" },
            new Area { X = 739, Y = 340, Width =  65, Height =  415, Image = "Cloudia3", Sound = "chui" },
            new Area { X = 271, Y = 772, Width = 590, Height = 1148, Image = "Cloudia4", Sound = "oujougiwa" },
        };

        private async void ShowImage(Uri imageUri)
        {
            var bitmap = new BitmapImage();
            var file = await StorageFile.GetFileFromApplicationUriAsync(imageUri);
            var stream = await file.OpenReadAsync();
            await bitmap.SetSourceAsync(stream);
            image.Source = bitmap;
        }

        //MediaElement media = new MediaElement();

        private async void PlaySound(Uri mediaUri)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(mediaUri);
            var stream = await file.OpenReadAsync();
            mediaElement.SetSource(stream, file.ContentType);
            mediaElement.Play();
        }
    }
}
