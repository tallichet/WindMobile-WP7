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

namespace Ch.Epix.WindMobile.WP7.Model.Design
{
    public class StationInfo : IStationInfo
    {

        public MaintenanceStatus MaintenanceStatus
        {
            get { return Model.MaintenanceStatus.Green; }
        }

        public int Altitude
        {
            get { return 1234; }
        }

        public double Wgs84Longitude
        {
            get { return 45.22244; }
        }

        public double Wgs84Latitude
        {
            get { return 45.22244; }
        }

        public int DataValidity
        {
            get { return 0; }
        }

        public string Name
        {
            get { return "Station Name"; }
        }

        public string ShortName
        {
            get { return "ShortName"; }
        }

        public string Id
        {
            get { return "Id"; }
        }

        public override bool Equals(object obj)
        {
            var info = obj as StationInfo;
            return info != null && info.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
