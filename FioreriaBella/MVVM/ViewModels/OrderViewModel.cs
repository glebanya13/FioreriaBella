using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Input;

public class OrderViewModel : BaseViewModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSessionService _sessionService;

    public string CustomerName
    {
        get => _customerName;
        set { SetProperty(ref _customerName, value); ValidateCustomerName(); }
    }
    private string _customerName = string.Empty;

    public string Address
    {
        get => _address;
        set { SetProperty(ref _address, value); ValidateAddress(); }
    }
    private string _address = string.Empty;

    public string PaymentMethod
    {
        get => _paymentMethod;
        set { SetProperty(ref _paymentMethod, value); ValidatePaymentMethod(); }
    }
    private string _paymentMethod = "Наличные";

    public decimal TotalAmount
    {
        get => _totalAmount;
        set => SetProperty(ref _totalAmount, value);
    }
    private decimal _totalAmount;

    public string Summary
    {
        get => _summary;
        set => SetProperty(ref _summary, value);
    }
    private string _summary = string.Empty;

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }
    private string _statusMessage = string.Empty;

    public string CustomerNameHint { get => _customerNameHint; set => SetProperty(ref _customerNameHint, value); }
    private string _customerNameHint = string.Empty;

    public string AddressHint { get => _addressHint; set => SetProperty(ref _addressHint, value); }
    private string _addressHint = string.Empty;

    public string PaymentMethodHint { get => _paymentMethodHint; set => SetProperty(ref _paymentMethodHint, value); }
    private string _paymentMethodHint = string.Empty;

    public string CustomerNameColor { get => _customerNameColor; set => SetProperty(ref _customerNameColor, value); }
    private string _customerNameColor = "Red";

    public string AddressColor { get => _addressColor; set => SetProperty(ref _addressColor, value); }
    private string _addressColor = "Red";

    public string PaymentMethodColor { get => _paymentMethodColor; set => SetProperty(ref _paymentMethodColor, value); }
    private string _paymentMethodColor = "Red";

    public ICommand ConfirmOrderCommand { get; }
    public ICommand BackCommand { get; }

    public event Action? OrderConfirmed;
    public event Action? BackRequested;

    public OrderViewModel(IUnitOfWork unitOfWork, UserSessionService sessionService)
    {
        _unitOfWork = unitOfWork;
        _sessionService = sessionService;

        ConfirmOrderCommand = new RelayCommand(_ => ConfirmOrder());
        BackCommand = new RelayCommand(_ => BackRequested?.Invoke());

        LoadSummary();
    }

    private void LoadSummary()
    {
        var userId = _sessionService.CurrentUser?.Id;
        if (userId == null)
        {
            Summary = "Пользователь не авторизован.";
            return;
        }

        var cartItems = _unitOfWork.Carts.GetAll().Where(c => c.UserId == userId).ToList();
        if (!cartItems.Any())
        {
            Summary = "Корзина пуста.";
            return;
        }

        decimal total = 0;
        var sb = new StringBuilder();

        foreach (var item in cartItems)
        {
            if (item.Product != null)
            {
                var lineTotal = item.Product.Price * item.Quantity;
                total += lineTotal;
                sb.AppendLine($"{item.Product.Name} x {item.Quantity} = ₽{lineTotal:F2}");
            }
        }

        sb.AppendLine($"\nИтого: ₽{total:F2}");
        Summary = sb.ToString();
        TotalAmount = total;
    }

    private void ValidateCustomerName()
    {
        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            CustomerNameHint = "Введите имя";
            CustomerNameColor = "Red";
        }
        else if (CustomerName.Length < 3)
        {
            CustomerNameHint = "Имя должно содержать минимум 3 символа";
            CustomerNameColor = "Red";
        }
        else
        {
            CustomerNameHint = "✓";
            CustomerNameColor = "Green";
        }
    }

    private void ValidateAddress()
    {
        if (string.IsNullOrWhiteSpace(Address))
        {
            AddressHint = "Введите адрес";
            AddressColor = "Red";
        }
        else if (Address.Length < 5)
        {
            AddressHint = "Адрес слишком короткий";
            AddressColor = "Red";
        }
        else
        {
            AddressHint = "✓";
            AddressColor = "Green";
        }
    }

    private void ValidatePaymentMethod()
    {
        if (string.IsNullOrWhiteSpace(PaymentMethod))
        {
            PaymentMethodHint = "Выберите способ оплаты";
            PaymentMethodColor = "Red";
        }
        else
        {
            PaymentMethodHint = "✓";
            PaymentMethodColor = "Green";
        }
    }

    private void ConfirmOrder()
    {
        ValidateCustomerName();
        ValidateAddress();
        ValidatePaymentMethod();

        if (CustomerNameColor == "Red" || AddressColor == "Red" || PaymentMethodColor == "Red")
            return;

        var userId = _sessionService.CurrentUser?.Id;
        if (userId == null)
        {
            MessageBox.Show("Пользователь не авторизован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var cartItems = _unitOfWork.Carts.GetAll().Where(c => c.UserId == userId).ToList();
        if (!cartItems.Any())
        {
            MessageBox.Show("Корзина пуста.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Проверяем наличие товаров на складе
        var insufficientProducts = new List<string>();
        foreach (var cartItem in cartItems)
        {
            if (cartItem.Product == null)
            {
                insufficientProducts.Add($"Товар с ID {cartItem.ProductId} не найден");
                continue;
            }

            // Загружаем актуальные данные о товаре из БД
            var product = _unitOfWork.Products.GetById(cartItem.ProductId);
            if (product == null)
            {
                insufficientProducts.Add($"Товар '{cartItem.Product.Name}' не найден");
                continue;
            }

            if (product.Quantity < cartItem.Quantity)
            {
                insufficientProducts.Add(
                    $"Товар '{product.Name}': запрошено {cartItem.Quantity}, доступно {product.Quantity}");
            }
        }

        if (insufficientProducts.Any())
        {
            var message = "Недостаточно товаров на складе:\n\n" + string.Join("\n", insufficientProducts);
            MessageBox.Show(message, "Ошибка оформления заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var order = new Order
        {
            UserId = userId.Value,
            Status = "В обработке",
            OrderDate = DateTime.Now,
            OrderItems = cartItems.Select(c => new OrderItem
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Product = c.Product
            }).ToList()
        };

        _unitOfWork.Orders.Add(order);
        _unitOfWork.SaveChanges();

        // Обновляем количество товаров на складе
        foreach (var cartItem in cartItems)
        {
            var product = _unitOfWork.Products.GetById(cartItem.ProductId);
            if (product != null)
            {
                product.Quantity -= cartItem.Quantity;
                _unitOfWork.Products.Update(product);
            }
        }

        var payment = new Payment
        {
            OrderId = order.Id,
            PaymentDate = DateTime.Now,
            Amount = TotalAmount,
            PaymentMethod = PaymentMethod
        };
        _unitOfWork.Payments.Add(payment);

        foreach (var item in cartItems)
            _unitOfWork.Carts.Remove(item);

        _unitOfWork.SaveChanges();

        MessageBox.Show($"Ваш заказ оформлен! Сумма: ₽{TotalAmount:F2}",
                        "Заказ оформлен",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

        OrderConfirmed?.Invoke();
    }
}
