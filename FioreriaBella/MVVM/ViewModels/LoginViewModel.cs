using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class LoginViewModel : BaseViewModel
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSessionService _userSessionService;

    private string email;
    private string password;
    private string emailValidationHint;
    private string passwordValidationHint;
    private string emailValidationColor = "Red";
    private string passwordValidationColor = "Red";
    private string errorMessage;

    public string Email
    {
      get => email;
      set
      {
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
      }
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

    public string EmailValidationColor
    {
      get => emailValidationColor;
      set => SetProperty(ref emailValidationColor, value);
    }

    public string PasswordValidationColor
    {
      get => passwordValidationColor;
      set => SetProperty(ref passwordValidationColor, value);
    }

    public string ErrorMessage
    {
      get => errorMessage;
      set => SetProperty(ref errorMessage, value);
    }

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }

    public event Action RegisterRequested;
    public event Action LoginSuccess;

    public LoginViewModel(IUnitOfWork unitOfWork, UserSessionService userSessionService)
    {
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      _userSessionService = userSessionService ?? throw new ArgumentNullException(nameof(userSessionService));

      LoginCommand = new RelayCommand(_ => Login());
      RegisterCommand = new RelayCommand(_ => Register());

      EmailValidationColor = "Red";
      PasswordValidationColor = "Red";
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
      else
      {
        EmailValidationHint = "✓";
        EmailValidationColor = "Green";
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

    private void Login()
    {
      ErrorMessage = string.Empty;

      if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
      {
        ErrorMessage = "Пожалуйста, заполните все поля";
        return;
      }

      var user = _unitOfWork.Users
          .Find(u => u.Email == Email && u.Password == Password)
          .FirstOrDefault();

      if (user != null)
      {
        _userSessionService.SetUser(user);

        LoginSuccess?.Invoke();
      }
      else
      {
        EmailValidationHint = "✖ Проверьте email";
        EmailValidationColor = "Red";

        PasswordValidationHint = "✖ Проверьте пароль";
        PasswordValidationColor = "Red";

        ErrorMessage = "Неверный email или пароль";
      }
    }

    private void Register()
    {
      RegisterRequested?.Invoke();
    }
  }
}
