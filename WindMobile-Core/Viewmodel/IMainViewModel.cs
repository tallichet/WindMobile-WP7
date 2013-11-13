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
        ObservableCollection<Model.Station> Stations { get; }

        bool InProgress { get; }
    }
}
