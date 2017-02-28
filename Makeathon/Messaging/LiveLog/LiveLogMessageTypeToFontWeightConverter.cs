using Makeathon.Messaging.LiveLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Makeathon.Messaging.LiveLog
{

    class LiveLogMessageTypeToFontWeightConverter : IValueConverter
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

                if (messageType == LiveLogViewModel.MessageType.ATTENTION)
                {
                    return FontWeights.Bold;
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
