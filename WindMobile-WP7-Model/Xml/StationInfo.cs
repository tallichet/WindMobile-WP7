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
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Globalization;

namespace Ch.Epyx.WindMobile.WP7.Model.Xml
{
    public class StationInfo : XmlBase, IStationInfo
    {
        public StationInfo(XElement e)
            : base(e)
        {
        }

        public MaintenanceStatus MaintenanceStatus
        {
            get { return (MaintenanceStatus) Enum.Parse(typeof(MaintenanceStatus), element.Attribute("maintenanceStatus").Value, true); }
        }

        public int Altitude
        {
            get { return int.Parse(element.Attribute("altitude").Value); }
        }

        public double Wgs84Longitude
        {
            get { return double.Parse(element.Attribute("wgs84Longitude").Value, CultureInfo.InvariantCulture.NumberFormat); }
        }

        public double Wgs84Latitude
        {
            get { return double.Parse(element.Attribute("wgs84Latitude").Value, CultureInfo.InvariantCulture.NumberFormat); }
        }

        public int DataValidity
        {
            get { return int.Parse(element.Attribute("dataValidity").Value); }
        }

        public string Name
        {
            get { return element.Attribute("name").Value; }
        }

        public string ShortName
        {
            get { return element.Attribute("shortName").Value; }
        }

        public string Id
        {
            get { return element.Attribute("id").Value; }
        }

        public int CompareTo(IStationInfo other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
