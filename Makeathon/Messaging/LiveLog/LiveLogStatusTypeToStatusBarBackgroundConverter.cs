using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Makeathon.Messaging.LiveLog
{
    [ValueConversion(typeof(LiveLogViewModel.StatusType), typeof(Brush))]
    class LiveLogStatusTypeToStatusBarBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!(value is LiveLogViewModel.StatusType))
                {
                    throw new ArgumentException("Value is not of type  LiveLogViewModel.StatusType");
                }

                LiveLogViewModel.StatusType statusType = (LiveLogViewModel.StatusType)value;

                var Theme = new ResourceDictionary();
                Theme.Source = new Uri("pack://application:,,,/Makeathon;component/Theme.xaml",
                            UriKind.RelativeOrAbsolute);

                if (statusType == LiveLogViewModel.StatusType.ERROR)
                    return new SolidColorBrush((Color)Theme["MsgErrorForegroundColor"]);
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
            return null;
        }
    }
}
