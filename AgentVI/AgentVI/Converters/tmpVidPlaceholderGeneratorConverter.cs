using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using System.Text;

namespace AgentVI.Converters
{
    public class tmpVidPlaceholderGeneratorConverter : IValueConverter, IDisposable
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = null;

            if (value != null)
            {
                res = "https://vjs.zencdn.net/v/oceans.mp4";
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }
    }
}
