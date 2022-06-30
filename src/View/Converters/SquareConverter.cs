using Model.MineSweeper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ViewModel;

namespace View.Converters
{
    class SquareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var square = (Square)value;

            if (square.Status == SquareStatus.Uncovered)
            {
                int x = square.NeighboringMineCount;
               
                return x == 0 ? "" : x;
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}