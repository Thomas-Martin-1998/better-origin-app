using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class HomePage : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuth;
    public HomePage(FirebaseAuthClient firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
        InitializeComponent();
    }
    
    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        _firebaseAuth.SignOut();
        Navigation.PopToRootAsync();
    }
}