using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;

namespace withLuckAndWisdomProject.Android
{
    [Activity(
        Label = "With Luck And Wisdom",
        Icon = "@drawable/icon",
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var game = new Main();
            // AndroidGameWindow registers its view as an Android.Views.View
            // service; that is the content view for the activity.
            SetContentView((View)game.Services.GetService(typeof(View)));
            game.Run();
        }
    }
}
