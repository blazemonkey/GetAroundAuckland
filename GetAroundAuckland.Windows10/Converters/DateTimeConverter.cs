using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GetAroundAuckland.Windows10.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || value.GetType() != typeof(string))
                return string.Empty;

            var content = value.ToString();
            DateTime dateTime;

            if (!DateTime.TryParseExact(content, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dateTime))
                return string.Empty;

            return dateTime.ToString("dddd, MMMM d, yyyy");
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
