using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AgentVI.Utils;
using System.Globalization;

namespace AgentVI.Converters
{
    public class TimestampConverter : IValueConverter, IDisposable
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (new DateTime(1970,1,1,0,0,0,0)).AddMilliseconds((ulong)value).ToLocalTime().ToString(Settings.DateTimeFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
        }

        public void Dispose() { }
    }
}
