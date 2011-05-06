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
    public interface IStationData
    {
        string StationId { get; }
        MaintenanceStatus Status { get; }
        DateTime ExpirationDate { get; }
        DateTime LastUpdate { get; }
        double WindAverage { get; }
        double WindMax { get; }
        int WindTrend { get; }
        double WindHistoryMin { get; }
        double WindHistoryAverage { get; }
        double WindHistoryMax { get; }
        double AirTemperature { get; }
        double AirHumidity { get; }
        List<IChartPoint> DirectionChartPoints { get;}
        int DirectionChartDuration { get; }
    }
}
