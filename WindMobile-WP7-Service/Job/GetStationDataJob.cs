using System;
using System.Xml.Linq;
using Ch.Epyx.WindMobile.WP7.Model.Xml;
using Ch.Epyx.WindMobile.WP7.Model;

namespace Ch.Epyx.WindMobile.WP7.Service.Job
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
            return new Uri(BaseUrl + "stationdatas/" + StationInfo.Id);
        }

        protected override IStationData JobRun(ref bool cancel, string arg)
        {
            return new StationData(XElement.Parse((string)arg));
        }
    }
}
