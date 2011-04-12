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
using Ch.Epix.WindMobile.WP7.Model;
using System.Collections.Generic;
using Ch.Epix.WindMobile.WP7.Model.Xml;

namespace Ch.Epix.WindMobile.WP7.Service.Job
{
    public class ListStationInfoJob : JobBase
    {
        protected override Uri GetUrl()
        {
            return new Uri(baseUrl + "stationinfos");
        }

        protected override object OnDownloadStringCompleted(string downloadedString)
        {
            return downloadedString;
        }

        protected override object JobRun(ref bool cancel, object arg)
        {
            var listElement = XElement.Parse((string)arg);
            var result = new List<IStationInfo>();

            foreach (var station in listElement.Elements("stationInfo"))
            {
                result.Add(new StationInfo(station));
            }
            return result;
        }
    }
}
