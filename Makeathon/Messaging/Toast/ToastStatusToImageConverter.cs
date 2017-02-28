using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Makeathon.Messaging.Toast
{
    [ValueConversion(typeof(Toast.ToastMessageType), typeof(BitmapImage))]
    class ToastStatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!(value is Toast.ToastMessageType))
                {
                    throw new ArgumentException("Value is not of type  LiveLogViewModel.MessageType");
                }

                Toast.ToastMessageType messageType = (Toast.ToastMessageType)value;


                var Theme = new ResourceDictionary();
                Theme.Source = new Uri("pack://application:,,,/Makeathon;component/Theme.xaml",
                            UriKind.RelativeOrAbsolute);

                if (messageType == Toast.ToastMessageType.Error)
                {
                    return (BitmapImage)Theme["ToastErrorBitmapImage"];
                }
                else if (messageType == Toast.ToastMessageType.Warning)
                {
                    return (BitmapImage)Theme["ToastWarningBitmapImage"];
                }
                else if (messageType == Toast.ToastMessageType.Info)
                {
                    return (BitmapImage)Theme["ToastInfoBitmapImage"];
                }
                else if (messageType == Toast.ToastMessageType.Success)
                {
                    return (BitmapImage)Theme["ToastSuccessBitmapImage"];
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
            throw new NotImplementedException();
        }
    }
}
