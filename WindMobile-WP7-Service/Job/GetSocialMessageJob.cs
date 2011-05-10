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
using Ch.Epyx.WindMobile.WP7.Model;
using Ch.Epyx.WindMobile.WP7.Model.Xml;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Ch.Epyx.WindMobile.WP7.Service.Job
{
    public class GetSocialMessageJob : JobBase<IStationInfo, string, List<ISocialMessage>>
    {
        string chatRoomId;
        int maxCount;

        public GetSocialMessageJob(int maxCount)
        {
            this.maxCount = maxCount;
        }

        public override void Execute(string o)
        {
            chatRoomId = o;
            StartDownloadJob();
        }

        protected override Uri GetUrl()
        {
            return new Uri(BaseUrl + string.Format("chatrooms/{0}/lastmessages/{1}", chatRoomId, maxCount), UriKind.Absolute);
        }

        protected override List<ISocialMessage> JobRun(ref bool cancel, string arg)
        {
            var result = new List<ISocialMessage>();
            var messages = XElement.Parse(arg);
            foreach (var msg in messages.Elements("message"))
            {
                result.Add(new SocialMessage(msg));
            }
            return result;
        }
    }
}
