using System;
using System.Windows.Data;

namespace ImageTool.Converters
{
    public class StringToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = false;
            if (values.Length > 1 && values[0] is int && values[1] is int)
            {
                var currentPageIndex = (int)values[0];
                var currentItem = (int)values[1];
                if (currentPageIndex + 1 != currentItem)
                {
                    result = true;
                }
            }

            if (values.Length > 1 && values[0] is ImageFiltersLibrary.Parameters.AlgorithmParameter && values[1] is ImageFiltersLibrary.Parameters.AlgorithmParameter)
            {
                var currentAlgorithm = (ImageFiltersLibrary.Parameters.AlgorithmParameter)values[0];
                var currentItem = (ImageFiltersLibrary.Parameters.AlgorithmParameter)values[1];
                result = (currentAlgorithm.Value == currentItem.Value);
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
