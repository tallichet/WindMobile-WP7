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
using Ch.Epix.WindMobile.WP7.Model;
using Ch.Epix.WindMobile.WP7.Model.Xml;
using System.Xml.Linq;

namespace Ch.Epix.WindMobile.WP7.Service.Job
{
    public class GetStationChartJob : JobBase
    {
        private IStationInfo stationInfo;
        private string duration;

        public GetStationChartJob (IStationInfo station) : base()
        {
            stationInfo = station;
        }

        protected override Uri GetUrl()
        {
            return new Uri(baseUrl + "/windchart/" + stationInfo.Id + "/" + duration);
        }

        public override void Execute(string s)
        {
            duration = s;
            StartDownloadJob();
        }

        protected override object JobRun(ref bool cancel, object arg)
        {
            return new Chart(XElement.Parse((string)arg));
        }
    }
}
