using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GetAroundAuckland.Windows10.Converters
{
    public class ArrivingTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || value.GetType() != typeof(DateTime))
                return "";

            var expectedArrivalTime = (DateTime)value;

            return expectedArrivalTime.Subtract(DateTime.UtcNow).Minutes.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
