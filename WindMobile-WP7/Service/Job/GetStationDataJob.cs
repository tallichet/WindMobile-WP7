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
using Ch.Epix.WindMobile.WP7.Model;

namespace Ch.Epix.WindMobile.WP7.Service.Job
{
    public class GetStationDataJob : JobBase<IStationInfo, object, IStationData>
    {
        public GetStationDataJob(IStationInfo info)
        {
            StationInfo = info;
        }

        public IStationInfo StationInfo
        {
            get;
            private set;
        }

        public override void Execute(object arg)
        {
            StartDownloadJob();
        }

        protected override Uri GetUrl()
        {
            return new Uri(baseUrl + "stationdatas/" + StationInfo.Id);
        }

        protected override IStationData JobRun(ref bool cancel, string arg)
        {
            return new StationData(XElement.Parse((string)arg));
        }
    }
}
