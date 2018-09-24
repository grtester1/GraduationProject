using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;

namespace AgentVI.Converters
{
    public class TimestampConverter : IValueConverter, IDisposable
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.FullName == typeof(ulong).FullName)
            {
                return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddMilliseconds((ulong)value);
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.FullName == typeof(DateTime).FullName)
            {
                return ((DateTime)value).Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            }
            else
            {
                return null;
            }
        }

        public void Dispose() { }
    }
}
