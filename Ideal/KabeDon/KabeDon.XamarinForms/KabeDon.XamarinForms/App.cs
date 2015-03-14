using KabeDon.Engine;
using KabeDon.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KabeDon.XamarinForms
{
    public class App : Application
    {
        private string _serverUrl;
        private IStorage _root;

        public App(string serverUrl = null, IStorage root = null)
        {
            _serverUrl = serverUrl;
            _root = root;

            //var button = new Button
            //{
            //    Text = "click here",
            //};
            //button.Clicked += Button_Clicked;

            //_label = new Label
            //{
            //    XAlign = TextAlignment.Center,
            //    Text = "Welcome to Xamarin Forms!"
            //};

            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            button,
            //            _label,
            //        }
            //    }
            //};
        }

        //private Label _label;

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //string[] list = await LoadLevelList();
        //        var c = new HttpClient();
        //        var res = await c.GetAsync(_serverUrl + "Level?name=SampleCloudia");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        CancellationTokenSource _endOfGame = new CancellationTokenSource();

        protected override async void OnStart()
        {
            PackageManager m = await Load();

            var vm = new KabeDonViewModel(m);

            var image = new Image();

            var page = new ContentPage
            {
                Content = new Grid
                {
                    Children =
                    {
                        image,
                        new StackLayout
                        {
                        }
                    },
                },
            };
            MainPage = page;
            page.BindingContext = vm;

            SetBinding(vm, nameof(vm.Image), image, (s, t) => t.Source = ImageSource.FromFile(s.Image));

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

        private static void SetBinding<TTarget>(KabeDonViewModel vm, string sourceName, TTarget target, Action<KabeDonViewModel, TTarget> bind)
        {
            bind(vm, target);

            vm.PropertyChanged += (sender, arg) =>
            {
                if (arg.PropertyName == sourceName) bind(vm, target);
            };
        }

        protected override void OnSleep()
        {
            // 今のところ、resume 機能なし。スリープした瞬間終了。
            _endOfGame.Cancel();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async Task<PackageManager> Load()
        {
            await PackageManager.Synchronize(_serverUrl, _root);

            //todo: 複数のレベルを読める場合、どれを読むかの選択。今は1個目固定。
            var paths = await _root.GetSubfolderPathsAsync();
            var first = paths.First();
            var levelFolder = await _root.GetSubfolderAsync(new Uri(first, UriKind.Absolute));

            var m = new PackageManager();
            await m.LoadFrom(levelFolder);
            return m;
        }

        private async Task<string[]> LoadLevelList()
        {
            var c = new HttpClient();
            var res = await c.GetAsync(_serverUrl + "Level");
            var content = await res.Content.ReadAsStringAsync();
            var list = content.Split(',');
            return list;
        }
    }
}
