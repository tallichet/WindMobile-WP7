using System.ComponentModel;
using Ch.Epyx.WindMobile.WP7.Model;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Ch.Epyx.WindMobile.WP7.Service
{
    /// <summary>
    /// Contains information shared along application execution, like current item selected
    /// </summary>
    public class ApplicationRunData : INotifyPropertyChanged
    {
        public static IStationInfo currentStation;

        public event PropertyChangedEventHandler PropertyChanged;

        public static IStationInfo CurrentStationStatic 
        {
            get { return currentStation; }
            set {
                currentStation = value;
            }
        }

        public IStationInfo CurrentStation 
        {
            get { return CurrentStationStatic; }
            set { 
                CurrentStationStatic = value;
                RaisePropertyChanged("CurrentStation");
            }
        }
        

        protected void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public static void SaveState(IDictionary<string, object> state)
        {
            if (currentStation != null)
            {
                state["application-currentStation"] = currentStation;
            }
        }

        public static void LoadState(IDictionary<string, object> state)
        {
            if (state.ContainsKey("application-currentStation"))
            {
                currentStation = (IStationInfo)state["application-currentStation"];
            }
        }
    }
}
