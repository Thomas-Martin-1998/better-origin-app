using System.ComponentModel;
using System.Runtime.CompilerServices;
using better_origin.ViewModels;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class MainPage : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuth;
    private readonly MainPageViewModel _viewModel;
    
    public MainPage(FirebaseAuthClient firebaseAuth)
    {
        InitializeComponent();
        _firebaseAuth = firebaseAuth;
        BindingContext = new MainPageViewModel();
        _viewModel = (MainPageViewModel)BindingContext;
        
    }
    
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage(_firebaseAuth));
    }
    
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        _viewModel.IsBusy = true;
        
        if (!MainPageViewModel.IsValidEmail(EmailEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a valid email", "OK");
        }
        else if (PasswordEntry?.Text == null)
        {
            await DisplayAlert("Error", "Password field cannot be empty", "OK");
        }
        else if(PasswordEntry.Text.Length < 6)
        {
            await DisplayAlert("Error", "Password must be at least 6 characters", "OK");
        }
        else
        {
            SignIn();
        }
        
        _viewModel.IsBusy = false;
    }
    
    private async void OnForgotButtonClicked(object sender, EventArgs e)
    {
        _viewModel.ToggleLabelEnabled();
        _viewModel.IsBusy = true;
        
        if (MainPageViewModel.IsValidEmail(EmailEntry.Text))
        {
            await _firebaseAuth.ResetEmailPasswordAsync(EmailEntry.Text);
            await DisplayAlert("Email Sent", "Check spam folder and verify that your account exists.", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Please enter a valid email", "OK");
        }
        
        _viewModel.IsBusy = false;
        _viewModel.ToggleLabelEnabled();
    }

    private async void SignIn()
    {
        try
        {
            await _firebaseAuth.SignInWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
        }
        catch (FirebaseAuthException ex)
        {
            Console.WriteLine(ex.Message);
            await DisplayAlert("Error", "Login failed", "OK");
        }
        
        if (_firebaseAuth.User != null)
        {
            await Navigation.PushAsync(new HomePage(_firebaseAuth));
            PasswordEntry.Text = "";
        } else Console.WriteLine("Login Failed, User null");
    }
    
    private static void TestEnv() //TODO remove
    {
       var firebaseKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY");
       Console.WriteLine($"FIREBASE_API_KEY: {firebaseKey}"); 
    }
}