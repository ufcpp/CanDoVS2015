using KabeDon.Engine;
using KabeDon.Packaging;
using KabeDon.Sound;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Windows.UI.Xaml;

namespace KabeDon
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
            Unloaded += (sender, e) => _endOfGame.Cancel();
        }

        CancellationTokenSource _endOfGame = new CancellationTokenSource();

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // サーバーとデータ同期
            var servarUrl = "http://testkabedoncloudia.azurewebsites.net/";
            var m = await PackageManager.LoadAsync(servarUrl, FileStorage.Root(), new SoundPlayerFactory());
            var vm = new KabeDonViewModel(m);
            DataContext = vm;

            //↓ behavior 化したい
            Cloudia.Tapped += (_, me) =>
            {
                var position = me.GetPosition(Cloudia);
                position.X *= 1080 / Cloudia.ActualWidth;
                position.Y *= 1920 / Cloudia.ActualHeight;

                var p = new DataModels.Point((int)position.X, (int)position.Y);
                vm.TapCommand.Execute(p);
            };
            //↑ behavior 化したい

            try
            {
                while (!_endOfGame.IsCancellationRequested)
                {
                    await vm.Engine.ExecuteAsync(_endOfGame.Token);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void PlaySound(string path, KabeDonViewModel vm)
        {
            try
            {
                var media = new Windows.UI.Xaml.Controls.MediaElement();
                media.AutoPlay = false;
                media.Source = new Uri(path, UriKind.Absolute);
                media.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
