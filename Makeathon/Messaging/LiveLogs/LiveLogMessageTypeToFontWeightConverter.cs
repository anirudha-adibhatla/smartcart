using Makeathon.Messaging.LiveLog;
using Makeathon.Messaging.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Makeathon.Messaging.LiveLogs
{

    class LiveLogMessageTypeToFontWeightConverter : IValueConverter
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

                if (messageType == LogMessageType.ATTENTION)
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
