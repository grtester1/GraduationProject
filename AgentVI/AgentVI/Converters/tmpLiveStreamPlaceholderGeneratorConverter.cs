using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using System.Text;

namespace AgentVI.Converters
{
    public class tmpLiveStreamPlaceholderGeneratorConverter : IValueConverter, IDisposable
    {
        private string[] dict = new string[]
        {
            "http://96.70.163.66/mjpg/video.mjpg",
            "http://166.161.110.55/-wvhttp-01-/GetOneShot?image_size=640x480&frame_count=1000000000",
            "http://24.221.30.55:8080/-wvhttp-01-/GetOneShot?image_size=640x480&frame_count=1000000000"
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return dict[new Random().Next(0, 3)];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }
    }
}
