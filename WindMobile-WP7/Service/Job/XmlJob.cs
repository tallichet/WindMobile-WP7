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
using System.Xml.Linq;

namespace Ch.Epix.WindMobile.WP7.Service.Job
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
