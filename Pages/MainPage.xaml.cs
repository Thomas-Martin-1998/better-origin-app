using Firebase.Auth;

namespace better_origin.Pages;

public partial class MainPage : ContentPage
{
    
    private readonly FirebaseAuthClient _firebaseAuth;
    
    public MainPage(FirebaseAuthClient firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
        InitializeComponent();
    }
    
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage(_firebaseAuth));
    }
    
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        SignIn();
    }
    
    private void OnForgotButtonClicked(object sender, EventArgs e)
    {
        _firebaseAuth.ResetEmailPasswordAsync(EmailEntry.Text);
        Console.WriteLine("Email sent if email valid");
        //TODO add popup saying email sent
    }

    private async void SignIn()
    {
        await _firebaseAuth.SignInWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
        
        if (_firebaseAuth.User != null)
        {
          await Navigation.PushAsync(new HomePage(_firebaseAuth));  
        } else Console.WriteLine("Login Failed"); //TODO error handler
        
    }
    
    private static void TestEnv() //TODO remove
    {
       var firebaseKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY");
       Console.WriteLine($"FIREBASE_API_KEY: {firebaseKey}"); 
    }
}