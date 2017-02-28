using Makeathon.Messaging.LiveLog;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Makeathon.Messaging.LiveLog
{
    [ValueConversion(typeof(LiveLogViewModel.MessageType), typeof(BitmapImage))]
    class LiveLogMessageTypeToIconImageConverter : IValueConverter
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
                    return (BitmapImage)Theme["ErrorBitmapImage"];
                }
                else if (messageType == LiveLogViewModel.MessageType.WARNING)
                {
                    return (BitmapImage)Theme["WarningBitmapImage"];
                }
                else if (messageType == LiveLogViewModel.MessageType.INFO)
                {
                    return null;
                }
                else if (messageType == LiveLogViewModel.MessageType.SUCCESS)
                {
                    return (BitmapImage)Theme["SuccessBitmapImage"];
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
