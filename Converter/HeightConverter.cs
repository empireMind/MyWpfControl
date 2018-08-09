using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SmoothListView.Converter
{
    class HeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double ret = 0;
            double height = (double)values[0];
            int count = (int)values[1];
            System.Windows.Controls.Orientation orient = (System.Windows.Controls.Orientation)values[2];
            if (orient == System.Windows.Controls.Orientation.Horizontal)
                ret = height;
            else if (orient == System.Windows.Controls.Orientation.Vertical)
                ret = Math.Floor(height / count);
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
