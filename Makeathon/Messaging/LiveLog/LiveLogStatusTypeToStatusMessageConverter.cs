using System;
using System.Globalization;
using System.Windows.Data;


namespace Makeathon.Messaging.LiveLog
{
    [ValueConversion(typeof(LiveLogViewModel.StatusType), typeof(string))]
    class LiveLogStatusTypeToStatusMessageConverter : IValueConverter
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

                if (statusType == LiveLogViewModel.StatusType.ERROR)
                    return "Error";
                else if (statusType == LiveLogViewModel.StatusType.READY)
                    return "Ready";
                else if (statusType == LiveLogViewModel.StatusType.BUSY)
                    return "Busy";

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
