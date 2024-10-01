using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;

namespace better_origin.Pages;

public partial class HomePage : ContentPage
{
    public HomePage(FirebaseAuthClient firebaseAuth)
    {
        InitializeComponent();
    }
}