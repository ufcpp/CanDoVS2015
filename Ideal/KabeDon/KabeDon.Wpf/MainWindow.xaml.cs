using KabeDon.Engine;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Reactive.Linq;
using MediaPlayer = System.Windows.Media.MediaPlayer;

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
            var m = new Packages.PackageManager();

            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var path = Path.Combine(desktop, "temp/KabeDonSample");
            await m.LoadFrom(new FileStorage(path));

            var engine = new KabeDonEngine();
            DataContext = engine;

            //↓ behavior 化したい
            engine.SoundRequested.SubscribeOn(SynchronizationContext.Current).Select(name => m.GetSound(name)).Subscribe(PlaySound);
            Cloudia.MouseDown += (_, me) =>
            {
                var position = me.GetPosition(Cloudia);
                position.X *= 1080 / Cloudia.ActualWidth;
                position.Y *= 1920 / Cloudia.ActualHeight;

                var p = new DataModels.Point((int)position.X, (int)position.Y);
                engine.TapCommand.Execute(p);
            };
            //↑ behavior 化したい

            await engine.ExecuteAsync(m, _endOfGame.Token);
        }

        private void PlaySound(string path)
        {
            var player = new MediaPlayer();
            player.Open(new Uri(path, UriKind.Absolute));
            player.Play();
        }
    }
}
