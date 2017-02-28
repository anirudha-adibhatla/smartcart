using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Makeathon.Messaging.LiveLog
{
    [ValueConversion(typeof(LiveLogViewModel.MessageType), typeof(Brush))]
    class LiveLogMessageTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!(value is LiveLogViewModel.MessageType))
                {
                    throw new ArgumentException("Value is not of type  LiveLogViewModel.MessageType");
                }

                LiveLogViewModel.MessageType messageType = (LiveLogViewModel.MessageType)value;


                var Theme = new ResourceDictionary();
                Theme.Source = new Uri("pack://application:,,,/Makeathon;component/Theme.xaml",
                            UriKind.RelativeOrAbsolute);

                if (messageType == LiveLogViewModel.MessageType.ERROR)
                {
                    return new SolidColorBrush((Color)Theme["MsgErrorForegroundColor"]);
                }
                else if (messageType == LiveLogViewModel.MessageType.WARNING)
                {
                    return new SolidColorBrush((Color)Theme["MsgWarningForegroundColor"]);
                }
                else if (messageType == LiveLogViewModel.MessageType.INFO)
                {
                    return new SolidColorBrush((Color)Theme["MsgInfoForegroundColor"]);
                }
                else if (messageType == LiveLogViewModel.MessageType.SUCCESS)
                {
                    return new SolidColorBrush((Color)Theme["MsgSuccessForegroundColor"]);
                }
                else if (messageType == LiveLogViewModel.MessageType.ATTENTION)
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
