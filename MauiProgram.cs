using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;
using dotenv.net;

namespace better_origin;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        DotEnv.Load();
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyBWoQLLF-gSpC4p3xN413WVSWK5zZDaWyI",
            AuthDomain = "better-origin-firebase.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
    
        }));
        return builder.Build();
    }
}
