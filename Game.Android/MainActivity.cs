using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Xna.Framework;

namespace withLuckAndWisdomProject.Android
{
    [Activity(
        Label = "With Luck And Wisdom",
        Icon = "@mipmap/icon",
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : AndroidGameActivity
    {
        private Main _game;
        private GameWindow _window;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new Main();
            _window = new GameWindow(this, _game);
            SetContentView(_window);
            _game.Run();
        }
    }
}
