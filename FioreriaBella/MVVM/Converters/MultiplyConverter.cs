using FioreriaBella.Models.Entities;
using System.Globalization;
using System.Windows.Data;

namespace FioreriaBella.MVVM.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OrderItem item && item.Product != null)
            {
                return item.Product.Price * item.Quantity;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}