using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuth;
    public RegisterPage(FirebaseAuthClient firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
        InitializeComponent();
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
        await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
        
        if (_firebaseAuth.User != null)
        { 
            Navigation.InsertPageBefore(new HomePage(_firebaseAuth), this);
            await Navigation.PopAsync();
        } else Console.WriteLine("SignUp Failed"); //Todo error handler
    }

}