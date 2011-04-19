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
using System.Collections.Generic;

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
            get { return Double.Parse(element.Element("windAverage").Value); }
        }

        public double WindMax
        {
            get { return Double.Parse(element.Element("windMax").Value); }
        }

        public int WindTrend
        {
            get { return int.Parse(element.Element("windTrend").Value); }
        }

        public double WindHistoryMin
        {
            get { return Double.Parse(element.Element("windHistoryMin").Value); }
        }

        public double WindHistoryAverage
        {
            get { return Double.Parse(element.Element("windHistoryAverage").Value); }
        }

        public double WindHistoryMax
        {
            get { return Double.Parse(element.Element("windHistoryMax").Value); }
        }

        public double AirTemperature
        {
            get { return Double.Parse(element.Element("airTemperature").Value); }
        }

        public double AirHumidity
        {
            get { return Double.Parse(element.Element("airHumidity").Value); }
        }

        private List<IChartPoint> chartPoints;
        public List<IChartPoint> DirectionChartPoints
        {
            get 
            {
                if (chartPoints == null)
                {
                    chartPoints = new List<IChartPoint>();
                    foreach (var pointElement in element.Element("windDirectionChart").Element("serie").Elements("points"))
                    {
                        chartPoints.Add(new ChartPoint(pointElement));
                    }
                }
                return chartPoints;
            }
        }


        public int DirectionChartDuration
        {
            get { return int.Parse(element.Element("windDirectionChart").Attribute("duration").Value); }
        }
    }
}
