using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace Ch.Epyx.WindMobile.WP7.ViewModel.Converter
{
    public class DateToMessageDateFormatConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;

                if (date.Date == DateTime.Now.Date)
                {
                    return date.ToString("HH:mm");
                }
                else if (date.Year == DateTime.Now.Year)
                {
                    return date.ToString("dd.MM, HH:mm");
                }
                else
                {
                    return date.ToString("dd.MM.YYYY, HH:mm");
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
