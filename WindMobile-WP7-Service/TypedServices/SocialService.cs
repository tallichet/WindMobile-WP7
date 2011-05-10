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
using System.Collections.Generic;
using Ch.Epyx.WindMobile.WP7.Service.Job;

namespace Ch.Epyx.WindMobile.WP7.Service.TypedServices
{
    public class SocialService : BaseService<string, List<ISocialMessage>>
    {
        public SocialService() : base(() => { return new GetSocialMessageJob(30); }) { } // return only the latest 30 messages
    }
}
