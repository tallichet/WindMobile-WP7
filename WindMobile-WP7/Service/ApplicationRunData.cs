using System;
using System.ComponentModel;
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

namespace Ch.Epix.WindMobile.WP7.Service
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
    }
}
