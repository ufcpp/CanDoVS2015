using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace KabeDon.Android
{
    [Activity(Label = "KabeDon.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var i = 0;
            var imageIds = new[]
            {
                Resource.Drawable.Cloudia1,
                Resource.Drawable.Cloudia2,
                Resource.Drawable.Cloudia3,
                Resource.Drawable.Cloudia4,
            };

            var imageView = FindViewById<ImageView>(Resource.Id.CloudiaImage);
            imageView.Click += async (sender, e) =>
            {
                imageView.Enabled = false;
                ++i;
                if (i >= imageIds.Length) i = 0;
                var imageId = imageIds[i];
                using (var bmp = await BitmapFactory.DecodeResourceAsync(Resources, imageId))
                    imageView.SetImageBitmap(bmp);
                imageView.Enabled = true;
            };
        }
    }
}

