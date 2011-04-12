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
using Ch.Epix.WindMobile.WP7.Service.Job;

namespace Ch.Epix.WindMobile.WP7.Service.Design
{
    public class DesignJobBase : IJob
    {

        public bool IsBusy
        {
            get { return true; }
        }

        public event EventHandler<JobFinishedEventArgs> JobCompleted;


        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
