using System;
using System.Xml.Linq;
using Ch.Epyx.WindMobile.WP7.Model;
using System.Collections.Generic;
using Ch.Epyx.WindMobile.WP7.Model.Xml;
using System.Windows;

namespace Ch.Epyx.WindMobile.WP7.Service.Job
{
    public class ListStationInfoJob : JobBase<Application, object, List<IStationInfo>>
    {
        protected override Uri GetUrl()
        {
            return new Uri(BaseUrl + "stationinfos");
        }

        protected override List<IStationInfo> JobRun(ref bool cancel, string arg)
        {
            var listElement = XElement.Parse(arg);
            var result = new List<IStationInfo>();

            foreach (var station in listElement.Elements("stationInfo"))
            {
                result.Add(new StationInfo(station));
            }
            return result;
        }

        public override void Execute(object o)
        {
            StartDownloadJob();
        }
    }
}
