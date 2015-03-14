using KabeDon.Engine;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Reactive.Linq;
using MediaPlayer = System.Windows.Media.MediaPlayer;
using KabeDon.Packaging;

namespace KabeDon.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            Closed += (sender, e) => _endOfGame.Cancel();
        }

        CancellationTokenSource _endOfGame = new CancellationTokenSource();

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var m = new PackageManager();

            var folder = Path.GetDirectoryName(typeof(MainWindow).Assembly.Location);
            var path = Path.Combine(folder, "仮データ");
            await m.LoadFrom(FileStorage.Level(path));

            var vm = new KabeDonViewModel(m);
            DataContext = vm;

            //↓ behavior 化したい
            vm.SoundRequested.SubscribeOn(SynchronizationContext.Current).Subscribe(PlaySound);
            Cloudia.MouseDown += (_, me) =>
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

        private void PlaySound(string path)
        {
            var player = new MediaPlayer();
            player.Open(new Uri(path, UriKind.Absolute));
            player.Play();
        }
    }
}
