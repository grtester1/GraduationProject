using System;
using Android.App;
using Android.Content.PM;
using AgentVI.Droid;
using Xamarin.Forms;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Reflection;
using Octane.Xamarin.Forms.VideoPlayer.Android;
using FFImageLoading.Forms.Platform;
using AgentVI.Interfaces;

[assembly: Dependency(typeof(MainActivity))]
namespace AgentVI.Droid
{
    [Activity(Label = "AgentVI", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IBackButtonPressed
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);
            FormsVideoPlayer.Init();
            LoadApplication(new App());
        }

        public void NativeOnBackButtonPressed()
        {
            OnBackPressed();
        }

        public override void OnBackPressed()
        {
            MoveTaskToBack(true);
        }
    }
}