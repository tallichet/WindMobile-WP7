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
using System.Device.Location;

namespace Ch.Epix.WindMobile.WP7.ViewModel.Converter
{
    public class StationInfoToGeoCoordinateConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IStationInfo && targetType == typeof(GeoCoordinate))
            {
                var station = value as IStationInfo;
                return new GeoCoordinate(station.Wgs84Latitude, station.Wgs84Longitude, station.Altitude);
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
