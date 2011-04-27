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

namespace Ch.Epix.WindMobile.WP7.Service
{
    /// <summary>
    /// Base service that is executing a job and keeping the last valid result and last error (if any)
    /// </summary>
    /// <typeparam name="P">Parameter type, if any, for the job execution. 
    /// use object as type and null as value if no paramter</typeparam>
    /// <typeparam name="R">Result of the background job. Type of the Item returned by the service</typeparam>
    public class BaseService<P, R>
    {
        IJob<P, R> job;

        public delegate IJob<P, R> GetJobAction();

        public R LastResult { get; protected set; }
        public Exception LastException { get; protected set; }
        public GetJobAction GetJob { get; protected set; }

        public event EventHandler<LastResultChangeEventArgs<R>> LastResultChanged;
        public event EventHandler<ServiceErrorEventArgs> ErrorOccured;

        public BaseService(GetJobAction jobDelegate)
        {
            GetJob = jobDelegate;
        }

        public void Refresh(P param)
        {
            InitJob();
            if (job.IsBusy == false) 
            {
                job.Execute(param);
            }
        }

        protected void InitJob()
        {
            if (job == null)
            {
                job = GetJob();
                job.JobCompleted += (s, e) =>
                {
                    this.LastResult = e.Result;
                    this.RaiseLastResultChanged(e.Result);
                };
                job.JobError += (s, e) =>
                {
                    LastException = e.Exception;
                    this.RaiseError(e.SourceName, e.Exception);
                };
            }
        }

        protected void RaiseError(string source, Exception exception)
        {
            if (ErrorOccured != null)
            {
                ErrorOccured(this, new ServiceErrorEventArgs(source, exception));
            }
        }

        protected void RaiseLastResultChanged(R newResult) 
        {
            if (LastResultChanged != null) 
            {
                LastResultChanged(this, new LastResultChangeEventArgs<R>(newResult));
            }
        }

        
    }

    public class LastResultChangeEventArgs<T> : EventArgs
    {
        public LastResultChangeEventArgs(T result)
        {
            Result = result;
        }

        public T Result { get; private set; }
    }

    public class ServiceErrorEventArgs : EventArgs
    {
        public ServiceErrorEventArgs(string source, Exception ex)
        {
            this.SourceName = source;
            this.Exception = ex;
        }

        public Exception Exception { get; private set; }
        public string SourceName { get; private set; }
    }
}
