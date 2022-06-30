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
    class SquareStatusConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (SquareStatus)value;

            switch (status)
            {
                case SquareStatus.Mine:
                    return Mine;
                case SquareStatus.Uncovered:
                    return Uncovered;
                case SquareStatus.Covered:
                    return Covered;
                case SquareStatus.Flagged:
                    return Flagged;
                default:
                    throw new ArgumentException("de value.Status is niet gelijk aan een enum check de SquareTextConverter");
            }

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object? Covered { get; set; }

        public object? Mine { get; set; }

        public object? Uncovered { get; set; }

        public object? Flagged { get; set; }
    }
}