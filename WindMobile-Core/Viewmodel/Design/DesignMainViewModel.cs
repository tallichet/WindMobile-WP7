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
            Stations = new System.Collections.ObjectModel.ObservableCollection<Model.Station>();
            Stations.Add(new Model.Station()
            {
                ShortName = "Yverdon",
                DisplayName = "Yverdon-les-bains",
                ID = "jdc-1000",
                Altitude = 458,
                StatusString = "green"
            });
            Stations.Add(new Model.Station()
            {
                ShortName = "Suchet",
                DisplayName = "Sommet du suchet",
                ID = "jdc-1001",
                Altitude = 1580,
                StatusString = "green"
            });
        }

        public System.Collections.ObjectModel.ObservableCollection<Model.Station> Stations
        {
            get; private set;
        }


        public bool InProgress
        {
            get { return true; }
        }
    }
}
