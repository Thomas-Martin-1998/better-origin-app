using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Firebase.Auth;

namespace better_origin.ViewModels;

public partial class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private bool _isBusy;
    private bool _isForgotLabelEnabled = true;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
        }
    }
    public bool IsForgotLabelEnabled
    {
        get => _isForgotLabelEnabled;
        set
        {
            _isForgotLabelEnabled = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsForgotLabelEnabled)));
        }
    }

    public void ToggleLabelEnabled()
    {
        IsForgotLabelEnabled = !IsForgotLabelEnabled;
    }
    
    public static bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && MyRegex().IsMatch(email);
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex MyRegex();
}