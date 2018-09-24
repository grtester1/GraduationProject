using System;
using System.Globalization;
using Xamarin.Forms;
using AgentVI.Models;
using AgentVI.Utils;

namespace AgentVI.Converters
{
    public class EnumStatusColorConverter : IValueConverter, IDisposable
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String res = "Black";

            if (value != null)
            {
                res = ((SensorModel.ESensorColorHealth)value).convertEnumToString();
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public void Dispose() { }
    }
}
