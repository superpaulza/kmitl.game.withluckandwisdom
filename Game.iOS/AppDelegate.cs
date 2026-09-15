using Foundation;
using UIKit;

namespace withLuckAndWisdomProject.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var game = new Main();
            game.Run();
            return true;
        }
    }
}
