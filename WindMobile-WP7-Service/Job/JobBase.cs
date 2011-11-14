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
using System.ComponentModel;
using System.Reflection;

namespace Ch.Epyx.WindMobile.WP7.Service.Job
{
    /// <summary>
    /// Base class for jobs
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="P">Parameter type, if any, for the job execution. 
    /// use object as type and null as value if no paramter</typeparam>
    /// <typeparam name="R">Result of the background job. Type of the Item returned by the job</typeparam>
    public abstract class JobBase<S, P, R> : IJob<P, R>
    {
        private BackgroundWorker worker;
        private WebClient client;

        private string downloadedString;
        private R result;

        public static string BaseUrl
        {
            get { return Constants.BaseUrl; }
        }

        public JobBase()
        {
            InitWebClient();
            InitWorker();
            IsBusy = false;
        }

        public virtual bool IsBusy
        {
            get;
            private set;
        }

        public abstract void Execute(P o);

        /// <summary>
        /// Executed before doing anything else. 
        /// Thread: UI
        /// </summary>
        protected virtual void Init() 
        {
            IsBusy = true;
        }

        /// <summary>
        /// Just return the URL for the download string
        /// </summary>
        /// <returns>Url to access the data</returns>
        protected abstract Uri GetUrl();

        protected virtual WebHeaderCollection GetWebHeaders() { return new WebHeaderCollection(); }

        /// <summary>
        /// Occurs when the string was downloaded correctly
        /// </summary>
        /// <param name="downloadedString">The resulting string</param>
        /// <returns></returns>
        protected virtual void OnDownloadStringCompleted(String downloadedString) { }
        /// <summary>
        /// Allow to process download error message
        /// </summary>
        /// <param name="exception">Error exception</param>
        protected virtual void OnDownloadStringError(Exception exception) { RaiseJobError("unknown source", exception); }

        /// <summary>
        /// This happen in the UI Thread, just before starting the background worker
        /// </summary>
        /// <param name="arg">Object to use as argument for the background work</param>
        protected virtual void OnJobStarting(string arg) { }

        /// <summary>
        /// Background work. Done in the Background Thread (no access to UI thread Dependency properties)
        /// </summary>
        /// <param name="cancel">Was the job canceled?</param>
        /// <param name="arg">argument for the background job</param>
        /// <returns>Object return by the </returns>
        protected abstract R JobRun(ref bool cancel, string arg);

        /// <summary>
        /// Occurs after the job ends, in the UI Thread, just before the JobCompleted event raise
        /// </summary>
        /// <param name="result">Result of the background job</param>
        protected virtual void OnJobCompleted(R result) { }

        /// <summary>
        /// Occurs when the job was completed
        /// </summary>
        public event EventHandler<JobFinishedEventArgs<R>> JobCompleted;

        /// <summary>
        /// Occurs when the job got an error (like a download error)
        /// </summary>
        public event EventHandler<JobErrorEventArgs> JobError;

        /// <summary>
        /// Start a job when needing to download
        /// </summary>
        public void StartDownloadJob()
        {
            Init();
            if (client.IsBusy == false)
            {
                client.DownloadStringAsync(GetUrl());
            }
        }

        /// <summary>
        /// Start a background job without download
        /// </summary>
        /// <param name="arg">Argument for the background job</param>
        public void StartBackgroundJob(string arg)
        {
            Init();
            worker.RunWorkerAsync(arg);
        }

        private void InitWebClient() 
        {
            client = new WebClient();
            client.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    OnDownloadStringError(e.Error);
                }
                else if (e.Cancelled == false)
                {
                    downloadedString = e.Result;
                    OnDownloadStringCompleted(e.Result);
                    worker.RunWorkerAsync(downloadedString);
                }
            };

            client.Headers = GetWebHeaders();
            client.Headers[HttpRequestHeader.UserAgent] = Assembly.GetExecutingAssembly().FullName + "/" +
            System.Environment.OSVersion.Platform + "(" + System.Environment.OSVersion.Version + ") CLR:" +
            System.Environment.Version;

            client.Headers["Cache-Control"] = "no-cache";
            client.Headers["Pragma"] = "no-cache";            
        }

        private void InitWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += (s, e) =>
                {
                    bool cancel = false;
                    e.Result = JobRun(ref cancel, (string)e.Argument);
                    e.Cancel = cancel;
                };
            worker.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Cancelled && e.Error != null)
                    {
                        // result = null;
                    }
                    else {
                        result = (R)e.Result;
                        OnJobCompleted(result);
                    }
                    IsBusy = false;
                    RaiseJobCompleted(result);
                };
        }

        private void RaiseJobCompleted(R result)
        {
            if (JobCompleted != null)
            {
                JobCompleted(this, new JobFinishedEventArgs<R>(result));
            }
        }

        private void RaiseJobError(String source, Exception e)
        {
            if (JobError != null)
            {
                JobError(this, new JobErrorEventArgs(source, e));
            }
        }
    }

    
}
