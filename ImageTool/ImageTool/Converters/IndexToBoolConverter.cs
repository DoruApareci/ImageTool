using System;
using System.Windows.Data;

namespace ImageTool.Converters
{
    public class IndexToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((value is ImageFiltersLibrary.Parameters.AlgorithmParameter) && ((ImageFiltersLibrary.Parameters.AlgorithmParameter)value).Value == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
