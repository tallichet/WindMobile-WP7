using Ch.Epyx.WindMobile.WP7.Model;

namespace Ch.Epyx.WindMobile.WP7.Service.TypedServices
{
    public class StationChartService: BaseService<int, IChart>
    {
        public StationChartService(GetJobAction action)
            : base(action)
        {

        }

    }
}
