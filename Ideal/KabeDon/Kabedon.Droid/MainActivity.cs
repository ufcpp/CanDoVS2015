using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using KabeDon.Packaging;
using System.Threading.Tasks;
using KabeDon.Engine;
using System.Net.Http;
using System.Linq;
using System.Threading;
using KabeDon.Sound;

namespace Kabedon.Droid
{
    [Activity(Label = "Kabedon.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        CancellationTokenSource _endOfGame = new CancellationTokenSource();

        protected override void OnStop()
        {
            base.OnStop();
            _endOfGame.Cancel();
        }

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var url = Resources.GetString(Resource.String.ServerUrl);
            var root = FileStorage.Root();

            var m = await Load(url, root);

            var vm = new KabeDonViewModel(m);

            var image = FindViewById<ImageView>(Resource.Id.CloudiaImage);
            SetBinding(vm, nameof(vm.Image), image, (s, t) =>
            {
                if (s.Image == null) return;

                var file = new Java.IO.File(s.Image);
                var uri = Android.Net.Uri.FromFile(file);
                t.SetImageURI(uri);
            });

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

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

        public override bool OnTouchEvent(MotionEvent e)
        {
            return base.OnTouchEvent(e);
        }

        private static void SetBinding<TTarget>(KabeDonViewModel vm, string sourceName, TTarget target, Action<KabeDonViewModel, TTarget> bind)
        {
            bind(vm, target);

            vm.PropertyChanged += (sender, arg) =>
            {
                if (arg.PropertyName == sourceName) bind(vm, target);
            };
        }

        private async Task<PackageManager> Load(string serverUrl, IStorage root)
        {
            await KabeDon.Packaging.PackageManager.Synchronize(serverUrl, root);

            //todo: 複数のレベルを読める場合、どれを読むかの選択。今は1個目固定。
            var paths = await root.GetSubfolderPathsAsync();
            var first = paths.First();
            var levelFolder = await root.GetSubfolderAsync(new Uri(first, UriKind.Absolute));

            var m = new PackageManager();
            await m.LoadFrom(levelFolder, new SoundPlayerFactory());
            return m;
        }

        private async Task<string[]> LoadLevelList(string serverUrl)
        {
            var c = new HttpClient();
            var res = await c.GetAsync(serverUrl + "Level");
            var content = await res.Content.ReadAsStringAsync();
            var list = content.Split(',');
            return list;
        }
    }
}

