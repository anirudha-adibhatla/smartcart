using System;
using System.Globalization;
using System.Windows.Data;

namespace Makeathon.StatusBar
{
    [ValueConversion(typeof(MainUserControlViewModel.State), typeof(bool))]
    class StateToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!(value is MainUserControlViewModel.State))
                {
                    throw new ArgumentException("Value is not of type  MainUserControlViewModel.State");
                }

                MainUserControlViewModel.State statusType = (MainUserControlViewModel.State)value;

                if (statusType == MainUserControlViewModel.State.Busy)
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
            throw new NotImplementedException();
        }
    }
}
