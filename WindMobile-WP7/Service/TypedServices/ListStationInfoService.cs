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
using System.Collections.Generic;
using Ch.Epix.WindMobile.WP7.Service.Job;

namespace Ch.Epix.WindMobile.WP7.Service.TypedServices
{
    public class ListStationInfoService :  BaseService<object, List<IStationInfo>>
    {
        public ListStationInfoService()
            : base(() => { return new ListStationInfoJob(); })
        {

        }
    }
}
