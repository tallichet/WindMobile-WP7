using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core.Viewmodel.Design
{
    public class DesignMainViewModel : ViewModelBase, IMainViewModel
    {
        public DesignMainViewModel()
        {
            CloseStations = new System.Collections.ObjectModel.ObservableCollection<Model.Station>();
            CloseStations.Add(new Model.Station()
            {
                ShortName = "Yverdon",
                DisplayName = "Yverdon-les-bains",
                ID = "jdc-1000",
                Altitude = 458,
                StatusString = "green"
            });
            CloseStations.Add(new Model.Station()
            {
                ShortName = "Suchet",
                DisplayName = "Sommet du suchet",
                ID = "jdc-1001",
                Altitude = 1580,
                StatusString = "green"
            });
        }

        public System.Collections.ObjectModel.ObservableCollection<Model.Station> CloseStations
        {
            get; private set;
        }


        public bool InProgress
        {
            get { return true; }
        }

        public Model.Station CurrentStation
        {
            get
            {
                return CloseStations.First();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Model.StationData> CurrentStationData
        {
            get; private set;
        }

        public void SetLocation(Model.Location CurrentLocation)
        {
            throw new NotImplementedException();
        }
    }
}
