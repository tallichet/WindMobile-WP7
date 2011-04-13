using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch.Epix.WindMobile.WP7.Model.Design
{
    public class StationData : IStationData
    {
        public string StationId
        {
            get { return "Id"; }
        }

        public MaintenanceStatus Status
        {
            get { return MaintenanceStatus.Green; }
        }

        public DateTime ExpirationDate
        {
            get { return DateTime.MaxValue; }
        }

        public DateTime LastUpdate
        {
            get { return DateTime.Now; }
        }

        public double WindAverage
        {
            get { return 1.123; }
        }

        public double WindMax
        {
            get { return 10.12; }
        }

        public int WindTrend
        {
            get { return 2; }
        }

        public double WindHistoryMin
        {
            get { return 0; }
        }

        public double WindHistoryAverage
        {
            get { return 12; }
        }

        public double WindHistoryMax
        {
            get { return 28; }
        }

        public double AirTemperature
        {
            get { return 20; }
        }

        public double AirHumidity
        {
            get { return 35; }
        }
    }
}
