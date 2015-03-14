using KabeDon.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

            var button = new Button
            {
                Text = "click here",
            };
            button.Clicked += Button_Clicked;

            _label = new Label
            {
                XAlign = TextAlignment.Center,
                Text = "Welcome to Xamarin Forms!"
            };

            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        button,
                        _label,
                    }
                }
            };

            MainPage.Appearing += MainPage_Appearing;
        }

        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            PackageManager m = await Load();

            var level = m.Level;

            _label.Text = level.InitialState;
        }

        private Label _label;

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                //string[] list = await LoadLevelList();
                var c = new HttpClient();
                var res = await c.GetAsync(_serverUrl + "Level?name=SampleCloudia");
            }
            catch (Exception ex)
            {

            }
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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
