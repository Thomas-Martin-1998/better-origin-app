using System.ComponentModel;
using System.Runtime.CompilerServices;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class MainPage : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuth;
    private bool _isBusy;
    public new bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged(nameof(IsBusy)); // Notify UI of property change
        }
    }
    
    public MainPage(FirebaseAuthClient firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
        InitializeComponent();
        BindingContext = this;
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
        try
        {
            IsBusy = true; // Show loading indicator
            await _firebaseAuth.SignInWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert("Error", ex.Reason.ToString(), "OK");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await DisplayAlert("Error", "An unexpected error occurred. Please try again.", "OK");
        }
        finally
        {
            IsBusy = false; // Hide loading indicator
        }
        
        if (_firebaseAuth.User != null)
        {
          await Navigation.PushAsync(new HomePage(_firebaseAuth));
          PasswordEntry.Text = "";
        } else Console.WriteLine("Login Failed"); //TODO error handler
        
    }
    
    private static void TestEnv() //TODO remove
    {
       var firebaseKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY");
       Console.WriteLine($"FIREBASE_API_KEY: {firebaseKey}"); 
    }
    
    
}