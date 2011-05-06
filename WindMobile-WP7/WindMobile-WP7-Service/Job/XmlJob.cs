using System;
using System.Xml.Linq;
using Ch.Epyx.WindMobile.WP7.Model;

namespace Ch.Epyx.WindMobile.WP7.Service.Job
{
    public class XmlJob<P, R> : JobBase<IStationInfo, P, R>
    {
        public delegate R CreateResultObjectAction(XElement xml);
        public delegate string GetUrlStringAction();

        public CreateResultObjectAction CreateResultObject { get; private set; }
        public GetUrlStringAction GetUrlString { get; private set; }


        public XmlJob(CreateResultObjectAction resultDelegate, GetUrlStringAction urlDelegate)
        {
            CreateResultObject = resultDelegate;
            GetUrlString = urlDelegate;
        }

        protected override Uri GetUrl()
        {
            return new Uri(GetUrlString());
        }

        protected override R JobRun(ref bool cancel, string arg)
        {
            return CreateResultObject(XElement.Parse(arg));
        }

        public override void Execute(P o)
        {
            StartDownloadJob();
        }
    }
}
