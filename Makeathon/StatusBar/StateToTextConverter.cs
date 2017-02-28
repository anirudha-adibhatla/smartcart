using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Makeathon.StatusBar
{
    [ValueConversion(typeof(MainUserControlViewModel.State), typeof(string))]
    class StateToTextConverter : IValueConverter
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

                if (statusType == MainUserControlViewModel.State.Ready)
                    return "Ready";
                else if (statusType == MainUserControlViewModel.State.Busy)
                    return "Busy";

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
