using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UiSampleMigrat.Models
{
    public class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float rating = (float)System.Convert.ToDecimal(value);
            if (rating < 2)
                return Color.Red;
            else if (rating < 4.5)
                return Color.FromHex("#efd225");
            else
                return Color.ForestGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
