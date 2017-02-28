using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Makeathon.Messaging.LiveLog
{
    [ValueConversion(typeof(LiveLogViewModel.StatusType), typeof(bool))]
    class LiveLogStatusTypeToBooleanConverter : IValueConverter
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

                if (statusType == LiveLogViewModel.StatusType.BUSY)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
