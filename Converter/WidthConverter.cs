using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SmoothListView.Converter
{
    public class WidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double ret = 0;
            double width = (double)values[0];
            int count = (int)values[1];
            System.Windows.Controls.Orientation orient = (System.Windows.Controls.Orientation)values[2];
            if (orient == System.Windows.Controls.Orientation.Horizontal)
                ret = Math.Floor(width / count);
            else if (orient == System.Windows.Controls.Orientation.Vertical)
                ret = width;
            return ret; 
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
