﻿using System;
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

namespace Ch.Epix.WindMobile.WP7.Model.Xml
{
    public class StationData : XmlBase, IStationData
    {
        public StationData(XElement e)
            : base(e)
        {
        }

        public string StationId
        {
            get { return element.Attribute("stationId").Value; }
        }

        public MaintenanceStatus Status
        {
            get { return (MaintenanceStatus)Enum.Parse(typeof(MaintenanceStatus), element.Attribute("status").Value, true); }
        }

        public DateTime ExpirationDate
        {
            get { return DateTime.Parse(element.Attribute("expirationDate").Value); }
        }

        public DateTime LastUpdate
        {
            get { return DateTime.Parse(element.Attribute("lastUpdate").Value); }
        }

        public double WindAverage
        {
            get { return Double.Parse(element.Attribute("windAverage").Value); }
        }

        public double WindMax
        {
            get { return Double.Parse(element.Attribute("windMax").Value); }
        }

        public int WindTrend
        {
            get { return int.Parse(element.Attribute("windTrend").Value); }
        }

        public double WindHistoryMin
        {
            get { return Double.Parse(element.Attribute("windHistoryMin").Value); }
        }

        public double WindHistoryAverage
        {
            get { return Double.Parse(element.Attribute("windHistoryAverage").Value); }
        }

        public double WindHistoryMax
        {
            get { return Double.Parse(element.Attribute("windHistoryMax").Value); }
        }

        public double AirTemperature
        {
            get { return Double.Parse(element.Attribute("airTemperature").Value); }
        }

        public double AirHumidity
        {
            get { return Double.Parse(element.Attribute("airHumidity").Value); }
        }
    }
}
