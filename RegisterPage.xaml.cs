using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace better_origin;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnAlreadyLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}