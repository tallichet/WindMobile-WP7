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
    public class TrendToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(Brush) && value is int)
            {
                if ((int)value > 0)
                {
                    if (parameter.ToString().ToLower() == "stroke")
                    {
                        return new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        return new SolidColorBrush(Color.FromArgb(155, 255, 0, 0));
                    }
                }
                else
                {
                    if (parameter.ToString().ToLower() == "stroke")
                    {
                        return new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        return new SolidColorBrush(Color.FromArgb(155, 0, 255, 0));
                    }
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
