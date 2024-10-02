using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class RegisterPage : ContentPage
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
    public RegisterPage(FirebaseAuthClient firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnAlreadyLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    
    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        if (PasswordEntry.Text == ConfirmPasswordEntry.Text)
        {
            SignUp();
        } //TODO add else
    }

    private async void SignUp()
    {
        try
        {
            await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
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
        
        
        if (_firebaseAuth.User != null)
        { 
            Navigation.InsertPageBefore(new HomePage(_firebaseAuth), this);
            await Navigation.PopAsync();
        } else Console.WriteLine("SignUp Failed"); //Todo error handler
    }

}