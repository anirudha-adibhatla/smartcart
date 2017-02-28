using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Makeathon.Messaging.Toast
{
    [ValueConversion(typeof(Toast.ToastMessageType), typeof(string))]
    class ToastStatusToTextConverter : IValueConverter
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

                if (messageType == Toast.ToastMessageType.Error)
                {
                    return "ERROR";
                }
                else if (messageType == Toast.ToastMessageType.Warning)
                {
                    return "Warning";
                }
                else if (messageType == Toast.ToastMessageType.Info)
                {
                    return "Info";
                }
                else if (messageType == Toast.ToastMessageType.Success)
                {
                    return "Success";
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
