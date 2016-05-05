using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GetAroundAuckland.Windows10.Converters
{
    public class StopIdToStopNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || value.GetType() != typeof(string))
                return string.Empty;

            var stopId = value.ToString();

            return Stop.ListOfStops.First(x => x.Id == stopId).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
