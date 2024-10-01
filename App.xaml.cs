using better_origin.Pages;
using Firebase.Auth;

namespace better_origin;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        var authClient = serviceProvider.GetRequiredService<FirebaseAuthClient>();
        MainPage = new NavigationPage(new MainPage(authClient));
    }
}