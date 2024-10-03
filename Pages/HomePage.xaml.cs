using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using better_origin.ViewModels;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class HomePage : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuth;
    public HomePage(FirebaseAuthClient firebaseAuth)
    {
        
        InitializeComponent();
        _firebaseAuth = firebaseAuth;
        BindingContext = (HomePageViewModel)BindingContext;
    }
    
    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        _firebaseAuth.SignOut();
        Navigation.PopToRootAsync();
    }
}