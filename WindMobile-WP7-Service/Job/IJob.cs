using System;

namespace Ch.Epyx.WindMobile.WP7.Service.Job
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="P">Parameter type, if any, for the job execution. 
    /// use object as type and null as value if no paramter</typeparam>
    /// <typeparam name="R">Result of the background job. Type of the Item returned by the job</typeparam>
    public interface IJob<P, R>
    {
        bool IsBusy { get; }
        void Execute(P param);
        event EventHandler<JobFinishedEventArgs<R>> JobCompleted;
        event EventHandler<JobErrorEventArgs> JobError;        
    }

    public class JobFinishedEventArgs<T> : EventArgs
    {
        internal JobFinishedEventArgs(T r)
        {
            Result = r;
        }

        public T Result { get; private set; }
    }

    public class JobErrorEventArgs: EventArgs
    {
        internal JobErrorEventArgs(String sourceName, Exception ex)
        {
            Exception = ex;
            SourceName = sourceName;
        }

        public Exception Exception { get; private set; }

        public string SourceName { get; private set; }
    }
}
