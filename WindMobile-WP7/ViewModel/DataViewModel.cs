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
using System.Collections.Generic;
using Ch.Epyx.WindMobile.WP7.Service;

namespace Ch.Epyx.WindMobile.WP7.ViewModel
{
    public class DataViewModel : ApplicationViewModel
    {
        public IEnumerable<StationInfoViewModel> StationInfoViewModels
        {
            get
            {
                return ViewModelLocator.InfoViewModelsStatic.Values;
            }
        }

        public ChartViewModel CurrentChartViewModel
        {
            get
            {
                return ViewModelLocator.CurrentChartViewModelStatic;
            }
        }

        public Model.IStationInfo CurrentStationInfo 
        {
            get
            {
                return ApplicationRunData.CurrentStationStatic;
            }
            set 
            {
                ApplicationRunData.CurrentStationStatic = value;
                RaisePropertyChanged("CurrentStationInfo");
                RaisePropertyChanged("CurrentChartViewModel");                
            }
        }
    }
}
