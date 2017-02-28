using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Makeathon.Converters
{
    class SubTotalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if((values.Count() == 2) && values[0] != null || values[1] != null)
            {
                double price, count;

                double.TryParse(values[0].ToString(), out price);
                double.TryParse(values[1].ToString(), out count);
                return (price* count).ToString();
            }
            return -1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
