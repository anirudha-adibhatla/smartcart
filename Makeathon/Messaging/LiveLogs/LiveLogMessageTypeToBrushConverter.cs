using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using Makeathon.Messaging.Models;

namespace Makeathon.Messaging.LiveLogs
{
    [ValueConversion(typeof(LogMessageType), typeof(Brush))]
    class LiveLogMessageTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!(value is LogMessageType))
                {
                    throw new ArgumentException("Value is not of type  LogMessageType");
                }

                LogMessageType messageType = (LogMessageType)value;


                var Theme = new ResourceDictionary();
                Theme.Source = new Uri("pack://application:,,,/Makeathon;component/Theme.xaml",
                            UriKind.RelativeOrAbsolute);

                if (messageType == LogMessageType.ERROR)
                {
                    return new SolidColorBrush((Color)Theme["MsgErrorForegroundColor"]);
                }
                else if (messageType == LogMessageType.WARNING)
                {
                    return new SolidColorBrush((Color)Theme["MsgWarningForegroundColor"]);
                }
                else if (messageType == LogMessageType.INFO)
                {
                    return new SolidColorBrush((Color)Theme["MsgInfoForegroundColor"]);
                }
                else if (messageType == LogMessageType.SUCCESS)
                {
                    return new SolidColorBrush((Color)Theme["MsgSuccessForegroundColor"]);
                }
                else if (messageType == LogMessageType.ATTENTION)
                {
                    return new SolidColorBrush((Color)Theme["MsgAttetionForegroundColor"]);
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
