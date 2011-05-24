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
using Ch.Epyx.WindMobile.WP7.Resources;

namespace Ch.Epyx.WindMobile.WP7.ViewModel
{
    public class ExceptionViewModel : ApplicationViewModel
    {
        public Exception Exception { get; private set; }

        public string PageName
        {
            get
            {
                return AppResources.PageName_Excpetion;
            }
        }

        public ExceptionViewModel(Exception ex)
        {
            Exception = ex;
        }

        public Type Type
        {
            get { return Exception.GetType(); }
        }
    }
}
