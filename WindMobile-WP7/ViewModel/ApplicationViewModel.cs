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
using GalaSoft.MvvmLight;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
using Ch.Epyx.WindMobile.WP7.Service;

namespace Ch.Epyx.WindMobile.WP7.ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {
        public string ApplicationTitle
        {
            get
            {
                return "WIND MOBILE";
            }
        }

        public CredentialsProvider CredentialsProvider
        {
            get
            {
                return new ApplicationIdCredentialsProvider("Aru7Ud6JR_vLA3MC_Vof2xFOXVejAASIjZzfy5pZuh3OUWLGkwMj--c8GWkutwCj");
            }
        }

        public GeoCoordinate Location
        {
            get
            {
                if (ServiceCentral.GeoCoordinateWatcher.Position.Location.IsUnknown)
                {
                    return new GeoCoordinate(46.681609, 6.723654);
                }
                else
                {
                    return ServiceCentral.GeoCoordinateWatcher.Position.Location;
                }
            }
        }
    }
}
