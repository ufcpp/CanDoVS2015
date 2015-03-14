using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using KabeDon.Packaging;
using Android.Media;

namespace KabeDon.XamarinForms.Droid
{
    [Activity(Label = "KabeDon.XamarinForms", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var url = Resources.GetString(Resource.String.ServerUrl);
            var s = FileStorage.Root();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(url, s));
        }
    }
}

