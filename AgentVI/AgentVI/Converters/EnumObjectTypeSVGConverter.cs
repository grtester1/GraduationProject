using AgentVI.Utils;
using InnoviApiProxy;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace AgentVI.Converters
{
    public class EnumObjectTypeSVGConverter : IValueConverter, IDisposable
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = null;

            if (value != null)
            {
                res = svgPathForObjectType((SensorEvent.eObjectType)value);
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }

        private string svgPathForObjectType(SensorEvent.eObjectType i_ObjectTypeEnum)
        {
            string res;

            switch(i_ObjectTypeEnum)
            {
                case SensorEvent.eObjectType.Bicycle:
                    res = Settings.ObjectTypeBicycleSVGPath;
                    break;
                case SensorEvent.eObjectType.Motorcycle:
                    res = Settings.ObjectTypeMotorcycleSVGPath;
                    break;
                case SensorEvent.eObjectType.Object:
                    res = Settings.ObjectTypePersonSVGPath;
                    //res = Settings.
                    break;
                case SensorEvent.eObjectType.Person:
                    res = Settings.ObjectTypePersonSVGPath;
                    break;
                case SensorEvent.eObjectType.Undefined:
                    res = Settings.ObjectTypePersonSVGPath;
                    //res = Settings.
                    break;
                case SensorEvent.eObjectType.Unknown:
                    res = Settings.ObjectTypePersonSVGPath;
                    //res = Settings.
                    break;
                case SensorEvent.eObjectType.Vehicle:
                    res = Settings.ObjectTypeCarSVGPath;
                    break;
                default:
                    res = Settings.ObjectTypePersonSVGPath;
                    break;
            }

            return res;
        }
    }
}
