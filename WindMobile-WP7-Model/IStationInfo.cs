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
using System.Collections.Generic;

namespace Ch.Epyx.WindMobile.WP7.Model
{
    public interface IStationInfo : IComparable<IStationInfo>
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

    public class StationInfoComparer : IEqualityComparer<IStationInfo>
    {
        public bool Equals(IStationInfo x, IStationInfo y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IStationInfo obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
