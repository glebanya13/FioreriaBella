using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace FioreriaBella.MVVM.ViewModels
{
  public class UserDialogViewModel : BaseViewModel
  {
    private readonly User? _original;

    private string username;
    private string email;
    private string password;
    private bool isAdmin;

    private string usernameValidationHint = "";
    private string emailValidationHint = "";
    private string passwordValidationHint = "";

    private Brush usernameValidationColor = Brushes.Transparent;
    private Brush emailValidationColor = Brushes.Transparent;
    private Brush passwordValidationColor = Brushes.Transparent;

    private bool usernameTouched = false;
    private bool emailTouched = false;
    private bool passwordTouched = false;

    public string Username
    {
      get => username;
      set
      {
        SetProperty(ref username, value);
        usernameTouched = true;
        ValidateUsername();
      }
    }

    public string Email
    {
      get => email;
      set
      {
        SetProperty(ref email, value);
        emailTouched = true;
        ValidateEmail();
      }
    }

    public string Password
    {
      get => password;
      set
      {
        SetProperty(ref password, value);
        passwordTouched = true;
        ValidatePassword();
      }
    }

    public bool IsAdmin
    {
      get => isAdmin;
      set => SetProperty(ref isAdmin, value);
    }

    public string UsernameValidationHint
    {
      get => usernameValidationHint;
      set => SetProperty(ref usernameValidationHint, value);
    }

    public string EmailValidationHint
    {
      get => emailValidationHint;
      set => SetProperty(ref emailValidationHint, value);
    }

    public string PasswordValidationHint
    {
      get => passwordValidationHint;
      set => SetProperty(ref passwordValidationHint, value);
    }

    public Brush UsernameValidationColor
    {
      get => usernameValidationColor;
      set => SetProperty(ref usernameValidationColor, value);
    }

    public Brush EmailValidationColor
    {
      get => emailValidationColor;
      set => SetProperty(ref emailValidationColor, value);
    }

    public Brush PasswordValidationColor
    {
      get => passwordValidationColor;
      set => SetProperty(ref passwordValidationColor, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event Action<bool, User?> RequestClose;

    public UserDialogViewModel(User? user = null)
    {
      _original = user;

      username = user?.Username ?? string.Empty;
      email = user?.Email ?? string.Empty;
      password = user?.Password ?? string.Empty;
      isAdmin = user?.IsAdmin ?? false;

      SaveCommand = new RelayCommand(_ => Save());
      CancelCommand = new RelayCommand(_ => Cancel());
    }

    private void ValidateUsername()
    {
      if (!usernameTouched)
      {
        UsernameValidationHint = "";
        UsernameValidationColor = Brushes.Transparent;
        return;
      }

      if (string.IsNullOrWhiteSpace(Username))
      {
        UsernameValidationHint = "Имя обязательно";
        UsernameValidationColor = Brushes.Red;
      }
      else if (Username.Length < 3)
      {
        UsernameValidationHint = "Слишком короткое имя";
        UsernameValidationColor = Brushes.Red;
      }
      else
      {
        UsernameValidationHint = "✓";
        UsernameValidationColor = Brushes.Green;
      }
    }

    private void ValidateEmail()
    {
      if (!emailTouched)
      {
        EmailValidationHint = "";
        EmailValidationColor = Brushes.Transparent;
        return;
      }

      if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
      {
        EmailValidationHint = "Некорректный email";
        EmailValidationColor = Brushes.Red;
      }
      else
      {
        EmailValidationHint = "✓";
        EmailValidationColor = Brushes.Green;
      }
    }

    private void ValidatePassword()
    {
      if (!passwordTouched)
      {
        PasswordValidationHint = "";
        PasswordValidationColor = Brushes.Transparent;
        return;
      }

      if (string.IsNullOrWhiteSpace(Password) || Password.Length < 5)
      {
        PasswordValidationHint = "Пароль должен быть не менее 5 символов";
        PasswordValidationColor = Brushes.Red;
      }
      else
      {
        PasswordValidationHint = "✓";
        PasswordValidationColor = Brushes.Green;
      }
    }

    private void Save()
    {
      usernameTouched = true;
      emailTouched = true;
      passwordTouched = true;

      ValidateUsername();
      ValidateEmail();
      ValidatePassword();

      if (UsernameValidationColor != Brushes.Green ||
          EmailValidationColor != Brushes.Green ||
          PasswordValidationColor != Brushes.Green)
        return;

      var user = new User
      {
        Id = _original?.Id ?? 0,
        Username = Username.Trim(),
        Email = Email.Trim(),
        Password = Password.Trim(),
        IsAdmin = IsAdmin
      };

      RequestClose?.Invoke(true, user);
    }

    private void Cancel()
    {
      RequestClose?.Invoke(false, null);
    }
  }
}
