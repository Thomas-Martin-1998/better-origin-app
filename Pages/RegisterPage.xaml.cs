using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using better_origin.ViewModels;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuth;
    private readonly MainPageViewModel _viewModel;
   
    public RegisterPage(FirebaseAuthClient firebaseAuth)
    {
        
        InitializeComponent();
        _firebaseAuth = firebaseAuth;
        BindingContext = new MainPageViewModel(); //We can share a view model with Login page
        _viewModel = (MainPageViewModel)BindingContext;
    }

    private async void OnAlreadyLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        _viewModel.IsBusy = true;
        
        if (EmailEntry?.Text == null || MainPageViewModel.IsValidEmail(EmailEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a valid email", "OK");
        }
        else if (PasswordEntry?.Text == null || ConfirmPasswordEntry?.Text == null)
        {
            await DisplayAlert("Error", "Password fields cannot be empty", "OK");
        }
        else if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            await DisplayAlert("Error", "Passwords do not match", "OK");
        }
        else if(PasswordEntry.Text.Length < 6)
        {
            await DisplayAlert("Error", "Password must be at least 6 characters", "OK");
        }
        else
        {
            SignUp();
        }
        
        _viewModel.IsBusy = false;
    }

    private async void SignUp()
    {
        try
        {
            await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
        }
        catch (FirebaseAuthException ex)
        {
            Console.WriteLine(ex);
            await DisplayAlert("Error", "Sign up failed", "OK");
        }
        
        if (_firebaseAuth.User != null)
        { 
            Navigation.InsertPageBefore(new HomePage(_firebaseAuth), this);
            await Navigation.PopAsync();
        } else Console.WriteLine("SignUp Failed, User null");
    }

}