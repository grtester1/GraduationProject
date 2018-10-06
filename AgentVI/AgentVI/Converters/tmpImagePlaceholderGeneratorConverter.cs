using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using System.Text;

namespace AgentVI.Converters
{
    public class tmpImagePlaceholderGeneratorConverter : IValueConverter, IDisposable
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = null;

            if (value != null)
            {
                res = new StringBuilder().Append("https://picsum.photos/")
                    .Append(new Random().Next(200,800)).Append("/")
                    .Append(new Random(Guid.NewGuid().GetHashCode()).Next(200,800)).ToString();
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
