using Foundation;
using UIKit;

namespace better_origin;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    
    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        // Initialize Firebase
        //Firebase.Core.App.Configure();
        return base.FinishedLaunching(application, launchOptions);
    }
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

}