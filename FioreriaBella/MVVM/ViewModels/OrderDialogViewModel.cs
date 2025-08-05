using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class OrderDialogViewModel : BaseViewModel
  {
    private readonly Order _originalOrder;

    private string _status = string.Empty;
    public string Status
    {
      get => _status;
      set => SetProperty(ref _status, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event Action<bool, Order?>? RequestClose;

    public OrderDialogViewModel(Order order)
    {
      _originalOrder = order;

      _status = order.Status;

      SaveCommand = new RelayCommand(_ => Save());
      CancelCommand = new RelayCommand(_ => Cancel());
    }

    private void Save()
    {
      var updatedOrder = new Order
      {
        Id = _originalOrder.Id,
        UserId = _originalOrder.UserId,
        OrderDate = _originalOrder.OrderDate,
        Status = Status,
        User = _originalOrder.User,
        OrderItems = _originalOrder.OrderItems,
        Payments = _originalOrder.Payments
      };

      RequestClose?.Invoke(true, updatedOrder);
    }

    private void Cancel()
    {
      RequestClose?.Invoke(false, null);
    }
  }
}
