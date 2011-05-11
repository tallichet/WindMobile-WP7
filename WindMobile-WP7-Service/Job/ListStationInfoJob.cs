using System;
using System.Collections.Generic;
using System.Windows;
using Ch.Epyx.WindMobile.WP7.Model;
using Ch.Epyx.WindMobile.WP7.Model.Json;
using Newtonsoft.Json;
using System.Net;

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
            //var listElement = XElement.Parse(arg);
            //var result = new List<IStationInfo>();

            //foreach (var station in listElement.Elements("stationInfo"))
            //{
            //    result.Add(new StationInfo(station));
            //}
            //return result;

            var result = JsonConvert.DeserializeObject<StationInfoList>(arg);
            return result.GetList();
        }

        public override void Execute(object o)
        {
            StartDownloadJob();
        }

        protected override System.Net.WebHeaderCollection GetWebHeaders()
        {
            var header = new WebHeaderCollection();
            header["Accept"] = "Application/Json";
            return header;
        }

        public class StationInfoList
        {
            public List<StationInfo> StationInfo { get; set; }

            public List<IStationInfo> GetList()
            {
                var result = new List<IStationInfo>();
                foreach (var info in StationInfo)
                {
                    result.Add(info);
                }
                return result;
            }
        }
    }
}
