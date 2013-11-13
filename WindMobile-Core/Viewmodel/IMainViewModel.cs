using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core.Viewmodel
{
    public interface IMainViewModel
    {
        Model.Station CurrentStation { get; set; }

        List<Model.StationData> CurrentStationData { get; }

        void SetLocation(Model.Location CurrentLocation);

        /// <summary>
        /// List of closest stations
        /// </summary>
        ObservableCollection<Model.Station> CloseStations { get; }

        bool InProgress { get; }
    }
}
