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
using Ch.Epix.WindMobile.WP7.Model.Xml;

namespace Ch.Epix.WindMobile.WP7.Service.Job
{
    public class GetStationDataJob : JobBase
    {

        public string StationId
        {
            get;
            set;
        }

        public override void Execute()
        {
            if (String.IsNullOrEmpty(StationId))
            {
                throw new Exception("No station ID defined");
            }
            StartDownloadJob();
        }

        public override void Execute(string s)
        {
            StationId = s;
            Execute();
        }

        protected override Uri GetUrl()
        {
            return new Uri(baseUrl + "stationdatas/" + StationId);
        }

        protected override object JobRun(ref bool cancel, object arg)
        {
            return new StationData(XElement.Parse((string)arg));
        }
    }
}
