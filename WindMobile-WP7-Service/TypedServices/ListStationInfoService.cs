using System.Collections.Generic;
using Ch.Epyx.WindMobile.WP7.Model;
using Ch.Epyx.WindMobile.WP7.Service.Job;

namespace Ch.Epyx.WindMobile.WP7.Service.TypedServices
{
    public class ListStationInfoService :  BaseService<object, List<IStationInfo>>
    {
        public ListStationInfoService()
            : base(() => { return new ListStationInfoJob(); })
        {

        }
    }
}
