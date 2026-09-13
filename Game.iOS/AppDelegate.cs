using Foundation;
using Microsoft.Xna.Framework;
using UIKit;

namespace withLuckAndWisdomProject.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private Main _game;
        private GameWindow _window;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            _game = new Main();
            _window = new GameWindow(_game);
            _window.MakeKeyAndVisible();
            _game.Run();
            return true;
        }
    }
}
