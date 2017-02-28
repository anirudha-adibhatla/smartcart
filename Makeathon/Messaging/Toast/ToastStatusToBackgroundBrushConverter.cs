using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Makeathon.Messaging.Toast
{
    [ValueConversion(typeof(Toast.ToastMessageType), typeof(Brush))]
    class ToastStatusToBackgroundBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!(value is Toast.ToastMessageType))
                {
                    throw new ArgumentException("Value is not of type  LiveLogViewModel.StatusType");
                }

                Toast.ToastMessageType statusType = (Toast.ToastMessageType)value;

                var Theme = new ResourceDictionary();
                Theme.Source = new Uri("pack://application:,,,/Makeathon;component/Theme.xaml",
                            UriKind.RelativeOrAbsolute);

                if (statusType == Toast.ToastMessageType.Error)
                    return new SolidColorBrush((Color)Theme["MsgErrorForegroundColor"]);
                else if (statusType == Toast.ToastMessageType.Success)
                    return new SolidColorBrush((Color)Theme["MsgSuccessForegroundColor"]);
                else if (statusType == Toast.ToastMessageType.Warning)
                    return new SolidColorBrush((Color)Theme["MsgWarningForegroundColor"]);
                else
                    return new SolidColorBrush((Color)Theme["MainAccentColor"]);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
