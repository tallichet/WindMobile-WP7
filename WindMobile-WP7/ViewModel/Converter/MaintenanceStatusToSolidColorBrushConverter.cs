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
using Ch.Epix.WindMobile.WP7.Model;

namespace Ch.Epix.WindMobile.WP7.ViewModel.Converter
{
    public class MaintenanceStatusToSolidColorBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(Brush) && value is MaintenanceStatus)
            {
                var status = (MaintenanceStatus)value;
                switch (status)
                {
                    case MaintenanceStatus.Green :
                        return new SolidColorBrush(Colors.Green);
                    case MaintenanceStatus.Orange:
                        return new SolidColorBrush(Colors.Orange);
                    case MaintenanceStatus.Red:
                        return new SolidColorBrush(Colors.Red);
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
