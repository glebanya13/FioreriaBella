using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class RegisterViewModel : BaseViewModel
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSessionService _userSessionService;

    private string username;
    private string password;
    private string confirmPassword;
    private string email;

    private string errorMessage;
    private string emailValidationHint;
    private string usernameValidationHint;
    private string passwordValidationHint;
    private string confirmPasswordValidationHint;

    private string emailValidationColor;
    private string usernameValidationColor;
    private string passwordValidationColor;
    private string confirmPasswordValidationColor;

    public string Username
    {
      get => username;
      set
      {
        Debug.WriteLine($"Username set to '{value}'");
        SetProperty(ref username, value);
        ValidateUsername();
      }
    }

    public string Email
    {
      get => email;
      set
      {
        Debug.WriteLine($"Email set to '{value}'");
        SetProperty(ref email, value);
        ValidateEmail();
      }
    }


    public string Password
    {
      get => password;
      set
      {
        SetProperty(ref password, value);
        ValidatePassword();
        ValidateConfirmPassword();
      }
    }

    public string ConfirmPassword
    {
      get => confirmPassword;
      set
      {
        SetProperty(ref confirmPassword, value);
        ValidateConfirmPassword();
      }
    }

    public string EmailValidationHint { get => emailValidationHint; set => SetProperty(ref emailValidationHint, value); }
    public string UsernameValidationHint { get => usernameValidationHint; set => SetProperty(ref usernameValidationHint, value); }
    public string PasswordValidationHint { get => passwordValidationHint; set => SetProperty(ref passwordValidationHint, value); }
    public string ConfirmPasswordValidationHint { get => confirmPasswordValidationHint; set => SetProperty(ref confirmPasswordValidationHint, value); }

    public string EmailValidationColor { get => emailValidationColor; set => SetProperty(ref emailValidationColor, value); }
    public string UsernameValidationColor { get => usernameValidationColor; set => SetProperty(ref usernameValidationColor, value); }
    public string PasswordValidationColor { get => passwordValidationColor; set => SetProperty(ref passwordValidationColor, value); }
    public string ConfirmPasswordValidationColor { get => confirmPasswordValidationColor; set => SetProperty(ref confirmPasswordValidationColor, value); }

    public string ErrorMessage { get => errorMessage; set => SetProperty(ref errorMessage, value); }

    public ICommand RegisterCommand { get; }
    public ICommand NavigateToLoginCommand { get; }

    public event Action RegistrationSuccessful;
    public event Action NavigateToLoginRequested;

    public RegisterViewModel(IUnitOfWork unitOfWork, UserSessionService userSessionService)
    {
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      _userSessionService = userSessionService ?? throw new ArgumentNullException(nameof(userSessionService));

      RegisterCommand = new RelayCommand(Register);
      NavigateToLoginCommand = new RelayCommand(_ => NavigateToLogin());

      EmailValidationColor = "Red";
      UsernameValidationColor = "Red";
      PasswordValidationColor = "Red";
      ConfirmPasswordValidationColor = "Red";
    }

    private void ValidateEmail()
    {
      if (string.IsNullOrWhiteSpace(Email))
      {
        EmailValidationHint = "Email не может быть пустым";
        EmailValidationColor = "Red";
      }
      else if (!IsValidEmail(Email))
      {
        EmailValidationHint = "Введите корректный email";
        EmailValidationColor = "Red";
      }
      else if (_unitOfWork.Users.Find(u => u.Email == Email).Any())
      {
        EmailValidationHint = "Пользователь с таким email уже существует";
        EmailValidationColor = "Red";
      }
      else
      {
        EmailValidationHint = "✓";
        EmailValidationColor = "Green";
      }
    }

    private void ValidateUsername()
    {
      if (string.IsNullOrWhiteSpace(Username))
      {
        UsernameValidationHint = "Имя пользователя не может быть пустым";
        UsernameValidationColor = "Red";
      }
      else if (Username.Length < 3)
      {
        UsernameValidationHint = "Имя пользователя должно содержать минимум 3 символа";
        UsernameValidationColor = "Red";
      }
      else if (_unitOfWork.Users.Find(u => u.Username == Username).Any())
      {
        UsernameValidationHint = "Пользователь с таким именем уже существует";
        UsernameValidationColor = "Red";
      }
      else
      {
        UsernameValidationHint = "✓";
        UsernameValidationColor = "Green";
      }
    }

    private void ValidatePassword()
    {
      if (string.IsNullOrWhiteSpace(Password))
      {
        PasswordValidationHint = "Пароль не может быть пустым";
        PasswordValidationColor = "Red";
      }
      else if (Password.Length < 6)
      {
        PasswordValidationHint = "Пароль должен содержать минимум 6 символов";
        PasswordValidationColor = "Red";
      }
      else
      {
        PasswordValidationHint = "✓";
        PasswordValidationColor = "Green";
      }
    }

    private void ValidateConfirmPassword()
    {
      if (string.IsNullOrWhiteSpace(ConfirmPassword))
      {
        ConfirmPasswordValidationHint = "Подтвердите пароль";
        ConfirmPasswordValidationColor = "Red";
      }
      else if (Password != ConfirmPassword)
      {
        ConfirmPasswordValidationHint = "Пароли не совпадают";
        ConfirmPasswordValidationColor = "Red";
      }
      else
      {
        ConfirmPasswordValidationHint = "✓";
        ConfirmPasswordValidationColor = "Green";
      }
    }

    private bool IsValidEmail(string email)
    {
      if (string.IsNullOrWhiteSpace(email))
        return false;

      if (email.Contains("/") || email.Contains("\\") || email.Contains(" ") ||
          email.Contains("#") || email.Contains("?"))
      {
        return false;
      }

      string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
      if (!Regex.IsMatch(email, pattern))
        return false;

      string domainPart = email.Split('@').Last();
      string[] domainParts = domainPart.Split('.', StringSplitOptions.RemoveEmptyEntries);

      if (domainParts.Length < 2 || domainParts.Length > 4)
        return false;

      string tld = domainParts.Last();
      if (tld.Length < 2)
        return false;

      for (int i = 1; i < domainParts.Length; i++)
      {
        if (domainParts[i].Length <= 3 && domainParts[i - 1].Length <= 3)
          return false;
      }

      return true;
    }

    private void Register(object parameter)
    {
      try
      {
        if (parameter is PasswordBox confirmPasswordBox)
        {
          ConfirmPassword = confirmPasswordBox.Password;
        }

        ValidateEmail();
        ValidateUsername();
        ValidatePassword();
        ValidateConfirmPassword();

        if (EmailValidationColor == "Red" || UsernameValidationColor == "Red" ||
            PasswordValidationColor == "Red" || ConfirmPasswordValidationColor == "Red")
        {
          ErrorMessage = "Пожалуйста, исправьте ошибки в форме";
          return;
        }

        var newUser = new User
        {
          Username = Username,
          Password = Password,
          Email = Email,
          IsAdmin = false
        };

        _unitOfWork.Users.Add(newUser);
        _unitOfWork.SaveChanges();

        _userSessionService.SetUser(newUser);
        RegistrationSuccessful?.Invoke();
      }
      catch (Exception ex)
      {
        ErrorMessage = $"Ошибка при регистрации: {ex.Message}";
      }
    }

    private void NavigateToLogin()
    {
      NavigateToLoginRequested?.Invoke();
    }
  }
}
