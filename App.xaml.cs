namespace better_origin;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
        NavigationPage.SetHasNavigationBar(MainPage, false);
    }
}