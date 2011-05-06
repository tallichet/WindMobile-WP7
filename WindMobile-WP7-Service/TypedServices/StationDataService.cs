using Ch.Epyx.WindMobile.WP7.Model;

namespace Ch.Epyx.WindMobile.WP7.Service.TypedServices
{
    public class StationDataService : BaseService<object, IStationData>
    {
        public StationDataService(GetJobAction action)
            : base(action)
        {

        }
    }
}
