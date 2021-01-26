using System;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;

namespace ConsultApp.Helpers.Converters
{
    public class TitleCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
