using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch.Epix.WindMobile.WP7.Service.Job
{
    public interface IJob
    {
        bool IsBusy { get; }
        void Execute();
        void Execute(string s);
        event EventHandler<JobFinishedEventArgs> JobCompleted;

        
    }

    public class JobFinishedEventArgs : EventArgs
    {
        internal JobFinishedEventArgs(object r)
        {
            Result = r;
        }

        public object Result { get; private set; }
    }
}
