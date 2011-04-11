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

namespace Ch.Epix.WindMobile.WP7.Model
{
    public interface IStationInfo
    {
        MaintenanceStatus MaintenanceStatus { get; }
        int Altitude { get; }
        double Wgs84Longitude { get; }
        double Wgs84Latitude { get; }
        int DataValidity { get; }
        string Name { get; }
        string ShortName { get; }
        string Id { get; }
    }
}
